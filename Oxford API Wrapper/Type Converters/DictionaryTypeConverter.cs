using System;
using Newtonsoft.Json;

namespace OxfordAPIWrapper.TypeConverters
{
    internal class DictionaryTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;
            return Enum.Parse(typeof(OxfordDictionaryType), enumString, true);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = (OxfordDictionaryType)value;
            writer.WriteValue(value.ToString().ToLower());
        }

    }
}