using Shared.Common.Enums;
using Shared.PageBuilder.Classes;
using System.Text.Json;

namespace Shared.Common.Widgets
{
    public abstract class Widget
    {
        public float? Width { get; set; }
        public float? Height { get; set; }
        public WidgetType WidgetType { get; set; }



        public virtual void SetProperty(string? property, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (property)
            {
                case "width":
                    Width = (float?)JsonSerializer.Deserialize(ref reader, typeof(float), options);
                    break;

                case "height":
                    Height = (float?)JsonSerializer.Deserialize(ref reader, typeof(float), options);
                    break;
            }
        }


        public virtual Task SetData(PageParams pageParams)
        {
            return Task.CompletedTask;
        }
    }
}