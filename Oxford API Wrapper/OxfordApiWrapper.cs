using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using OxfordAPIWrapper.Objects;

namespace OxfordAPIWrapper
{
    /// <summary>
    /// A concrete implementation of the IOxfordApiWrapper interface.
    /// Uses HttpClient to communicate with the API.
    /// </summary>
    public class OxfordApiWrapper : IOxfordApiWrapper
    {
        private string baseUrl = "https://od-api.oxforddictionaries.com/api/v1";
        private List<OxfordDictionary> availableDictionaries;
        public string AppKey { get; private set; }
        public string AppId { get; private set; }

        public OxfordApiWrapper()
        {
            AppKey = SecretStore.APP_KEY;
            AppId = SecretStore.APP_ID;
        }

        
        /// <summary>
        /// Constructs a query to the given endpoint, launches it on a background thread, then returns with the response content 
        /// </summary>
        /// <param name="url">Endpoint URL</param>
        /// <returns>Response content. Empty string on Status Codes other than 2xx</returns>
        private async Task<string> GetResponseString(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("app_id", AppId);
                client.DefaultRequestHeaders.Add("app_key", AppKey);
                var response = await Task.Run(() => client.GetAsync(url));
                //Resource was most probably not found
                if (!response.IsSuccessStatusCode)
                {
                    return "";
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        public async Task<List<string>> GetTranslations(string word, string sourceLanguage, string targetLanguage)
        {
            List<string> translations = new List<string>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/entries/{sourceLanguage}/{word}/translations={targetLanguage}");
            string response = await GetResponseString(targetUrl);
            if (String.IsNullOrWhiteSpace(response))
            {
                return translations;
            }
            var jObject = JObject.Parse(response);
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].translations[*].text"))
            {
                translations.Add(token.ToString());
            }
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].subsenses[*].translations[*].text"))
            {
                translations.Add(token.ToString());
            }

            return translations;
        }

        public async Task<List<string>> GetLemmas(string word, string language)
        {
            List<string> lemmas = new List<string>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/inflections/{language}/{word}");
            string response = await GetResponseString(targetUrl);
            if (String.IsNullOrWhiteSpace(response))
            {
                return lemmas;
            }
            var jObject = JObject.Parse(response);
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].inflectionOf[*].text"))
            {
                lemmas.Add(token.ToString());
            }
            return lemmas;
        }

        public async Task<List<string>> GetExamples(string word, string language)
        {
            List<string> examples = new List<string>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/entries/{language}/{word}/examples");
            string response = await GetResponseString(targetUrl);
            if (String.IsNullOrWhiteSpace(response))
            {
                return examples;
            }
            var jObject = JObject.Parse(response);
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].examples[*].text"))
            {
                examples.Add(token.ToString());
            }
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].subsenses[*].examples[*].text"))
            {
                examples.Add(token.ToString());
            }
            return examples;
        }

        public async Task<List<string>> GetSynonyms(string word, string language)
        {
            List<string> synonyms = new List<string>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/entries/{language}/{word}/synonyms");
            string response = await GetResponseString(targetUrl);
            if (String.IsNullOrWhiteSpace(response))
            {
                return synonyms;
            }
            var jObject = JObject.Parse(response);
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].synonyms[*].text"))
            {
                synonyms.Add(token.ToString());
            }
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].subsenses[*].synonyms[*].text"))
            {
                synonyms.Add(token.ToString());
            }
            return synonyms;
        }

        public async Task<List<string>> GetAntonyms(string word, string language)
        {
            List<string> antonyms = new List<string>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/entries/{language}/{word}/antonyms");
            string response = await GetResponseString(targetUrl);
            if (String.IsNullOrWhiteSpace(response))
            {
                return antonyms;
            }
            var jObject = JObject.Parse(response);
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].antonyms[*].text"))
            {
                antonyms.Add(token.ToString());
            }
            foreach (var token in jObject.SelectTokens("results[*].lexicalEntries[*].entries[*].senses[*].subsenses[*].antonyms[*].text"))
            {
                antonyms.Add(token.ToString());
            }
            return antonyms;
        }

        public async Task<List<OxfordDictionary>> GetDictionaries()
        {
            if (availableDictionaries != null)
            {
                return availableDictionaries;
            }
            var dicts = new List<OxfordDictionary>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/languages");
            string response = await GetResponseString(targetUrl);
            var jObject = JObject.Parse(response);
            foreach (var token in jObject.SelectTokens("results[*]"))
            {
                if (token["type"].ToString() == "bilingual")
                {
                    dicts.Add(JsonConvert.DeserializeObject<OxfordDictionary>(token.ToString()));
                }
            }
            availableDictionaries = dicts;
            return dicts;
        }

    }
}

