using D365CEMarketingListImportTool.Controls;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Tooling.Connector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.Xrm.Tooling.Connector.CrmServiceClient;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    public class MarketingEntitiesMetadataProvider
    {
        private readonly CrmServiceClient crmServiceClient;

        public MarketingEntitiesMetadataProvider(CrmServiceClient crmServiceClient)
        {
            this.crmServiceClient = crmServiceClient;
        }

        public async Task<List<TargetedEntityMetaData>> GetEntityMetaData()
        {
            var pickListData = await Task.Run(() => crmServiceClient.GetPickListElementFromMetadataEntity("list", "createdfromcode"));

            var objectTypeCodes = pickListData.Items.Select(x => x.PickListItemId).ToArray();
            return await GetEntityMetadataByTypeCode(pickListData, objectTypeCodes);
        }

        private async Task<List<TargetedEntityMetaData>> GetEntityMetadataByTypeCode(PickListMetaElement pickListData, int[] objectTypeCodes)
        {
            EntityQueryExpression entityQueryExpression = new EntityQueryExpression();
            entityQueryExpression.Criteria.Conditions.Add(new MetadataConditionExpression("ObjectTypeCode", MetadataConditionOperator.In, objectTypeCodes));
            entityQueryExpression.Properties = new MetadataPropertiesExpression("LogicalName", "ObjectTypeCode");

            RetrieveMetadataChangesRequest retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest
            {
                Query = entityQueryExpression,
                ClientVersionStamp = null,
                DeletedMetadataFilters = DeletedMetadataFilters.Default
            };

            var retrieveMetadataChangesResponse = await Task.Run(() => (RetrieveMetadataChangesResponse)crmServiceClient.Execute(retrieveMetadataChangesRequest));
            
            return AggregatePickListWithEntityMetaData(pickListData, retrieveMetadataChangesResponse);
        }

        private List<TargetedEntityMetaData> AggregatePickListWithEntityMetaData(PickListMetaElement pickListData, RetrieveMetadataChangesResponse retrieveMetadataChangesResponse)
        {
            IEnumerable<TargetedEntityMetaData> joinMetaDataItemsQuery =
                from pickListItem in pickListData.Items
                join entityMetadata in retrieveMetadataChangesResponse.EntityMetadata on pickListItem.PickListItemId equals entityMetadata.ObjectTypeCode.Value
                select new TargetedEntityMetaData(entityMetadata.LogicalName, pickListItem.DisplayLabel, entityMetadata.ObjectTypeCode.Value);

            return joinMetaDataItemsQuery.ToList();
        }
    }
}
