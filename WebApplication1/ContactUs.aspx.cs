using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void stateDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Retrieve the selected state from the dropdown
            string selectedState = stateDropdown.SelectedValue;

            // Construct the connection string
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";

            // Construct the query
            string query = "SELECT officeAddress, phone_num, email " +
                           "FROM office " +
                           "WHERE officeAddress LIKE '%[State]%'";

            query = query.Replace("[State]", selectedState);

            // Retrieve the office data from the database
            DataTable officeData = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(officeData);
            }

            // Generate the location information
            string locationInfo = "<ul>";
            foreach (DataRow row in officeData.Rows)
            {
                locationInfo += "<li class='location-item'>";
                locationInfo += "<strong>" + row["officeAddress"].ToString() + "</strong><br />";
                locationInfo += "Phone: " + row["phone_num"].ToString() + "<br />";
                locationInfo += "Email: <a href='mailto:" + row["email"].ToString() + "'>" + row["email"].ToString() + "</a>";
                locationInfo += "</li>";
            }
            locationInfo += "</ul>";

            // Add the location information to the page
            locationInfoDiv.Text = locationInfo;

        }
    }
}