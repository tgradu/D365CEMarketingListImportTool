using System.Windows;

namespace D365CEMarketingListImportTool.Views
{
    /// <summary>
    /// Interaction logic for ExcelToCrmListWindow.xaml
    /// </summary>
    public partial class ExcelToCrmListWindow : Window
    {
        public ExcelToCrmListWindow()
        {
            InitializeComponent();
        }

        public ExcelToCrmListWindow(object dataContext) : this()
        {
            this.DataContext = dataContext;
        }
    }
}
