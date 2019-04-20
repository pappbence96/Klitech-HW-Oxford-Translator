using System.Collections.Generic;


namespace OxfordAPIWrapper.Translations
{
    public class Sense
    {
        public string Id { get; set; }
        public List<Subsense> Subsenses { get; set; }
        public List<CrossReference> CrossReferences { get; set; }
        public List<string> Registers { get; set; }
        public List<string> Definitions { get; set; }
        public List<Translation> Translations { get; set; }

        public Sense()
        {
            Subsenses = new List<Subsense>();
        }
    }
}