using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Controls
{
    public class TargetEntityAttributesFactory
    {
        public IEnumerable<TargetEntityAttribute> Create(IEnumerable<AttributeMetadata> attributeMetadatas)
        {
            foreach (AttributeMetadata attributeMetadata in attributeMetadatas)
            {
                string logicalName = attributeMetadata.LogicalName;
                string label = ExtractLabel(attributeMetadata, logicalName);
                yield return new TargetEntityAttribute(label, logicalName);
            }
        }

        private string ExtractLabel(AttributeMetadata attributeMetadata, string logicalName)
        {
            return attributeMetadata.DisplayName.UserLocalizedLabel == null || 
                string.IsNullOrEmpty(attributeMetadata.DisplayName.UserLocalizedLabel.Label) ? 
                logicalName : attributeMetadata.DisplayName.UserLocalizedLabel.Label;
        }
    }
}
