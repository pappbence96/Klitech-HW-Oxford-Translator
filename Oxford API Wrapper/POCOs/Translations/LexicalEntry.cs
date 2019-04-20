using System.Collections.Generic;


namespace OxfordAPIWrapper.Translations
{
    public class LexicalEntry
    {
        public List<Entry> Entries { get; set; }
        public string Language { get; set; }
        public string LexicalCategory { get; set; }
        public string Text { get; set; }
    }
}