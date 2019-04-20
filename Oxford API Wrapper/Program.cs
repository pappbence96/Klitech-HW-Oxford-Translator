﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace OxfordAPIWrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            OxfordApiWrapper wrapper = new OxfordApiWrapper(SecretStore.APP_KEY, SecretStore.APP_ID);
            var languages = await wrapper.GetLanguages();
            var bilingualDicts = languages.Results.Where(x => x.Type == OxfordDictionaryType.Bilingual);
            ;
        }
    }
}
