using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Oxford_Translator_UWP.Interfaces
{
    /// <summary>
    /// Dialog Service Interface, used to display various modal dialogs
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Display an error dialog with message, return user choice
        /// </summary>
        /// <param name="message">error message</param>
        /// <returns>user choice</returns>
        IAsyncOperation<ContentDialogResult> ShowError(string message);
    }
}
