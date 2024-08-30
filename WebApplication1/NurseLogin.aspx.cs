using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();
            string query = $"SELECT * FROM login WHERE username=@username AND passwrd=@passwrd AND nurseID IS NOT NULL";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", UserName.Text);
            command.Parameters.AddWithValue("@passwrd", Password.Text);


            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (reader.HasRows)

            {
                int nurseID = (int)reader["nurseID"];
                Response.Redirect("NurseView.aspx?nurseID=" + nurseID);
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