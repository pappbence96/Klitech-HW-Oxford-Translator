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
    class OxfordApiWrapper
    {
        private string baseUrl = "https://od-api.oxforddictionaries.com/api/v1";
        public string AppKey { get; private set; }
        public string AppId { get; private set; }

        public OxfordApiWrapper(string appKey, string appId)
        {
            AppKey = appKey;
            AppId = appId;
        }

        private async Task<string> GetResponseString(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("app_id", AppId);
                client.DefaultRequestHeaders.Add("app_key", AppKey);
                var response = client.GetAsync(url);
                var responseContent = await response.Result.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        public async Task<List<string>> GetTranslations(string word, string sourceLanguage, string targetLanguage)
        {
            List<string> translations = new List<string>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/entries/{sourceLanguage}/{word}/translations={targetLanguage}");
            string response = await GetResponseString(targetUrl);
            var jObject = JObject.Parse(response);
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
            var dicts = new List<OxfordDictionary>();
            var targetUrl = Flurl.Url.Combine(baseUrl, $"/languages");
            string response = await GetResponseString(targetUrl);
            var jObject = JObject.Parse(response);
            foreach (var token in jObject.SelectTokens("results[*]"))
            {
                if(token["type"].ToString() == "bilingual")
                {
                    dicts.Add(JsonConvert.DeserializeObject<OxfordDictionary>(token.ToString()));
                }
            }
            return dicts;
        }

    }
}

