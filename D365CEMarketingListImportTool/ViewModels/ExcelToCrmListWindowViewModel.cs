using D365CEMarketingListImportTool.Controls;
using D365CEMarketingListImportTool.MVVMFramework;
using D365CEMarketingListImportTool.Services.Excel;
using D365CEMarketingListImportTool.Services.Xrm;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace D365CEMarketingListImportTool.ViewModels
{

    public class ExcelToCrmListWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly CrmServiceClient crmServiceClient;
        private string loggedUser;
        private string connectedTo;
        private string excelPath;
        private MarketingExcel marketingExcel;

        private FromExcelColumn selectedExcelColumn;
        private TargetedEntityMetaData selectedMarketingListEntity;

        private readonly Loader excelLoader = new Loader();

        #endregion Fields

        #region Properties
        public string LoggedUser
        {
            get => loggedUser;
            set
            {
                loggedUser = value;
                NotifyPropertyChanged();
            }
        }
        public string ConnectedTo
        {
            get => connectedTo;
            set
            {
                connectedTo = value;
                NotifyPropertyChanged();
            }
        }
        public string ExcelPath
        {
            get => excelPath;
            set
            {
                excelPath = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<FromExcelColumn> ExcelColumns { get; set; } = new ObservableCollection<FromExcelColumn>();
        public FromExcelColumn SelectedExcelColumn
        {
            get => selectedExcelColumn;
            set
            {
                selectedExcelColumn = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<TargetedEntityMetaData> MarketingListEntities { get; set; } = new ObservableCollection<TargetedEntityMetaData>();
        public TargetedEntityMetaData SelectedMarketingListEntity
        {
            get => selectedMarketingListEntity;
            set
            {
                selectedMarketingListEntity = value;
                NotifyPropertyChanged();
            }
        }

        #endregion Properties

        #region Constructors
        public ExcelToCrmListWindowViewModel(XrmContext xrmContext)
        {
            crmServiceClient = xrmContext.CrmServiceClient;
            LoggedUser = xrmContext.LoggedUser;
            ConnectedTo = crmServiceClient.ConnectedOrgFriendlyName;
        }

        #endregion Constructors

        #region Commands
        public ICommand LoadExcelCommand
        {
            get
            {
                return new RelayCommand(LoadExcel);
            }
        }
        public ICommand FetchEntityAttributesCommand
        {
            get
            {
                return new RelayCommandAsync(FetchEntityAttributes);
            }
        }
        public ICommand InitializeViewControlsCommand
        {
            get
            {
                return new RelayCommandAsync(InitializeViewControls);
            }
        }

        #endregion Commands

        #region Methods

        private void LoadExcel(object obj)
        {
            marketingExcel = excelLoader.Load();
            InitializeExcelControlls(marketingExcel);
        }

        private Task FetchEntityAttributes(object obj)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }

        private async Task InitializeViewControls(object obj)
        {
            MarketingEntitiesMetadataProvider marketingEntitiesMetadataProvider = new MarketingEntitiesMetadataProvider(crmServiceClient);
            var crmEntities = await marketingEntitiesMetadataProvider.GetEntityMetaData();

            foreach(var marketingEntity in crmEntities)
            {
                MarketingListEntities.Add(marketingEntity);
            }
        }

        private void InitializeExcelControlls(MarketingExcel marketingExcel)
        {
            ExcelPath = marketingExcel.Path;

            IEnumerable<FromExcelColumn> columns = marketingExcel.ColumnHeaders.Select(header => new FromExcelColumn(header));
            
            foreach(FromExcelColumn excelColumn in columns)
            {
                ExcelColumns.Add(excelColumn);
            }

            SelectedExcelColumn = ExcelColumns.FirstOrDefault();
        }

        private void NotifyPropertyChanged([CallerMemberName]String propertyName = "")
        {
            // This method is called by the Set accessor of each property.
            // The CallerMemberName attribute that is applied to the optional propertyName
            // parameter causes the property name of the caller to be substituted as an argument.

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods


    }
}
