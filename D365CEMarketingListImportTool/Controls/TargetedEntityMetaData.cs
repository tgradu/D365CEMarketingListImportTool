namespace D365CEMarketingListImportTool.Controls
{
    public class TargetedEntityMetadata
    {
        public string PlatformName { get; }
        public int TypeCode { get; }
        public string LocalizedLabel { get; }

        public TargetedEntityMetadata(string platformName, string localizedLabel, int typeCode)
        {
            PlatformName = platformName;
            TypeCode = typeCode;
            LocalizedLabel = localizedLabel;
        }
    }
}
