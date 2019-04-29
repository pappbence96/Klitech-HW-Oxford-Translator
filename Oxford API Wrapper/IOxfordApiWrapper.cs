using System.Collections.Generic;
using System.Threading.Tasks;
using OxfordAPIWrapper.Objects;

namespace OxfordAPIWrapper
{
    /// <summary>
    /// Interface for the API wrapper
    /// Supported features: querying dictionaries, translating words, lemmatizing, thesaurus (synonyms, antonyms, example sentences) 
    /// </summary>
    public interface IOxfordApiWrapper
    {
        /// <summary>
        /// Application ID, required for authenticating with the API
        /// </summary>
        string AppId { get; }

        /// <summary>
        /// Application Key, required for authenticating with the API
        /// </summary>
        string AppKey { get; }

        /// <summary>
        /// Returns the antonyms of the given word of the given language
        /// </summary>
        /// <param name="word">queried word</param>
        /// <param name="language">source language of the word</param>
        /// <returns>A list of antonyms of the word, empty on error</returns>
        Task<List<string>> GetAntonyms(string word, string language);
        /// <summary>
        /// Returns the list of supported BILINGUAL dictionaries 
        /// </summary>
        /// <returns>The list of supported BILINGUAL dictionaries, empty on error</returns>
        Task<List<OxfordDictionary>> GetDictionaries();
        /// <summary>
        /// Returns a list of example sentences featuring the given word of a given language
        /// </summary>
        /// <param name="word">queried word</param>
        /// <param name="language">source language of the word</param>
        /// <returns>List of example sentences featuring the word, empty on error</returns>
        Task<List<string>> GetExamples(string word, string language);
        /// <summary>
        /// Returns the word root of a word of a given language. Useful for giving hints when translation lookup fails.
        /// </summary>
        /// <param name="word">queried word</param>
        /// <param name="language">source language of the word</param>
        /// <returns>List of word roots of the queried word, empty on error.</returns>
        Task<List<string>> GetLemmas(string word, string language);
        /// <summary>
        /// Returns the synonyms of the given word of the given language
        /// </summary>
        /// <param name="word">queried word</param>
        /// <param name="language">source language of the word</param>
        /// <returns>A list of synonyms of the word, empty on error</returns>
        Task<List<string>> GetSynonyms(string word, string language);
        /// <summary>
        /// Returns the available translations of a word from the source language to the target language.
        ///  Use the GetLemmas() function to give "Did you mean..." hints to the user when the lookup fails.
        /// </summary>
        /// <param name="word">queried word</param>
        /// <param name="sourceLanguage">source language of the word</param>
        /// <param name="targetLanguage">target language of the translation</param>
        /// <returns>List of translations, empty on error.</returns>
        Task<List<string>> GetTranslations(string word, string sourceLanguage, string targetLanguage);
    }
}