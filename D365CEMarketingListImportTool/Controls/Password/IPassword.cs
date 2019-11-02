using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Controls.Password
{
    interface IPassword
    {
        SecureString PasswordValue { get; }
    }
}
