using Newtonsoft.Json;

namespace Oazis.Domain.Helpers
{
    public class JsonStringToIntConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    return Convert.ToInt32(reader.Value);
                default:
                    return serializer.Deserialize(reader);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long)
                   || objectType == typeof(int)
                   || objectType == typeof(object);
        }
    }
}
