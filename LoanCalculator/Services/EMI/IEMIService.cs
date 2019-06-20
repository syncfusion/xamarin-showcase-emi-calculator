using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanCalculator
{
    public interface IEMIService
    {
        double CalculateEMI(double loanAmount, double interest, int term, LoanTermType termType);

        double GetPayableInterestAmount(int term, LoanTermType termType, double loanAmount);

        double GetPayablePrincipalAmount(int term, LoanTermType termType);

        Task<Dictionary<string, object>> GetAmortizationDetails(double loanAmount, double interest, double emi, int term, LoanTermType termType, DateTime paymentStartMonth);
    }
}