using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Controls
{
    public class TargetEntityAttribute
    {
        public string DisplayName { get; }
        public TargetEntityAttribute(string name)
        {
            DisplayName = name;
        }
    }
}
