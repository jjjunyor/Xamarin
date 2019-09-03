using System;
using System.Globalization;
using Xamarin.Forms;

namespace AppECM
{
    public class StatusToVisibilityGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, null))
                return new GridLength(0);          

            switch (value)
            {
                case (true):
                    {
                        return new GridLength(1, GridUnitType.Auto);
                    }
                default:
                    {
                        return new GridLength(0);
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Apenas bindings suportado para o converter");
        }
    }
}
