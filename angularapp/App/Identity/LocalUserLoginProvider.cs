using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;

namespace App.Identity
{
    public class LocalUserLoginProvider : ILoginProvider
    {
        public bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity)
        {
            var strName = System.Security.Principal.WindowsIdentity.GetCurrent()?.Name;
            var strType = System.Security.Principal.WindowsIdentity.GetCurrent()?.AuthenticationType;

            using (var pc = new PrincipalContext(ContextType.Machine))
            {
                bool isValid = pc.ValidateCredentials(userName, password);
                if (isValid)
                {
                    identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                }
                else
                {
                    identity = null;
                }

                return isValid;
            }
        }
    }
}