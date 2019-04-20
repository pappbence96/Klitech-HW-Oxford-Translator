using Newtonsoft.Json;

namespace OxfordAPIWrapper.Languages
{
    public class Language
    {
        public string Id { get; set; }

        [JsonProperty("language")]
        public string Name { get; set; }
    }
}
