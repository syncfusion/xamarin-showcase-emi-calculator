using System.IO;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class ExportDataService : IExportDataService
    {
        public void ExportStatisticsData(SfDataGrid dataGrid)
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            var excelEngine = excelExport.ExportToExcel(dataGrid);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();
            DependencyService.Get<ISave>().Save("LoanCalculator.xlsx", "application/msexcel", stream);
        }
    }
}