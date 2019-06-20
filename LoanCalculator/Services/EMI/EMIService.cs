using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LoanCalculator
{
    public class EMIService : IEMIService
    {
        private double interestFactor, currentEMI;
        private int tenure;
        private ObservableCollection<MonthlyPaymentDetail> monthlyPaymentDetails;
        private ObservableCollection<YearlyPaymentDetail> yearlyPaymentDetails;

        public EMIService()
        {
        }

        public double CalculateEMI(double loanAmount, double interest, int term, LoanTermType termType)
        {
            interestFactor = Convert.ToDouble((interest / 12) / 100);
            tenure = (termType == LoanTermType.Years) ? (term * 12) : term;
            if (!interestFactor.Equals(0))
            {
                double emprical = Math.Pow(1 + interestFactor, tenure);
                currentEMI = (loanAmount * interestFactor * emprical) / (emprical - 1);
            }
            else
            {
                currentEMI = loanAmount / tenure;
            }

            return currentEMI;
        }

        public double GetPayableInterestAmount(int term, LoanTermType termType, double loanAmount)
        {
            tenure = (termType == LoanTermType.Years) ? (term * 12) : term;
            return Math.Round(((currentEMI * tenure) - loanAmount),2);
        }

        public double GetPayablePrincipalAmount(int term, LoanTermType termType)
        {
            tenure = (termType == LoanTermType.Years) ? (term * 12) : term;
            return Math.Round((currentEMI * tenure),2);
        }

        private void ResetCollections()
        {
            if (monthlyPaymentDetails != null && monthlyPaymentDetails.Count > 0)
            {
                monthlyPaymentDetails.Clear();
            }

            if (yearlyPaymentDetails != null && yearlyPaymentDetails.Count > 0)
            {
                yearlyPaymentDetails.Clear();
            }
        }

        public async Task<Dictionary<string, object>> GetAmortizationDetails(double interest, double emi, double loanAmount, int term, LoanTermType termType, DateTime paymentStartMonth)
        {
            DateTime date = paymentStartMonth;
            double beginBalance, endBalance, currentInterest, totalEmiAmount = 0, totalPrincipal = 0, totalInterest = 0;
            double emiPaidPerYear = 0, prncPaidPerYear = 0, totalInterestYear = 0, currentPrinicipal = loanAmount;

            interestFactor = Convert.ToDouble((interest / 12) / 100);
            tenure = (termType == LoanTermType.Years) ? (term * 12) : term;
            monthlyPaymentDetails = new ObservableCollection<MonthlyPaymentDetail>();
            yearlyPaymentDetails = new ObservableCollection<YearlyPaymentDetail>();

            for (int i = 0; i < tenure; i++)
            {
                currentInterest = !interestFactor.Equals(0) ? (currentPrinicipal * interestFactor) : loanAmount;
                totalInterest += currentInterest;
                totalEmiAmount += emi;
                totalPrincipal += emi - currentInterest;
                endBalance = currentPrinicipal - (emi - currentInterest);
                emiPaidPerYear += emi;
                prncPaidPerYear += emi - currentInterest;
                totalInterestYear += currentInterest;

                monthlyPaymentDetails.Add(
                    new MonthlyPaymentDetail
                    {
                        Principal = Math.Round((emi - currentInterest),2),
                        Interest = Math.Round((currentInterest),2),
                        Balance = Math.Round((endBalance),2),
                        Payment = Math.Round((emi),2),
                        Month = new DateTime(date.Year, date.Month, date.Day)
                    });
                if (i == 0 || date.Month == 1)
                {
                    beginBalance = currentPrinicipal;
                }

                if (date.Month == 12 || i.Equals(tenure - 1))
                {
                    yearlyPaymentDetails.Add(
                        new YearlyPaymentDetail
                        {
                            Principal = Math.Round((prncPaidPerYear),2),
                            Interest = Math.Round((totalInterestYear),2),
                            Balance = Math.Round((endBalance),2),
                            Payment = Math.Round((emiPaidPerYear),2),
                            Year = new DateTime(date.Year, date.Month, date.Day)
                        });
                    emiPaidPerYear = 0;
                    prncPaidPerYear = 0;
                    totalInterestYear = 0;
                }

                currentPrinicipal = endBalance;
                if (i < tenure - 1)
                {
                    date = date.AddMonths(1);
                }
            }
            return new Dictionary<string, object>
            {
                { "monthlyDetails", monthlyPaymentDetails},
                { "yearlyDetails", yearlyPaymentDetails}
            };
        }
    }
}