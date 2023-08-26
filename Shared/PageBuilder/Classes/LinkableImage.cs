using Shared.Common.Classes;
using Shared.Common.Interfaces;

namespace Shared.PageBuilder.Classes
{
    public sealed class LinkableImage
    {
        public PageImage Image { get; set; } = null!;
        public PageImage Image2 { get; set; } = null!;
        public Link Link { get; set; } = null!;
        public string Title { get; set; } = string.Empty;


        public async Task SetData(IRepository repository)
        {
            if (Image != null && Image.Id != Guid.Empty)
            {
                await Image.SetData(repository);
            }

            if (Image2 != null && Image2.Id != Guid.Empty)
            {
                await Image2.SetData(repository);
            }

            if (Link != null)
            {
                await Link.SetData(repository);
            }
        }
    }
}