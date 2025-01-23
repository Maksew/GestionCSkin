using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GestionCSkin.Converters
{
    public class ProfitColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal profit)
            {
                return profit >= 0 ? Brushes.LimeGreen : Brushes.OrangeRed;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}