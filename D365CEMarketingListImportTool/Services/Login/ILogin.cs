using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Login
{
    public interface ILogin
    {
        SecureString PasswordValue { get; }
        void Close();
    }
}
