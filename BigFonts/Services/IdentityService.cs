using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BigFonts.Services
{
    public class IdentityService
    {
        public static string GetCurrentSID()
        {
            var user = WindowsIdentity.GetCurrent().User;
            return user.Value;
        }
    }
}
