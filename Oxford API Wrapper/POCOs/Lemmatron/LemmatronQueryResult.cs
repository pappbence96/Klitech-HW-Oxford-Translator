using System;
using System.Collections.Generic;


namespace OxfordAPIWrapper.Lemmatron
{
    public class LemmatronQueryResult
    {
        public Metadata Metadata { get; set; }
        public List<LemmatronResult> Results { get; set; }

        internal List<Inflection> GetRoots()
        {
            var resultList = new List<Inflection>();
            foreach(var result in Results)
            {
                foreach(var lexentry in result.LexicalEntries)
                {
                    resultList.AddRange(lexentry.InflectionOf);
                }
            }
            return resultList;
        }
    }

}