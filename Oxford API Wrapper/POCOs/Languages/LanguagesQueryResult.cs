using System.Collections.Generic;

namespace OxfordAPIWrapper.Languages
{
    public class LanguagesQueryResult
    {
        public Metadata Metadata { get; set; }
        public List<OxfordDictionary> Results { get; set; }
    }
}
