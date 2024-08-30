using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class NewLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SUBMIT_Click_AddPer(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);

            connection.Open();

            try
            {
                if (DropDownList1.SelectedValue == "Doctor")
                {
                    string sql = "INSERT INTO login (username, passwrd, doctorID) VALUES (@username, @passwrd, @doctorID)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@username", username.Text);
                    command.Parameters.AddWithValue("@passwrd", password.Text);
                    command.Parameters.AddWithValue("@doctorID", id.Text);

                    command.ExecuteNonQuery();
                }
                if (DropDownList1.SelectedValue == "Nurse")
                {
                    string sql = "INSERT INTO login (username, passwrd, nurseID) VALUES (@username, @passwrd, @nurseID)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@username", username.Text);
                    command.Parameters.AddWithValue("@passwrd", password.Text);
                    command.Parameters.AddWithValue("@nurseID", id.Text);
                    command.ExecuteNonQuery();
                }
                if (DropDownList1.SelectedValue == "Admin")
                {
                    string sql = "INSERT INTO login (username, passwrd, adminID) VALUES (@username, @passwrd, @adminId)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@username", username.Text);
                    command.Parameters.AddWithValue("@passwrd", password.Text);
                    command.Parameters.AddWithValue("@adminID", id.Text);
                    command.ExecuteNonQuery();
                }
                if (DropDownList1.SelectedValue == "Patient")
                {
                    string sql = "INSERT INTO login (username, passwrd, patientID) VALUES (@username, @passwrd, @patientId)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@username", username.Text);
                    command.Parameters.AddWithValue("@passwrd", password.Text);
                    command.Parameters.AddWithValue("@patientId", id.Text);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Response.Redirect("unsuccessful.aspx");
                Response.Write("Error: " + ex.Message + '\n');
            }

            connection.Close();
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
            id.Text = "";
        }
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminView.aspx?AdminID=");//doesn't have + adminId in url
        }
    }
}