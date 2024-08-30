using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Net.Mail;
using MySql.Data.MySqlClient;

public class AppointmentDataAccessLayer
{
    private string connectionString;

    public AppointmentDataAccessLayer(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void DeleteAppointments()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            /*connection.Open();
            string query = "UPDATE appointment SET archive = true, cancellation_reason = 'Patient failed to confirm' WHERE DATEDIFF(appointmentDate, CURDATE()) <= 1 AND (PATIENT_CONFIRM = false OR Approval = false) AND appointment.archive = false";
            string query2 = "SELECT DISTINCT patients.email as email, appointmentDate as date, appointmentTime as time, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as doctorName FROM patients, appointment, doctor WHERE appointment.patientID=patients.patientID AND appointment.doctorID = doctor.doctorID AND DATEDIFF(appointmentDate, CURDATE()) <= 1 AND (PATIENT_CONFIRM = false OR Approval = false) AND appointment.archive = false";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                MySqlDataReader reader = command2.ExecuteReader();
                string email, date, time, doctorName;
                while (reader.Read())
                {
                    try
                    {
                        email = reader["email"].ToString();
                        date = reader["date"].ToString();
                        time = reader["time"].ToString();
                        doctorName = reader["doctorName"].ToString();
                        date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");
                        time = DateTime.ParseExact(time, "h:mm:ss", CultureInfo.InvariantCulture).ToString("h:mm");
                        // Email to notify of appointment cancellation
                        MailMessage mail = new MailMessage();
                        mail.To.Add(email);
                        mail.Subject = "Your " + date + " Appointment with " + doctorName + " has been Cancelled";
                        mail.Body = "We are sorry to say your appointment on " + date + " at " + time + " with " + doctorName + " has been cancelled because you did not confirm at least one day prior to the scheduled date. Please try to schedule your appointment again. We hope to see you soon!";
                        SmtpClient smtp = new SmtpClient();
                        smtp.Send(mail);
                    }
                    catch
                    {
                        //unable to send
                    }
                }
                reader.Close();
            }
            catch
            {
                //do nothing
            }
            command.ExecuteNonQuery();
            connection.Close();*/
        }
        
    }
}