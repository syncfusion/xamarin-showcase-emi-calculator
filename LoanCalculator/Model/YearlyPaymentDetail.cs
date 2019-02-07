using System;
using System.ComponentModel;

namespace LoanCalculator
{
    public class YearlyPaymentDetail : INotifyPropertyChanged
    {
        #region Properties

        private double balance;
        private double payment;
        private DateTime year;
        private double principal;
        private double interest;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime Year
        {
            get
            {
                return year;
            }

            set
            {
                if (year.Equals(value))
                {
                    return;
                }

                year = value;
                OnNotifyPropertyChanged("Year");
            }
        }

        public double Payment
        {
            get
            {
                return payment;
            }

            set
            {
                if (payment.Equals(value))
                {
                    return;
                }

                payment = value;
                OnNotifyPropertyChanged("Payment");
            }
        }

        public double Principal
        {
            get
            {
                return principal;
            }

            set
            {
                if (principal.Equals(value))
                {
                    return;
                }

                principal = value;
                OnNotifyPropertyChanged("Principal");
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
                OnNotifyPropertyChanged("Interest");
            }
        }

        public double Balance
        {
            get
            {
                return balance;
            }

            set
            {
                if (balance.Equals(value))
                {
                    return;
                }

                balance = value;
                OnNotifyPropertyChanged("Balance");
            }
        }
        #endregion

        #region methods
        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}