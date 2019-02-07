using Xamarin.Forms;
using LoanCalculator;
using Syncfusion.SfNumericUpDown.XForms.Droid;
using LoanCalculator.Droid;
using Syncfusion.SfNumericUpDown.XForms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomNumericUpDown), typeof(CustomNumericUpDownRenderer))]

namespace LoanCalculator.Droid
{
    public class CustomNumericUpDownRenderer : SfNumericUpDownRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SfNumericUpDown> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                for (int i = 0; i < this.Control.ChildCount; i++)
                {
                    var editText = this.Control.GetChildAt(i) as Android.Widget.EditText;
                    if (editText !=null)
                    {
                        editText.SetBackgroundResource(0);
                    }
                }
            }
        }
    }
}