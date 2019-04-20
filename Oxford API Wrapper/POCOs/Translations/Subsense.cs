using System.Collections.Generic;


namespace OxfordAPIWrapper.Translations
{
    public class Subsense
    {
        public string Id { get; set; }
        public List<Note> Notes { get; set; }
        public List<Translation> Translations { get; set; }
        public List<Example> Examples { get; set; }
        public List<string> Registers { get; set; }
        public List<string> Regions { get; set; }

        public Subsense()
        {
            Translations = new List<Translation>();
        }
    }
}