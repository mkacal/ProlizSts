using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProlizSts
{
    public partial class Router : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("/");
            }

            var user = Page.User as System.Security.Claims.ClaimsPrincipal;
            Label1.Text = Request.QueryString["type"];
            Label2.Text = user.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value;
        }
    }
}