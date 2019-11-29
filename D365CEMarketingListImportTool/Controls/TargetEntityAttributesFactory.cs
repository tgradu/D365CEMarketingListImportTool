using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace D365CEMarketingListImportTool.Controls
{
    public static class TargetEntityAttributesFactory
    {
        public static IEnumerable<TargetEntityAttribute> Create(IEnumerable<AttributeMetadata> attributeMetadatas)
        {
            foreach (AttributeMetadata attributeMetadata in attributeMetadatas)
            {
                string logicalName = attributeMetadata.LogicalName;
                string label = ExtractLabel(attributeMetadata, logicalName);
                yield return new TargetEntityAttribute(label, logicalName);
            }
        }

        private static string ExtractLabel(AttributeMetadata attributeMetadata, string logicalName)
        {
            return attributeMetadata.DisplayName.UserLocalizedLabel == null || 
                string.IsNullOrEmpty(attributeMetadata.DisplayName.UserLocalizedLabel.Label) ? 
                logicalName : attributeMetadata.DisplayName.UserLocalizedLabel.Label;
        }
    }
}
