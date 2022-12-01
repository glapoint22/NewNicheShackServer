using Shared.Common.Classes;
using Shared.Common.Interfaces;

namespace Shared.PageBuilder.Classes
{
    public sealed class LinkableImage
    {
        public PageImage Image { get; set; } = null!;
        public Link Link { get; set; } = null!;


        public async Task SetData(IRepository repository)
        {
            if (Image != null)
            {
                await Image.SetData(repository);
            }

            if (Link != null)
            {
                await Link.SetData(repository);
            }
        }
    }
}