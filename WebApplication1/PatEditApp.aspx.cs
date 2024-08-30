using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Net.Mail;


namespace WebApplication1
{
    public partial class PatEditApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int appointmentID = Convert.ToInt32(Request.QueryString["appointmentID"]);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connect = new MySqlConnection(connectionString);
            string pat_query = "SELECT CONCAT(fname, ' ', lname) from patients WHERE patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(pat_query, connect);
            cmd.Parameters.AddWithValue("patientID", patientID);
            connect.Open();
            object result = cmd.ExecuteScalar();
            string fullname = result.ToString();
            connect.Close();
            LinkButton1.Text = "Logged in as: " + fullname;
            // check if selected appointment available
            if (IsPostBack)
            {
                ErrorMessage_date2.Text = "";
                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connection = new MySqlConnection(connString);
                string query = "SELECT * FROM appointment, doctor WHERE appointmentDate = @AppDate AND appointmentTime = @AppTime AND appointment.doctorID = (SELECT doctorID FROM APPOINTMENT WHERE appointmentID = @AID)";

                MySqlCommand command = new MySqlCommand(query, connection);

                string AppDate = date_requested.Text;
                string AppTime = ddlTimeRequested.SelectedValue.ToString();
                if (AppTime != "")
                {
                    DateTime dateTime = DateTime.ParseExact(AppTime, "h:mm tt", CultureInfo.InvariantCulture);
                    AppTime = dateTime.ToString("hh:mm");
                }

                command.Parameters.AddWithValue("@AppDate", AppDate);
                command.Parameters.AddWithValue("@AppTime", AppTime);
                command.Parameters.AddWithValue("@AID", appointmentID);
                connection.Open();
                DateTime currentDate = DateTime.Now;

                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (AppDate != "" && AppTime != "")
                    {
                        DateTime selected_date = DateTime.Parse(AppDate);

                        if (currentDate > selected_date || selected_date.DayOfWeek.ToString() == "Saturday" || selected_date.DayOfWeek.ToString() == "Sunday")
                        {
                            ErrorMessage_date.Text = "";
                            ErrorMessage_date2.Text = "Invalid Date";
                        }

                        else if (reader.HasRows)
                        {
                            ErrorMessage_date.Text = "Appointment Unavailable. Please make different selection!";
                        }
                        else
                        {
                            ErrorMessage_date.Text = "Appointment Available!";
                        }
                    }
                    else
                    {
                        ErrorMessage_date.Text = "Please make sure to complete all preceeding selections";
                    }
                }

                catch (Exception)
                {
                    ErrorMessage_date.Text = "Please make sure to complete all preceeding selections";
                }
                connection.Close();
                // scroll to appointment section on post back
                ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight/2);", true);
            }


            if (!IsPostBack)
            {

                // Create a list to hold the time slots
                List<string> timeSlots = new List<string>();
                timeSlots.Add("");
                // Add the time slots to the list
                DateTime startTime = DateTime.Parse("8:00 AM");
                DateTime endTime = DateTime.Parse("6:00 PM");
                while (startTime <= endTime)
                {
                    timeSlots.Add(startTime.ToString("h:mm tt"));
                    startTime = startTime.AddMinutes(30);
                }

                // Bind the list to the dropdown list
                ddlTimeRequested.DataSource = timeSlots;
                ddlTimeRequested.DataBind();
                ddlTimeRequested.SelectedIndex = 0;

               

            }

            //date_requested.Attributes.Add("placeholder", "yyyy-mm-dd");
            //date_requested.Attributes.Add("type", "date");
            //date_requested.Attributes.Add("onkeydown", "return false");

        }

        protected void SUBMIT_Click(object sender, EventArgs e)
        {
            int appointmentID = Convert.ToInt32(Request.QueryString["appointmentID"]);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);

            string AppDate = date_requested.Text;
            string time_selected = ddlTimeRequested.SelectedValue.ToString();
            DateTime dateTime = DateTime.ParseExact(time_selected, "h:mm tt", CultureInfo.InvariantCulture);
            string AppTime = dateTime.ToString("hh:mm");

            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();


            string update_query = "UPDATE APPOINTMENT SET appointmentDate = @Date, appointmentTime = @Time WHERE appointmentID = @AID";
            MySqlCommand cmd = new MySqlCommand(update_query, connection);
            cmd.Parameters.AddWithValue("@Date", AppDate);
            cmd.Parameters.AddWithValue("@Time", AppTime);
            cmd.Parameters.AddWithValue("@AID", appointmentID);
            cmd.ExecuteNonQuery();

            string email_query = "SELECT email FROM patients WHERE patientID = @PID";
            MySqlCommand cmd2 = new MySqlCommand(email_query, connection);
            cmd2.Parameters.AddWithValue("@PID", patientID);
            object result2 = cmd2.ExecuteScalar();
            string email = Convert.ToString(result2);

           // Send confirmation email to patient
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.Subject = "Appointment Rescheduled";
            mail.Body = "Your appointment has been rescheduled. You primary doctor will reach out with confirmation of your appointment. Afterwards, make sure to log in to your patient portal to confirm before the date.";
            SmtpClient smtp = new SmtpClient();
            smtp.Send(mail);

            connection.Close();
            Response.Redirect("PatientPortal.aspx?patientID=" + patientID);

        }

        protected void Return_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);

            Response.Redirect("PatientPortal.aspx?patientID="+ patientID);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PatientPortal.aspx?patientID=" + patientID);
        }
    }
}