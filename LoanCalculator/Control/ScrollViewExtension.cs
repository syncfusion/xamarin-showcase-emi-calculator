using Xamarin.Forms;

namespace LoanCalculator
{
    public class ScrollViewExt : ScrollView
    {
        public ScrollViewExt()
        {
            MessagingCenter.Subscribe<LoanDetailsViewModel>(
                this,
                "ScrollToEnd",
                (sender) => { ScrollToAsync(Content, ScrollToPosition.End, true); });
        }
    }
}