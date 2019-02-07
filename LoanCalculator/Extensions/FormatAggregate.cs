using Syncfusion.Data;
using System;
using System.Collections;
using System.Globalization;
using System.Reflection;

namespace LoanCalculator
{
    public class FormatAggregate : ISummaryAggregate
    {
        public string Year
        {
            get;
            set;
        }

        public string Balance
        {
            get;
            set;
        }

        public Action<IEnumerable, string, PropertyInfo> CalculateAggregateFunc()
        {
            return (items, property, pd) =>
            {
                var enumerator = items.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var data = enumerator.Current as MonthlyPaymentDetail;
                    if (pd.Name == "Year")
                    {
                        Year = data.Month.ToString("yyyy");
                    }
                    else if (pd.Name == "Balance")
                    {
                        if (data.Month.Month == 12 || data.Balance.Equals(0))
                        {
                            Balance = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + data.Balance.ToString("N2");
                        }
                    }
                }
            };
        }
    }
}