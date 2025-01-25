using System;
using System.Globalization;
using System.Windows.Data;

namespace GestionCSkin.Converters
{
    public class NewFloatPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double floatValue = (double)value;
            
            double position = 12; 
            
            if (floatValue <= 0.07)
            {
                position += (floatValue / 0.07) * 32;
            }
            else if (floatValue <= 0.15)
            {
                position += 32 + ((floatValue - 0.07) / 0.08) * 32;
            }
            else if (floatValue <= 0.38)
            {
                position += 64 + ((floatValue - 0.15) / 0.23) * 32;
            }
            else if (floatValue <= 0.45)
            {
                position += 96 + ((floatValue - 0.38) / 0.07) * 32;
            }
            else
            {
                position += 128 + ((floatValue - 0.45) / 0.55) * 32;
            }
            
            double offset = 8; 
            if (parameter is string param)
            {
                if (param == "text")
                {
                    offset = 12; 
                }
            }

            return position - offset;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
