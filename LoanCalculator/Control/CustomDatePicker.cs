using Syncfusion.SfPicker.XForms;
using System.Collections.ObjectModel;
using System.Globalization;

namespace LoanCalculator
{
    public class CustomDatePicker : SfPicker
    {
        public ObservableCollection<object> Date
        {
            get;
            set;
        }

        public ObservableCollection<object> Month
        {
            get;
            set;
        }

        public ObservableCollection<object> Year
        {
            get;
            set;
        }

        public CustomDatePicker()
        {
            Date = new ObservableCollection<object>();

            Month = new ObservableCollection<object>();
            Year = new ObservableCollection<object>();

            PopulateDateCollection();
            this.ItemsSource = Date;
        }

        private void PopulateDateCollection()
        {
            //populate months
            for (int i = 1; i < 13; i++)
            {
                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));
            }

            //populate year
            for (int i = 1990; i < 2100; i++)
            {
                Year.Add(i.ToString());
            }

            Date.Add(Month);
            Date.Add(Year);
        }
    }
}