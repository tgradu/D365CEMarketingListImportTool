using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    public class XrmContextBuilder
    {
        private readonly CrmServiceClient crmServiceClient;

        public XrmContextBuilder(CrmServiceClient crmServiceClient)
        {
            this.crmServiceClient = crmServiceClient;
        }

        public async Task<XrmContext> Build()
        {
            string loggedInUser = await GetLoggedInUser(crmServiceClient);
            return new XrmContext(crmServiceClient, loggedInUser);
        }

        private async Task<string> GetLoggedInUser(CrmServiceClient crmServiceClient)
        {
            var whoAmIResponse = await Task.Run(() => (WhoAmIResponse)crmServiceClient.Execute(new WhoAmIRequest()));
            Entity loggedInUser = await Task.Run(() => crmServiceClient.Retrieve("systemuser", whoAmIResponse.UserId, new ColumnSet("fullname")));

            return loggedInUser["fullname"].ToString();
        }
    }
}
