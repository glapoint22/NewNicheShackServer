using Shared.Common.Enums;
using Shared.Common.Interfaces;

namespace Shared.Common.Classes
{
    public sealed class Link
    {
        public int Id { get; set; }
        public LinkType LinkType { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;


        public async Task SetData(IRepository repository)
        {

        }
    }
}