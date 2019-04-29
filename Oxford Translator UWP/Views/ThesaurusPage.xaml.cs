using Oxford_Translator_UWP.ViewModels;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Oxford_Translator_UWP.Views
{
    public sealed partial class ThesaurusPage : SessionStateAwarePage, INotifyPropertyChanged
    {
        public ThesaurusPage()
        {
            this.InitializeComponent();
            DataContextChanged += ThesaurusPage_DataContextChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ThesaurusPageViewModel ConcreteDataContext {
            get {
                return DataContext as ThesaurusPageViewModel;
            }
        }

        private void ThesaurusPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
            }
        }
    }
}
