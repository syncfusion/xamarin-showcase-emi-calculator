using System;
using System.Globalization;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class PaymentDetailToMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var monthlyPaymentDetail = value as MonthlyPaymentDetail;
            if (monthlyPaymentDetail == null)
            {
                return null;
            }

            return (value as MonthlyPaymentDetail).Month.ToString("yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}