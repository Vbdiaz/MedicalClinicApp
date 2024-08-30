using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class AdminView : System.Web.UI.Page
    {
        private string adminID;

        protected void Page_Load(object sender, EventArgs e)
        {
            adminID = Request.QueryString["adminID"];
        }

        protected void ButtonAP_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewPersonnel.aspx?adminID=" + adminID);
        }
        protected void ButtonRP_Click(object sender, EventArgs e)
        {
            Response.Redirect("RemPersonnel.aspx?adminID=" + adminID);
        }
        protected void ButtonAO_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewOffice.aspx?adminID=" + adminID);
        }
        protected void ButtonRO_Click(object sender, EventArgs e)
        {
            Response.Redirect("RemOffice.aspx?adminID=" + adminID);
        }

        protected void Report_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminReports.aspx?adminID=" + adminID);
        }

        protected void login_click(object sender, EventArgs e)
        {
            Response.Redirect("NewLogin.aspx?adminID=" + adminID);
        }
    }
}