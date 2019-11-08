using D365CEMarketingListImportTool.Controls;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    public class MetaDataProvider
    {
        private readonly CrmServiceClient crmServiceClient;

        public MetaDataProvider(CrmServiceClient crmServiceClient)
        {
            this.crmServiceClient = crmServiceClient;
        }

        public async Task<List<TargetedEntity>> GetTargetEntities()
        {
            var ceva = await Task.Run(() => crmServiceClient.GetPickListElementFromMetadataEntity("list", "createdfromcode"));

            ceva.
        }
    }
}
