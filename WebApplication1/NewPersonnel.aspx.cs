using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class NewPersonnel : System.Web.UI.Page
    {

        private string adminID;
        protected void Page_Load(object sender, EventArgs e)
        {
            dob.Attributes.Add("placeholder", "yyyy-mm-dd");
            dob.Attributes.Add("type", "date");
            dob.Attributes.Add("onkeydown", "return false");
            adminID = Request.QueryString["adminID"];

        }
        protected void SUBMIT_Click_addPer(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);

            connection.Open();

            try
            {
                if (DropDownList1.SelectedValue == "Doctor")
                {
                    string sql = "INSERT INTO doctor (Fname, Mname, Lname, Specialty, DOB, Work_email, Phone_num) VALUES (@fname, @Minitial, @lname, @specialty,  @dob, @email, @phone_num)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@fname", fname.Text);
                    command.Parameters.AddWithValue("@Minitial", mi.Text);
                    command.Parameters.AddWithValue("@lname", lname.Text);
                    command.Parameters.AddWithValue("@dob", dob.Text);
                    command.Parameters.AddWithValue("@specialty", specialty.Text);
                    command.Parameters.AddWithValue("@phone_num", phone_num.Text);
                    command.Parameters.AddWithValue("@email", email.Text);

                    command.ExecuteNonQuery();
                }
                if (DropDownList1.SelectedValue == "Nurse")
                {
                    string sql = "INSERT INTO nurse (Fname, Mname, Lname, Work_email, officeID, Phone_num, DOB) VALUES (@fname, @Minitial, @lname, @email, @office, @phone_num, @dob)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@fname", fname.Text);
                    command.Parameters.AddWithValue("@Minitial", mi.Text);
                    command.Parameters.AddWithValue("@lname", lname.Text);
                    command.Parameters.AddWithValue("@dob", dob.Text);
                    command.Parameters.AddWithValue("@phone_num", phone_num.Text);
                    command.Parameters.AddWithValue("@email", email.Text);
                    command.Parameters.AddWithValue("@office", office.Text);

                    command.ExecuteNonQuery();
                }
                if (DropDownList1.SelectedValue == "Staff")
                {
                    string sql = "INSERT INTO staff (Fname, Lname, SRole, Work_email, officeID, Phone_num, DOB, Salary) VALUES (@fname, @lname, @role, @email, @office, @phone_num, @dob, @Salary)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@fname", fname.Text);
                    command.Parameters.AddWithValue("@role", role.Text);
                    command.Parameters.AddWithValue("@lname", lname.Text);
                    command.Parameters.AddWithValue("@dob", dob.Text);
                    command.Parameters.AddWithValue("@phone_num", phone_num.Text);
                    command.Parameters.AddWithValue("@email", email.Text);
                    command.Parameters.AddWithValue("@office", office.Text);
                    command.Parameters.AddWithValue("@Salary", Salary.Text);

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
            fname.Text = "";
            role.Text = "";
            lname.Text = "";
            dob.Text = "";
            phone_num.Text = "";
            email.Text = "";
            office.Text = "";
            specialty.Text = "";
            mi.Text = "";
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