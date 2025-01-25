using System;
using System.Windows.Data;
using System.Windows.Media;

namespace GestionCSkin.Converters
{
    public class FloatColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, 
            System.Globalization.CultureInfo culture)
        {
            if (!(value is double floatValue)) 
                return Brushes.White;

            if (floatValue <= 0.07)
                return Brushes.DarkGreen;
            else if (floatValue <= 0.15)
                return Brushes.LightGreen;
            else if (floatValue <= 0.38)
                return Brushes.Yellow;
            else if (floatValue <= 0.45)
                return Brushes.Orange;
            else
                return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, 
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}