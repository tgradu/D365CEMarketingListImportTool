using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    interface ICrmServiceClientBuilder
    {
        Task<CrmServiceClient> BuildAsync(ConnectionDetails connectionDetails);
    }
}
