﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<LanguagesQueryResult> GetLanguages()
        {
            using(var client = new HttpClient())
            {
                var targetUrl = Flurl.Url.Combine(baseUrl, "/languages");
                client.DefaultRequestHeaders.Add("app_id", AppId);
                client.DefaultRequestHeaders.Add("app_key", AppKey);
                var response = client.GetAsync(targetUrl);
                var responseContent = await response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LanguagesQueryResult>(responseContent);
                return result;
            }
        }
    }
}
