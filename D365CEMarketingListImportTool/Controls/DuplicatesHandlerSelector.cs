namespace D365CEMarketingListImportTool.Controls
{
    public class DuplicatesHandlerSelector
    {
        public string ActionName { get; }
        public int ActionCode { get; }

        public DuplicatesHandlerSelector(string actionName, int actionCode)
        {
            ActionName = actionName;
            ActionCode = actionCode;
        }
    }
}
