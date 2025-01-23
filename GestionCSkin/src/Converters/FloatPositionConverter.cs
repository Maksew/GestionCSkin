using System;
using System.Globalization;
using System.Windows.Data;

namespace GestionCSkin.Converters
{
    public class FloatPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double floatValue)
            {
                // Exemple : Calcul identique à NewSkin.xaml.cs
                return (floatValue * 200) + 10; 
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}