using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Web.UI.ScriptManager;
using System.Globalization;

namespace WebApplication1
{
    public partial class NursePatientReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get Doctor Name for Welcome Header
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT('RN ', fname, ' ', lname) from nurse WHERE NID = @nurseID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nurseID", nurseID);
            connection.Open();
            object result = cmd.ExecuteScalar();
            string fullname = result.ToString();
            connection.Close();

            LinkButton1.Text = "Logged in as: " + fullname;
        }

        protected void Report1_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            Response.Redirect("NursePatReport.aspx?nurseID=" + nurseID);
        }

        protected void Report2_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            Response.Redirect("NursePrescriptionReports.aspx?nurseID=" + nurseID);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            Response.Redirect("NurseView.aspx?nurseID=" + nurseID);

        }
    }
}