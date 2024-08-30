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
    public partial class PatientReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get Doctor Name for Welcome Header
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT('Dr. ', fname, ' ', lname, ' - ', specialty) from doctor WHERE doctorID = @doctorID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@doctorID", doctorID);
            connection.Open();
            object result = cmd.ExecuteScalar();
            string fullname = result.ToString();
            connection.Close();
            
            LinkButton1.Text = "Logged in as: " + fullname;

            string query4 = "SELECT DISTINCT patients.patientID as patientID, CONCAT(patients.fname, ' ', patients.lname) as PatientName FROM patients, appointment WHERE patients.doctorID = @DoctorID OR (appointment.patientID=patients.patientID AND appointment.doctorID = @DoctorID)";
            DataTable dt4 = new DataTable();
            string app_query = "SELECT MAX(appointmentDate) FROM appointment WHERE PATIENT_CONFIRM = true AND Approval = true AND appointmentDate < CURRENT_DATE() AND patientID = @patientID";


            using (MySqlConnection connection2 = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query4, connection2))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {

                        adapter.Fill(dt4);
                        dt4.Columns.Add("LastVisitDate");
                        foreach (DataRow row in dt4.Rows)
                        {
                            int patientID = Convert.ToInt32(row["patientID"]);

                            MySqlCommand app_cmd = new MySqlCommand(app_query, connection);
                            app_cmd.Parameters.AddWithValue("@patientID", patientID);
                            object last_date = app_cmd.ExecuteScalar();

                            if (last_date != null && last_date != DBNull.Value)
                            {
                                string date = DateTime.ParseExact(last_date.ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToString("M/d/yyyy");

                                row["LastVisitDate"] = date;
                            }
                            else
                            {
                                row["LastVisitDate"] = DBNull.Value;
                            }
                        }

                        GridView4.DataSource = dt4;
                        GridView4.DataBind();
                    }
                }
                connection.Close();
            }
        }

        protected void Report1_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("ReportGenerator.aspx?doctorID=" + doctorID);
        }

        protected void Report2_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("DoctorViewPrescriptionsReport.aspx?doctorID=" + doctorID);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);

        }
        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OrderPrescription" || e.CommandName == "OrderTest")
            {
                int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
                int patientID = Convert.ToInt32(GridView4.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                string command = e.CommandName == "OrderPrescription" ? "OrderPrescription.aspx" : "OrderTest.aspx";
                Response.Redirect(command + "?patientID=" + patientID + "&doctorID=" + doctorID);
            }
        }
    }
}