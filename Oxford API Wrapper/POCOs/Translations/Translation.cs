using System.Collections.Generic;


namespace OxfordAPIWrapper.Translations
{
    public class Translation
    {
        public string Language { get; set; }
        public string Text { get; set; }
        public List<string> Registers { get; set; }
    }
}