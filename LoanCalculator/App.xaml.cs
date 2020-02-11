using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Preserve]

namespace LoanCalculator
{
    public partial class App : Application
    {

        static App()
        {
            IntializeBuildContainer();
        }

        public App()
        {
            InitializeComponent();

            //Todo Disable licensing when publishing
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
            InitNavigation();
        }

        private static void IntializeBuildContainer()
        {
            ViewModelLocator.Start();
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.NavigateAppAsync<LoanDetailsViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}