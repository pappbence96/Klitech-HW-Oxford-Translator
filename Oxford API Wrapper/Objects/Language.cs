using Newtonsoft.Json;
using System.Collections.Generic;

namespace OxfordAPIWrapper.Objects
{
    /// <summary>
    /// Language class representing a language supported by the API
    /// </summary>
    public class Language
    {
        public string Id { get; set; }

        [JsonProperty("language")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Language language &&
                   Id == language.Id &&
                   Name == language.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = -1919740922;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}
