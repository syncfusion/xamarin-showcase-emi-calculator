using Xamarin.Forms;

namespace LoanCalculator
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height && Device.Idiom != TargetIdiom.Phone)
            {
                HorizontalView.IsVisible = true;
                VerticalView.IsVisible = false;
            }
            else
            {
                HorizontalView.IsVisible = false;
                VerticalView.IsVisible = true;
            }
        }
    }
}