using Oxford_Translator_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oxford_Translator_UWP.Services
{
    /// <summary>
    /// Concrete implementation of the ISaveUtility interface
    /// </summary>
    class SaveUtility : ISaveUtility
    {
        public async Task SaveToFile(List<string> text, string suggestedName)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Text file", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = suggestedName;

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteLinesAsync(file, text);
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
    }
}
