using System.Collections.Generic;


namespace OxfordAPIWrapper.Lemmatron
{
    public class LemmatronResult
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public List<LexicalEntry> LexicalEntries { get; set; }
        public string Word { get; set; }
    }

}