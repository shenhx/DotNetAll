using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace webApiJwtAuthDemo
{
    /// <summary>
    /// created by Grissom at 2016-10-09 12:00:00
    /// 自定义 jwt oauth 的授权验证
    /// </summary>
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var username = context.UserName;
            var password = context.Password;
            string userid;
            if (!CheckCredential(username, password, out userid))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }
            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, userid, username), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, string userid, string usercode)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim("userid", userid));
            identity.AddClaim(new Claim("username", usercode));
            return identity;
        }

        private static bool CheckCredential(string usernme, string password, out string userid)
        {
            var success = false;
            // 用户名和密码验证
            if (usernme == "admin" && password == "admin")
            {
                userid = "1";
                success = true;
            }
            else
            {
                userid = "";
            }
            return success;
        }
    }
}
