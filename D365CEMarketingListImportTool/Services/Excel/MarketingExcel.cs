using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace D365CEMarketingListImportTool.Services.Excel
{
    public class MarketingExcel
    {
        public string Path { get; }
        public List<string> ColumnHeaders { get; }
        public ISheet Sheet { get; }

        public MarketingExcel(string path, List<string> columnHeaders, ISheet sheet)
        {
            Path = path;
            ColumnHeaders = columnHeaders;
            Sheet = sheet;
        }
    }
}
