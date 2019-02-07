using System;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace LoanCalculator
{
    public partial class StatisticPage : ContentPage
    {
        public StatisticPage()
        {
            InitializeComponent();
        }

        private void DataGrid_ItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
        {
            this.DataGrid.SortColumnDescriptions.Clear();
            this.DataGrid.View.SortDescriptions.Clear();
        }

        private void NumericalAxis_LabelCreated(object sender, ChartAxisLabelEventArgs e)
        {
            var yValue = Convert.ToDouble(e.LabelContent);
            if (yValue > 1000000000)
            {
                e.LabelContent = (yValue / 1000000000) + "B";
            }
            else if (yValue <= 1000000000 && yValue > 1000000)
            {
                e.LabelContent = (yValue / 1000000) + "M";
            }
            else if (yValue <= 1000000 && yValue >= 1000)
            {
                e.LabelContent = (yValue / 1000) + "K";
            }
        }
    }
}