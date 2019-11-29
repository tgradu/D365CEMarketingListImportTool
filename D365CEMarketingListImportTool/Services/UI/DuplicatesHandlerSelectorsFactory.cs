using D365CEMarketingListImportTool.Controls;
using System.Collections.ObjectModel;

namespace D365CEMarketingListImportTool.Services.UI
{
    public static class DuplicatesHandlerSelectorsFactory
    {
        public static ObservableCollection<DuplicatesHandlerSelector> Create()
        {
            return new ObservableCollection<DuplicatesHandlerSelector>
            {
                new DuplicatesHandlerSelector("Add First Active", 1),
                new DuplicatesHandlerSelector("Add All", 2),
                new DuplicatesHandlerSelector("Add None", 3)
            };
        }
    }
}
