namespace OxfordAPIWrapper.Objects
{
    /// <summary>
    /// Dictionary class representing a mono- or bilingual dictionary supported by the API. 
    /// In the case of monolingual dictionaries, TargetLanguage field is null.
    /// </summary>
    public class OxfordDictionary
    {
        public string Source { get; set; }
        public Language SourceLanguage { get; set; }

        public Language TargetLanguage { get; set; }
    }
}