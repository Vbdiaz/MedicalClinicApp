using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Drawing.Printing;

namespace WebApplication1
{
    public partial class AppointmentReport : System.Web.UI.Page
    {
        private string adminID;
        protected void Page_Load(object sender, EventArgs e)
        {
            adminID = Request.QueryString["adminID"];
        }

        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminView.aspx?adminID=" + adminID);
        }
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = GetDataFromMySQL();
            MemoryStream pdfStream = GeneratePDF(dataTable);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=AppointmentsReport.pdf");
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.BinaryWrite(pdfStream.ToArray());
            Response.End();
        }
        private DataTable GetDataFromMySQL()
        {
            // Replace with your MySQL credentials
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string office = Office.SelectedValue;
                string start_date = Request.Form["start-date"]; // Retrieve the value of the start-date field
                string end_date = Request.Form["end-date"]; // Retrieve the value of the end-date field

                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = "";
                    if (office == "All Offices")
                    {
                        if (end_date == "" && start_date == "")
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10) as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                        else if (end_date == "")
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10) as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID WHERE appointment.appointmentDate >= @start_date ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                        else if (start_date == "")
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10) as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID WHERE appointment.appointmentDate <= @end_date ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                        else
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10) as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID WHERE appointment.appointmentDate >= @start_date AND appointment.appointmentDate <= @end_date ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                    }
                    else
                    {
                        if (end_date == "" && start_date == "")
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10)as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID WHERE office.officeAddress = @office ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                        else if (end_date == "")
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10) as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID WHERE office.officeAddress = @office AND appointment.appointmentDate >= @start_date ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                        else if (start_date == "")
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10) as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID WHERE office.officeAddress = @office AND appointment.appointmentDate <= @end_date ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                        else
                        {
                            query = "SELECT office.officeAddress as Office, appointment.AppointmentID as Appointment, CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as Doctor, CONCAT(patients.fname, ' ', patients.lname) as Patient, IF(appointment.cancellation_reason IS NULL AND (appointment.AppointmentDate < current_date() OR (appointment.AppointmentDate = current_date() AND appointment.AppointmentTime < current_time())), 'Completed.', IF(appointment.cancellation_reason IS NULL, 'Current.', appointment.cancellation_reason)) as Status, LEFT(appointment.AppointmentDate, 10) as Date, appointment.AppointmentTime as Time FROM office INNER JOIN appointment ON office.officeID = appointment.officeID INNER JOIN doctor ON appointment.doctorID = doctor.doctorID INNER JOIN patients ON appointment.patientID = patients.patientID WHERE office.officeAddress = @office AND appointment.appointmentDate >= @start_date AND appointment.appointmentDate <= @end_date ORDER BY office.officeAddress, appointment.AppointmentDate, appointment.AppointmentTime;";
                        }
                    }

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@office", office);
                    command.Parameters.AddWithValue("@start_date", start_date);
                    command.Parameters.AddWithValue("@end_date", end_date);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        private MemoryStream GeneratePDF(DataTable dataTable)
        {
            MemoryStream pdfStream = new MemoryStream();
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, pdfStream);

            pdfDoc.Open();

            Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            string selectedValue = Office.SelectedValue;
            string title = "Appointment Report";
            Paragraph titleParagraph = new Paragraph(title, titleFont);
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            titleParagraph.SpacingAfter = 20;
            pdfDoc.Add(titleParagraph);

            
            PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            Font cellFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            foreach (DataColumn column in dataTable.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, cellFont));
                cell.BackgroundColor = new BaseColor(240, 240, 240);
                table.AddCell(cell);
            }

            int rowcount = 0;
            int completedRowCount = 0;
            int currentRowCount = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (object cellData in row.ItemArray)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(cellData.ToString(), cellFont));
                    table.AddCell(cell);
                }
                if (row["status"].ToString() == "Completed.") // Check if status is Completed
                {
                    completedRowCount++; // Increment completed row count
                }
                if (row["status"].ToString() == "Current.") // Check if status is Completed
                {
                    currentRowCount++; // Increment completed row count
                }
                rowcount++;
            }

            PdfPCell currentCell = new PdfPCell(new Phrase("Current: " + currentRowCount.ToString()));
            currentCell.Colspan = dataTable.Columns.Count;
            currentCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            currentCell.PaddingTop = 10f;
            currentCell.PaddingBottom = 10f;
            currentCell.Border = Rectangle.BOTTOM_BORDER;
            currentCell.BorderColorBottom = BaseColor.BLACK;
            currentCell.BorderWidthBottom = 1f;

            table.AddCell(currentCell);

            PdfPCell completedCell = new PdfPCell(new Phrase("Completed: " + completedRowCount.ToString()));
            completedCell.Colspan = dataTable.Columns.Count;
            completedCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            completedCell.PaddingTop = 10f;
            completedCell.PaddingBottom = 10f;
            completedCell.Border = Rectangle.BOTTOM_BORDER;
            completedCell.BorderColorBottom = BaseColor.BLACK;
            completedCell.BorderWidthBottom = 1f;

            table.AddCell(completedCell);

            PdfPCell canceledCell = new PdfPCell(new Phrase("Canceled: " + (rowcount - (currentRowCount + completedRowCount)).ToString()));
            canceledCell.Colspan = dataTable.Columns.Count;
            canceledCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            canceledCell.PaddingTop = 10f;
            canceledCell.PaddingBottom = 10f;
            canceledCell.Border = Rectangle.BOTTOM_BORDER;
            canceledCell.BorderColorBottom = BaseColor.BLACK;
            canceledCell.BorderWidthBottom = 1f;

            table.AddCell(canceledCell);

            PdfPCell totalCell = new PdfPCell(new Phrase("Total: " + rowcount.ToString()));
            totalCell.Colspan = dataTable.Columns.Count;
            totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalCell.PaddingTop = 10f;
            totalCell.PaddingBottom = 10f;
            totalCell.Border = Rectangle.BOTTOM_BORDER;
            totalCell.BorderColorBottom = BaseColor.BLACK;
            totalCell.BorderWidthBottom = 1f;
            table.AddCell(totalCell);

            pdfDoc.Add(table);
            pdfDoc.Close();

            return pdfStream;
        }
    }
}