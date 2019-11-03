using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    public class Office365ConnectionBuilder : ICrmServiceClientBuilder
    {
        const string authType = "Office365";
        private readonly PasswordConverter passwordConverter;

        public Office365ConnectionBuilder()
        {
            passwordConverter = new PasswordConverter();
        }

        public async Task<CrmServiceClient> BuildAsync(ConnectionDetails connectionDetails)
        {
            return await Task.Run(() => new CrmServiceClient($"Url={connectionDetails.Url};AuthType={authType};UserName={connectionDetails.UserName};Password={passwordConverter.ConvertToUnsecureString(connectionDetails.Password)}"));
        }
    }
}
