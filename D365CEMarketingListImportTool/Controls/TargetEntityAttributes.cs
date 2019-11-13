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
        public string LogicalName { get; }
        public TargetEntityAttribute(string displayName, string logicalName)
        {
            DisplayName = displayName;
            LogicalName = logicalName;
        }
    }
}
