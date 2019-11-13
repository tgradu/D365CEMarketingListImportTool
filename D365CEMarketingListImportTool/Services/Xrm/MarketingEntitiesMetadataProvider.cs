using D365CEMarketingListImportTool.Controls;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
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

        public async Task<IEnumerable<TargetedEntityMetadata>> GetEntityMetaData()
        {
            var pickListData = await Task.Run(() => crmServiceClient.GetPickListElementFromMetadataEntity("list", "createdfromcode"));

            var objectTypeCodes = pickListData.Items.Select(x => x.PickListItemId).ToArray();
            return await GetEntityMetadataByTypeCode(pickListData, objectTypeCodes);
        }

        public async Task<IEnumerable<AttributeMetadata>> GetAttributeMetadata(string marketingListEntityName)
        {
            var stringAttributes = await Task.Run(() => crmServiceClient.GetAllAttributesForEntity(marketingListEntityName));

            return stringAttributes.Where(attr => attr is StringAttributeMetadata && attr.IsLogical == false)
                .OrderBy(attr => attr.LogicalName);
        }

        private async Task<IEnumerable<TargetedEntityMetadata>> GetEntityMetadataByTypeCode(PickListMetaElement pickListData, int[] objectTypeCodes)
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

        private IEnumerable<TargetedEntityMetadata> AggregatePickListWithEntityMetaData(PickListMetaElement pickListData, RetrieveMetadataChangesResponse retrieveMetadataChangesResponse)
        {
            return from pickListItem in pickListData.Items
                   join entityMetadata in retrieveMetadataChangesResponse.EntityMetadata on pickListItem.PickListItemId equals entityMetadata.ObjectTypeCode.Value
                   select new TargetedEntityMetadata(entityMetadata.LogicalName, pickListItem.DisplayLabel, entityMetadata.ObjectTypeCode.Value);
        }
    }
}
