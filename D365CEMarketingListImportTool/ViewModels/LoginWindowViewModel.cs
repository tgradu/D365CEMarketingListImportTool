using D365CEMarketingListImportTool.MVVMFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace D365CEMarketingListImportTool.ViewModels
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields
        #endregion Fields

        #region Properties

        public string UserName { get; set; }
        public string D365CEUrl { get; set; }
        public SecureString Password { get; set; }

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
            throw new NotImplementedException();
        }
        #endregion Methods
    }
}
