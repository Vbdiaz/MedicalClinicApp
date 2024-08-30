using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace WebApplication1
{
    public partial class OrderTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connect = new MySqlConnection(connectionString);
            string pat_query = "SELECT CONCAT(doctor.fname, ' ', doctor.lname) as dr_fn, CONCAT(patients.fname, ' ', patients.lname) as pat_fn from doctor,patients WHERE doctor.doctorID = @doctorID and patients.patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(pat_query, connect);
            cmd.Parameters.AddWithValue("@doctorID", doctorID);
            cmd.Parameters.AddWithValue("@patientID", patientID);

            connect.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string docfullname = reader["dr_fn"].ToString();
            string patfullname = reader["pat_fn"].ToString();
            LinkButton1.Text = "Logged in as: " + docfullname;
            patientName.Text = patfullname;

        }

        protected void SUBMIT_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);

            int test_code = Convert.ToInt32(DropDownList2.SelectedValue);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connect = new MySqlConnection(connectionString);
            string query = "INSERT into test(patientID, test_code) VALUES (@patientID, @test_code)";
            MySqlCommand command = new MySqlCommand(query,connect);
            command.Parameters.AddWithValue("@patientID", patientID);
            command.Parameters.AddWithValue("@test_code", test_code);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);
        }
    }
}