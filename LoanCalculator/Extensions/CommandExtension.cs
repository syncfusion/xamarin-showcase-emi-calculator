using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<Task> execute) : base(() => execute())
        {
        }
    }
}