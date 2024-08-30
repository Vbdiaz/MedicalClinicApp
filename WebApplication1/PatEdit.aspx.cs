using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace WebApplication1
{
    public partial class PatAppEditaspx : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
                string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
                MySqlConnection connect = new MySqlConnection(connectionString);
                string pat_query = "SELECT CONCAT(fname, ' ', lname) from patients WHERE patientID = @patientID";
                MySqlCommand cmd = new MySqlCommand(pat_query, connect);
                cmd.Parameters.AddWithValue("patientID", patientID);
                connect.Open();
                object result = cmd.ExecuteScalar();
                string fullname = result.ToString();
                LinkButton1.Text = "Logged in as: " + fullname;

                string infoquery = "Select * FROM patients WHERE patientID = @patientID";
                string ECquery = "Select * FROM emergency_contact_patient WHERE patientID = @patientID";

                MySqlCommand cmd2 = new MySqlCommand(infoquery, connect);
                MySqlCommand cmd3 = new MySqlCommand(ECquery, connect);
                cmd2.Parameters.AddWithValue("@patientID", patientID);
                cmd3.Parameters.AddWithValue("@patientID", patientID);

                MySqlDataReader reader = cmd2.ExecuteReader();
                reader.Read();
                fname.Text = reader["fName"].ToString();
                mi.Text = reader["Minitial"].ToString();
                lname.Text = reader["lName"].ToString();
                phone_num.Text = reader["Phone_num"].ToString();
                email.Text = reader["email"].ToString();
                address.Text = reader["Address"].ToString();

                reader.Close();

                MySqlDataReader reader2 = cmd3.ExecuteReader();
                reader2.Read();
                emergency_contact.Text = reader2["fullname"].ToString();
                ECemail.Text = reader2["email"].ToString();
                ECphone.Text = reader2["Phone_num"].ToString();
                ECRelation.Text = reader2["Relation"].ToString();
                reader2.Close();
                connect.Close();

            }
            if (IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight);", true);
            }
        }

        protected void SUBMIT_Click(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);
            string sql = "UPDATE patients SET fname = @fname, Minitial= @Minitial, lname= @lname, phone_num = @phone_num, email = @email, Address = @Address  WHERE patientID = @patientID";
            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, connection);
            int patID = Convert.ToInt32(Request.QueryString["patientID"]);


            command.Parameters.AddWithValue("@fname", fname.Text);
            command.Parameters.AddWithValue("@Minitial", mi.Text);
            command.Parameters.AddWithValue("@lname", lname.Text);
            command.Parameters.AddWithValue("@phone_num", phone_num.Text);
            command.Parameters.AddWithValue("@email", email.Text);
            command.Parameters.AddWithValue("@patientID", patID);
            command.Parameters.AddWithValue("@Address", address.Text);
            command.ExecuteNonQuery();

            string ec_query = "UPDATE emergency_contact_patient SET fullname = @fullname, Relation = @Relation, phone_num=@ECphone_num, email= @ECemail WHERE patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(ec_query, connection);
            cmd.Parameters.AddWithValue("@fullname", emergency_contact.Text);
            cmd.Parameters.AddWithValue("@Relation", ECRelation.Text);
            cmd.Parameters.AddWithValue("@ECphone_num", ECphone.Text);
            cmd.Parameters.AddWithValue("@ECemail", ECemail.Text);
            cmd.Parameters.AddWithValue("@patientID", patID);
            cmd.ExecuteNonQuery();
            connection.Close();
            Response.Redirect(Request.RawUrl);

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PatientPortal.aspx?patientID=" + patientID);

        }

        protected void EDIT_CLICK(object sender, EventArgs e)
        {
            fname.ReadOnly = false;
            mi.ReadOnly = false;
            lname.ReadOnly = false;
            phone_num.ReadOnly = false;
            email.ReadOnly = false;
            address.ReadOnly = false;


            emergency_contact.ReadOnly = false;
            ECemail.ReadOnly = false;
            ECphone.ReadOnly = false;
            ECRelation.ReadOnly = false;

            fname.BackColor = Color.White;
            mi.BackColor = Color.White;
            lname.BackColor = Color.White;
            phone_num.BackColor = Color.White;
            email.BackColor = Color.White;
            address.BackColor = Color.White;
            emergency_contact.BackColor = Color.White;
            ECemail.BackColor = Color.White;
            ECphone.BackColor = Color.White;
            ECRelation.BackColor = Color.White;


        }
    }
}