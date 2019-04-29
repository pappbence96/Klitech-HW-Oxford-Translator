using Oxford_Translator_UWP.Interfaces;
using OxfordAPIWrapper;
using OxfordAPIWrapper.Objects;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Oxford_Translator_UWP.ViewModels
{
    public class ThesaurusPageViewModel : ViewModelBase
    {
        private IOxfordApiWrapper wrapper;
        private IDialogService dialogService;
        private bool isReady;
        private List<Language> availableLanguages;
        private Language selectedLanguage;


        public bool IsReady {
            get => isReady;
            set => SetProperty(ref isReady, value);
        }

        public List<Language> AvailableLanguages {
            get => availableLanguages;
            set => SetProperty(ref availableLanguages, value);
        }

        public Language SelectedLanguage {
            get => selectedLanguage;
            set => SetProperty(ref selectedLanguage, value);
        }
        public ObservableCollection<String> ThesaurusWords { get; set; }
        public string SearchedWord { get; set; }

        public ICommand SynonymCommand { get; set; }
        public ICommand AntonymCommand { get; set; }
        public ICommand ExampleCommand { get; set; }

        public ThesaurusPageViewModel(IOxfordApiWrapper wrapper, IDialogService dialogService)
        {
            this.wrapper = wrapper;
            this.dialogService = dialogService;
            SynonymCommand = new DelegateCommand(GetSynonymsAsync, CanPerformLookup)
                .ObservesProperty(() => IsReady)
                .ObservesProperty(() => SelectedLanguage);
            AntonymCommand = new DelegateCommand(GetAntonymsAsync, CanPerformLookup)
                .ObservesProperty(() => IsReady)
                .ObservesProperty(() => SelectedLanguage);
            ExampleCommand = new DelegateCommand(GetExamplesAsync, CanPerformLookup)
                .ObservesProperty(() => IsReady)
                .ObservesProperty(() => SelectedLanguage);
            ThesaurusWords = new ObservableCollection<string>();
            isReady = true;
        }

        private async void GetExamplesAsync()
        {
            IsReady = false;
            ThesaurusWords.Clear();
            ThesaurusWords.Add("Loading example sentences...");
            List<string> examples = new List<string>();
            try
            {
                examples = await wrapper.GetExamples(SearchedWord, SelectedLanguage.Id);
            }
            catch (Exception)
            {
                await dialogService.ShowError("An error occurred during the lookup. Please check your internet connection and try again.");
            }
            ThesaurusWords.Clear();
            if (examples.Count == 0)
            {
                ThesaurusWords.Add("No example sentences found.");
            }
            foreach (var example in examples)
            {
                ThesaurusWords.Add(example);
            }
            IsReady = true;
        }

        private bool CanPerformLookup()
        {
            return IsReady && SelectedLanguage != null;
        }

        private async void GetAntonymsAsync()
        {
            IsReady = false;
            ThesaurusWords.Clear();
            ThesaurusWords.Add("Loading antonyms...");
            List<string> antonyms = new List<string>();
            try
            {
                antonyms = await wrapper.GetAntonyms(SearchedWord, SelectedLanguage.Id);
            }
            catch (Exception)
            {
                await dialogService.ShowError("An error occurred during the lookup. Please check your internet connection and try again.");
            }
            ThesaurusWords.Clear();
            if (antonyms.Count == 0)
            {
                ThesaurusWords.Add("No antonyms found.");
            }
            foreach (var antonym in antonyms)
            {
                ThesaurusWords.Add(antonym);
            }
            IsReady = true;
        }

        private async void GetSynonymsAsync()
        {
            IsReady = false;
            ThesaurusWords.Clear();
            ThesaurusWords.Add("Loading synonyms...");
            List<string> synonyms = new List<string>();
            try
            {
                synonyms = await wrapper.GetSynonyms(SearchedWord, SelectedLanguage.Id);
            }
            catch (Exception)
            {
                await dialogService.ShowError("An error occurred during the lookup. Please check your internet connection and try again.");
            }
            ThesaurusWords.Clear();
            if (synonyms.Count == 0)
            {
                ThesaurusWords.Add("No synonyms found.");
            }
            foreach (var synonym in synonyms)
            {
                ThesaurusWords.Add(synonym);
            }
            IsReady = true;
        }

        private async Task LoadLanguagesAsync()
        {
            List<OxfordDictionary> availableDictionaries;
            if (availableLanguages?.Count > 0)
            {
                return;
            }
            try
            {
                availableDictionaries = await wrapper.GetDictionaries();

            }
            catch (Exception)
            {
                dialogService.ShowError("Could not get available dictionaries. Please check your internet connection and restart the application.");
                return;
            }

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AvailableLanguages = availableDictionaries.Select(x => x.SourceLanguage).Distinct().ToList();
            });
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            await LoadLanguagesAsync();
        }
    }
}
