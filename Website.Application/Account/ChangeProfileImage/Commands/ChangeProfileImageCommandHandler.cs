using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using Shared.Common.Classes;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Website.Application.Account.Common;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeProfileImage.Commands
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class ChangeProfileImageCommandHandler : UpdateUserCommandHandler, IRequestHandler<ChangeProfileImageCommand, Result>
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

            using (System.Drawing.Image image = System.Drawing.Image.FromStream(imageFile.OpenReadStream()))
            {
                left = Convert.ToDouble(percentLeft) * image.Width;
                right = Convert.ToDouble(percentRight) * image.Width;
                top = Convert.ToDouble(percentTop) * image.Height;
                bottom = Convert.ToDouble(percentBottom) * image.Height;
            }

            //Convert from image file to bitmap
            System.Drawing.Bitmap tempBitmap;
            using (MemoryStream memoryStream = new())
            {
                await imageFile.CopyToAsync(memoryStream);

                using (var tempImage = System.Drawing.Image.FromStream(memoryStream))
                {
                    tempBitmap = new System.Drawing.Bitmap(tempImage);
                }
            }


            int size = (int)(right - left);

            //Crop
            System.Drawing.Bitmap croppedBitmap = new(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int y = 0; y < size; y++)
                {
                    System.Drawing.Color pxlclr = tempBitmap.GetPixel((int)left + i, (int)top + y);
                    croppedBitmap.SetPixel(i, y, pxlclr);
                }
            }



            //Scale
            System.Drawing.Bitmap scaledBitmap = new(70, 70);
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(scaledBitmap);
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

            // Change the image
            user.ChangeImage(imageName);
            user.AddDomainEvent(new UserChangedProfileImageEvent(user.Id));


            IdentityResult result = await _userService.UpdateAsync(user);
            if (!result.Succeeded) return Result.Failed();

            // Update the user cookie
            await UpdateUserCookie(user);

            return Result.Succeeded();
        }
    }
}