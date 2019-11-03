using D365CEMarketingListImportTool.Services.Xrm;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.ViewModels
{

    public class ExcelToCrmListWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private CrmServiceClient crmServiceClient;
        private string loggedUser;

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
        #endregion Properties

        #region Constructors
        public ExcelToCrmListWindowViewModel()
        {
            
        }
        #endregion Constructors

        #region Commands
        #endregion Commands

        #region Methods
        private void NotifyPropertyChanged([CallerMemberName]String propertyName = "")
        {
            // This method is called by the Set accessor of each property.
            // The CallerMemberName attribute that is applied to the optional propertyName
            // parameter causes the property name of the caller to be substituted as an argument.

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetXrmContext(XrmContext xrmContext)
        {
            crmServiceClient = xrmContext.CrmServiceClient;
            LoggedUser = xrmContext.LoggedUser;

        }
        #endregion Methods


    }
}
