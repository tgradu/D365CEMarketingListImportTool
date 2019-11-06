using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
