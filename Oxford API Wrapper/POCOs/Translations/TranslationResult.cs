using System.Collections.Generic;


namespace OxfordAPIWrapper.Translations
{
    public class TranslationResult
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public List<LexicalEntry> LexicalEntries { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
    }

}