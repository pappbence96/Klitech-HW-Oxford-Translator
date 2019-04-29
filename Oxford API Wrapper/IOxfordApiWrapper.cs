using System.Collections.Generic;
using System.Threading.Tasks;
using OxfordAPIWrapper.Objects;

namespace OxfordAPIWrapper
{
    public interface IOxfordApiWrapper
    {
        string AppId { get; }
        string AppKey { get; }

        Task<List<string>> GetAntonyms(string word, string language);
        Task<List<OxfordDictionary>> GetDictionaries();
        Task<List<string>> GetExamples(string word, string language);
        Task<List<string>> GetLemmas(string word, string language);
        Task<List<string>> GetSynonyms(string word, string language);
        Task<List<string>> GetTranslations(string word, string sourceLanguage, string targetLanguage);
    }
}