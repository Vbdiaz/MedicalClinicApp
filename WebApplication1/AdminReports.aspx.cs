using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
namespace WebApplication1
{
    public partial class AdminReportsaspx : System.Web.UI.Page
    {
        private string adminID;
        protected void Page_Load(object sender, EventArgs e)
        {
            adminID = Request.QueryString["adminID"];
        }

        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminView.aspx?adminID=" + adminID);
        }
        protected void Report1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppointmentReport.aspx?adminID=" + adminID);
        }
        protected void Report2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminInvoiceReport.aspx?adminID" + adminID);
        }
        protected void Report3_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminScheduleReport.aspx?adminID=" + adminID);
        }

    }
}
