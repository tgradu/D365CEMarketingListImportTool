using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Controls
{
    public class TargetedEntity
    {
        public string Name { get; private set; }
        public int TypeCode { get; private set; }
    
        public TargetedEntity(string name, int typeCode)
        {
            Name = name;
            TypeCode = typeCode;
        }
    }
}
