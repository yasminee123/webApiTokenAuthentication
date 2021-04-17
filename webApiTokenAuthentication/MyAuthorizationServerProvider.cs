using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace webApiTokenAuthentication
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            COG_KSEntities db = new COG_KSEntities();

            VM_Collaborateur _collaborateur = new VM_Collaborateur();
            _collaborateur = db.VM_Collaborateur.FirstOrDefault(cc => cc.Login == context.UserName && cc.Psw == context.Password);
            

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == _collaborateur.Login && context.Password == _collaborateur.Psw && _collaborateur.CO_NO != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "commercial"));
                identity.AddClaim(new Claim(_collaborateur.Login, "commercial"));
                identity.AddClaim(new Claim(ClaimTypes.Name, _collaborateur.CO_NO.ToString()));
                context.Validated(identity);
            }
            else if (context.UserName == _collaborateur.Login && context.Password == _collaborateur.Psw && _collaborateur.CO_NO == null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "client"));
                identity.AddClaim(new Claim(_collaborateur.Login, "client"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "0"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
        }
    }
}