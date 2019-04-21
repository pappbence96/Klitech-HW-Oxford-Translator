using OxfordAPIWrapper.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OxfordAPIWrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            OxfordApiWrapper wrapper = new OxfordApiWrapper(SecretStore.APP_KEY, SecretStore.APP_ID);
            //var cool = await wrapper.GetTranslations("cool", "en", "es");
            //var lemma = await wrapper.GetLemmas("infected", "en");
            //var examples = await wrapper.GetExamples("infect", "en");
            //var synonyms = await wrapper.GetSynonyms("cool", "en");
            //var antonyms = await wrapper.GetAntonyms("cool", "en");
            var dicts = await wrapper.GetDictionaries();
            ;
        }
    }
}
