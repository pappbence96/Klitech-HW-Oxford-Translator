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
    /// <summary>
    /// View Model of the Translator page
    /// </summary>
    public class TranslatorPageViewModel : ViewModelBase
    {
        private IOxfordApiWrapper wrapper;
        private IDialogService dialogService;
        private ISaveUtility saveUtility;
        private List<OxfordDictionary> availableDictionaries;
        private List<Language> sourceLanguages = new List<Language>();
        private List<Language> targetLanguages = new List<Language>();
        private Language selectedSource;
        private Language selectedTarget;
        private bool isReady;


        public List<Language> SourceLanguages {
            get => sourceLanguages;
            set => SetProperty(ref sourceLanguages, value); 
        }
        public List<Language> TargetLanguages {
            get => targetLanguages;
            set => SetProperty(ref targetLanguages, value);
        }
        public Language SelectedSource {
            get => selectedSource;
            set => SetProperty(ref selectedSource, value);
        }
        public Language SelectedTarget {
            get => selectedTarget;
            set => SetProperty(ref selectedTarget, value);
        }
        public bool IsReady {
            get => isReady;
            set => SetProperty(ref isReady, value);
        }

        public string SourceText { get; set; }
        public ObservableCollection<string> ResultTranslations { get; set; }

        public ICommand SourceSelectedCommand { get; private set; }
        public ICommand TranslateCommand { get; private set; }
        public ICommand SwitchLanguagesCommand { get; private set; }
        public ICommand SaveCommand { get; }
        public ICommand ClearCommand { get; private set; }

        public TranslatorPageViewModel(IOxfordApiWrapper wrapper, IDialogService dialogService, ISaveUtility saveUtility) {
            this.wrapper = wrapper;
            this.dialogService = dialogService;
            this.saveUtility = saveUtility;
            SourceSelectedCommand = new DelegateCommand(SetAvailableTargetLanguages);
            TranslateCommand = new DelegateCommand(TranslateWordAsync, CanTranslate)
                .ObservesProperty(() => IsReady)
                .ObservesProperty(() => SelectedSource)
                .ObservesProperty(() => SelectedTarget);
            SwitchLanguagesCommand = new DelegateCommand(SwitchLanguages);
            SaveCommand = new DelegateCommand(SaveResultsToFile, CanTranslate)
                .ObservesProperty(() => IsReady)
                .ObservesProperty(() => SelectedSource)
                .ObservesProperty(() => SelectedTarget);
            ClearCommand = new DelegateCommand(() => ResultTranslations.Clear());
            ResultTranslations = new ObservableCollection<string>();
            IsReady = true;
        }

        /// <summary>
        /// Saves the page state to file
        /// </summary>
        private void SaveResultsToFile()
        {
            var text = new List<string>();
            text.Add($"Translations of the word '{SourceText}' from {SelectedSource.Name} to {SelectedTarget.Name}");
            text.AddRange(ResultTranslations);
            saveUtility.SaveToFile(text, $"{SourceText}_{SelectedSource.Id}-{SelectedTarget.Id}.txt");
        }

        /// <summary>
        /// Determines whether the translation can be performed
        /// </summary>
        /// <returns></returns>
        private bool CanTranslate()
        {
            return (SelectedSource != null)
                && (SelectedTarget != null)
                && IsReady;
        }

        /// <summary>
        /// Switches the selected target and source languages
        /// </summary>
        private void SwitchLanguages()
        {
            var tmp = SelectedSource;
            SelectedSource = SelectedTarget;
            SelectedTarget = tmp;
        }

        /// <summary>
        /// Gets the translation of the entered word through the wrapper. 
        /// Displays an error message in the list if the lookup fails.
        /// Gets the word root in order to give suggestions on error.
        /// </summary>
        private async void TranslateWordAsync()
        {
            IsReady = false;
            ResultTranslations.Clear();
            ResultTranslations.Add("Loading translations...");
            List<string> translations = new List<string>();
            Task<List<string>> inflections = null;
            try
            {
                inflections = wrapper.GetLemmas(SourceText, SelectedSource.Id);
                translations = await wrapper.GetTranslations(SourceText, SelectedSource.Id, SelectedTarget.Id);
            }
            catch (Exception)
            {
                await dialogService.ShowError("An error occurred during the translation. Please check your internet connection and try again.");
            }

            ResultTranslations.Clear();
            if (translations.Count == 0)
            {
                try
                {
                    await inflections;
                }
                catch (Exception)
                {
                    await dialogService.ShowError("An error occurred during fetching recommendations. Please check your internet connection and try again.");
                }

                ResultTranslations.Add("No translation found.");
                if(inflections.Result.Count > 0)
                {
                    ResultTranslations.Add($"See instead: {string.Join(", ", inflections.Result)}");
                }
            }
            foreach(var translation in translations)
            {
                ResultTranslations.Add(translation);
            }
            IsReady = true;
        }

        /// <summary>
        /// When the user selects a translation source language, this function sets the list of target languages to those that are supported.
        /// </summary>
        private void SetAvailableTargetLanguages()
        {
            if(availableDictionaries == null || SelectedSource?.Id == null)
            {
                return;
            }
            var result = availableDictionaries
                .Where(x => x.SourceLanguage.Id == SelectedSource.Id) //Where the source matches the selected language
                .Select(x => x.TargetLanguage)
                .ToList();
            TargetLanguages = result;
        }

        /// <summary>
        /// Downloads the available languages if they are not loaded yet.
        /// </summary>
        private async Task LoadLanguagesAsync()
        {
            if(availableDictionaries?.Count > 0)
            {
                return;
            }
            try
            {
                availableDictionaries = await wrapper.GetDictionaries();
            } catch (Exception)
            {
                await dialogService.ShowError("Could not get available dictionaries. Please check your internet connection and restart the application.");
                return;
            }
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 SourceLanguages = availableDictionaries.Select(x => x.SourceLanguage).Distinct().ToList();
             });
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            await LoadLanguagesAsync();
        }
    }
}
