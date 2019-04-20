using Newtonsoft.Json;
using OxfordAPIWrapper.TypeConverters;

namespace OxfordAPIWrapper.Languages
{
    public class OxfordDictionary
    {
        public string Region { get; set; }
        public string Source { get; set; }
        public Language SourceLanguage { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(DictionaryTypeConverter))]
        public OxfordDictionaryType Type { get; set; }
        public Language TargetLanguage { get; set; }
    }
}