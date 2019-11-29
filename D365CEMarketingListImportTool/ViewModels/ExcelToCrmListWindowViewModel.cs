using D365CEMarketingListImportTool.Controls;
using D365CEMarketingListImportTool.MVVMFramework;
using D365CEMarketingListImportTool.Services.Excel;
using D365CEMarketingListImportTool.Services.UI;
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
        private bool searchActiveOnly;
        private readonly MarketingEntitiesMetadataProvider marketingEntitiesMetadataProvider;

        private FromExcelColumn selectedExcelColumn;
        private TargetedEntityMetadata selectedMarketingListEntity;
        private TargetEntityAttribute selectedEntityAttribute;
        private DuplicatesHandlerSelector selectedDuplicatesHandler;

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
        public bool SearchActiveOnly
        {
            get => searchActiveOnly;
            set
            {
                searchActiveOnly = value;
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
        public ObservableCollection<TargetedEntityMetadata> MarketingListEntities { get; set; } = new ObservableCollection<TargetedEntityMetadata>();
        public TargetedEntityMetadata SelectedMarketingListEntity
        {
            get => selectedMarketingListEntity;
            set
            {
                selectedMarketingListEntity = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<TargetEntityAttribute> TargetEntityAttributes { get; set; } = new ObservableCollection<TargetEntityAttribute>();
        public TargetEntityAttribute SelectedEntityAttribute
        {
            get => selectedEntityAttribute;
            set
            {
                selectedEntityAttribute = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<DuplicatesHandlerSelector> DuplicatesHandlerSelectors { get; set; }
        public DuplicatesHandlerSelector SelectedDuplicatesHandler
        {
            get => selectedDuplicatesHandler;
            set
            {
                selectedDuplicatesHandler = value;
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
            marketingEntitiesMetadataProvider = new MarketingEntitiesMetadataProvider(crmServiceClient);
            DuplicatesHandlerSelectors = DuplicatesHandlerSelectorsFactory.Create();
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
            InitializeExcelControls(marketingExcel);
        }

        private async Task FetchEntityAttributes(object obj)
        {
            TargetEntityAttributes.Clear();

            var atrributes = await marketingEntitiesMetadataProvider.GetAttributeMetadata(SelectedMarketingListEntity.PlatformName);
            IEnumerable<TargetEntityAttribute> targetEntityAttributes = TargetEntityAttributesFactory.Create(atrributes);

            foreach(TargetEntityAttribute targetEntityAttribute in targetEntityAttributes)
            {
                TargetEntityAttributes.Add(targetEntityAttribute);
            }

            SelectedEntityAttribute = TargetEntityAttributes.FirstOrDefault();
        }

        private async Task InitializeViewControls(object obj)
        {
            
            var crmEntities = await marketingEntitiesMetadataProvider.GetEntityMetaData();

            foreach(var marketingEntity in crmEntities)
            {
                MarketingListEntities.Add(marketingEntity);
            }
        }

        private void InitializeExcelControls(MarketingExcel marketingExcel)
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
