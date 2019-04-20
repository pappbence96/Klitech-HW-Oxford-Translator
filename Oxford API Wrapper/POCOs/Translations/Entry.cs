using System.Collections.Generic;


namespace OxfordAPIWrapper.Translations
{
    public class Entry
    {
        public List<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public int HomographNumber { get; set; }
        public List<Sense> Senses { get; set; }
        public List<Note> Notes { get; set; }
    }
}