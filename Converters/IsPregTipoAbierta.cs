using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Encuestas;

namespace TodoList.Converters
{
    class IsPregTipoAbierta : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (int)eTipoPregunta.Abierta == (int)value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }

    class IsPregTipoNumerica : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (int)eTipoPregunta.Numerica == (int)value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }

    class IsPregTipoSiNo : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (int)eTipoPregunta.SiNo == (int)value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }

    class IsPregTipoOptMulti : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (int)eTipoPregunta.OpcionMultiple == (int)value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }

    class IsPregTipoOptUnic : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (int)eTipoPregunta.OpcionUnica == (int)value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }

    class IsPregTipoFecha : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (int)eTipoPregunta.Fecha == (int)value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }

}

