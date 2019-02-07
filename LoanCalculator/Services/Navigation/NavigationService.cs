using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> MappingPageAndViewModel;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public async Task InitializeAppAsync()
        {
            await NavigateAppAsync<LoanDetailsViewModel>();
        }

        public NavigationService()
        {
            MappingPageAndViewModel = new Dictionary<Type, Type>();

            SetPageViewModelMappings();
        }

        public Task NavigateAppAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return NavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return NavigateToAsync(typeof(TViewModel), parameter);
        }

        protected virtual async Task NavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is HomePage)
            {
                CurrentApplication.MainPage = new NavigationPage(page)
                {
                    BarTextColor = Color.FromHex("#DAE1FF"),
                    BarBackgroundColor = Color.FromHex("#252C48")
                };
            }
            else if (page is StatisticPage)
            {
                var mainPage = CurrentApplication.MainPage as NavigationPage;
                await mainPage.PushAsync(page);
            }

            await ( page.BindingContext as ViewModelBase).InitializeViewModelAsync(parameter);
        }

        protected Type GetPageForViewModel(Type viewModelType)
        {
            if (!MappingPageAndViewModel.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return MappingPageAndViewModel[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as ViewModelBase;
            var statisticsViewModel = viewModel as StatisticPageViewModel;
            if (statisticsViewModel != null)
            {
                //Populating Chart/Grid collection before page loading
                var paymentDetails = parameter as Dictionary<string, object>;
                if (paymentDetails != null)
                {
                    statisticsViewModel.MonthlyPaymentDetails = (ObservableCollection<MonthlyPaymentDetail>)paymentDetails["monthlyDetails"];
                    statisticsViewModel.YearlyPaymentDetails = (ObservableCollection<YearlyPaymentDetail>)paymentDetails["yearlyDetails"];
                }
            }

            page.BindingContext = viewModel;

            return page;
        }

        private void SetPageViewModelMappings()
        {
            MappingPageAndViewModel.Add(typeof(LoanDetailsViewModel), typeof(HomePage));
            MappingPageAndViewModel.Add(typeof(StatisticPageViewModel), typeof(StatisticPage));
        }
    }
}