using D365CEMarketingListImportTool.MVVMFramework;
using D365CEMarketingListImportTool.Services.Xrm;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace D365CEMarketingListImportTool.ViewModels
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields
        private ICrmServiceClientBuilder crmServiceClientBuilder;
        #endregion Fields

        #region Properties

        public string UserName { get; set; }
        public string D365CEUrl { get; set; }

        #endregion Properties

        #region Constructors
        public LoginWindowViewModel()
        {
        }

        #endregion Constructors

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommandAsync(BuildCrmConnection);
            }
        }


        #endregion Commands

        #region Methods
        private async Task BuildCrmConnection(object parameter)
        {
            var connectionDetails = new ConnectionDetails
            {
                UserName = this.UserName,
                Url = D365CEUrl,
                Password = ((PasswordBox)parameter).SecurePassword
            };

            crmServiceClientBuilder = new Office365ConnectionBuilder();
            CrmServiceClient crmServiceClient = await crmServiceClientBuilder.Build(connectionDetails);

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
