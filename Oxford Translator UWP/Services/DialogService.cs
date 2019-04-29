using Oxford_Translator_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Oxford_Translator_UWP.Services
{
    /// <summary>
    /// Concrete implemetation of the IDialogService Interface
    /// </summary>
    class DialogService : IDialogService
    {
        public IAsyncOperation<ContentDialogResult> ShowError(string message)
        {
            var contentDialog = new ContentDialog()
            {
                Title = "Error",
                Content = message,
                PrimaryButtonText = "Ok"
            };
            return contentDialog.ShowAsync();
        }
    }
}
