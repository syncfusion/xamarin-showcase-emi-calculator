using System;
using System.Globalization;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                var totalPayment = ((parameter as Grid).BindingContext as LoanDetailsViewModel).TotalPayment;
                var percentValue = (double)value / totalPayment * 100;
                value = (double.IsNaN(percentValue) || double.IsInfinity(percentValue)) ? "0.00%" : percentValue.ToString("0.##'%'");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}