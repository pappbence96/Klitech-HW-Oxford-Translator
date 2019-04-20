using System.Collections.Generic;


namespace OxfordAPIWrapper.Translations
{
    public class TranslationQueryResult
    {
        public Metadata Metadata { get; set; }
        public List<TranslationResult> Results { get; set; }

        public List<Translation> GetTranslations()
        {
            var resultList = new List<Translation>();

            foreach (var result in Results)
            {
                foreach(var lexEntry in result.LexicalEntries)
                {
                    foreach(var entry in lexEntry.Entries)
                    {
                        foreach(var sense in entry.Senses)
                        {
                            if (sense.Translations != null)
                            {
                                resultList.AddRange(sense.Translations);
                            }

                            foreach(var subsense in sense.Subsenses)
                            {
                                if(subsense.Translations != null)
                                {
                                    resultList.AddRange(subsense.Translations);
                                }
                            }
                        }
                    }
                }
            }

            return resultList;
        }
    }
}