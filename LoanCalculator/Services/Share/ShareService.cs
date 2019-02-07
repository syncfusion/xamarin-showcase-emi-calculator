using System.Globalization;
using Xamarin.Essentials;

namespace LoanCalculator
{
    public class ShareService : IShareService
    {
        public void ShareLoanDetails(double loanAmount, double interest, int term, LoanTermType termType, double emiAmount, double totalPricipalAmount, double totalInterstAmount)
        {
            string currencySymbol = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
            string loanTermType = termType.Equals(LoanTermType.Years) ? "Years" : "Months";
            string principalAmount = "Principal Loan Amount : " + currencySymbol + " " + loanAmount.ToString("N2", CultureInfo.InvariantCulture) + "\n";
            string interestPercent = "Interest Rate (in percentage): " + interest.ToString("N2", CultureInfo.InvariantCulture) + "p/a \n";
            string loanTerm = "Loan Term : " + term + " " + loanTermType + "\n\n";
            string emiAmountDetails = "Monthly Payment (EMI) : " + currencySymbol + " " + emiAmount.ToString("N2", CultureInfo.InvariantCulture) + "\n";
            string totalAmount = "Total Interest Payment : " + currencySymbol + " " + totalInterstAmount.ToString("N2", CultureInfo.InvariantCulture) + "\n";
            string payableAmount = "Total Amount (Principal + Interest): " + currencySymbol + " " + totalPricipalAmount.ToString("N2", CultureInfo.InvariantCulture) + "\n";

            DataTransfer.RequestAsync(new ShareTextRequest
            {
                Subject = "Loan Details",
                Text = "Loan Details" + "\n\n" + principalAmount + interestPercent + loanTerm + emiAmountDetails + totalAmount + payableAmount,
                Title = "Loan Details"
            });
        }
    }
}