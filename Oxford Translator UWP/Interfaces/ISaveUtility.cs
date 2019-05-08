using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oxford_Translator_UWP.Interfaces
{
    /// <summary>
    /// A small utility to allow saving search results to a file
    /// </summary>
    public interface ISaveUtility
    {
        /// <summary>
        /// Opens a file picker and saves the text into the selected text file.
        /// </summary>
        /// <param name="text">Text to be saved</param>
        /// <param name="suggestedName">Default filename</param>
        /// <returns></returns>
        Task SaveToFile(List<String> text, string suggestedName);
    }
}
