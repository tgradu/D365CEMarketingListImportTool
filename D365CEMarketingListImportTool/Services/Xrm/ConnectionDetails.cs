using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    public class ConnectionDetails
    {
        public String UserName { get; set; }
        public SecureString Password { get; set; }
        public String Url { get; set; }
    }
}
