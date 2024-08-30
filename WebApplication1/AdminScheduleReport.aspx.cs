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
using System.Text;

namespace WebApplication1
{
    public partial class AdminScheduleReport : System.Web.UI.Page
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
            List<DataTable> dataTables = GetDataFromMySQL();
            MemoryStream pdfStream = GeneratePDF(dataTables);
            GenerateHTMLTable(dataTables);

            // Save the PDF file to the server
            string reportsDirectory = Server.MapPath("~/GeneratedReports");

            // Check if the directory exists, if not, create it
            if (!Directory.Exists(reportsDirectory))
            {
                Directory.CreateDirectory(reportsDirectory);
            }

            string pdfFileName = Path.Combine(reportsDirectory, "SchedulingReport.pdf");
            File.WriteAllBytes(pdfFileName, pdfStream.ToArray());

            // Provide a download link to the generated PDF
            btnDownloadPDF.NavigateUrl = "~/GeneratedReports/SchedulingReport.pdf";
            btnDownloadPDF.Visible = true;
        }



        private List<DataTable> GetDataFromMySQL()
        {
            List<DataTable> dataTables = new List<DataTable>();
            // Replace with your MySQL credentials
            string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string selectedValue = DropDownList3.SelectedValue;
                string query = "";
                if (selectedValue == "Doctor")
                {
                    query = "SELECT \r\n  d.DoctorID, d.Fname, d.Lname, d.Specialty, d.Phone_num, d.Salary,\r\n  SUBSTRING_INDEX(o1.officeAddress, ',', 1) AS Monday,\r\n  SUBSTRING_INDEX(o2.officeAddress, ',', 1) AS Tuesday,\r\n  SUBSTRING_INDEX(o3.officeAddress, ',', 1) AS Wednesday,\r\n  SUBSTRING_INDEX(o4.officeAddress, ',', 1) AS Thursday,\r\n  SUBSTRING_INDEX(o5.officeAddress, ',', 1) AS Friday\r\nFROM schedule s\r\nJOIN doctor d ON s.doctor = d.DoctorID\r\nLEFT JOIN office o1 ON s.Monday = o1.officeID\r\nLEFT JOIN office o2 ON s.Tuesday = o2.officeID\r\nLEFT JOIN office o3 ON s.Wednesday = o3.officeID\r\nLEFT JOIN office o4 ON s.Thursday = o4.officeID\r\nLEFT JOIN office o5 ON s.Friday = o5.officeID;";
                }
                else if (selectedValue == "Nurse")
                {
                    query = "SELECT\r\n    n.NID,\r\n    n.Fname,\r\n    n.Lname,\r\n    n.Work_email,\r\n    SUBSTRING_INDEX(o.officeAddress, ',', 1) AS office,\r\n    n.pay\r\nFROM\r\n    nurse AS n\r\nJOIN\r\n    office AS o ON n.officeID = o.officeID;\r\n";
                }
                else if (selectedValue == "Staff")
                {
                    query = "SELECT\r\n    s.StaffID,\r\n    s.FName,\r\n    s.LName,\r\n    s.Work_email,\r\n    SUBSTRING_INDEX(o.officeAddress, ',', 1) AS office,\r\n    s.Pay\r\nFROM\r\n    staff AS s\r\nJOIN\r\n    office AS o ON s.officeID = o.officeID;";
                }
                else if (selectedValue == "Patient")
                {
                    query = "SELECT\r\n    p.PatientID,\r\n    p.fName AS Name,\r\n    p.lName AS Surname,\r\n    p.DOB,\r\n    p.Phone_num as Phone,\r\n    d.DoctorID,\r\n    CONCAT(d.Fname, ' ', d.Lname) AS Doctor,\r\n    p.Balance,\r\n    p.Payment\r\nFROM\r\n    patients AS p\r\nJOIN\r\n    doctor AS d ON p.doctorID = d.DoctorID;";
                }
                else // Default to login table
                {
                    query = "SELECT \r\n  d.DoctorID, d.Fname, d.Lname, d.Specialty, d.Phone_num,\r\n  SUBSTRING_INDEX(o1.officeAddress, ',', 1) AS Monday,\r\n  SUBSTRING_INDEX(o2.officeAddress, ',', 1) AS Tuesday,\r\n  SUBSTRING_INDEX(o3.officeAddress, ',', 1) AS Wednesday,\r\n  SUBSTRING_INDEX(o4.officeAddress, ',', 1) AS Thursday,\r\n  SUBSTRING_INDEX(o5.officeAddress, ',', 1) AS Friday\r\nFROM schedule s\r\nJOIN doctor d ON s.doctor = d.DoctorID\r\nLEFT JOIN office o1 ON s.Monday = o1.officeID\r\nLEFT JOIN office o2 ON s.Tuesday = o2.officeID\r\nLEFT JOIN office o3 ON s.Wednesday = o3.officeID\r\nLEFT JOIN office o4 ON s.Thursday = o4.officeID\r\nLEFT JOIN office o5 ON s.Friday = o5.officeID;";
                }
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataTables.Add(dataTable);

                if (selectedValue == "Doctor")
                {
                    string officeQuery = "SELECT \r\n  office_address,\r\n  COUNT(CASE WHEN Monday = office_address THEN 1 END) AS Monday,\r\n  COUNT(CASE WHEN Tuesday = office_address THEN 1 END) AS Tuesday,\r\n  COUNT(CASE WHEN Wednesday = office_address THEN 1 END) AS Wednesday,\r\n  COUNT(CASE WHEN Thursday = office_address THEN 1 END) AS Thursday,\r\n  COUNT(CASE WHEN Friday = office_address THEN 1 END) AS Friday\r\nFROM (\r\n  SELECT\r\n    SUBSTRING_INDEX(o1.officeAddress, ',', 1) AS Monday,\r\n    SUBSTRING_INDEX(o2.officeAddress, ',', 1) AS Tuesday,\r\n    SUBSTRING_INDEX(o3.officeAddress, ',', 1) AS Wednesday,\r\n    SUBSTRING_INDEX(o4.officeAddress, ',', 1) AS Thursday,\r\n    SUBSTRING_INDEX(o5.officeAddress, ',', 1) AS Friday\r\n  FROM schedule s\r\n  JOIN doctor d ON s.doctor = d.DoctorID\r\n  LEFT JOIN office o1 ON s.Monday = o1.officeID\r\n  LEFT JOIN office o2 ON s.Tuesday = o2.officeID\r\n  LEFT JOIN office o3 ON s.Wednesday = o3.officeID\r\n  LEFT JOIN office o4 ON s.Thursday = o4.officeID\r\n  LEFT JOIN office o5 ON s.Friday = o5.officeID\r\n) AS doctor_schedule\r\nCROSS JOIN (SELECT '123 Main St.' AS office_address UNION SELECT '134 Baker St.') AS office_list\r\nGROUP BY office_address;";
                    // Create a new MySqlCommand object with the new query and the existing connection
                    MySqlCommand officeCommand = new MySqlCommand(officeQuery, connection);
                    // Create a new MySqlDataAdapter object with the new command
                    MySqlDataAdapter officeAdapter = new MySqlDataAdapter(officeCommand);
                    // Create a new DataTable object, fill it with the adapter, and add it to the dataTables list
                    DataTable officeDataTable = new DataTable();
                    officeAdapter.Fill(officeDataTable);
                    dataTables.Add(officeDataTable);
                    string officeQuery2 = "SELECT\r\n    AVG(Salary) AS 'Average Pay',\r\n    MAX(Salary) AS 'Highest Pay',\r\n    MIN(Salary) AS 'Lowest Pay'\r\nFROM\r\n    doctor\r\nWHERE\r\n    Salary IS NOT NULL;";
                    // Create a new MySqlCommand object with the new query and the existing connection
                    MySqlCommand officeCommand2 = new MySqlCommand(officeQuery2, connection);
                    // Create a new MySqlDataAdapter object with the new command
                    MySqlDataAdapter officeAdapter2 = new MySqlDataAdapter(officeCommand2);
                    // Create a new DataTable object, fill it with the adapter, and add it to the dataTables list
                    DataTable officeDataTable2 = new DataTable();
                    officeAdapter2.Fill(officeDataTable2);
                    dataTables.Add(officeDataTable2);
                }
                else if (selectedValue == "Nurse")
                {
                    string officeQuery = "SELECT\r\n    AVG(pay) AS 'Average Pay',\r\n    MAX(pay) AS 'Highest Pay',\r\n    MIN(pay) AS 'Lowest Pay'\r\nFROM\r\n    nurse;";
                    MySqlCommand officeCommand = new MySqlCommand(officeQuery, connection);
                    // Create a new MySqlDataAdapter object with the new command
                    MySqlDataAdapter officeAdapter = new MySqlDataAdapter(officeCommand);
                    // Create a new DataTable object, fill it with the adapter, and add it to the dataTables list
                    DataTable officeDataTable = new DataTable();
                    officeAdapter.Fill(officeDataTable);
                    dataTables.Add(officeDataTable);
                }
                else if (selectedValue == "Staff")
                {
                    string officeQuery = "SELECT\r\n    AVG(Pay) AS 'Average Pay',\r\n    MAX(Pay) AS 'Highest Pay',\r\n    MIN(Pay) AS 'Lowest Pay'\r\nFROM\r\n    staff\r\nWHERE\r\n    Pay IS NOT NULL;";
                    MySqlCommand officeCommand = new MySqlCommand(officeQuery, connection);
                    // Create a new MySqlDataAdapter object with the new command
                    MySqlDataAdapter officeAdapter = new MySqlDataAdapter(officeCommand);
                    // Create a new DataTable object, fill it with the adapter, and add it to the dataTables list
                    DataTable officeDataTable = new DataTable();
                    officeAdapter.Fill(officeDataTable);
                    dataTables.Add(officeDataTable);
                }
                else if (selectedValue == "Patient")
                {
                    string officeQuery = "SELECT\r\n    SUM(balance) AS 'Total Invoiced',\r\n    SUM(payment) AS 'Total Payments',\r\n    COUNT(CASE WHEN balance = 0 THEN 1 ELSE NULL END) AS 'Number of Patient Current',\r\n    COUNT(CASE WHEN balance > 0 THEN 1 ELSE NULL END) AS 'Number of Patient Past Due'\r\nFROM\r\n    patients;\r\n";
                    MySqlCommand officeCommand = new MySqlCommand(officeQuery, connection);
                    // Create a new MySqlDataAdapter object with the new command
                    MySqlDataAdapter officeAdapter = new MySqlDataAdapter(officeCommand);
                    // Create a new DataTable object, fill it with the adapter, and add it to the dataTables list
                    DataTable officeDataTable = new DataTable();
                    officeAdapter.Fill(officeDataTable);
                    dataTables.Add(officeDataTable);
                }
                else // Default to login table
                {
                    string officeQuery = "SELECT \r\n  office_address,\r\n  COUNT(CASE WHEN Monday = office_address THEN 1 END) AS Monday,\r\n  COUNT(CASE WHEN Tuesday = office_address THEN 1 END) AS Tuesday,\r\n  COUNT(CASE WHEN Wednesday = office_address THEN 1 END) AS Wednesday,\r\n  COUNT(CASE WHEN Thursday = office_address THEN 1 END) AS Thursday,\r\n  COUNT(CASE WHEN Friday = office_address THEN 1 END) AS Friday\r\nFROM (\r\n  SELECT\r\n    SUBSTRING_INDEX(o1.officeAddress, ',', 1) AS Monday,\r\n    SUBSTRING_INDEX(o2.officeAddress, ',', 1) AS Tuesday,\r\n    SUBSTRING_INDEX(o3.officeAddress, ',', 1) AS Wednesday,\r\n    SUBSTRING_INDEX(o4.officeAddress, ',', 1) AS Thursday,\r\n    SUBSTRING_INDEX(o5.officeAddress, ',', 1) AS Friday\r\n  FROM schedule s\r\n  JOIN doctor d ON s.doctor = d.DoctorID\r\n  LEFT JOIN office o1 ON s.Monday = o1.officeID\r\n  LEFT JOIN office o2 ON s.Tuesday = o2.officeID\r\n  LEFT JOIN office o3 ON s.Wednesday = o3.officeID\r\n  LEFT JOIN office o4 ON s.Thursday = o4.officeID\r\n  LEFT JOIN office o5 ON s.Friday = o5.officeID\r\n) AS doctor_schedule\r\nCROSS JOIN (SELECT '123 Main St.' AS office_address UNION SELECT '134 Baker St.') AS office_list\r\nGROUP BY office_address;";
                    // Create a new MySqlCommand object with the new query and the existing connection
                    MySqlCommand officeCommand = new MySqlCommand(officeQuery, connection);
                    // Create a new MySqlDataAdapter object with the new command
                    MySqlDataAdapter officeAdapter = new MySqlDataAdapter(officeCommand);
                    // Create a new DataTable object, fill it with the adapter, and add it to the dataTables list
                    DataTable officeDataTable = new DataTable();
                    officeAdapter.Fill(officeDataTable);
                    dataTables.Add(officeDataTable);
                }

            }

            return dataTables;
        }

        private MemoryStream GeneratePDF(List<DataTable> dataTables)
        {
            MemoryStream pdfStream = new MemoryStream();
            Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, pdfStream);

            pdfDoc.Open();

            Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            string selectedValue = DropDownList3.SelectedValue;
            string title = "";
            if (selectedValue == "Doctor")
            {
                title = "Report of Doctors";
            }
            else if (selectedValue == "Nurse")
            {
                title = "Report of Nurses";
            }
            else if (selectedValue == "Staff")
            {
                title = "Report of Staff";
            }
            else if (selectedValue == "Patient")
            {
                title = "Report of Patients";
            }
            else // Default to doctor table
            {
                title = "Report of Doctors";
            }
            Paragraph titleParagraph = new Paragraph(title, titleFont);
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            titleParagraph.SpacingAfter = 10;
            pdfDoc.Add(titleParagraph);
            int tableIndex = 0;

            // Loop through each DataTable in the list
            foreach (DataTable dataTable in dataTables)
            {
                tableIndex++;
                // Create a new PdfPTable for each DataTable
                PdfPTable table = new PdfPTable(dataTable.Columns.Count);

                if (tableIndex == 2)
                {
                    Font subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                    Paragraph subTitleParagraph = new Paragraph("Number of Doctors Working on Certain Day", subTitleFont);
                    if (selectedValue == "Doctor")
                    {
                        subTitleParagraph = new Paragraph("Summary of Doctor Personnel", subTitleFont);
                    }
                    else if (selectedValue == "Nurse")
                    {
                        subTitleParagraph = new Paragraph("Summary of Nurse Personnel", subTitleFont);
                    }
                    else if (selectedValue == "Staff")
                    {
                        subTitleParagraph = new Paragraph("Summary of Staff Personel", subTitleFont);
                    }
                    else if (selectedValue == "Patient")
                    {
                        subTitleParagraph = new Paragraph("Summary of Patient Personnel", subTitleFont);
                    }
                    else // Default to doctor table
                    {
                        subTitleParagraph = new Paragraph("Summary of Doctor Personnel", subTitleFont);
                    }

                    subTitleParagraph.Alignment = Element.ALIGN_CENTER;
                    subTitleParagraph.SpacingAfter = 10;
                    pdfDoc.Add(subTitleParagraph);
                }
                // Add header row
                foreach (DataColumn column in dataTable.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName));
                    cell.BackgroundColor = new BaseColor(240, 240, 240);
                    table.AddCell(cell);
                }

                // Add data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (object cellData in row.ItemArray)
                    {
                        table.AddCell(cellData.ToString());
                    }
                }

                // Add the PdfPTable to the PDF document
                pdfDoc.Add(table);

                // Add some spacing between tables
                pdfDoc.Add(new Chunk("\n"));
            }

            pdfDoc.Close();

            return pdfStream;
        }
        private void GenerateHTMLTable(List<DataTable> dataTables)
        {
            StringBuilder htmlTables = new StringBuilder();

            foreach (DataTable dataTable in dataTables)
            {
                htmlTables.Append("<table border='1' cellspacing='0' cellpadding='5'>");

                // Add header row
                htmlTables.Append("<tr style='background-color: #f0f0f0;'>");
                foreach (DataColumn column in dataTable.Columns)
                {
                    htmlTables.AppendFormat("<th>{0}</th>", column.ColumnName);
                }
                htmlTables.Append("</tr>");

                // Add data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    htmlTables.Append("<tr>");
                    foreach (object cellData in row.ItemArray)
                    {
                        htmlTables.AppendFormat("<td>{0}</td>", cellData.ToString());
                    }
                    htmlTables.Append("</tr>");
                }

                htmlTables.Append("</table>");
                htmlTables.Append("<br>"); // Add a line break between tables
            }

            // Assign the generated HTML tables to the Literal control
            TableLiteral.Text = htmlTables.ToString();
        }



    }
}