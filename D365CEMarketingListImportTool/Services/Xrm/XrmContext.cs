using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    public class XrmContext
    {
        public CrmServiceClient CrmServiceClient { get; }
        public string LoggedUser { get; }
        public string OrganizationName { get; }

        public XrmContext(CrmServiceClient crmServiceClient, string loggedUser)
        {
            CrmServiceClient = crmServiceClient;
            OrganizationName = crmServiceClient.ConnectedOrgFriendlyName;
            LoggedUser = loggedUser;
        }
    }
}
