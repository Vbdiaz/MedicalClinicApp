using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace WebApplication1
{
    public partial class PatBilling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                CalculateInvoiceSummary();
            }
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT(fname, ' ', lname) from patients WHERE patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("patientID", patientID);
            connection.Open();
            object result = cmd.ExecuteScalar();
            string fullname = result.ToString();
            connection.Close();
            LinkButton1.Text = "Logged in as: " + fullname;
           
        }

        protected void BindData()
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            DataTable dt = new DataTable();

            // Retrieve data from database into invoice grid
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            string query = "SELECT invoiceID,total,claim,paid_amount,reportID,due_date FROM invoice WHERE patientID = @PatientID ORDER BY invoiceID ASC";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt);

                        // Bind data
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    connection.Close();
                }
            }
        }

        protected void CalculateInvoiceSummary()
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            DataTable dt = new DataTable();

            // Retrieve invoice summary from database
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            string query = "SELECT SUM(total) AS total, SUM(claim) AS claim, SUM(paid_amount) AS paid_amount, MAX(due_date) AS due_date FROM invoice WHERE patientID = @PatientID";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                decimal paidAmount,total,claim, insuranceAdjustments, totalDue;
                
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt);

                        // Calculate invoice summary
                        total = Convert.ToDecimal(dt.Rows[0]["total"]);
                        claim = Convert.ToDecimal(dt.Rows[0]["claim"]);
                        paidAmount = Convert.ToDecimal(dt.Rows[0]["paid_amount"]);
                        DateTime dueDate = Convert.ToDateTime(dt.Rows[0]["due_date"]);
                        insuranceAdjustments = claim;
                        totalDue = total - insuranceAdjustments - paidAmount;

                        // Set invoice summary values
                        lblDueDate.Text = dueDate.ToString("MMM dd");
                        lblBillAmount.Text = "$" + total.ToString("F2");
                        lblInsuranceAdjustments.Text = "-$" + insuranceAdjustments.ToString("F2");
                        lblpayments.Text = "-$" + paidAmount.ToString("F2");
                    }
                }
                string past_due_query = "SELECT SUM(total) - SUM(claim) - SUM(paid_amount) as past_due FROM invoice WHERE patientID = @PatientID AND CURRENT_DATE() > due_date";
                MySqlCommand past_cmd = new MySqlCommand(past_due_query, connection);
                past_cmd.Parameters.AddWithValue("@PatientID", patientID);
                object result = past_cmd.ExecuteScalar();
                decimal past_due;
                if (result == DBNull.Value)
                {
                    past_due = 0;
                }
                else
                {
                    past_due = Convert.ToDecimal(result);
                }


                string pay_query = "SELECT payment from patients where patientID = @patientID";
                MySqlCommand pay_cmd = new MySqlCommand(pay_query, connection);
                pay_cmd.Parameters.AddWithValue("@patientID", patientID);
                object pay = pay_cmd.ExecuteScalar();
                decimal paid = Convert.ToDecimal(pay);
                totalDue -= paid;
                lblmanpay.Text = "-$" + paid.ToString("F2");
                lblTotalDue.Text = "$" + totalDue.ToString("F2");

                if (past_due > 0)
                {
                    past_due -= paid;
                    if (past_due < 0)
                    {
                        past_due = 0;
                    }
                }
                lblPastDue.Text = "$" + past_due.ToString("F2");

                connection.Close();
            }
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("patpayment.aspx?patientID=" + patientID);
        }

        protected void btnPrintSummary_Click(object sender, EventArgs e)
        {
            
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT(fname, ' ', lname) from patients WHERE patientID = @patientID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("patientID", patientID);
            connection.Open();
            object result = cmd.ExecuteScalar();
            string fullname = result.ToString();
            connection.Close();

            // Generate PDF
            var pdfDoc = new iTextSharp.text.Document();
            var memoryStream = new System.IO.MemoryStream();
            var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();


            // Add bill summary header
            var headerFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var headerParagraph = new iTextSharp.text.Paragraph("Bill Summary for "+fullname +"\n\n", headerFont);
            headerParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(headerParagraph);

            // Add current bill section
            var currentBillFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var currentBillParagraph = new iTextSharp.text.Paragraph($"Current Bill | Due on {lblDueDate.Text}\n\n", currentBillFont);
            currentBillParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(currentBillParagraph);

            // Add past due section
            var pastDueFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            var pastDueParagraph = new iTextSharp.text.Paragraph($"Total past due: {lblPastDue.Text}\n\n", pastDueFont);
            pastDueParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(pastDueParagraph);

            // Add bill amount section
            var billAmountParagraph = new iTextSharp.text.Paragraph($"Bill amount: {lblBillAmount.Text}\n\n", pastDueFont);
            billAmountParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(billAmountParagraph);

            // Add insurance adjustments section
            var insuranceAdjustmentsParagraph = new iTextSharp.text.Paragraph($"Insurance Adjustments: {lblInsuranceAdjustments.Text}\n\n", pastDueFont);
            insuranceAdjustmentsParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(insuranceAdjustmentsParagraph);

            // Add payments section
            var paymentsParagraph = new iTextSharp.text.Paragraph($"Co-Payments: {lblpayments.Text}\n\n", pastDueFont);
            paymentsParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(paymentsParagraph)
                ;
            // Add payments section
            var payments2Paragraph = new iTextSharp.text.Paragraph($"Payments: {lblmanpay.Text}\n\n", pastDueFont);
            payments2Paragraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(payments2Paragraph);

            // Add total due section
            var totalDueParagraph = new iTextSharp.text.Paragraph($"Total due: {lblTotalDue.Text}\n\n", pastDueFont);
            totalDueParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(totalDueParagraph);

            // Add invoice list section
            // Add invoice list section
            var table = new PdfPTable(GridView1.Columns.Count-2);
            for (int i = 0; i < GridView1.Columns.Count-2; i++)
            {
                var cell = new PdfPCell(new Phrase(GridView1.Columns[i].HeaderText));
                table.AddCell(cell);
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                for (int j = 0; j < GridView1.Columns.Count-2; j++)
                {
                    var cell = new PdfPCell(new Phrase(GridView1.Rows[i].Cells[j].Text));
                    table.AddCell(cell);
                }
            }
            pdfDoc.Add(table);

            pdfDoc.Close();

            // Download PDF
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=BillSummary.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(memoryStream.ToArray());
            Response.End();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int patientID = Convert.ToInt32(Request.QueryString["patientID"]);
            Response.Redirect("PatientPortal.aspx?patientID=" + patientID);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int reportID = Convert.ToInt32(GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["reportID"]);
                int patientID = Convert.ToInt32(Request.QueryString["patientID"]);

                if (e.CommandName == "viewReport")
                {
                    Response.Redirect("ReportView.aspx?ReportID=" + reportID + "&patientID=" + patientID);
                }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Report is not available yet');", true);

            }

        }
    }
}
