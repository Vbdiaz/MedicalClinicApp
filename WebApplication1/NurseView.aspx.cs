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
    public partial class NurseView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

            // Get Doctor Name for Welcome Header
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT('RN ', fname, ' ', lname) from nurse WHERE NID = @NID";
            string query2 = "SELECT officeAddress FROM office, nurse where nurse.NID = @NID AND nurse.officeID = office.officeID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlCommand cmd2 = new MySqlCommand(query2, connection);
            cmd2.Parameters.AddWithValue("@NID", nurseID);
            cmd.Parameters.AddWithValue("@NID", nurseID);
            connection.Open();
            object result = cmd.ExecuteScalar();
            string officeloc = cmd2.ExecuteScalar().ToString();
            string fullname = result.ToString();
            welcomeHeader.InnerText = "Welcome, " + fullname;
            officeHeader.InnerText = "Office Location: " + officeloc;
            connection.Close();
            LinkButton1.Text = "Logged in as: " + fullname;

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            Response.Redirect("NurseView.aspx?nurseID=" + nurseID);

        }

        protected void BindData()
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            DataTable dt = new DataTable();

            // Retrieve data from database into upcoming appointment grid
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            string query = "SELECT IFNULL(CONCAT(n.fname, ' ', n.lname), NULL) as Nurse, CONCAT(patients.fname, ' ', patients.lname) as PatientName, appointment.nurseID as nurseID, doctor.Specialty as Specialty, office.officeAddress as OfficeLocation, appointment.appointmentID as appointmentID, appointment.approval as Approval, appointmentTime as Time, appointmentDate as Date, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as docName FROM appointment JOIN patients ON appointment.patientID = patients.patientID JOIN office ON appointment.OfficeID = office.officeID JOIN nurse as rn ON rn.officeID = office.officeID JOIN doctor ON appointment.doctorID = doctor.doctorID LEFT JOIN nurse as n ON n.NID = appointment.nurseID WHERE rn.NID = 2006 AND appointmentDate >= current_date() AND appointment.archive = false AND appointment.reportID IS NULL ORDER BY appointmentDate DESC";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NID", nurseID); 
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


            // Retrieve data from database into past appointments 
            string query3 = "SELECT CONCAT(patients.fname, ' ', patients.lname) as PatientName, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as docName, office.officeAddress as OfficeLocation, appointment.appointmentID as appointmentID, Specialty, appointmentTime as Time, appointmentDate as Date FROM appointment, patients, office, doctor, nurse WHERE NID = @NID AND appointment.PatientID = Patients.patientID AND Appointment.OfficeID = Office.officeID AND nurse.officeID = Office.officeID AND appointment.doctorID = doctor.doctorID AND (appointmentDate < current_date() OR (appointmentDate = current_date() AND appointment.reportID IS NOT NULL)) AND PATIENT_CONFIRM = true ORDER BY appointmentDate ASC";
            DataTable dt3 = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query3, connection))
                {
                    command.Parameters.AddWithValue("@NID", nurseID);
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

            MySqlConnection con = new MySqlConnection(connectionString);
            DataTable dt_test = new DataTable();
            string officeID_query = "SELECT officeID from nurse WHERE NID = @NID";
            string test_query = "SELECT IFNULL(CONCAT(n.fname, ' ', n.lname), NULL) as Nurse, test.testID as TestID, evaluation.test as Test, test.test_date as Date, test.test_time as Time, test.nurseID as nurseID, CONCAT(patients.fname, ' ', patients.lname) as patname FROM patients JOIN test ON test.patientID = patients.patientID JOIN evaluation ON test.test_code = evaluation.code LEFT JOIN nurse as n ON test.nurseID = n.NID WHERE test.officeID = 1";
            MySqlCommand test_cmd = new MySqlCommand(test_query, con);
            MySqlCommand officeID_cmd = new MySqlCommand(officeID_query, con);
            officeID_cmd.Parameters.AddWithValue("@NID", nurseID);
            con.Open();
            string officeID = officeID_cmd.ExecuteScalar().ToString();

            test_cmd.Parameters.AddWithValue("@officeID", officeID);

            MySqlDataAdapter adp = new MySqlDataAdapter(test_cmd);

            adp.Fill(dt_test);

            GridView2.DataSource = dt_test;
            GridView2.DataBind();
            con.Close();

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
                int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);

                if (e.CommandName == "VIEW")
                {
                    Response.Redirect("ReportView.aspx?ReportID=" + ReportID + "&nurseID=" + nurseID);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Report is not available yet');", true);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            int appointmentID = Convert.ToInt32(GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
            string date = (GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text);
            DateTime selected_date = DateTime.Parse(date, CultureInfo.InvariantCulture);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            DateTime currentDate = DateTime.Now;

            if (e.CommandName == "GENERATE")
            {
                if (GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["nurseID"] != DBNull.Value)
                {
                    int NID = Convert.ToInt32(GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["nurseID"]);
                    if (!(currentDate < selected_date) && NID == nurseID)
                    {
                            Response.Redirect("nurseReport.aspx?appID=" + appointmentID + "&NID=" + nurseID);
                    }
                }
            }
            else if(e.CommandName == "assignApp")
            {
                string query = "UPDATE appointment SET nurseID = @nurseID WHERE appointmentID = @appID";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@nurseID", nurseID);
                command.Parameters.AddWithValue("@appID", appointmentID);
                command.ExecuteNonQuery();
                connection.Close();

            }
            BindData();
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int testID = Convert.ToInt32(GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            string query = "UPDATE test SET nurseID = @NID WHERE testID = @testID";
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NID", nurseID);
            cmd.Parameters.AddWithValue("@testID", testID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            BindData();
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
                int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
                Response.Redirect("NursePatientReports.aspx?nurseID=" + nurseID);

        }
    }
}


