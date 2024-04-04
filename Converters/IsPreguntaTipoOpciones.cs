using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Encuestas;

namespace TodoList.Converters
{
    class IsPreguntaTipoOpciones : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (int)value is ((int)eTipoPregunta.OpcionMultiple) or ((int)eTipoPregunta.OpcionUnica);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
