using Shared.Common.Dtos;
using Shared.Common.Enums;

namespace Shared.Common.Classes
{
    public class PageImage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Src { get; set; } = string.Empty;
        public ImageSizeType ImageSizeType { get; set; }

        public void SetData(MediaDto media)
        {
            Name = media.Name;
            Src = (ImageSizeType == ImageSizeType.AnySize ? media.ImageAnySize : ImageSizeType == ImageSizeType.Thumbnail ? media.Thumbnail : ImageSizeType == ImageSizeType.Small ? media.ImageSm : ImageSizeType == ImageSizeType.Medium ? media.ImageMd : media.ImageLg)!;
        }
    }
}