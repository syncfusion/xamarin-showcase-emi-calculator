using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class StatisticPageViewModel : ViewModelBase
    {
        #region properties
        private double emiAmount, loanAmount, interest, term;
        private LoanTermType termType = LoanTermType.Years;
        private IExportDataService exportDataService;
        private IEMIService emiService;
        private bool pickerVisiblity;
        private ObservableCollection<object> pickerSelectedItem;
        private string paymentStartDate = string.Empty;
        private Dictionary<string, object> paymentDetails;
        private ObservableCollection<MonthlyPaymentDetail> monthlyPaymentDetails;
        private ObservableCollection<YearlyPaymentDetail> yearlyPaymentDetails;

        public bool PickerVisiblity
        {
            get
            {
                return pickerVisiblity;
            }

            set
            {
                if (pickerVisiblity.Equals(value))
                {
                    return;
                }

                pickerVisiblity = value;
                OnPropertyChanged("PickerVisiblity");
            }
        }

        public ObservableCollection<object> PickerSelectedItem
        {
            get
            {
                return pickerSelectedItem;
            }

            set
            {
                pickerSelectedItem = value;
                OnPropertyChanged("PickerSelectedItem");
            }
        }

        public string PaymentStartDate
        {
            get
            {
                return paymentStartDate;
            }

            set
            {
                paymentStartDate = value;
                OnPropertyChanged("PaymentStartDate");
            }
        }

        public ObservableCollection<MonthlyPaymentDetail> MonthlyPaymentDetails
        {
            get
            {
                return monthlyPaymentDetails;
            }

            set
            {
                monthlyPaymentDetails = value;
                OnPropertyChanged("MonthlyPaymentDetails");
            }
        }

        public ObservableCollection<YearlyPaymentDetail> YearlyPaymentDetails
        {
            get
            {
                return yearlyPaymentDetails;
            }

            set
            {
                yearlyPaymentDetails = value;
                OnPropertyChanged("YearlyPaymentDetails");
            }
        }

        #endregion

        #region Command

        public ICommand PaymentDateSelectionCommand { get; private set; }

        public ICommand ShowHidePickerDialog { get; private set; }

        public ICommand ExportStatisticsCommand { get; private set; }
        #endregion

        public StatisticPageViewModel(IEMIService emiService, IExportDataService exportService)
        {
            pickerSelectedItem = new ObservableCollection<object>();
            exportDataService = exportService;
            this.emiService = emiService;
            ExportStatisticsCommand = new Command<SfDataGrid>(ExportStatisticsData);
            ShowHidePickerDialog = new Command(() =>
            {
                PickerVisiblity = !PickerVisiblity;
            });
            PaymentDateSelectionCommand = new Command(PaymentStartDateChanged);

            paymentStartDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(PaymentStartMonth.Month).Substring(0, 3);
            paymentStartDate += " " + PaymentStartMonth.Year.ToString();

            pickerSelectedItem.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(PaymentStartMonth.Month).Substring(0, 3));
            pickerSelectedItem.Add(PaymentStartMonth.Year.ToString());
        }

        private async void PaymentStartDateChanged()
        {
            int selectedItemYear = int.Parse((PickerSelectedItem as ObservableCollection<object>)[1].ToString()), selectedItemMonth = DateTime.ParseExact((PickerSelectedItem as ObservableCollection<object>)[0].ToString(), "MMM", CultureInfo.CurrentCulture).Month;
            PaymentStartMonth = new DateTime(selectedItemYear, selectedItemMonth, 1);
            PaymentStartDate = string.Empty;
            PaymentStartDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(PaymentStartMonth.Month).Substring(0, 3);
            PaymentStartDate += " " + PaymentStartMonth.Year.ToString();

            paymentDetails = await emiService.GetAmortizationDetails(interest, emiAmount, loanAmount, (int)term, termType, PaymentStartMonth);
            MonthlyPaymentDetails = (ObservableCollection<MonthlyPaymentDetail>)paymentDetails["monthlyDetails"];
            YearlyPaymentDetails = (ObservableCollection<YearlyPaymentDetail>)paymentDetails["yearlyDetails"];
        }

        private void ExportStatisticsData(SfDataGrid dataGrid)
        {
            exportDataService.ExportStatisticsData(dataGrid);
        }

        public override Task InitializeViewModelAsync(object navigationData)
        {
            paymentDetails = navigationData as Dictionary<string, object>;
            loanAmount = (double)paymentDetails["loanAmount"];
            interest = (double)paymentDetails["interest"];
            term = (int)paymentDetails["term"];
            termType = (LoanTermType)paymentDetails["termType"];
            emiAmount = (double)paymentDetails["emi"];
            return base.InitializeViewModelAsync(navigationData);
        }
    }
}