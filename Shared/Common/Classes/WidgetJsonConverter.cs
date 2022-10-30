using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Widgets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Common.Classes
{
    public sealed class WidgetJsonConverter : JsonConverter<Widget>
    {
        private readonly IRepository? _repository;

        public WidgetJsonConverter()
        {

        }

        public WidgetJsonConverter(IRepository repository)
        {
            _repository = repository;
        }

        public override Widget Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Widget widget = null!;
            int startDepth = reader.CurrentDepth;


            while (reader.Read())
            {
                // Return the widget when we are at the end of the object
                if (reader.TokenType == JsonTokenType.EndObject && reader.CurrentDepth == startDepth)
                {
                    return widget;
                }



                // If we are reading a property name
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? property = reader.GetString();

                    // If the property name is widget type
                    if (property == "widgetType")
                    {
                        reader.Read();
                        WidgetType widgetType = (WidgetType)reader.GetInt32();

                        // Get the widget
                        switch (widgetType)
                        {
                            case WidgetType.Button:
                                widget = new ButtonWidget(_repository!);
                                break;
                            case WidgetType.Text:
                                widget = new TextWidget(_repository!);
                                break;
                            case WidgetType.Image:
                                widget = new ImageWidget(_repository!);
                                break;
                            case WidgetType.Container:
                                widget = new ContainerWidget(_repository!);
                                break;
                            case WidgetType.Line:
                                widget = new LineWidget();
                                break;
                            case WidgetType.Video:
                                widget = new VideoWidget(_repository!);
                                break;
                            case WidgetType.ProductSlider:
                                widget = new ProductSliderWidget(_repository!);
                                break;
                            case WidgetType.Carousel:
                                widget = new CarouselWidget(_repository!);
                                break;
                            case WidgetType.Grid:
                                widget = new GridWidget(_repository!);
                                break;
                        }

                        widget.WidgetType = widgetType;
                    }
                    else
                    {
                        // Set each property for the widget
                        widget.SetProperty(property, ref reader, options);
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Widget widget, JsonSerializerOptions options)
        {
            switch (widget.WidgetType)
            {
                case WidgetType.Button:
                    JsonSerializer.Serialize(writer, (ButtonWidget)widget, options);
                    break;
                case WidgetType.Text:
                    JsonSerializer.Serialize(writer, (TextWidget)widget, options);
                    break;
                case WidgetType.Image:
                    JsonSerializer.Serialize(writer, (ImageWidget)widget, options);
                    break;
                case WidgetType.Container:
                    JsonSerializer.Serialize(writer, (ContainerWidget)widget, options);
                    break;
                case WidgetType.Line:
                    JsonSerializer.Serialize(writer, (LineWidget)widget, options);
                    break;
                case WidgetType.Video:
                    JsonSerializer.Serialize(writer, (VideoWidget)widget, options);
                    break;
                case WidgetType.ProductSlider:
                    JsonSerializer.Serialize(writer, (ProductSliderWidget)widget, options);
                    break;
                case WidgetType.Carousel:
                    JsonSerializer.Serialize(writer, (CarouselWidget)widget, options);
                    break;
                case WidgetType.Grid:
                    JsonSerializer.Serialize(writer, (GridWidget)widget, options);
                    break;
            }
        }
    }
}