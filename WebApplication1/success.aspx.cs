using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using MySql.Data.MySqlClient;


namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the patientID from the url
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            // Get email for new patient
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();
            string query = $"SELECT email FROM Patients WHERE patientID = '{patientID}'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string email = reader["email"].ToString();
      
            reader.Close();
            connection.Close();



            // Send an email to the patient
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.Subject = "New Patient Form Submission Received";
            mail.Body = "Thank you for submitting your form. We have received your information and a doctor will approve or deny your appointment request shortly. Your new PatientID is: " + patientID + ". Use this to create your new user account.";
            SmtpClient smtp = new SmtpClient();
            smtp.Send(mail);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                string sql = "INSERT INTO login (username, passwrd, patientID) VALUES (@username, @passwrd, @patientID)";
                MySqlCommand command = new MySqlCommand(sql, conn);


                command.Parameters.AddWithValue("@username", UserName.Text);
                command.Parameters.AddWithValue("@passwrd", Password.Text);
                command.Parameters.AddWithValue("@patientID", PatientID.Text);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("PatientPortal.aspx?patientID="+PatientID.Text);
            }
            catch (Exception ex)
            {
                //Response.Write("Error accepting your response. Please try again later.");
                Response.Write("Error: " + ex.ToString());
                //Response.Redirect("unsuccessful.aspx");
            }
        }
    }
}