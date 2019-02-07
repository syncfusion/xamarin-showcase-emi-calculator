using System;
using System.Globalization;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class AppendCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.IsNaN((double)value))
            {
                return 0;
            }

            return culture.NumberFormat.CurrencySymbol + ((double)value).ToString("N2", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}