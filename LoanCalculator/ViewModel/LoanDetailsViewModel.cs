using System.Collections.ObjectModel;
using System.Windows.Input;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace LoanCalculator
{
    public class LoanDetailsViewModel : ViewModelBase
    {
        #region private properties

        private double totalInterestAmount, totalPrincipalAmount, totalPayment, monthlyPayment, loanAmount, interest;
        private int term, loanTerm = 1;
        private ObservableCollection<ChartDataPoint> totalPaymentCollection;
        private LoanTermType termType = LoanTermType.Years;
        private double emiAmount;
        private IEMIService emiService;
        private IShareService shareService;
        private Dictionary<string, object> paymentDetails;
        #endregion

        #region public properties

        public double LoanAmount
        {
            get
            {
                return loanAmount;
            }

            set
            {
                loanAmount = value;
                OnPropertyChanged("LoanAmount");
            }
        }

        public CultureInfo CurrentUICulture
        {
            get
            {
                return CultureInfo.CurrentUICulture;
            }
        }

        public double Interest
        {
            get
            {
                return interest;
            }

            set
            {
                if (interest.Equals(value))
                {
                    return;
                }

                interest = value;
                OnPropertyChanged("Interest");
            }
        }

        public int Term
        {
            get
            {
                return term;
            }

            set
            {
                if (term == value)
                {
                    return;
                }

                term = value;
                OnPropertyChanged("Term");
            }
        }

        public int LoanTerm
        {
            get
            {
                return loanTerm;
            }

            set
            {
                if (loanTerm.Equals(value))
                {
                    return;
                }

                loanTerm = value;
                TermType = (loanTerm == 0) ? LoanTermType.Months : LoanTermType.Years;
                OnPropertyChanged("LoanTerm");
            }
        }

        public LoanTermType TermType
        {
            get
            {
                return termType;
            }

            set
            {
                if (termType == value)
                {
                    return;
                }

                Term = termType == LoanTermType.Years ? Term * 12 : Term / 12;
                termType = value;
                OnPropertyChanged("TermType");
            }
        }

        public double TotalPrincipalAmount
        {
            get
            {
                return totalPrincipalAmount;
            }

            set
            {
                totalPrincipalAmount = value;
                OnPropertyChanged("TotalPrincipalAmount");
            }
        }

        public double TotalInterestAmount
        {
            get
            {
                return totalInterestAmount;
            }

            set
            {
                if (totalInterestAmount.Equals(value))
                {
                    return;
                }

                totalInterestAmount = value;
                OnPropertyChanged("TotalInterestAmount");
            }
        }

        public double TotalPayment
        {
            get
            {
                return totalPayment;
            }

            set
            {
                if (totalPayment.Equals(value))
                {
                    return;
                }

                totalPayment = value;
                OnPropertyChanged("TotalPayment");
            }
        }

        public double MonthlyPayment
        {
            get
            {
                return monthlyPayment;
            }

            set
            {
                if (monthlyPayment.Equals(value))
                {
                    return;
                }

                monthlyPayment = value;
                OnPropertyChanged("MonthlyPayment");
            }
        }

        #region ObservableCollections

        public ObservableCollection<ChartDataPoint> TotalPaymentCollection
        {
            get
            {
                return totalPaymentCollection;
            }

            set
            {
                totalPaymentCollection = value;
                OnPropertyChanged("TotalPaymentCollection");
            }
        }
        #endregion

        #endregion

        #region Command
        public ICommand CalculatePaymentCommand { get; set; }

        public ICommand ResetValuesCommand { get; set; }

        public ICommand ViewStatisticsCommand => new AsyncCommand(InitializeAsync);

        public ICommand ShareLoanDetailsCommand => new Command(ShareLoanDetails);

        #endregion

        public override Task InitializeViewModelAsync(object navigationData)
        {
            return base.InitializeViewModelAsync(navigationData);
        }

        #region Constructor
        public LoanDetailsViewModel(IEMIService emiService, IShareService shareService)
        {
            this.emiService = emiService;
            this.shareService = shareService;

            //Command intialization
            CalculatePaymentCommand = new Command(Calculate);
            ResetValuesCommand = new Command(ResetFields);
            PaymentStartMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //Collection intialization
            TotalPaymentCollection = new ObservableCollection<ChartDataPoint>() { new ChartDataPoint("Principal", 1) };
        }

        #endregion

        #region methods
        private void Calculate()
        {
            if (Interest.Equals(0) || LoanAmount.Equals(0) || Term.Equals(0))
            {
                Validation();
                return;
            }

            emiAmount = emiService.CalculateEMI(LoanAmount, Interest, Term, TermType);
            TotalInterestAmount = emiService.GetPayableInterestAmount(Term, TermType, LoanAmount);
            TotalPayment = emiService.GetPayablePrincipalAmount(Term, TermType);
            MonthlyPayment = emiAmount;
            TotalPrincipalAmount = LoanAmount;

            TotalPaymentCollection = new ObservableCollection<ChartDataPoint>
            {
                new ChartDataPoint("Principal", TotalPrincipalAmount),
                new ChartDataPoint("Interest", TotalInterestAmount)
            };

            MessagingCenter.Send(this, "ScrollToEnd");
        }

        private void Validation()
        {
            string validationString = string.Empty;

            if (LoanAmount.Equals(0))
            {
                validationString += "principal amount";
            }

            if (Interest.Equals(0))
            {
                if (validationString != string.Empty)
                {
                    validationString += ", ";
                }

                validationString += "interest rate";
            }

            if (Term.Equals(0))
            {
                if (validationString != string.Empty)
                {
                    validationString += ", ";
                }

                validationString += "loan term";
            }

            Application.Current.MainPage.DisplayAlert(string.Empty, "Please enter valid " + validationString + ".", "Ok");
        }

        private void ResetFields()
        {
            Term = (int)(Interest = LoanAmount = 0);
            TermType = LoanTermType.Years;

            if (TotalPaymentCollection != null && TotalPaymentCollection.Count > 0)
            {
                TotalPaymentCollection = new ObservableCollection<ChartDataPoint>() { new ChartDataPoint("Principal", 1) };
            }

            TotalPrincipalAmount = MonthlyPayment = TotalPayment = TotalInterestAmount = 0;
        }

        private void ShareLoanDetails()
        {
            if (Interest.Equals(0) || LoanAmount.Equals(0) || Term.Equals(0))
            {
                Validation();
                return;
            }
            Calculate();
            shareService.ShareLoanDetails(LoanAmount, Interest, Term, TermType, emiAmount, TotalPayment, TotalInterestAmount);
        }

        private void AddValueKeyPair(string key, object value)
        {
            if (!paymentDetails.ContainsKey(key))
            {
                paymentDetails.Add(key, value);
            }
        }

        public async Task InitializeAsync()
        {
            if (Interest.Equals(0) || LoanAmount.Equals(0) || Term.Equals(0))
            {
                Validation();
                return;
            }

            Calculate();
            paymentDetails = emiService.GetAmortizationDetails(Interest, MonthlyPayment, LoanAmount, Term, TermType, PaymentStartMonth);
            AddValueKeyPair("loanAmount", LoanAmount);
            AddValueKeyPair("interest", Interest);
            AddValueKeyPair("emi", MonthlyPayment);
            AddValueKeyPair("term", Term);
            AddValueKeyPair("termType", TermType);
            await NavigationService.NavigateToAsync<StatisticPageViewModel>(paymentDetails);
        }
    }
    #endregion
}