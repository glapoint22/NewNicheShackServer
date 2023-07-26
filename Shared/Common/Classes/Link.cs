using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.PageBuilder.Enums;

namespace Shared.Common.Classes
{
    public sealed class Link
    {
        public Guid Id { get; set; }
        public LinkType LinkType { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;


        public async Task SetData(IRepository repository)
        {
            if (LinkType != LinkType.Page && LinkType != LinkType.Product && LinkType != LinkType.Browse) return;

            if (LinkType == LinkType.Page)
            {
                var page = await repository
                    .Pages(x => x.Id == Id)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.PageType,
                        x.UrlName,
                    }).SingleOrDefaultAsync();

                if (page != null)
                {
                    Name = page.Name;
                    Url = "cp/" + page.UrlName + "/" + page.Id;
                }
            }
            else if (LinkType == LinkType.Product)
            {
                var product = await repository
                    .Products(x => x.Id == Id)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.UrlName,
                    })
                    .SingleOrDefaultAsync();

                if (product != null)
                {
                    Name = product.Name;
                    Url = product.UrlName + "/" + product.Id;
                }
            }
            else if (LinkType == LinkType.Browse)
            {
                var subniche = await repository
                    .Subniches(x => x.Id == Id)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.UrlName,
                    })
                    .SingleOrDefaultAsync();

                if (subniche != null)
                {
                    Name = subniche.Name;
                    Url = "browse?subnicheName=" + subniche.UrlName + "&subnicheId=" + subniche.Id;
                }
            }

        }




        public void GenerateHtml(HtmlNode node)
        {
            node.SetAttributeValue("href", LinkType == LinkType.WebAddress ? Url : "{host}/" + Url);
            node.SetAttributeValue("target", "_blank");
        }
    }
}