using Prism.Commands;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oxford_Translator_UWP.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        //All credits go to https://github.com/PrismLibrary/Prism-Samples-Windows
        private const string CurrentPageTokenKey = "CurrentPageToken";
        private Dictionary<PageToken, bool> canNavigateLookup;
        private PageToken currentPageToken;
        private INavigationService navigationService;
        private ISessionStateService sessionStateService;

        public ObservableCollection<MenuItemViewModel> Commands { get; set; }

        public MenuViewModel(IEventAggregator eventAggregator, INavigationService navigationService, ISessionStateService sessionStateService)
        {
            eventAggregator.GetEvent<NavigationStateChangedEvent>().Subscribe(OnNavigationStateChanged);
            this.navigationService = navigationService;
            this.sessionStateService = sessionStateService;

            Commands = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { DisplayName = "Translator", SymbolIcon = "Translator", Command = new DelegateCommand(() => NavigateToPage(PageToken.Translator), () => CanNavigateToPage(PageToken.Translator)) },
                new MenuItemViewModel { DisplayName = "Thesaurus", SymbolIcon = "Thesaurus", Command = new DelegateCommand(() => NavigateToPage(PageToken.Thesaurus), () => CanNavigateToPage(PageToken.Thesaurus)) }
            };

            canNavigateLookup = new Dictionary<PageToken, bool>();

            foreach (PageToken pageToken in Enum.GetValues(typeof(PageToken)))
            {
                canNavigateLookup.Add(pageToken, true);
            }

            if (this.sessionStateService.SessionState.ContainsKey(CurrentPageTokenKey))
            {
                // Resuming, so update the menu to reflect the current page correctly
                PageToken currentPageToken;
                if (Enum.TryParse(this.sessionStateService.SessionState[CurrentPageTokenKey].ToString(), out currentPageToken))
                {
                    UpdateCanNavigateLookup(currentPageToken);
                    RaiseCanExecuteChanged();
                }
            }
        }

        private void OnNavigationStateChanged(NavigationStateChangedEventArgs args)
        {
            PageToken currentPageToken;
            if (Enum.TryParse(args.Sender.Content.GetType().Name.Replace("Page", string.Empty), out currentPageToken))
            {
                sessionStateService.SessionState[CurrentPageTokenKey] = currentPageToken.ToString();
                UpdateCanNavigateLookup(currentPageToken);
                RaiseCanExecuteChanged();
            }
        }

        private void NavigateToPage(PageToken pageToken)
        {
            if (CanNavigateToPage(pageToken))
            {
                if (navigationService.Navigate(pageToken.ToString(), null))
                {
                    UpdateCanNavigateLookup(pageToken);
                    RaiseCanExecuteChanged();
                }
            }
        }

        private bool CanNavigateToPage(PageToken pageToken)
        {
            return canNavigateLookup[pageToken];
        }

        private void UpdateCanNavigateLookup(PageToken navigatedTo)
        {
            canNavigateLookup[currentPageToken] = true;
            canNavigateLookup[navigatedTo] = false;
            currentPageToken = navigatedTo;
        }

        private void RaiseCanExecuteChanged()
        {
            foreach (var item in Commands)
            {
                (item.Command as DelegateCommand).RaiseCanExecuteChanged();
            }
        }
    }
}
