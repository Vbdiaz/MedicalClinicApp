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
    public partial class ProviderView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

            // Get Doctor Name for Welcome Header
            int doctorID =Convert.ToInt32 (Request.QueryString["doctorID"]);
            DateTime currentDate = DateTime.Now;
            CultureInfo culture = new CultureInfo("en-US");
            string dayName = currentDate.ToString("dddd", culture);

            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT('Dr. ', fname, ' ', lname, ' - ', specialty) as DocName, officeAddress from doctor, office,schedule WHERE doctorID = @doctorID AND schedule.doctor = doctor.doctorID AND schedule."+ dayName+"=office.officeID";
            string query2 = "SELECT CONCAT('Dr. ', fname, ' ', lname, ' - ', specialty) as DocName from doctor WHERE doctorID = @doctorID";
            string fullname;
            string officeLoca = "Not Scheduled";
            connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("doctorID", doctorID);
                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                fullname = reader["DocName"].ToString();
                officeLoca = reader["officeAddress"].ToString();
                reader.Close();
                connection.Close();
                connection.Close();
            }
            catch
            {
                MySqlCommand cmd = new MySqlCommand(query2, connection);
                cmd.Parameters.AddWithValue("@doctorID", doctorID);
                fullname = cmd.ExecuteScalar().ToString();
            }
            welcomeHeader.InnerText = "Welcome, " + fullname;
            LinkButton1.Text = "Logged in as: " + fullname;
            officeLocale.InnerText = dayName + "'s office: " + officeLoca;

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);

        }
        protected void BindData()
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            DataTable dt = new DataTable();

            // Retrieve data from database into upcoming appointment grid
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            string query = "SELECT CONCAT(patients.fname, ' ', patients.lname) as PatientName, office.officeAddress as OfficeLocation, appointment.appointmentID as appointmentID, appointment.approval as Approval, appointmentTime as Time, appointmentDate as Date FROM appointment INNER JOIN patients ON appointment.PatientID = Patients.patientID INNER JOIN office ON Appointment.OfficeID = Office.officeID WHERE appointment.doctorID = @DoctorID AND appointmentDate >= current_date() AND appointment.archive = false AND (referral = true OR referral is NULL) ORDER BY appointmentDate ASC";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                     
                    }
                }
                connection.Close();
            }


            // Retrive data from database into referral review
            string query2 = "SELECT appointment.appointmentID as appointmentID2, CONCAT(patients.fname, ' ', patients.lname) as PatientName2, spec.specialty as Specialist, appointment.Referral as Referral,CONCAT(spec.fname, ' ', spec.lname) as Doctor, appointmentDate as Date2 FROM appointment, patients, doctor as prim, doctor as spec  WHERE appointment.doctorID = spec.doctorID AND prim.doctorID = patients.doctorID AND appointment.patientID = patients.patientID AND prim.doctorID = @DoctorID AND prim.doctorID != spec.doctorID AND appointmentDate >= current_date() AND appointment.archive = false ORDER BY appointmentDate ASC";
            DataTable dt2 = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt2);
                        GridView2.DataSource = dt2;
                        GridView2.DataBind();
                    }
                }
                connection.Close();
            }

            // Retrieve data from database into past appointments 
            string query3 = "SELECT CONCAT(patients.fname, ' ', patients.lname) as PatientName, office.officeAddress as OfficeLocation, appointment.appointmentID as appointmentID, appointmentTime as Time, appointmentDate as Date FROM appointment INNER JOIN patients ON appointment.PatientID = Patients.patientID INNER JOIN office ON Appointment.OfficeID = Office.officeID WHERE appointment.doctorID = @DoctorID AND PATIENT_CONFIRM = true AND Approval = true AND appointmentDate < current_date() ORDER BY appointmentDate ASC";
            DataTable dt3 = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query3, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt3);
                        GridView3.DataSource = dt3;
                        GridView3.DataBind();
                    }
                }
                connection.Close();
            }

            //retrive data into prescriptions needed table

            string prescriptionTableQuery = "SELECT appointment.appointmentID, appointment.patientID as patientID, CONCAT(patients.fname, ' ', patients.lname) as PatientName, appointment.appointmentDate as Date, prescription as DrugClass FROM patients, appointment, visit_details WHERE prescriptionRequired = 1 AND visit_details.appointmentID = appointment.appointmentID AND appointment.patientID = patients.patientID AND appointment.doctorID = @DoctorID and visit_details.prescriptionID is NULL";

            DataTable prescriptionTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand prescription_cmd = new MySqlCommand(prescriptionTableQuery, connection);
                prescription_cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                connection.Open();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(prescription_cmd))
                {
                    adapter.Fill(prescriptionTable);
                    prescriptionGrid.DataSource = prescriptionTable;
                    prescriptionGrid.DataBind();
                }
                connection.Close();
            }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ApproveAppointment") {
                int appointmentID = Convert.ToInt32(GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

                // Get patient email
                string email_query = "SELECT DISTINCT patients.email as email, appointmentDate as date, appointmentTime as time, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as doctorName FROM patients, appointment, doctor WHERE appointment.patientID=patients.patientID AND appointment.doctorID = doctor.doctorID AND appointmentID = @APID";
                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connect = new MySqlConnection(connString);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(email_query, connect);
                cmd.Parameters.AddWithValue("@APID", appointmentID);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows) {
                    string email = reader["email"].ToString();
                    string doctorName = reader["doctorName"].ToString();
                    string date = reader["date"].ToString();
                    string time = reader["time"].ToString();
                    reader.Close();
                    connect.Close();
                    date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");

                    // Update approval status in database
                    string query = "UPDATE appointment SET Approval = @Approved WHERE appointmentID = @ID";
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Approved", true);
                        command.Parameters.AddWithValue("@ID", appointmentID);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                            if (rowsAffected > 0)
                            {
                                try
                                {
                                    // Send confirmation email to patient
                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(email);
                                    mail.Subject = "Appointment Approved";
                                    mail.Body = "Your appointment on " + date + " at " + time + " with " + doctorName + " has been confirmed by your doctor. Please log on to your patient portal to confirm your appointment at least 24 hours until before the scheduled time";
                                    SmtpClient smtp = new SmtpClient();

                                    smtp.Send(mail);


                                }
                                catch
                                {
                                    // unable to send email due to unforeseen circumstances
                                }
                                // Refresh data grid
                                BindData();
                            }
                        }
                    }
                }
            }
            else if (e.CommandName == "DenyAppointment")
            {
                int appointmentID = Convert.ToInt32(GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

                // Get patient email
                string email_query = "SELECT DISTINCT patients.email as email, appointmentDate as date, appointmentTime as time, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as doctorName FROM patients, appointment, doctor WHERE appointment.patientID=patients.patientID AND appointment.doctorID = doctor.doctorID AND appointmentID = @apid";
                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connect = new MySqlConnection(connString);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(email_query, connect);
                cmd.Parameters.AddWithValue("@apid", appointmentID);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string email = reader["email"].ToString();
                    string doctorName = reader["doctorName"].ToString();
                    string date = reader["date"].ToString();
                    string time = reader["time"].ToString();
                    reader.Close();
                    date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");


                    // Update approval status in database
                    string query = "UPDATE appointment SET archive = true, cancellation_reason = 'Canceled by Doctor.', Approval = @Approved WHERE appointmentID = @ID";
                    using (MySqlConnection connection = new MySqlConnection(connString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Approved", false);
                            command.Parameters.AddWithValue("@ID", appointmentID);
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected > 0)
                            {
                                try
                                {
                                    // Send confirmation email to patient
                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(email);
                                    mail.Subject = "Appointment Denied";
                                    mail.Body = "Your appointment on " + date + " at " + time + " with " + doctorName + " has been denied by your doctor. Please log on to your patient portal to schedule an appointment again";
                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Send(mail);
                                    // Refresh data grid
                                }
                                catch
                                {
                                    // unable to send
                                }
                                BindData();

                            }
                        }
                    }
                }
                connect.Close();
            }
        }


        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {  
            if (e.CommandName == "ApproveAppointment2")
            {
                int appointmentID = Convert.ToInt32(GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

                // Get patient email
                string email_query = "SELECT patients.email as email, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as docName, doctor.Specialty as specialty, AppointmentDate as date, AppointmentTime as time FROM patients, appointment, doctor WHERE appointment.appointmentID = @apid AND appointment.patientID = patients.patientID and appointment.doctorID = doctor.doctorID";

                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connect = new MySqlConnection(connString);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(email_query, connect);
                cmd.Parameters.AddWithValue("@apid", appointmentID);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string email = reader["email"].ToString();
                    string specialty = reader["specialty"].ToString();
                    string doctorName = reader["docName"].ToString();
                    string date = reader["date"].ToString();
                    string time = reader["time"].ToString();
                    date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");

                    reader.Close();

                    // Update approval status in database
                    string query = "UPDATE appointment SET appointment.referral = @Referral WHERE appointment.appointmentID = @ID";

                    using (MySqlConnection connection = new MySqlConnection(connString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Referral", true);
                            command.Parameters.AddWithValue("@ID", appointmentID);
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected > 0)
                            {
                                try
                                {
                                    // Send confirmation email to patient
                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(email);
                                    mail.Subject = "Referral Approved";
                                    mail.Body = "The referral for your " + specialty + " appointment on" + date + " at " + time + " with " + doctorName + " has been approved by your primary care physician. To avoid cancellation, please log on to your account to confirm at least 24 hours before your scheduled date. We look forward to seeing you!";
                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Send(mail);
                                }
                                catch
                                {
                                    //unable to send
                                }
                                // Refresh data grid
                                BindData();
                            }
                        }
                    }
                }
                connect.Close();

            }
            else if (e.CommandName == "DenyAppointment2")
            {
                int appointmentID = Convert.ToInt32(GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

                // Get patient email
                string email_query = "SELECT patients.email as email, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as docName, doctor.Specialty as specialty, AppointmentDate as date, AppointmentTime as time FROM patients, appointment, doctor WHERE appointment.appointmentID = @apid AND appointment.patientID = patients.patientID and appointment.doctorID = doctor.doctorID";
                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connect = new MySqlConnection(connString);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(email_query, connect);
                cmd.Parameters.AddWithValue("@apid", appointmentID);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string email = reader["email"].ToString();
                    string specialty = reader["specialty"].ToString();
                    string doctorName = reader["docName"].ToString();
                    string date = reader["date"].ToString();
                    string time = reader["time"].ToString();
                    date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");
                    reader.Close();

                    // Update referral status in database
                    using (MySqlConnection connection = new MySqlConnection(connString))
                    {
                        string query_ref = "UPDATE appointment SET appointment.Referral = @Referral, Archive = true, cancellation_reason = 'Referral Denied' WHERE appointment.appointmentID = @ID";
                        using (MySqlCommand command = new MySqlCommand(query_ref, connection))
                        {
                            command.Parameters.AddWithValue("@Referral", false);
                            command.Parameters.AddWithValue("@ID", appointmentID);
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected > 0)
                            {
                                try
                                {
                                    // Send confirmation email to patient
                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(email);
                                    mail.Subject = "Referral Denied";
                                    mail.Body = "The referral for your " + specialty + " appointment on" + date + " at " + time + " with " + doctorName + " has been denied by your primary care physician. Please consult with your primary care physician as to the reason why.";
                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Send(mail);
                                }
                                catch
                                {
                                    //unable to send
                                }
                                // Refresh data grid
                                BindData();
                            }
                        }
                    }
                    connect.Close();
                }
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int appointmentID = Convert.ToInt32(GridView3.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                string query = "SELECT reportID FROM appointment WHERE appointmentID = @AID";
                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connect = new MySqlConnection(connString);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@AID", appointmentID);
                object result = cmd.ExecuteScalar();
                int ReportID = Convert.ToInt32(result);
                connect.Close();
                int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);

                if (e.CommandName == "VIEW")
                {
                    Response.Redirect("ReportView.aspx?ReportID=" + ReportID + "&doctorID=" + doctorID);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Report is not available yet');", true);

            }
        }


        protected void prescriptions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int appointmentID = Convert.ToInt32(prescriptionGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            int patientID = Convert.ToInt32(prescriptionGrid.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["patientID"]);


            string query = "SELECT reportID FROM appointment WHERE appointmentID = @AID";
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connect = new MySqlConnection(connString);
            connect.Open();
            MySqlCommand cmd = new MySqlCommand(query, connect);
            cmd.Parameters.AddWithValue("@AID", appointmentID);
            object result = cmd.ExecuteScalar();
            int ReportID = Convert.ToInt32(result);
            connect.Close();
            if (e.CommandName == "create_prescription")
            {
                Response.Redirect("OrderPrescription.aspx?doctorID=" + doctorID + "&reportID=" + ReportID + "&patientID=" + patientID);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("PatientReports.aspx?doctorID=" + doctorID);
        }
    }
}





