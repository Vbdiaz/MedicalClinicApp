using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;";
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();
            string query = $"SELECT * FROM login WHERE username=@username AND passwrd=@passwrd AND patientID IS NOT NULL";
          
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", UserName.Text);
            command.Parameters.AddWithValue("@passwrd", Password.Text);


            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                int patientID = (int)reader["patientID"];

                Response.Redirect("PatientPortal.aspx?patientID=" + patientID);
            }

            else
            {
                ErrorMessage.Text = "Incorrect credentials";
            }

            reader.Close();
            connection.Close();
        }
    }
}