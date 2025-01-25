using System;
using System.Windows.Data;

namespace GestionCSkin.Converters
{
    public class TextPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            var floatValue = (double)value;
            var position = new NewFloatPositionConverter().Convert(floatValue, null, null, null);
            return (double)position - 15;
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}