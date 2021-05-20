using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProlizSts
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Request.GetOwinContext().Authentication.SignOut();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            HttpContext.Current.GetOwinContext().Authentication.Challenge(
                new AuthenticationProperties { RedirectUri = "Router.aspx?type=o" },
                OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HttpContext.Current.GetOwinContext().Authentication.Challenge(
                new AuthenticationProperties { RedirectUri = "Router.aspx?type=p" },
                OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }
    }
}