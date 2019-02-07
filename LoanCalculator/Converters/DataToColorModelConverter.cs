using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class DataToColorModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                var collection = value as ObservableCollection<ChartDataPoint>;
                ChartColorCollection colors = new ChartColorCollection();
                if (collection.Count == 1)
                {
                    colors.Add(Color.FromHex("#353D5D"));
                }
                else
                {
                    if (parameter.ToString() == "PrincipalChart")
                    {
                        colors.Add(Color.FromHex("#F76981"));
                        colors.Add(Color.FromHex("#353D5D"));
                    }
                    else
                    {
                        colors.Add(Color.FromHex("#353D5D"));
                        colors.Add(Color.FromHex("#2DB9D1"));
                    }
                }

                return colors;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}