using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Data;
using System;

namespace DB_Kursach
{
	public static class MyDataTableExtensions
	{
		public static async Task SaveExcelFile(DataTable dt, FileInfo file)
		{

			DeleteIfExists(file);
			try
			{
				using (var package = new ExcelPackage(file))
				{
					var ws = package.Workbook.Worksheets.Add("TheQuery");
					var range = ws.Cells["A2"].LoadFromDataTable(dt, true);

					range.AutoFitColumns();

                    //// Formats the header
                    //ws.Cells["A1"].Value = "Our Cool Report";
                    //ws.Cells["A1:C1"].Merge = true;
                    //ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //ws.Row(1).Style.Font.Size = 24;
                    //ws.Row(1).Style.Font.Color.SetColor(Color.Blue);

                    ws.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Row(1).Style.Font.Bold = true;
                    //ws.Column(3).Width = 20;

                    await package.SaveAsync();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private static void DeleteIfExists(FileInfo file)
		{
			try
			{
				if (file.Exists)
				{
					file.Delete();
				}
			}
			catch(Exception ex)
            {
				MessageBox.Show(ex.Message);
            }
		}
	}
}
