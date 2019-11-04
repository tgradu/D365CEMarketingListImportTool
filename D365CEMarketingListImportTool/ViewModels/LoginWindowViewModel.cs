using D365CEMarketingListImportTool.MVVMFramework;
using D365CEMarketingListImportTool.Services.Login;
using D365CEMarketingListImportTool.Services.Xrm;
using D365CEMarketingListImportTool.Views;
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
    // https://stackoverflow.com/questions/16652501/open-a-new-window-in-mvvm
    // https://stackoverflow.com/questions/43689124/adding-autofac-to-wpf-mvvm-application

    public class LoginWindowViewModel : INotifyPropertyChanged
    {

        #region Fields
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private ICrmServiceClientBuilder crmServiceClientBuilder;
        private string loginStatus;
        #endregion Fields

        #region Properties

        public string UserName { get; set; }
        public string D365CEUrl { get; set; }
        public string LoginStatus
        {
            get => loginStatus;
            set
            {
                loginStatus = value;
                NotifyPropertyChanged();
            }
        }

        #endregion Properties

        #region Constructors
        public LoginWindowViewModel()
        {
            InitializWindow();
        }

        #endregion Constructors

        #region Commands
        public ICommand LoginCommand
        {
            get => new RelayCommandAsync(BuildCrmConnectionAsync);
        }


        #endregion Commands

        #region Methods
        public void InitializWindow()
        {
            LoginStatus = "Welcome to the Excel to Marketing List Import tool. Please enter the URL to your instance together with the credentials to log in.";
        }
        private async Task BuildCrmConnectionAsync(object parameter)
        {
            if (!(parameter is ILogin))
            {
                throw new ArgumentException(nameof(ILogin));
            }

            var login = parameter as ILogin;
            await BuildCrmConnectionInternalAsync(login);
        }

        private async Task BuildCrmConnectionInternalAsync(ILogin login)
        {
            LoginStatus = "Connecting... Please wait.";

            var connectionDetails = new ConnectionDetails
            {
                UserName = this.UserName,
                Url = D365CEUrl,
                Password = login.PasswordValue
            };

            try
            {
                crmServiceClientBuilder = new Office365ConnectionBuilder();
                CrmServiceClient crmServiceClient = await crmServiceClientBuilder.BuildAsync(connectionDetails);

                if (crmServiceClient.IsReady)
                {
                    LoginStatus = "Connected. Initializing Components";
                    await LaunchExcelToMarketingListModuleAsync(crmServiceClient, login);
                }

                LoginStatus = "Unable to connect to CRM. Please check your credentials and try again";
            }
            catch(Exception ex)
            {
                LoginStatus = "There was an error during connection. Please check the log files for more details";
            }

        }

        private async Task LaunchExcelToMarketingListModuleAsync(CrmServiceClient crmServiceClient, ILogin login)
        {
            XrmContextBuilder xrmContextBuilder = new XrmContextBuilder(crmServiceClient);
            XrmContext xrmContext = await xrmContextBuilder.BuildAsync();

            var excelToCrmListWindowModel = new ExcelToCrmListWindowViewModel(xrmContext);
            ExcelToCrmListWindow excelToCrmListWindow = new ExcelToCrmListWindow();
            excelToCrmListWindow.DataContext = excelToCrmListWindowModel;

            excelToCrmListWindow.Show();
            login.Close();
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
