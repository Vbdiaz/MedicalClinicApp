using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;



namespace WebApplication1
{
    public partial class WebForm7 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            // Post Welcome Message for Specific Patient
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT(fname, ' ', lname) from patients WHERE patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("patientID", patientID);
            connection.Open();
            object result = cmd.ExecuteScalar();
            string fullname = result.ToString();
            welcomeHeader.InnerText = "Welcome, " + fullname;
            LinkButton1.Text = "Logged in as: " + fullname;

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PatientPortal.aspx?patientID=" + patientID);

        }

        protected void BindData()
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection con = new MySqlConnection(connectionString);
            DataTable dt_test = new DataTable();
            string nurse_query = "SELECT CONCAT(nurse.fname, ' ', nurse.lname) FROM nurse WHERE NID=@nurseID";
            string office_query = "SELECT officeAddress FROM office WHERE officeID=@officeID";
            string test_query = "SELECT test.testID as TestID, evaluation.test as Test, test.test_date as Date, test.test_time as Time, test.nurseID as nurseID, test.officeID FROM test, evaluation WHERE test.patientID = @patientID AND test.test_code = evaluation.code";
            MySqlCommand test_cmd = new MySqlCommand(test_query, con);

            test_cmd.Parameters.AddWithValue("@patientID", patientID);

            con.Open();
            MySqlDataAdapter adp = new MySqlDataAdapter(test_cmd);

            adp.Fill(dt_test);

            // Add columns for Nurse and OfficeLocation to the DataTable
            dt_test.Columns.Add("Nurse");
            dt_test.Columns.Add("OfficeLocation");

            string nurse_name;
            string officeLocation;

            // Set the Nurse and OfficeLocation values for each row
            foreach (DataRow row in dt_test.Rows)
            {
                MySqlCommand nurse_cmd = new MySqlCommand(nurse_query, con);
                MySqlCommand office_cmd = new MySqlCommand(office_query, con);

                string officeID = row["officeID"].ToString();
                string nurseID = row["nurseID"].ToString();


                nurse_cmd.Parameters.AddWithValue("@nurseID", nurseID);
                office_cmd.Parameters.AddWithValue("@officeID", officeID);
                try
                {
                    officeLocation = office_cmd.ExecuteScalar().ToString();
                }
                catch (Exception)
                {
                    officeLocation = DBNull.Value.ToString();
                }

                try
                {
                    nurse_name = nurse_cmd.ExecuteScalar().ToString();

                }
                catch (Exception)
                {
                    nurse_name = DBNull.Value.ToString();

                }

                row["OfficeLocation"] = officeLocation;
                row["Nurse"] = nurse_name;
            }

            GridView3.DataSource = dt_test;
            GridView3.DataBind();
            con.Close();


            DataTable dt = new DataTable();

            // Retrieve data from database into appointment grid
            string query = "SELECT CONCAT(doctor.fname, ' ', doctor.lname) as DoctorName, office.officeAddress as OfficeLocation, appointment.appointmentID as appointmentID, appointment.approval as Approval, appointmentTime as Time, appointmentDate as Date, PATIENT_CONFIRM as Confirm, doctor.specialty as SPEC FROM appointment, doctor, office WHERE appointment.patientID = @patientID AND appointment.doctorID = doctor.doctorID AND appointment.officeID = office.officeID AND appointmentDate >= current_date() AND appointment.archive = false ORDER BY appointmentDate ASC";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt);

                        // Add a column to the data table for Time to Confirm
                        dt.Columns.Add("TimeToConfirm");
                        dt.Columns.Add("ConfirmText");

                        // Calculate Time to Confirm for each row
                        foreach (DataRow row in dt.Rows)
                        {
                            // Set the value for the Confirm field
                            bool? confirmValue = row.Field<bool?>("Confirm");
                            string confirmText;
                            if (confirmValue.HasValue)
                            {
                                confirmText = confirmValue.Value ? "Patient Confirmed" : "Not Confirmed";
                            }
                            else
                            {
                                confirmText = "Needs Approval";
                            }


                            // Add a new column to the DataRow with the Confirm field value
                            row["ConfirmText"] = confirmText;

                            // calculate time to confirm
                            string appDate = Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd");
                            string appTime = Convert.ToString(row["Time"]);
                            DateTime appointmentDateTime;
                            DateTime.TryParseExact(appDate + " " + appTime, "yyyy-MM-dd h:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out appointmentDateTime);

                            try
                            {
                                DateTime timeToConfirm = appointmentDateTime.AddHours(-24);
                                TimeSpan timeRemaining = timeToConfirm - DateTime.Now;

                                if (timeRemaining < TimeSpan.Zero)
                                {
                                    row["TimeToConfirm"] = "Time Elapsed";
                                }
                                else
                                {
                                    row["TimeToConfirm"] = String.Format("{0} days, {1} hours, {2} minutes", timeRemaining.Days, timeRemaining.Hours, timeRemaining.Minutes);
                                }
                            }
                            catch (Exception)
                            {
                                Response.Write(appDate.ToString() + " " + appTime + " ");
                            }
                        }
                        // Bind data
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    connection.Close();
                }
            }



            DataTable dt2 = new DataTable();

            // Retrieve data from database into previous appointment grid
            string query2 = "SELECT CONCAT(doctor.fname, ' ', doctor.lname) as DoctorName, office.officeAddress as OfficeLocation, appointment.appointmentID as appointmentID, appointmentTime as Time, appointmentDate as Date, doctor.specialty as SPEC FROM appointment, doctor, office WHERE appointment.patientID = @PatientID AND appointment.doctorID = doctor.doctorID AND appointment.officeID = office.officeID AND PATIENT_CONFIRM = true AND Approval = true AND appointmentDate <= current_date() ORDER BY appointmentDate ASC";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt2);
                        GridView2.DataSource = dt2;
                        GridView2.DataBind();
                    }
                    connection.Close();
                }
            }

            DataTable dt3 = new DataTable();

            // Retrieve data from database into prescription grid
            string query3 = "SELECT prescriptionID as PrescriptionID, drug_name as DrugName, dosage as Dosage, refills as Refills, notes as Notes FROM prescriptions where patientID = @patientID";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query3, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt3);
                        GridView4.DataSource = dt3;
                        GridView4.DataBind();
                    }
                    connection.Close();
                }
            }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int appointmentID = Convert.ToInt32(GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            string timeRemaining = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text;

            if (e.CommandName == "ConfirmAppointment")
            {
                if (!timeRemaining.Contains("Time Elapsed"))
                {
                        // Get patient email
                        string email_query = "SELECT DISTINCT patients.email as email, appointmentDate as date, appointmentTime as time, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as doctorName FROM patients, appointment, doctor WHERE appointment.patientID=patients.patientID AND appointment.doctorID = doctor.doctorID AND appointmentID = @APID";
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
                            connect.Close();
                            date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");


                            // Update approval status in database
                            string query = "UPDATE appointment SET PATIENT_CONFIRM = @CONFIRM WHERE appointmentID = @ID";
                            using (MySqlConnection connection = new MySqlConnection(connString))
                            {
                                connection.Open();
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@CONFIRM", true);
                                    command.Parameters.AddWithValue("@ID", appointmentID);
                                    int rowsAffected = command.ExecuteNonQuery();
                                    connection.Close();
                                    // Refresh data grid
                                    BindData();
                                    string confirmed = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[9].Text;
                                    if (confirmed == "Patient Confirmed")
                                    {
                                        try
                                        {
                                            // Send confirmation email to patient
                                            MailMessage mail = new MailMessage();
                                            mail.To.Add(email);
                                            mail.Subject = "Appointment Confirmed";
                                            mail.Body = "You have successfully confirmed your appointment on " + date + " at " + time + " with " + doctorName + ". Please arrive to your appointment at least 15 minutes before the scheduled time";
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

                }
            }
            else if (e.CommandName == "EditAppointment")
            {
                Response.Redirect("PatEditApp.aspx?appointmentID=" + appointmentID + "&patientID=" + patientID);
            }
            else if (e.CommandName == "CancelAppointment")
            {
                // Get patient email
                string email_query = "SELECT DISTINCT patients.email as email, appointmentDate as date, appointmentTime as time, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as doctorName FROM patients, appointment, doctor WHERE appointment.patientID=patients.patientID AND appointment.doctorID = doctor.doctorID AND appointmentID = @APID";
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
                    connect.Close();
                    date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");
                    time = DateTime.ParseExact(time, "h:mm:ss", CultureInfo.InvariantCulture).ToString("h:mm");
                    // Update approval status in database
                    string query = "UPDATE appointment SET cancellation_reason = 'Canceled by Patient.', Archive = @archive WHERE appointmentID = @ID";
                    using (MySqlConnection connection = new MySqlConnection(connString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@archive", true);
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
                                    mail.Subject = "Appointment Cancelled";
                                    mail.Body = "You have successfully confirmed your appointment on " + date + " at " + time + " with " + doctorName + ". Please schedule a new appointment if you wish to see us again.";
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
            }
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
                int appointmentID = Convert.ToInt32(GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                string query = "SELECT reportID FROM appointment WHERE appointmentID = @AID";
                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connect = new MySqlConnection(connString);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@AID", appointmentID);
                object result = cmd.ExecuteScalar();

                    int ReportID = Convert.ToInt32(result);
                    connect.Close();


                    if (e.CommandName == "ViewReport")
                    {
                        Response.Redirect("ReportView.aspx?ReportID=" + ReportID + "&patientID=" + patientID);
                    }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Report is not available yet');", true);

            }

        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "schedule")
            {
                int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
                int testID = Convert.ToInt32(GridView3.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("ScheduleTest.aspx?patientID=" + patientID + "&testID=" + testID);
            }
        }
        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PatEdit.aspx?patientID=" + patientID);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PCPfollowup.aspx?patientID=" +patientID);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("NewAppointment.aspx?patientID=" + patientID);

        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PatBilling.aspx?patientID=" + patientID);
        }

    }
}