using Shared.Common.Enums;
using Shared.Common.Widgets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Common.Classes
{
    public abstract class WidgetJsonConverter : JsonConverter<Widget>
    {
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
                        widget = GetWidget(widgetType);
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
            SerializeWidget(writer, widget, options);
            
        }


        public abstract Widget GetWidget(WidgetType widgetType);

        public abstract void SerializeWidget(Utf8JsonWriter writer, Widget widget, JsonSerializerOptions options);
    }
}