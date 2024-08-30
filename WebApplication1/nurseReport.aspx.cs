using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class nurseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {                                                                           //TODO: match variable names in button redirect in nurseView.aspx.cs
            int appointmentID = Convert.ToInt32(Request.QueryString["appID"]);
            int nurseID = Convert.ToInt32(Request.QueryString["NID"]);

            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";

            string titleQuery = "SELECT CONCAT(fname,' ', lname, ' - Appointment on ', AppointmentDate) FROM patients, appointment WHERE appointment.patientID = patients.patientID AND appointment.appointmentID = @appointmentID";
            string patientNameQuery = "SELECT CONCAT(fname, ' ', lname) FROM appointment, patients WHERE appointment.appointmentID = @appointmentID AND appointment.patientID = patients.patientID";
            string doctorNameQuery = "SELECT CONCAT('Dr. ', fname, ' ', lname, ' - ', Specialty) from doctor, appointment WHERE appointment.appointmentID = @appointmentID AND appointment.doctorID = doctor.doctorID ";
            string nurseNameQuery = "SELECT CONCAT('RN ', fname, ' ', lname) from nurse WHERE NID = @nurseID";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand titleCommand = new MySqlCommand(titleQuery, connection);
            titleCommand.Parameters.AddWithValue("@appointmentID", appointmentID);

            MySqlCommand patientNameCmd = new MySqlCommand(patientNameQuery, connection);
            patientNameCmd.Parameters.AddWithValue("@appointmentID", appointmentID);

            MySqlCommand doctorNameCmd = new MySqlCommand(doctorNameQuery, connection);
            doctorNameCmd.Parameters.AddWithValue("@appointmentID", appointmentID);

            MySqlCommand nurseNameCmd = new MySqlCommand(nurseNameQuery, connection);
            nurseNameCmd.Parameters.AddWithValue("@nurseID", nurseID);
            connection.Open();

            string headerStr = (string)titleCommand.ExecuteScalar();
            string patientName = (string)patientNameCmd.ExecuteScalar();
            string docName = (string)doctorNameCmd.ExecuteScalar();
            string nurseName = (string)nurseNameCmd.ExecuteScalar();


            connection.Close();

            reportHeader.InnerText = "Report for " + headerStr;
            patientNameBox.Text = patientName;
            doctorNameBox.Text = docName;
            LinkButton1.Text = "Logged in as: " + nurseName;
            
        }

        protected void submitBttn_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["NID"]);
            int appointmentID = Convert.ToInt32(Request.QueryString["appID"]);
            string patientSymptoms = symptomsBox.Text;
            string patientDiagnosis = diagnosisBox.Text;
            string patientPrescription = prescriptionBox.Text;
            decimal appointmentTotal = decimal.Parse(apptTotalBox.Text);
            decimal insuranceCoverage = decimal.Parse(insuranceCovBox.Text);
            decimal copay = decimal.Parse(copayBox.Text);
            decimal temperature = decimal.Parse(temperatureBox.Text);
            string pressure_sys = p_sys.Text;
            string pressure_dia = p_dia.Text;
            int heartRate = int.Parse(heartrateBox.Text);
            string furtherEval = DropDownList1.SelectedValue;
            string height = heightBox.Text;
            string weight = weightBox.Text;


            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            string requiresPrescription = "UPDATE visit_details SET prescriptionRequired = true, prescription = @prescription WHERE reportID = @reportID";

            string insertVisit = "INSERT INTO visit_details (symptoms,diagnosis,temperature,bloodPressure_sys, bloodPressure_dia,heartRate,appointmentID, nurseID, height_inches, weight_lb) VALUES (@symptoms,@diagnosis,@temperature,@psys,@pdia,@heartRate, @appID, @nurseID,@height,@weight)";
            string getReportID = "Select reportID from visit_details WHERE appointmentID = @appID";
            string insertInvoice = "INSERT INTO invoice (total,claim,paid_amount,reportID) VALUES (@total,@claim,@copay,@reportID)";
            string eval = "UPDATE visit_details SET furtherEval = @furtherEval WHERE reportID = @reportID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand visitCmd = new MySqlCommand(insertVisit, connection);
                visitCmd.Parameters.AddWithValue("@symptoms", patientSymptoms);
                visitCmd.Parameters.AddWithValue("@diagnosis", patientDiagnosis);
                visitCmd.Parameters.AddWithValue("@temperature", temperature);
                visitCmd.Parameters.AddWithValue("@heartrate", heartRate);
                visitCmd.Parameters.AddWithValue("@psys", pressure_sys);
                visitCmd.Parameters.AddWithValue("@pdia", pressure_dia);
                visitCmd.Parameters.AddWithValue("@appID", appointmentID);
                visitCmd.Parameters.AddWithValue("@nurseID", nurseID);
                visitCmd.Parameters.AddWithValue("@height", height);
                visitCmd.Parameters.AddWithValue("@weight", weight);


                connection.Open();

                visitCmd.ExecuteNonQuery();

                MySqlCommand getReportID_cmd = new MySqlCommand(getReportID, connection);
                getReportID_cmd.Parameters.AddWithValue("@appID", appointmentID);
                int reportID = (int)getReportID_cmd.ExecuteScalar();

                MySqlCommand insertInvoice_cmd = new MySqlCommand(insertInvoice, connection);
                insertInvoice_cmd.Parameters.AddWithValue("@total", appointmentTotal);
                insertInvoice_cmd.Parameters.AddWithValue("@claim", insuranceCoverage);
                insertInvoice_cmd.Parameters.AddWithValue("@copay", copay);
                insertInvoice_cmd.Parameters.AddWithValue("@ReportID", reportID);

                insertInvoice_cmd.ExecuteNonQuery();

                if (evaluationCheckbox.Checked)
                {
                    MySqlCommand insertEval = new MySqlCommand(eval, connection);
                    insertEval.Parameters.AddWithValue("@reportID", reportID);
                    insertEval.Parameters.AddWithValue("@furtherEval", furtherEval);
                    insertEval.ExecuteNonQuery();
                }
                if (prescriptionCheckBox.Checked)
                {
                    MySqlCommand updatePrescription = new MySqlCommand(requiresPrescription, connection);
                    updatePrescription.Parameters.AddWithValue("@reportID", reportID);
                    updatePrescription.Parameters.AddWithValue("@prescription", patientPrescription);
                    updatePrescription.ExecuteNonQuery();
                }

                connection.Close();

            }
            Response.Redirect("NurseView.aspx?nurseID="+nurseID);

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["NID"]);
            Response.Redirect("NurseView.aspx?nurseID=" + nurseID);
        }
    }
}
