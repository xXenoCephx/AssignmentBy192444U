using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssignmentBy192444U
{
    public partial class Succeded : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null)
            {
                if (!Session["AuTok"].ToString().Equals(Request.Cookies["AuTok"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "pop", "alert('Invalid session')",true);
                }
                else
                {
                    LbMessage.Text = "Congratulations, you are logged in.";
                    LbMessage.ForeColor = System.Drawing.Color.Green;
                    BtnLogout.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuTok"] != null)
            {
                Response.Cookies["AuTok"].Value = string.Empty;
                Response.Cookies["AuTok"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
    }
}