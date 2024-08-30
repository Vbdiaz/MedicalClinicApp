using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class RemPersonnel : System.Web.UI.Page
    {
        private string adminID;
        protected void Page_Load(object sender, EventArgs e)
        {
            adminID = Request.QueryString["adminID"];
        }
        protected void SUBMIT_Click_RemPer(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);

            connection.Open();

            try
            {
                //  remove a Personnel
                if (DropDownList2.SelectedValue == "Doctor")
                {

                    string query = "SELECT CONCAT('Dr. ', fname, ' ', lname, ' - ', specialty) from doctor WHERE DoctorID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ID", perID.Text);
                    object result = cmd.ExecuteScalar();
                    string outcome = result.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('DELETED " + outcome + "');", true);


                    string sql = "DELETE FROM doctor WHERE DoctorID = @ID";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", perID.Text);
                    command.ExecuteNonQuery();
                }
                if (DropDownList2.SelectedValue == "Nurse")
                {

                    string query = "SELECT CONCAT( fname, ' ', lname) FROM nurse WHERE NID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ID", perID.Text);
                    object result = cmd.ExecuteScalar();
                    string outcome = result.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('DELETED " + outcome + "');", true);

                    string sql = "DELETE FROM nurse WHERE NID = @ID";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", perID.Text);
                    command.ExecuteNonQuery();
                }
                if (DropDownList2.SelectedValue == "Staff")
                {

                    string query = "SELECT CONCAT( fname, ' ', lname) FROM staff WHERE StaffID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ID", perID.Text);
                    object result = cmd.ExecuteScalar();
                    string outcome = result.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('DELETED " + outcome + "');", true);

                    string sql = "DELETE FROM staff WHERE StaffID = @ID";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", perID.Text);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Unable to delete - staff does not exist');", true);
            }

            connection.Close();
        }
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            perID.Text = "";
        }
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminView.aspx?adminID=" + adminID);
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}