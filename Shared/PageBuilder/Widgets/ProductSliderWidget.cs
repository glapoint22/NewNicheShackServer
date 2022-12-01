using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Dtos;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.QueryBuilder.Classes;
using System.Text.Json;

namespace Shared.PageBuilder.Widgets
{
    public sealed class ProductSliderWidget : Widget
    {
        public Caption? Caption { get; set; }
        public List<ProductDto>? Products { get; set; }
        public Query? Query { get; set; }


        public override void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            base.SetProperty(property, ref reader, options);

            switch (property)
            {
                case "caption":
                    Caption = (Caption?)JsonSerializer.Deserialize(ref reader, typeof(Caption), options);
                    break;
                case "products":
                    Products = (List<ProductDto>?)JsonSerializer.Deserialize(ref reader, typeof(List<ProductDto>), options);
                    break;
                case "query":
                    Query = (Query?)JsonSerializer.Deserialize(ref reader, typeof(Query), options);
                    break;
            }
        }


        public async override Task SetData(IRepository repository)
        {
            if (Query != null)
            {
                var queryBuilder = new QueryBuilder.Classes.QueryBuilder();
                var query = queryBuilder.BuildQuery<IProduct>(Query);

                Products = await repository.Products(query)
                    .Take(24)
                    .Select(x => new ProductDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlName = x.UrlName,
                        //MinPrice = x.ProductPrices.MinPrice(),
                        //MaxPrice = x.ProductPrices.MaxPrice(),
                        Image = new PageImage
                        {
                            //Name = x.Media.Name,
                            //Src = x.Media.ImageSm!
                        }
                    })
                    .ToListAsync();
            }

        }
    }
}