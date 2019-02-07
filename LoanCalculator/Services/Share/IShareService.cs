namespace LoanCalculator
{
    public interface IShareService
    {
        void ShareLoanDetails(double loanAmount, double interest, int term, LoanTermType termType, double emiAmount, double totalPricipalAmount, double totalInterstAmount);
    }
}
