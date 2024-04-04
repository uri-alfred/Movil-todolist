using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Encuestas;

namespace TodoList.Converters
{
    class TipoPreguntaToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var enumValue = (eTipoPregunta)value;
            return enumValue.ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || !Enum.TryParse(value.ToString(), out eTipoPregunta result))
                return eTipoPregunta.Abierta;

            return result;
        }
    }
}
