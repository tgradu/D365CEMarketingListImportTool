using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.UIMessages
{
    public static class LoginWindowMessages
    {
        public static string WelcomeMessage { get => "Welcome to the Excel to Marketing List Import tool. Please enter the URL to your instance together with the credentials to log in."; }
        public static string ConnectingMessage { get => "Connecting... Please wait."; }
        public static string ConnectedMessage { get => "Connected. Initializing Components"; }
        public static string CredentialsError { get => "Unable to connect to CRM. Please check your credentials and try again"; }
        public static string ConnectionError { get => "There was an error during connection. Please check the log files for more details"; }
    }
}
