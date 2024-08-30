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
    public partial class ScheduleTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            int testID = Convert.ToInt32(Request.QueryString["testID"]);

            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connect = new MySqlConnection(connectionString);
            string pat_query = "SELECT CONCAT(fname, ' ', lname) from patients WHERE patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(pat_query, connect);
            cmd.Parameters.AddWithValue("patientID", patientID);
            connect.Open();
            object result = cmd.ExecuteScalar();
            string patfullname = result.ToString();
            LinkButton1.Text = "Logged in as: " + patfullname;

            // get evaluation string
            string code_query = "SELECT evaluation.test from evaluation, test where test.test_code = evaluation.code AND test.testID=@testID";
            MySqlCommand code_cmd = new MySqlCommand(code_query, connect);
            code_cmd.Parameters.AddWithValue("@testID",testID);
            string test = code_cmd.ExecuteScalar().ToString();
            connect.Close();

            medicaltest.Text = test;

            date_requested.Attributes.Add("placeholder", "yyyy-mm-dd");
            date_requested.Attributes.Add("type", "date");
            date_requested.Attributes.Add("onkeydown", "return false");
            if (IsPostBack)
            {
                if (IsPostBack)
                {
                    string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                    MySqlConnection connection = new MySqlConnection(connString);
                    string query = "SELECT * FROM test WHERE test_code=@testID AND test_date=@AppDate AND test_time=@AppTime AND officeID=@officeID";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    // Get officeID
                    string query_oid = "SELECT officeID FROM office WHERE officeAddress = @OfficeAddress";
                    MySqlCommand cmd_oid = new MySqlCommand(query_oid, connection);
                    connection.Open();
                    cmd_oid.Parameters.AddWithValue("@OfficeAddress", DropDownList1.SelectedValue);
                    Object reader_oid = cmd_oid.ExecuteScalar();
                    int officeID = Convert.ToInt32(reader_oid);

                    string AppDate = date_requested.Text;
                    string AppTime = ddlTimeRequested.SelectedValue.ToString();
                    
                    if (AppTime != "")
                    {
                        DateTime dateTime = DateTime.ParseExact(AppTime, "h:mm tt", CultureInfo.InvariantCulture);
                        AppTime = dateTime.ToString("hh:mm");
                    }

                    command.Parameters.AddWithValue("@AppDate", AppDate);
                    command.Parameters.AddWithValue("@AppTime", AppTime);
                    command.Parameters.AddWithValue("@officeID", officeID);
                    command.Parameters.AddWithValue("@testID", testID);
                    DateTime currentDate = DateTime.Now;

                    try
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        if (AppTime != "" && AppDate != "" && DropDownList1.SelectedValue != "")
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

                    catch (Exception ex)
                    {
                        ErrorMessage_date.Text = "Please make sure to complete all preceeding selections";
                    }
                    connection.Close();
                }
            }
            if (!IsPostBack)
            {

                // Create a list to hold the time slots
                List<string> timeSlots = new List<string>();

                // Add the time slots to the list
                DateTime startTime = DateTime.Parse("8:00 AM");
                DateTime endTime = DateTime.Parse("6:00 PM");
                timeSlots.Add("");
                while (startTime <= endTime)
                {
                    timeSlots.Add(startTime.ToString("h:mm tt"));
                    startTime = startTime.AddMinutes(30);
                }

                // Bind the list to the dropdown list
                ddlTimeRequested.DataSource = timeSlots;
                ddlTimeRequested.DataBind();
                ddlTimeRequested.SelectedIndex = 0;

                // populate office locations
                string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                string query = "SELECT officeAddress from office";
                MySqlConnection connection = new MySqlConnection(connString);
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<string> officelocs = new List<string>();
                officelocs.Add("");
                while (reader.Read())
                {
                    officelocs.Add(reader["officeAddress"].ToString());

                }
                DropDownList1.DataSource = officelocs;
                DropDownList1.DataBind();
                reader.Close();
                connection.Close();
                DropDownList1.SelectedIndex = 0;

            }
        }

        protected void SUBMIT_Click(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);

            connection.Open();

            //get patientID/testID from url
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            int testID = Convert.ToInt32(Request.QueryString["testID"]);

            // Get officeID
            string query_oid = "SELECT officeID FROM office WHERE officeAddress = @OfficeAddress";
            MySqlCommand cmd_oid = new MySqlCommand(query_oid, connection);
            cmd_oid.Parameters.AddWithValue("@OfficeAddress", DropDownList1.SelectedValue);
            Object reader = cmd_oid.ExecuteScalar();
            int officeID = Convert.ToInt32(reader);

            try
            {
                //insert test app
                string time_selected = ddlTimeRequested.SelectedValue.ToString();
                DateTime dateTime = DateTime.ParseExact(time_selected, "h:mm tt", CultureInfo.InvariantCulture);
                string time = dateTime.ToString("hh:mm");

                string insert_app = "UPDATE test SET test_date = @date, test_time = @time, officeID = @officeID WHERE testID = @testID";
                MySqlCommand cmd2 = new MySqlCommand(insert_app, connection);
                cmd2.Parameters.AddWithValue("@testID", testID);
                cmd2.Parameters.AddWithValue("@officeID", officeID);
                cmd2.Parameters.AddWithValue("@date", date_requested.Text);
                cmd2.Parameters.AddWithValue("@time", time);

                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Redirect("unsuccessful.aspx?patientID=" + patientID);
            }
            connection.Close();
            Response.Redirect("PatientPortal.aspx?patientID=" + patientID);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void date_requested_TextChanged(object sender, EventArgs e)
        {

        }



        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PatientPortal.aspx?patientID=" + patientID);
        }
    }
}