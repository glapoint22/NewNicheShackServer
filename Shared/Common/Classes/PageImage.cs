using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Shared.Common.Interfaces;

namespace Shared.Common.Classes
{
    public class PageImage
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Src { get; set; } = string.Empty;
        public ImageSizeType ImageSizeType { get; set; }

        public async Task SetData(IRepository repository)
        {
            var media = await repository
                .Media(x => x.Id == Id)
                .Select(x => new
                {
                    x.Name,
                    x.ImageAnySize,
                    x.Thumbnail,
                    x.ImageSm,
                    x.ImageMd,
                    x.ImageLg
                }).SingleAsync();

            Name = media.Name;
            Src = (ImageSizeType == ImageSizeType.AnySize ? media.ImageAnySize :
                ImageSizeType == ImageSizeType.Thumbnail ? media.Thumbnail :
                ImageSizeType == ImageSizeType.Small ? media.ImageSm :
                ImageSizeType == ImageSizeType.Medium ? media.ImageMd :
                media.ImageLg)!;
        }



        public void GenerateHtml(HtmlNode node)
        {
            if (Name == "Image Placeholder")
            {
                Name = "{imageName}";
                Src = "{imageSrc}";
            }
            else if (Name == "Stars Placeholder")
            {
                Name = "Product Rating";
                Src = "{stars}";
            }

            node.SetAttributeValue("src", "{host}/images/" + Src);
            node.SetAttributeValue("title", Name);
            node.SetAttributeValue("alt", Name);
        }
    }
}