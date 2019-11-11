using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Controls
{
    public class TargetedEntityMetaData
    {
        public string PlatformName { get; }
        public int TypeCode { get; }
        public string LocalizedLabel { get; }

        public TargetedEntityMetaData(string platformName, string localizedLabel, int typeCode)
        {
            PlatformName = platformName;
            TypeCode = typeCode;
            LocalizedLabel = localizedLabel;
        }
    }
}
