using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Oxford_Translator_UWP.Converters
{
    class SymbolIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string symbolString = (string)value;
            if(symbolString == "Translator")
            {
                return Symbol.Character;
            }
            if(symbolString == "Thesaurus")
            {
                return Symbol.Find;
            }
            return Symbol.Cancel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
