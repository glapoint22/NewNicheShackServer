using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.ProductOrders.GetOrders.Classes;

namespace Website.Application.ProductOrders.GetOrders.Queries
{
    public sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public GetOrdersQueryHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }




        // -------------------------------------------------------------------------- Handle ---------------------------------------------------------------------------------
        public async Task<Result> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();
            string filter = request.Filter ?? "last-30";

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                List<ProductOrderDto> orders = await GetOrders(userId, filter, request.SearchTerm);

                if (orders.Count > 0) return Result.Succeeded(new
                {
                    orders
                });

                return Result.Succeeded(new
                {
                    Products = await GetOrderProducts(userId, request.SearchTerm)
                });
            }

            return Result.Succeeded(new
            {
                Orders = await GetOrders(userId, filter)
            });
        }





        // ------------------------------------------------------------------------ Get Orders -------------------------------------------------------------------------------
        private async Task<List<ProductOrderDto>> GetOrders(string userId, string filter, string? searchTerm = null)
        {
            return await _dbContext.ProductOrders
                .OrderByDescending(x => x.Date)
                .Where(x => x.UserId == userId)
                .Where(filter, searchTerm)
                .Select(x => new ProductOrderDto
                {
                    OrderNumber = x.Id,
                    Date = x.Date.ToString(),
                    PaymentMethod = GetPaymentMethod(x.PaymentMethod),
                    PaymentMethodImg = GetPaymentMethodImage(x.PaymentMethod),
                    Subtotal = x.Subtotal,
                    ShippingHandling = x.ShippingHandling,
                    Discount = x.Discount,
                    Tax = x.Tax,
                    Total = x.Total,
                    Hoplink = x.Product.GetHoplink(x.User.TrackingCode),
                    ProductId = x.ProductId,
                    Products = x.OrderProducts
                        .OrderByDescending(z => z.LineItemType == "ORIGINAL")
                        .Select(z => new ProductOrderProduct
                        {
                            Name = z.Name,
                            Quantity = z.Quantity > 1 ? z.Quantity : 0,
                            Price = z.Price,
                            Image = z.LineItemType == "ORIGINAL" ? new Image
                            {
                                Name = z.ProductOrder.Product.Media.Name,
                                Src = z.ProductOrder.Product.Media.ImageSm!
                            } : null!,
                            RebillFrequency = z.RebillFrequency,
                            RebillAmount = z.RebillAmount,
                            PaymentsRemaining = z.PaymentsRemaining,
                            UrlName = z.ProductOrder.Product.UrlName,
                            ProductId = z.ProductOrder.ProductId
                        })
                        .ToList()
                })
                .ToListAsync();
        }







        // -------------------------------------------------------------------- Get Order Products ---------------------------------------------------------------------------
        private async Task<List<OrderProductDto>> GetOrderProducts(string userId, string searchTerm)
        {
            return await _dbContext.OrderProducts
                .OrderByDescending(x => x.ProductOrder.Date)
                .ThenBy(x => x.OrderId)
                .Where(x => x.ProductOrder.UserId == userId)
                .Where(searchTerm)
                .Select(x => new OrderProductDto
                {
                    Date = x.ProductOrder.Date.ToString(),
                    Name = x.Name,
                    Image = x.LineItemType == "ORIGINAL" ? new Image
                    {
                        Name = x.ProductOrder.Product.Media.Name,
                        Src = x.ProductOrder.Product.Media.ImageSm!
                    } : null!,
                    Hoplink = x.ProductOrder.Product.GetHoplink(x.ProductOrder.User.TrackingCode),
                    OrderNumber = x.OrderId,
                    UrlName = x.ProductOrder.Product.UrlName
                })
                .ToListAsync();
        }







        // -------------------------------------------------------------------- Get Payment Method ---------------------------------------------------------------------------
        private static string GetPaymentMethod(int paymentMethodIndex)
        {
            string title = string.Empty;

            switch (paymentMethodIndex)
            {
                case 0:
                    title = "Paypal";
                    break;
                case 1:
                    title = "Visa";
                    break;
                case 2:
                    title = "Mastercard";
                    break;
                case 3:
                    title = "Discover";
                    break;
                case 4:
                    title = "American Express";
                    break;
                case 5:
                    title = "Solo";
                    break;
                case 6:
                    title = "Diners Club";
                    break;
                case 7:
                    title = "Maestro";
                    break;
                case 8:
                    title = "Mastercard";
                    break;
            }
            return title;
        }





        // ----------------------------------------------------------------- Get Payment Method Image ------------------------------------------------------------------------
        private static string GetPaymentMethodImage(int paymentMethodIndex)
        {
            string img = string.Empty;

            switch (paymentMethodIndex)
            {
                case 0:
                    img = "paypal.png";
                    break;
                case 1:
                    img = "visa.png";
                    break;
                case 2:
                    img = "master_card.png";
                    break;
                case 3:
                    img = "discover.png";
                    break;
                case 4:
                    img = "amex.png";
                    break;
                case 5:
                    img = "solo.png";
                    break;
                case 6:
                    img = "diners_club.png";
                    break;
                case 7:
                    img = "maestro.png";
                    break;
                case 8:
                    img = "master_card.png";
                    break;
            }

            return img;
        }
    }
}