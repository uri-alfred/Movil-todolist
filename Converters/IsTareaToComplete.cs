using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Converters
{
    class IsTareaToComplete : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (eEstado.Cancelado == (eEstado)value)
                return false;
            else if (eEstado.Completado == (eEstado)value)
                return false;

            return true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
