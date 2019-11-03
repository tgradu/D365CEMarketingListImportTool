using D365CEMarketingListImportTool.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace D365CEMarketingListImportTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, ILogin
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public SecureString PasswordValue
        {
            get
            {
                return Password.SecurePassword;
            }
        }
    }
}
