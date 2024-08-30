using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class ReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            int reportID = Convert.ToInt32(Request.QueryString["ReportID"]);
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            string query_dr = "SELECT CONCAT('Dr. ', fname, ' ', lname, ' - ', specialty) from doctor WHERE doctorID = @doctorID";
            string query_rn = "SELECT CONCAT('RN ', fname, ' ', lname) as fullname FROM nurse WHERE NID = @nurseID";
            string query_pat = "SELECT CONCAT(fname, ' ', lname) as fullname FROM patients WHERE patientID = @patientID";
            string fullname = "";
            if (doctorID != 0)
            {
                MySqlCommand cmd = new MySqlCommand(query_dr, connection);
                cmd.Parameters.AddWithValue("doctorID", doctorID);
                fullname = cmd.ExecuteScalar().ToString();
                
            }
                
            else if (nurseID != 0)
            {
                MySqlCommand cmd = new MySqlCommand(query_rn, connection);
                cmd.Parameters.AddWithValue("nurseID", nurseID);
                fullname = cmd.ExecuteScalar().ToString();
            }
            else if (patientID != 0)
            {
                MySqlCommand cmd = new MySqlCommand(query_pat, connection);
                cmd.Parameters.AddWithValue("patientID", patientID);
                fullname = cmd.ExecuteScalar().ToString();
            }
            LinkButton1.Text = "Logged in as: " + fullname;


            string nurseNameQuery = "SELECT CONCAT(fname, ' ', lname) FROM nurse, visit_details WHERE visit_details.reportID = @reportID AND visit_details.nurseID = nurse.NID"; 
            string titleQuery = "SELECT CONCAT(fname,' ', lname, ' - Appointment on ', AppointmentDate) FROM patients, appointment WHERE appointment.ReportID = @reportID AND appointment.patientID = patients.patientID";
            string patientNameQuery = "SELECT CONCAT(fname, ' ', lname) FROM appointment, patients WHERE appointment.reportID = @reportID AND appointment.patientID = patients.patientID";
            string doctorNameQuery = "SELECT CONCAT('Dr. ', Fname, ' ', Lname, ' - ', Specialty) from doctor, appointment WHERE appointment.reportID = @reportID AND appointment.doctorID = doctor.doctorID ";
            
            string diagnosisQuery = "SELECT Diagnosis FROM visit_details WHERE visit_details.ReportID = @reportID" ;
            string tempQuery = "SELECT temperature FROM visit_details WHERE visit_details.ReportID = @reportID";
            string phw_query = "SELECT bloodPressure_sys, bloodPressure_dia, height_inches, weight_lb FROM visit_details WHERE visit_details.ReportID = @reportID";
            string rateQuery = "SELECT heartRate FROM visit_details WHERE visit_details.ReportID = @reportID";
            string symptomsQuery = "SELECT symptoms FROM visit_details WHERE visit_details.ReportID = @reportID";
            string prescriptionQuery = "SELECT prescription FROM visit_details WHERE visit_details.ReportID = @reportID";
           
            string totalQuery = "SELECT total FROM invoice WHERE invoice.ReportID = @reportID";
            string claimQuery = "SELECT claim FROM invoice WHERE invoice.ReportID = @reportID";
            string copayQuery = "SELECT paid_amount FROM invoice WHERE invoice.ReportID = @reportID";

            string furtherEvalQuery = "SELECT test FROM evaluation WHERE code = (SELECT furtherEval FROM visit_details WHERE visit_details.ReportID = @reportID)";


            MySqlCommand titleCommand = new MySqlCommand(titleQuery, connection);
            titleCommand.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand nurseNameCmd = new MySqlCommand(nurseNameQuery, connection);
            nurseNameCmd.Parameters.AddWithValue("@reportID", reportID);


            MySqlCommand patientNameCmd = new MySqlCommand(patientNameQuery, connection);
            patientNameCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand doctorNameCmd = new MySqlCommand(doctorNameQuery, connection);
            doctorNameCmd.Parameters.AddWithValue("@reportID", reportID);
            
            MySqlCommand diagnosiscmd = new MySqlCommand(diagnosisQuery, connection);
            diagnosiscmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand tempCmd = new MySqlCommand(tempQuery, connection);
            tempCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand phwCmd = new MySqlCommand(phw_query, connection);
            phwCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand rateCmd = new MySqlCommand(rateQuery, connection);
            rateCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand symptCmd = new MySqlCommand(symptomsQuery, connection);
            symptCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand prescrCmd = new MySqlCommand(prescriptionQuery, connection);
            prescrCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand totalCmd = new MySqlCommand(totalQuery, connection);
            totalCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand claimCmd = new MySqlCommand(claimQuery, connection);
            claimCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlCommand copayCmd = new MySqlCommand(copayQuery, connection);
            copayCmd.Parameters.AddWithValue("@reportID", reportID);


            MySqlCommand checkBoxCmd = new MySqlCommand(furtherEvalQuery, connection);
            checkBoxCmd.Parameters.AddWithValue("@reportID", reportID);

            MySqlDataReader reader = phwCmd.ExecuteReader();
            reader.Read();

            string bp_sys = reader["bloodPressure_sys"].ToString();
            string bp_dia = reader["bloodPressure_dia"].ToString();
            string height = reader["height_inches"].ToString();
            string weight = reader["weight_lb"].ToString();
            reader.Close();



            string headerStr = (string)titleCommand.ExecuteScalar();
            string patientName = (string)patientNameCmd.ExecuteScalar();
            string docName = (string)doctorNameCmd.ExecuteScalar();
            string nurseName = (string)nurseNameCmd.ExecuteScalar();
            
            string diagnosis = (string)diagnosiscmd.ExecuteScalar();
            int temperature = Convert.ToInt32(tempCmd.ExecuteScalar());

            int heartRate = Convert.ToInt32(rateCmd.ExecuteScalar());
            string symptoms = (string)symptCmd.ExecuteScalar();
            string prescription = (string)prescrCmd.ExecuteScalar();

            int total = Convert.ToInt32(totalCmd.ExecuteScalar());
            int coverage = Convert.ToInt32(claimCmd.ExecuteScalar());
            int copay = Convert.ToInt32(copayCmd.ExecuteScalar());


            string eval = (string)checkBoxCmd.ExecuteScalar();
            connection.Close();


            reportHeader.InnerText = "Report for " + headerStr;
            reportBy.InnerText = "Prepared by RN " + nurseName;
            patientNameBox.Text = patientName;
            doctorNameBox.Text = docName;
            
            diagnosisBox.Text = diagnosis;
            temperatureBox.Text = temperature.ToString() + "\u00B0F";
            p_dia.Text = bp_dia.ToString() + " mmHg";
            p_sys.Text = bp_sys.ToString() + " mmHg";
            heightBox.Text = height.ToString() + " in";
            weightBox.Text = weight.ToString() + " lb";
            heartrateBox.Text = heartRate.ToString() + " bpm";
            symptomsBox.Text = symptoms;
            prescriptionBox.Text = prescription;

            apptTotalBox.Text = "$"+total.ToString();
            insuranceCovBox.Text ="$"+coverage.ToString();
            copayBox.Text = "$"+copay.ToString();

            evalBox.Text = eval;

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);

            if (nurseID != 0)
                Response.Redirect("nurseView.aspx?nurseID=" + nurseID);
            else if (patientID != 0)
                Response.Redirect("PatientPortal.aspx?patientID=" + patientID);
            else if (doctorID != 0)
                Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);


        }
        protected void returnBttn_Click(object sender, EventArgs e)
        {

            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);

            if (nurseID != 0)
                Response.Redirect("nurseView.aspx?nurseID=" + nurseID);
            else if (patientID != 0)
                Response.Redirect("PatientPortal.aspx?patientID=" + patientID);
            else if (doctorID != 0)
                Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);

        }
    }

}