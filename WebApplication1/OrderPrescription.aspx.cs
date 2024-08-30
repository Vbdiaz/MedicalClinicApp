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
    public partial class OrderPrescription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            int reportID = Convert.ToInt32(Request.QueryString["reportID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connect = new MySqlConnection(connectionString);
            connect.Open();


            if (reportID != 0)
            {
                string report_query = "SELECT prescription FROM visit_details WHERE reportID = @reportID";
                MySqlCommand report_cmd = new MySqlCommand(report_query, connect);
                report_cmd.Parameters.AddWithValue("@reportID", reportID);
                string prescription = report_cmd.ExecuteScalar().ToString();
                drugclass.Text = prescription;
            }
            string pat_query = "SELECT CONCAT(doctor.fname, ' ', doctor.lname) as dr_fn, CONCAT(patients.fname, ' ', patients.lname) as pat_fn from doctor,patients WHERE doctor.doctorID = @doctorID and patients.patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(pat_query, connect);
            cmd.Parameters.AddWithValue("@doctorID", doctorID);
            cmd.Parameters.AddWithValue("@patientID", patientID);

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
            int reportID = Convert.ToInt32(Request.QueryString["reportID"]);

            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connect = new MySqlConnection(connectionString);
            string query = "INSERT INTO prescriptions(NDC,drug_name,dosage,refills,patientID,doctorID,notes) VALUES (@NDC, @drug_name, @dosage, @refills, @patientID, @doctorID, @notes)";
            string query2 = "INSERT INTO prescriptions(NDC,drug_name,dosage,refills,patientID,doctorID,notes,reportID) VALUES (@NDC, @drug_name, @dosage, @refills, @patientID, @doctorID,@notes,@reportID)";
            if (reportID == 0)
            {
                MySqlCommand command = new MySqlCommand(query, connect);
                command.Parameters.AddWithValue("@patientID", patientID);
                command.Parameters.AddWithValue("@drug_name", drugname.Text);
                command.Parameters.AddWithValue("@dosage", dosage.Text);
                command.Parameters.AddWithValue("@refills", refills.Text);
                command.Parameters.AddWithValue("@NDC", NDC.Text);
                command.Parameters.AddWithValue("@notes", notes.Text);
                command.Parameters.AddWithValue("@doctorID", doctorID);

                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
            }
            else
            {
                MySqlCommand command2 = new MySqlCommand(query2, connect);
                command2.Parameters.AddWithValue("@patientID", patientID);
                command2.Parameters.AddWithValue("@drug_name", drugname.Text);
                command2.Parameters.AddWithValue("@dosage", dosage.Text);
                command2.Parameters.AddWithValue("@refills", refills.Text);
                command2.Parameters.AddWithValue("@NDC", NDC.Text);
                command2.Parameters.AddWithValue("@notes", notes.Text);
                command2.Parameters.AddWithValue("@reportID", reportID);
                command2.Parameters.AddWithValue("@doctorID", doctorID);

                connect.Open();
                command2.ExecuteNonQuery();
                connect.Close();
            }
            Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);
        }
    }
}