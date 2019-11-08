using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Controls
{
    public class FromExcelColumn
    {
        public string Name { get; private set; }

        public FromExcelColumn(string name)
        {
            Name = name;
        }
    }
}
