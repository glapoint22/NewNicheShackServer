using Microsoft.AspNetCore.Http;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Services.MediaService.Classes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Shared.Services.MediaService
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public abstract class MediaService
    {
        // ----------------------------------------------------------------------- Get Images Folder -------------------------------------------------------------------
        public string GetImagesFolder()
        {
            string wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            return Path.Combine(wwwroot, "images");
        }


        // ------------------------------------------------------------------------- Update Image ----------------------------------------------------------------------
        public string UpdateImage(Image updatedImage, IMedia media, ImageSizeType imageSize)
        {
            string imageSrc = string.Empty;

            // This is a list of all the image sizes this image has
            List<ImageSizeType> imageSizes = new()
            {
                imageSize
            };


            string imagesFolder = GetImagesFolder();
            string image;



            // Reset thumbnail
            if (media.Thumbnail != null)
            {
                imageSizes.Add(ImageSizeType.Thumbnail);

                image = Path.Combine(imagesFolder, media.Thumbnail);
                File.Delete(image);

                media.Thumbnail = null;
                media.ThumbnailWidth = 0;
                media.ThumbnailHeight = 0;
            }


            // Reset small
            if (media.ImageSm != null)
            {
                imageSizes.Add(ImageSizeType.Small);

                image = Path.Combine(imagesFolder, media.ImageSm);
                File.Delete(image);

                media.ImageSm = null;
                media.ImageSmWidth = 0;
                media.ImageSmHeight = 0;
            }



            // Reset medium
            if (media.ImageMd != null)
            {
                imageSizes.Add(ImageSizeType.Medium);

                image = Path.Combine(imagesFolder, media.ImageMd);
                File.Delete(image);

                media.ImageMd = null;
                media.ImageMdWidth = 0;
                media.ImageMdHeight = 0;
            }


            // Reset large
            if (media.ImageLg != null)
            {
                imageSizes.Add(ImageSizeType.Large);

                image = Path.Combine(imagesFolder, media.ImageLg);
                File.Delete(image);

                media.ImageLg = null;
                media.ImageLgWidth = 0;
                media.ImageLgHeight = 0;
            }





            // Reset any size
            if (media.ImageAnySize != null)
            {
                imageSizes.Add(ImageSizeType.AnySize);

                image = Path.Combine(imagesFolder, media.ImageAnySize);
                File.Delete(image);

                media.ImageAnySize = null;
                media.ImageAnySizeWidth = 0;
                media.ImageAnySizeHeight = 0;
            }


            imageSizes = imageSizes.Distinct().OrderBy(x => x).ToList();

            foreach (ImageSizeType imageSizeType in imageSizes)
            {
                CreateImageSizes(imageSizeType, updatedImage, media);
            }



            // Set the image src
            if (imageSize == ImageSizeType.AnySize)
            {
                imageSrc = media.ImageAnySize!;
            }
            else if (imageSize == ImageSizeType.Small)
            {
                imageSrc = media.ImageSm!;
            }
            else if (imageSize == ImageSizeType.Medium)
            {
                imageSrc = media.ImageMd!;
            }

            return imageSrc;
        }




        // ------------------------------------------------------------------------ Add Image Size ----------------------------------------------------------------------
        public string AddImageSize(IMedia media, ImageSizeType imageSizeType, string src)
        {
            string imagesFolder = GetImagesFolder();
            string imagePath = Path.Combine(imagesFolder, src);
            Image image = Image.FromFile(imagePath);

            // Set the image sizes for this image
            return CreateImageSizes(imageSizeType, image, media);
        }








        // --------------------------------------------------------------------- Get Image From File -------------------------------------------------------------------
        public async Task<Image> GetImageFromFile(IFormFile imageFile)
        {
            Image image;

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {
                    image = (Image)img.Clone();
                }
            }

            return image;
        }








        // ------------------------------------------------------------------------- Post Images -----------------------------------------------------------------------
        public async Task<HttpResponseMessage> PostImages(List<string> images, string requestUri, string authToken)
        {
            string imagesFolder = GetImagesFolder();

            using (HttpClient httpClient = new())
            {
                using (HttpRequestMessage request = new(HttpMethod.Post, requestUri))
                {
                    using (MultipartFormDataContent content = new())
                    {
                        foreach (var image in images)
                        {
                            string imagePath = Path.Combine(imagesFolder, image);
                            FileStream stream = File.OpenRead(imagePath);
                            content.Add(new StreamContent(stream), image, image);
                        }

                        request.Content = content;
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                        return await httpClient.SendAsync(request);
                    }
                }
            }
        }









        // ----------------------------------------------------------------------- Set Image Sizes ---------------------------------------------------------------------
        public string CreateImageSizes(ImageSizeType imageSizeType, Image image, IMedia media)
        {
            string imageSrc = string.Empty;

            // Medium
            if (imageSizeType == ImageSizeType.Medium)
            {
                // Set the large image size
                if (media.ImageLg == null)
                {
                    CreateLargeImageSize(image, media);
                }



                // Set the medium image
                if (media.ImageMd == null)
                {
                    imageSrc = CreateMediumImageSize(image, media);
                }





                // Set the small image
                if (media.ImageSm == null)
                {
                    if (image.Width > (int)ImageSizeType.Small || image.Height > (int)ImageSizeType.Small)
                    {
                        CreateSmallImageSize(image, media);
                    }
                    else
                    {
                        media.ImageSm = media.ImageMd;
                        media.ImageSmWidth = media.ImageMdWidth;
                        media.ImageSmHeight = media.ImageMdHeight;
                    }
                }






                // Set the thumbnail
                if (media.Thumbnail == null)
                {
                    if (image.Width > (int)ImageSizeType.Thumbnail || image.Height > (int)ImageSizeType.Thumbnail)
                    {
                        CreateThumbnailSize(image, media);
                    }
                    else
                    {
                        media.Thumbnail = media.ImageSm;
                        media.ThumbnailWidth = media.ImageSmWidth;
                        media.ThumbnailHeight = media.ImageSmHeight;
                    }
                }
            }



            // Small
            else if (imageSizeType == ImageSizeType.Small)
            {
                // Set the small image
                if (media.ImageSm == null)
                {
                    imageSrc = CreateSmallImageSize(image, media);
                }



                // Set the thumbnail
                if (media.Thumbnail == null)
                {
                    if (image.Width > (int)ImageSizeType.Thumbnail || image.Height > (int)ImageSizeType.Thumbnail)
                    {
                        CreateThumbnailSize(image, media);
                    }
                    else
                    {
                        media.Thumbnail = media.ImageSm;
                        media.ThumbnailWidth = media.ImageSmWidth;
                        media.ThumbnailHeight = media.ImageSmHeight;
                    }
                }
            }


            // Any Size
            else if (imageSizeType == ImageSizeType.AnySize)
            {
                // Set the any size image
                imageSrc = CreateImageAnySize(image, media);

                // Set the thumbnail
                if (media.Thumbnail == null)
                {
                    if (image.Width > (int)ImageSizeType.Thumbnail || image.Height > (int)ImageSizeType.Thumbnail)
                    {
                        CreateThumbnailSize(image, media);
                    }
                    else
                    {
                        media.Thumbnail = media.ImageAnySize;
                        media.ThumbnailWidth = media.ImageAnySizeWidth;
                        media.ThumbnailHeight = media.ImageAnySizeHeight;
                    }
                }
            }

            return imageSrc;
        }





        // -------------------------------------------------------------------- Set Large Image Size -------------------------------------------------------------------
        private void CreateLargeImageSize(Image image, IMedia media)
        {
            const float large = (float)ImageSizeType.Large;
            const float medium = (float)ImageSizeType.Medium;

            ImageSize? imageSize = GetImageSize((int)ImageSizeType.Large, media);

            if (imageSize != null)
            {
                media.ImageLg = imageSize.Src;
                media.ImageLgWidth = imageSize.Width;
                media.ImageLgHeight = imageSize.Height;
            }
            else
            {
                if (image.Width > large || image.Height > large)
                {
                    imageSize = ScaleImage(image, large);
                    media.ImageLg = imageSize.Src;
                    media.ImageLgWidth = imageSize.Width;
                    media.ImageLgHeight = imageSize.Height;
                }
                else if (image.Width == large || image.Height == large || image.Width > medium || image.Height > medium)
                {
                    imageSize = GetImageSize(Math.Max(image.Width, image.Height), media);

                    if (imageSize != null)
                    {
                        media.ImageLg = imageSize.Src;
                        media.ImageLgWidth = imageSize.Width;
                        media.ImageLgHeight = imageSize.Height;
                    }
                    else
                    {
                        media.ImageLg = CopyImage(image);
                        media.ImageLgWidth = image.Width;
                        media.ImageLgHeight = image.Height;
                    }
                }
            }
        }



        // -------------------------------------------------------------------- Set Medium Image Size ------------------------------------------------------------------
        private string CreateMediumImageSize(Image image, IMedia media)
        {
            const float medium = (float)ImageSizeType.Medium;
            string imageSrc;

            ImageSize? imageSize = GetImageSize((int)ImageSizeType.Medium, media);

            if (imageSize != null)
            {
                media.ImageMd = imageSrc = imageSize.Src;
                media.ImageMdWidth = imageSize.Width;
                media.ImageMdHeight = imageSize.Height;
            }
            else
            {
                if (image.Width > medium || image.Height > medium)
                {
                    imageSize = ScaleImage(image, medium);
                    media.ImageMd = imageSrc = imageSize.Src;
                    media.ImageMdWidth = imageSize.Width;
                    media.ImageMdHeight = imageSize.Height;
                }
                else
                {
                    imageSize = GetImageSize(Math.Max(image.Width, image.Height), media);

                    if (imageSize != null)
                    {
                        media.ImageMd = imageSrc = imageSize.Src;
                        media.ImageMdWidth = imageSize.Width;
                        media.ImageMdHeight = imageSize.Height;
                    }
                    else
                    {
                        media.ImageMd = imageSrc = CopyImage(image);
                        media.ImageMdWidth = image.Width;
                        media.ImageMdHeight = image.Height;
                    }
                }
            }

            if (media.ImageLg == null)
            {
                media.ImageLg = media.ImageMd;
                media.ImageLgWidth = media.ImageMdWidth;
                media.ImageLgHeight = media.ImageMdHeight;
            }

            return imageSrc;
        }




        // --------------------------------------------------------------------- Set Small Image Size ------------------------------------------------------------------
        private string CreateSmallImageSize(Image image, IMedia media)
        {
            const float small = (float)ImageSizeType.Small;
            string imageSrc;

            ImageSize? imageSize = GetImageSize((int)ImageSizeType.Small, media);

            if (imageSize != null)
            {
                media.ImageSm = imageSrc = imageSize.Src;
                media.ImageSmWidth = imageSize.Width;
                media.ImageSmHeight = imageSize.Height;
            }
            else
            {
                if (image.Width > small || image.Height > small)
                {
                    imageSize = ScaleImage(image, small);
                    media.ImageSm = imageSrc = imageSize.Src;
                    media.ImageSmWidth = imageSize.Width;
                    media.ImageSmHeight = imageSize.Height;
                }
                else
                {
                    imageSize = GetImageSize(Math.Max(image.Width, image.Height), media);

                    if (imageSize != null)
                    {
                        media.ImageSm = imageSrc = imageSize.Src;
                        media.ImageSmWidth = imageSize.Width;
                        media.ImageSmHeight = imageSize.Height;
                    }
                    else
                    {
                        media.ImageSm = imageSrc = CopyImage(image);
                        media.ImageSmWidth = image.Width;
                        media.ImageSmHeight = image.Height;
                    }
                }
            }

            return imageSrc;
        }





        // ---------------------------------------------------------------------- Set Thumbnail Size -------------------------------------------------------------------
        private void CreateThumbnailSize(Image image, IMedia media)
        {
            const float thumbnail = (float)ImageSizeType.Thumbnail;

            ImageSize? imageSize = GetImageSize((int)ImageSizeType.Thumbnail, media);

            if (imageSize != null)
            {
                media.Thumbnail = imageSize.Src;
                media.ThumbnailWidth = imageSize.Width;
                media.ThumbnailHeight = imageSize.Height;
            }
            else
            {
                imageSize = ScaleImage(image, thumbnail);
                media.Thumbnail = imageSize.Src;
                media.ThumbnailWidth = imageSize.Width;
                media.ThumbnailHeight = imageSize.Height;
            }
        }




        // ---------------------------------------------------------------------- Set Image Any Size -------------------------------------------------------------------
        private string CreateImageAnySize(Image image, IMedia media)
        {
            string imageSrc;

            ImageSize? imageSize = GetImageSize(Math.Max(image.Width, image.Height), media);

            if (imageSize != null)
            {
                media.ImageAnySize = imageSrc = imageSize.Src;
                media.ImageAnySizeWidth = imageSize.Width;
                media.ImageAnySizeHeight = imageSize.Height;
            }
            else
            {
                imageSize = GetImageSize(Math.Max(image.Width, image.Height), media);

                if (imageSize != null)
                {
                    media.ImageAnySize = imageSrc = imageSize.Src;
                    media.ImageAnySizeWidth = imageSize.Width;
                    media.ImageAnySizeHeight = imageSize.Height;
                }
                else
                {
                    media.ImageAnySize = imageSrc = CopyImage(image);
                    media.ImageAnySizeWidth = image.Width;
                    media.ImageAnySizeHeight = image.Height;
                }
            }

            return imageSrc;
        }




        // ------------------------------------------------------------------------ Get Image Size ---------------------------------------------------------------------
        private static ImageSize? GetImageSize(int imageSize, IMedia media)
        {
            int maxSize;

            // Thumbnail
            maxSize = Math.Max(media.ThumbnailWidth, media.ThumbnailHeight);

            if (imageSize == maxSize)
            {
                return new ImageSize
                {
                    Src = media.Thumbnail!,
                    Width = media.ThumbnailWidth,
                    Height = media.ThumbnailHeight
                };
            }



            // Small
            maxSize = Math.Max(media.ImageSmWidth, media.ImageSmHeight);

            if (imageSize == maxSize)
            {
                return new ImageSize
                {
                    Src = media.ImageSm!,
                    Width = media.ImageSmWidth,
                    Height = media.ImageSmHeight
                };
            }



            // Medium
            maxSize = Math.Max(media.ImageMdWidth, media.ImageMdHeight);

            if (imageSize == maxSize)
            {
                return new ImageSize
                {
                    Src = media.ImageMd!,
                    Width = media.ImageMdWidth,
                    Height = media.ImageMdHeight
                };
            }



            // Large
            maxSize = Math.Max(media.ImageLgWidth, media.ImageLgHeight);

            if (imageSize == maxSize)
            {
                return new ImageSize
                {
                    Src = media.ImageLg!,
                    Width = media.ImageLgWidth,
                    Height = media.ImageLgHeight
                };
            }




            // Any Size
            maxSize = Math.Max(media.ImageAnySizeWidth, media.ImageAnySizeHeight);

            if (imageSize == maxSize)
            {
                return new ImageSize
                {
                    Src = media.ImageAnySize!,
                    Width = media.ImageAnySizeWidth,
                    Height = media.ImageAnySizeHeight
                };
            }


            return null;
        }



        // -------------------------------------------------------------------- Get Image Extension --------------------------------------------------------------------
        private static string GetImageExtension(Image image)
        {
            return ImageCodecInfo
                .GetImageEncoders()
                .FirstOrDefault(x => x.FormatID == image.RawFormat.Guid)?
                .FilenameExtension?
                .Split(";")
                .First()
                .Trim('*')
                .ToLower()!;
        }




        // ------------------------------------------------------------------------- Copy Image ------------------------------------------------------------------------
        private string CopyImage(Image image)
        {
            // Create a new unique name for the image
            string ext = GetImageExtension(image);

            string imageFile = Guid.NewGuid().ToString("N") + ext;

            // Get the file path
            string imagesFolder = GetImagesFolder();
            string filePath = Path.Combine(imagesFolder, imageFile);


            // Save the image to the images folder
            SaveImage(image, filePath);

            return imageFile;
        }









        // ------------------------------------------------------------------------- Save Image ------------------------------------------------------------------------
        public void SaveImage(Image image, string path)
        {
            Bitmap bitmap= new(image);
            bitmap.Save(path);
        }





        // ------------------------------------------------------------------------ Scale Image ------------------------------------------------------------------------
        private ImageSize ScaleImage(Image image, float targetSize)
        {
            float maxSize = Math.Max(image.Width, image.Height);
            float multiplier = targetSize / maxSize;
            int scaledWidth = (int)Math.Round(image.Width * multiplier);
            int scaledHeight = (int)Math.Round(image.Height * multiplier);

            //Scale
            Bitmap scaledBitmap = new(scaledWidth, scaledHeight);
            Graphics graphics = Graphics.FromImage(scaledBitmap);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.DrawImage(image, 0, 0, scaledWidth, scaledHeight);

            // Get the path
            string imagesFolder = GetImagesFolder();

            //Create the new image
            string ext = GetImageExtension(image);
            string imageFile = Guid.NewGuid().ToString("N") + ext;
            string newImage = Path.Combine(imagesFolder, imageFile);
            scaledBitmap.Save(newImage);

            return new ImageSize
            {
                Src = imageFile,
                Width = scaledWidth,
                Height = scaledHeight
            };
        }






        // -------------------------------------------------------------------- Get Video Thumbnail --------------------------------------------------------------------
        public async Task<string?> GetVideoThumbnail(IMedia media)
        {
            string? thumbnailUrl = null;

            // YouTube
            if (media.VideoType == (int)VideoType.YouTube)
            {
                // Get the youtube thumbnail
                thumbnailUrl = "https://img.youtube.com/vi/" + media.VideoId + "/mqdefault.jpg";

                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync(thumbnailUrl))
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            thumbnailUrl = string.Empty;
                        }
                    }
                }
            }


            // Vimeo
            else if (media.VideoType == (int)VideoType.Vimeo)
            {
                // Get vimeo thumbnail
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync("https://vimeo.com/api/oembed.json?url=https://vimeo.com/" + media.VideoId))
                    {
                        if (response.StatusCode != HttpStatusCode.NotFound)
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            Vimeo? json = JsonSerializer.Deserialize<Vimeo>(result);
                            thumbnailUrl = json?.thumbnail_url;
                        }
                    }
                }
            }


            // Wistia
            else if (media.VideoType == (int)VideoType.Wistia)
            {
                // Get the Wistia thumbnail
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync("http://fast.wistia.net/oembed?url=http://home.wistia.com/medias/" + media.VideoId))
                    {
                        if (response.StatusCode != HttpStatusCode.NotFound)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            var json = JsonSerializer.Deserialize<Wistia>(result);
                            thumbnailUrl = json?.thumbnail_url;
                        }
                    }
                }
            }


            if (!string.IsNullOrEmpty(thumbnailUrl))
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(thumbnailUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var inputStream = await response.Content.ReadAsStreamAsync())
                            {
                                using (Image thumbnailImage = Image.FromStream(inputStream))
                                {
                                    string thumbnailName = Guid.NewGuid().ToString("N") + GetImageExtension(thumbnailImage);
                                    string imagesFolder = GetImagesFolder();
                                    string filePath = Path.Combine(imagesFolder, thumbnailName);

                                    SaveImage(thumbnailImage, filePath);

                                    return thumbnailName;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }



        // ------------------------------------------------------------------------ Delete Media -----------------------------------------------------------------------
        public void DeleteMedia(IMedia media)
        {
            string imagesFolder = GetImagesFolder();
            string image;

            // Remove thumbnail
            if (media.Thumbnail != null)
            {
                image = Path.Combine(imagesFolder, media.Thumbnail);
                File.Delete(image);
            }


            // Remove small
            if (media.ImageSm != null)
            {
                image = Path.Combine(imagesFolder, media.ImageSm);
                File.Delete(image);
            }



            // Remove medium
            if (media.ImageMd != null)
            {
                image = Path.Combine(imagesFolder, media.ImageMd);
                File.Delete(image);
            }


            // Remove large
            if (media.ImageLg != null)
            {
                image = Path.Combine(imagesFolder, media.ImageLg);
                File.Delete(image);
            }



            // Remove any size
            if (media.ImageAnySize != null)
            {
                image = Path.Combine(imagesFolder, media.ImageAnySize);
                File.Delete(image);
            }
        }
    }
}