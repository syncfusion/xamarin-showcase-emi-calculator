using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class CustomDataGridStyle : DataGridStyle
    {
        public CustomDataGridStyle()
        {
        }

        public override Color GetHeaderBackgroundColor() => Color.FromHex("#252C48");

        public override Color GetHeaderForegroundColor() => Color.FromHex("#DAE1FF");

        public override Color GetRecordBackgroundColor() => Color.FromHex("#1B1F35");

        public override Color GetRecordForegroundColor() => Color.FromHex("#B0BEF9");

        public override Color GetCaptionSummaryRowBackgroundColor() => Color.FromHex("#353D5D");

        public override Color GetCaptionSummaryRowForegroundColor() => Color.FromHex("#DAE1FF");

        public override Color GetIndentBackgroundColor(RowType rowType)
        {
            if (rowType == RowType.CaptionCoveredRow)
            {
                return Color.FromHex("#1B1F35");
            }
            else if (rowType == RowType.HeaderRow)
            {
                return Color.FromHex("#252C48");
            }

            return Color.Default;
        }

        public override GridLinesVisibility GetGridLinesVisibility()
        {
            return GridLinesVisibility.None;
        }
    }
}