using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Converters
{
    class IsOpcionCheck : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Verificar que el valor pasado sea una cadena y la colección sea ObservableCollection<string>
            if (value is string stringValue && parameter is ObservableCollection<string> collection)
            {
                // Verificar si el valor está presente en la colección
                return collection.Contains(stringValue);
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                return parameter.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
