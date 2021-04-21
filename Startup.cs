using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ProlizSts.Startup))]

namespace ProlizSts
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = "proliz",
                    Authority = "https://sts4.sdu.edu.tr/",
                    RedirectUri = "https://localhost:44379/", // Uygulamada login olduktan sonra geri dönüş adresi
                    PostLogoutRedirectUri = "https://localhost:44379/", // Uygulamada çıkış yapıldıktan sonra geri dönüş adresi
                    ResponseType = "id_token",
                    Scope = "openid profile kimlik_no",
                    SignInAsAuthenticationType = "Cookies",
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        SecurityTokenValidated = async notification =>
                        {
                            var id = notification.AuthenticationTicket.Identity;

                            id.AddClaim(new Claim("id_token", notification.ProtocolMessage.IdToken));
                            notification.AuthenticationTicket = new AuthenticationTicket(
                                   id,
                                   notification.AuthenticationTicket.Properties);
                        },


                        RedirectToIdentityProvider = async notification =>
                        {
                            if (notification.ProtocolMessage.RequestType == Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectRequestType.Logout)
                            {
                                var idTokenHint = notification.OwinContext.Authentication.User.FindFirst("id_token").Value;
                                notification.ProtocolMessage.IdTokenHint = idTokenHint;
                            }
                        }
                    }

                });
        }
    }
}
