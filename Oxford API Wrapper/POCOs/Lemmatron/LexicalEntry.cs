using OxfordAPIWrapper.Translations;
using System.Collections.Generic;


namespace OxfordAPIWrapper.Lemmatron
{
    public class LexicalEntry
    {
        public List<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public List<Inflection> InflectionOf { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public string text { get; set; }
    }

}