namespace D365CEMarketingListImportTool.Controls
{
    public class TargetedEntityMetaData
    {
        public string PlatformName { get; }
        public int TypeCode { get; }
        public string LocalizedLabel { get; }

        public TargetedEntityMetaData(string platformName, string localizedLabel, int typeCode)
        {
            PlatformName = platformName;
            TypeCode = typeCode;
            LocalizedLabel = localizedLabel;
        }
    }
}
