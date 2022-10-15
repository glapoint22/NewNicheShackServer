﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Website.Application.Account.Common;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeProfileImage.Commands
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class ChangeProfileImageCommandHandler : UpdateUserCommandHandler, IRequestHandler<ChangeProfileImageCommand, Result>
    {
        private readonly HttpContext _httpContext;
        private readonly IUserService _userService;

        public ChangeProfileImageCommandHandler(IHttpContextAccessor httpContextAccessor, IUserService userService, ICookieService cookieService) : base(userService, cookieService)
        {
            _httpContext = httpContextAccessor.HttpContext!;
            _userService = userService;
        }


        public async Task<Result> Handle(ChangeProfileImageCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user == null) throw new Exception("Error while trying to get user from claims.");

            string wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string imagesFolder = Path.Combine(wwwroot, "Images");


            //Get the form data
            IFormFile imageFile = _httpContext.Request.Form.Files["newImage"];
            _httpContext.Request.Form.TryGetValue("percentTop", out StringValues percentTop);
            _httpContext.Request.Form.TryGetValue("percentLeft", out StringValues percentLeft);
            _httpContext.Request.Form.TryGetValue("percentRight", out StringValues percentRight);
            _httpContext.Request.Form.TryGetValue("percentBottom", out StringValues percentBottom);
            _httpContext.Request.Form.TryGetValue("currentImage", out StringValues currentImage);

            double left;
            double right;
            double top;
            double bottom;

            using (Image image = Image.FromStream(imageFile.OpenReadStream()))
            {
                left = Convert.ToDouble(percentLeft) * image.Width;
                right = Convert.ToDouble(percentRight) * image.Width;
                top = Convert.ToDouble(percentTop) * image.Height;
                bottom = Convert.ToDouble(percentBottom) * image.Height;
            }

            //Convert from image file to bitmap
            Bitmap tempBitmap;
            using (MemoryStream memoryStream = new())
            {
                await imageFile.CopyToAsync(memoryStream);

                using (var tempImage = Image.FromStream(memoryStream))
                {
                    tempBitmap = new Bitmap(tempImage);
                }
            }


            int size = (int)(right - left);

            //Crop
            Bitmap croppedBitmap = new(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int y = 0; y < size; y++)
                {
                    Color pxlclr = tempBitmap.GetPixel((int)left + i, (int)top + y);
                    croppedBitmap.SetPixel(i, y, pxlclr);
                }
            }



            //Scale
            Bitmap scaledBitmap = new(70, 70);
            Graphics graph = Graphics.FromImage(scaledBitmap);
            graph.InterpolationMode = InterpolationMode.High;
            graph.DrawImage(croppedBitmap, 0, 0, 70, 70);



            //Create the new image
            string imageName = Guid.NewGuid().ToString("N") + ".png";
            string newImage = Path.Combine(imagesFolder, imageName);



            //If the user currently has an image assigned to their profile
            if (!string.IsNullOrEmpty(currentImage))
            {
                string path = imagesFolder + "\\" + currentImage;

                if (File.Exists(path))
                {
                    // Delete the user's current image
                    File.Delete(path);
                }
            }

            scaledBitmap.Save(newImage, ImageFormat.Png);


            user.Image = imageName;

            user.AddDomainEvent(new UserChangedProfileImageEvent(user.Id));
            IdentityResult result = await _userService.UpdateAsync(user);

            if (!result.Succeeded) return Result.Failed();


            // Send the confirmation email
            //if (user.EmailOnProfileImageChange == true)
            //{
            //    // TODO: Send email
            //}

            // Update the user cookie
            await UpdateUserCookie(user);

            return Result.Succeeded();
        }
    }
}