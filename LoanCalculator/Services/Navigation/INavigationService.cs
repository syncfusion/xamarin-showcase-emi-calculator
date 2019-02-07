using System.Threading.Tasks;

namespace LoanCalculator
{
    public interface INavigationService
    {
        Task InitializeAppAsync();

        Task NavigateAppAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
    }
}