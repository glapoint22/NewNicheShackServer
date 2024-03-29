﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.ProductOrders.PostOrder.Classes;
using Website.Domain.Entities;
using Website.Domain.Enums;

namespace Website.Application.ProductOrders.PostOrder.Commands
{
    public sealed class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public PostOrderCommandHandler(IWebsiteDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }





        public async Task<Result> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            string decryptedNotification = DecryptNotification(request.Iv, request.Notification);

            OrderNotification? orderNotification = JsonSerializer.Deserialize<OrderNotification>(decryptedNotification, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Retrun failed if orderNotification is null
            if (orderNotification == null) return Result.Failed();


            // If the transaction is a sale
            if (orderNotification.TransactionType == "SALE" || orderNotification.TransactionType == "TEST_SALE")
            {

                // Return if there are no tracking codes
                if (orderNotification.TrackingCodes == null || !orderNotification.TrackingCodes.Any()) return Result.Succeeded();


                // Id
                string id = orderNotification.Receipt;


                // Split the tracking codes into product id && user id
                string[] trackingCodes = orderNotification.TrackingCodes.ToArray()[0].Split('_');

                // Get the product id
                Guid productId = await _dbContext.Products
                    .Where(x => x.TrackingCode == trackingCodes[0])
                    .Select(x => x.Id)
                    .SingleOrDefaultAsync();

                // Return failed if we have no product id
                if (productId == Guid.Empty) return Result.Failed();


                // Get the user id
                string? userId = await _dbContext.Users
                    .Where(x => x.TrackingCode == trackingCodes[1])
                    .Select(x => x.Id)
                    .SingleOrDefaultAsync();


                // Return failed if we have no user id
                if (userId == null) return Result.Failed();


                // Payment method
                string paymentMethod = orderNotification.PaymentMethod;

                // prices
                double subtotal = 0;
                double tax = 0;
                double discount = 0;
                double shipping = 0;

                // Line items
                foreach (LineItem lineItem in orderNotification.LineItems)
                {
                    subtotal += lineItem.ProductPrice;
                    tax += lineItem.TaxAmount;
                    discount += lineItem.ProductDiscount;
                    shipping += lineItem.ShippingAmount;

                    OrderProduct orderProduct = new()
                    {
                        OrderId = id,
                        Name = lineItem.ProductTitle,
                        Quantity = lineItem.Quantity,
                        Price = lineItem.ProductPrice,
                        LineItemType = lineItem.LineItemType,
                        RebillFrequency = lineItem.Recurring ? lineItem.PaymentPlan.RebillFrequency : null!,
                        RebillAmount = lineItem.Recurring ? lineItem.PaymentPlan.RebillAmount : 0
                    };

                    _dbContext.OrderProducts.Add(orderProduct);

                }

                // Order
                ProductOrder productOrder = new()
                {
                    Id = id,
                    ProductId = productId,
                    UserId = userId,
                    Date = DateTime.UtcNow,
                    PaymentMethod = (int)Enum.Parse(typeof(PaymentMethod), paymentMethod),
                    Subtotal = subtotal,
                    ShippingHandling = shipping,
                    Discount = discount,
                    Tax = tax,
                    Total = orderNotification.TotalOrderAmount,
                    IsUpsell = orderNotification.Upsell != null
                };

                _dbContext.ProductOrders.Add(productOrder);


                await _dbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }





        private string DecryptNotification(string iv, string notification)
        {
            string decryptedString = null!;

            byte[] inputBytes = Encoding.UTF8.GetBytes(_configuration["OrderNotification:Key"]);

            SHA1 sha1 = SHA1.Create();
            byte[] key = sha1.ComputeHash(inputBytes);

            StringBuilder hex = new StringBuilder(key.Length * 2);
            foreach (byte b in key)
                hex.AppendFormat("{0:x2}", b);

            string secondPhaseKey = hex.ToString().Substring(0, 32);

            ASCIIEncoding asciiEncoding = new ASCIIEncoding();

            byte[] keyBytes = asciiEncoding.GetBytes(secondPhaseKey);
            byte[] ivBytes = Convert.FromBase64String(iv);



            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(notification)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            decryptedString = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedString;
        }
    }
}