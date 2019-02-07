using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoanCalculator
{
    public abstract class ViewModelBase : BindableObject
    {
        #region properties
        public static DateTime PaymentStartMonth
        {
            get;
            set;
        }

        protected INavigationService NavigationService { get; set; }
        #endregion

        #region constructor
        protected ViewModelBase()
        {
            NavigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
        }
        #endregion

        #region methods
        public virtual Task InitializeViewModelAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        #endregion
    }
}