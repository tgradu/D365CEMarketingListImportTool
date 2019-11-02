using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.Services.Xrm
{
    public class PasswordConverter
    {
        public string ConvertToUnsecureString(SecureString _password)
        {
            if (_password == null)
                throw new ArgumentNullException(typeof(SecureString).Name);

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(_password);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
