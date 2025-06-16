using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Media;

public class AvaloniaColorJsonConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the object { "A": ..., "R": ..., "G": ..., "B": ... }
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        byte a = 255, r = 0, g = 0, b = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propName = reader.GetString()!;
                reader.Read();
                switch (propName)
                {
                    case "A": a = reader.GetByte(); break;
                    case "R": r = reader.GetByte(); break;
                    case "G": g = reader.GetByte(); break;
                    case "B": b = reader.GetByte(); break;
                }
            }
        }
        return Color.FromArgb(a, r, g, b);
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("A", value.A);
        writer.WriteNumber("R", value.R);
        writer.WriteNumber("G", value.G);
        writer.WriteNumber("B", value.B);
        writer.WriteEndObject();
    }
}