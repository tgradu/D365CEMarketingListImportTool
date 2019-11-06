using D365CEMarketingListImportTool.MVVMFramework;
using D365CEMarketingListImportTool.Services.Excel;
using D365CEMarketingListImportTool.Services.Xrm;
using Microsoft.Win32;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
        private MarketingExcel marketingExcel;

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

        #endregion Commands

        #region Methods

        private void LoadExcel(object obj)
        {
            Loader excelLoader = new Loader();
            marketingExcel = excelLoader.Load();

            InitializeExcelControlls(marketingExcel);
        }

        private void InitializeExcelControlls(MarketingExcel marketingExcel)
        {
            //ToDo
            //Path
            //Dropdown list with headers
            throw new NotImplementedException();
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
