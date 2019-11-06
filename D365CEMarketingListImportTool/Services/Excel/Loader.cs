using D365CEMarketingListImportTool.UIMessages;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Excel
{
    public class Loader
    {
        public MarketingExcel Load()
        {
            string path = GetPath();
            ISheet sheet = LoadExcelSheet(path);
            List<string> columnHeaders = GetColumnHeadings(sheet);

            return new MarketingExcel(path, columnHeaders, sheet);
        }

        private List<string> GetColumnHeadings(ISheet sheet)
        {
            var columnHeadings = new List<string>();
            IRow headerRow = sheet.GetRow(0);

            if (!IsExcelValid(sheet, headerRow))
                throw new ArgumentException(ExcelErrors.InvalidColumnsError);

            foreach (ICell headerCell in headerRow)
            {
                columnHeadings.Add(headerCell.ToString());
            }

            return columnHeadings;
        }

        private bool IsExcelValid(ISheet sheet, IRow headerRow)
        {
            int headerColumns = headerRow.Cells.Count;

            if (headerColumns < 1)
                return false;

            int maxNumberOfColumns = 1;

            foreach (IRow row in sheet)
            {
                maxNumberOfColumns = Math.Max(headerRow.Cells.Count, maxNumberOfColumns);
            }
            
            return headerRow.Cells.Count >= maxNumberOfColumns;
        }

        private ISheet LoadExcelSheet(string path)
        {
            const int firstSheetIndex = 0;

            IWorkbook workbook = GenerateWorkbook(path);
            return workbook.GetSheetAt(firstSheetIndex);
        }

        private IWorkbook GenerateWorkbook(string path)
        {
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return WorkbookFactory.Create(file);
            }
        }

        private string GetPath()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XLSX Files (*.xlsx)|*.xlsx|XLS Files(*.xls)|*.xls";

            dlg.ShowDialog();

            return dlg.FileName;
        }
    }
}
