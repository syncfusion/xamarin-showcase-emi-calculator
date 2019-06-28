using Android.App;
using Android.OS;

namespace LoanCalculator.Droid
{
	[Activity(Label = "@string/app_name", Theme = "@style/Theme.Splash",
              MainLauncher = true,
              NoHistory = true, Icon = "@drawable/Icon")]
	public class SplashActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			System.Threading.Thread.Sleep(100);
			this.StartActivity(typeof(MainActivity));
        }
	}
}