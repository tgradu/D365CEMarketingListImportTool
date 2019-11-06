using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.UIMessages
{
    public static class LoginWindowMessages
    {
        public const string WelcomeMessage = "Welcome to the Excel to Marketing List Import tool. Please enter the URL to your instance together with the credentials to log in."; 
        public const string ConnectingMessage = "Connecting... Please wait."; 
        public const string ConnectedMessage = "Connected. Initializing Components"; 
        public const string CredentialsError = "Unable to connect to CRM. Please check your credentials and try again"; 
        public const string ConnectionError = "There was an error during connection. Please check the log files for more details"; 
    }
}
