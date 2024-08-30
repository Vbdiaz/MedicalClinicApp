using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Drawing;



namespace WebApplication1
{
    public partial class ReportGenerator : System.Web.UI.Page
    {
protected void Page_Load(object sender, EventArgs e)

{
    // Get Doctor Name for Welcome Header
    int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
    string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";
    MySqlConnection connection = new MySqlConnection(connectionString);
    string query = "SELECT CONCAT('Dr. ', fname, ' ', lname, ' - ', specialty) from doctor WHERE doctorID = @doctorID";
    MySqlCommand command = new MySqlCommand(query, connection);
    command.Parameters.AddWithValue("doctorID", doctorID);
    connection.Open();
    object result = command.ExecuteScalar();
    string fullname = result.ToString();
    LinkButton1.Text = "Logged in as: " + fullname;

    if (!IsPostBack)
    {
        startDate.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
        endDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        string query2 = "SELECT DISTINCT patients.patientID as patientID, CONCAT(patients.fname, ' ', patients.lname) as PatientName FROM patients, appointment WHERE patients.doctorID = @DoctorID OR (appointment.patientID=patients.patientID AND appointment.doctorID = @DoctorID)";
        MySqlCommand cmd = new MySqlCommand(query2, connection);
        cmd.Parameters.AddWithValue("@doctorID", doctorID);
        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            ListItem item = new ListItem(reader["PatientName"].ToString(), reader["patientID"].ToString());
            patientName.Items.Add(item);
        }

        reader.Close();
    }
    connection.Close();

}


        protected void GenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected patient name and date range
                string selectedPatient = patientName.SelectedValue;
                DateTime start, end;
                try
                {
                    start = DateTime.Parse(startDate.Text);
                    end = DateTime.Parse(endDate.Text);
                }
                catch
                {
                    start = DateTime.MinValue;
                    end = DateTime.MaxValue;
                }

                string connectionString = "Server=medicalclinic-medicalclinicdb.c.aivencloud.com;Port=11148;Database=medicaldb;Uid=avnadmin;Pwd=AVNS_8V0IkFJoP9KKvJoF4sy;SslMode=VerifyCA;SslCa=C:\\Users\\vbdia\\source\\repos\\MedicalClinicApp\\ca.pem";

                string query = "SELECT patientID, height_inches as height, weight_lb as weight, AppointmentDate, bloodPressure_dia, bloodPressure_sys, temperature, heartRate FROM visit_details, appointment WHERE appointment.appointmentID = visit_details.appointmentID AND appointment.patientID = @patientID AND appointmentDate >= @start AND appointmentDate <=@end ORDER BY appointmentDate ASC";
                DataTable visitDetails = new DataTable();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", selectedPatient);
                        command.Parameters.AddWithValue("@start", start);
                        command.Parameters.AddWithValue("@end", end);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(visitDetails);
                    }
                    connection.Close();
                }

                // Generate the report table
                Table reportTable = new Table();
                reportTable.ID = "reportTable";
                reportTable.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                reportTable.Attributes.Add("class", "table-class"); // Add class attribute for styling

                // Generate the header row
                TableHeaderRow headerRow = new TableHeaderRow();
                headerRow.TableSection = TableRowSection.TableHeader;
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Date" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "BP diastolic(mmHg)" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "BP systolic(mmHg)" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Temperature(F)" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Heart Rate(bpm)" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Height(in)" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Weight(lb)" });
                headerRow.Attributes.Add("class", "header-row-class"); // Add class attribute for styling
                reportTable.Rows.Add(headerRow);

                // Generate the data rows
                foreach (DataRow row in visitDetails.Rows)
                {
                    TableRow dataRow = new TableRow();
                    dataRow.TableSection = TableRowSection.TableBody;
                    dataRow.Cells.Add(new TableCell() { Text = ((DateTime)row["AppointmentDate"]).ToString("MM/dd/yyyy") });
                    dataRow.Cells.Add(new TableCell() { Text = row["bloodPressure_dia"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["bloodPressure_sys"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["temperature"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["heartRate"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["height"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["weight"].ToString() });
                    dataRow.Attributes.Add("class", "data-row-class"); // Add class attribute for styling
                    reportTable.Rows.Add(dataRow);

                }

                // Generate the cumulative average row
                TableRow avgRow = new TableRow();
                avgRow.TableSection = TableRowSection.TableFooter;

                TableCell avgLabelCell = new TableCell();
                avgLabelCell.Text = "Average";
                avgRow.Cells.Add(avgLabelCell);

                double avgBloodPressure_dia = 0;
                double avgBloodPressure_sys = 0;
                double avgTemperature = 0;
                double avgHeartRate = 0;
                double avgWeight = 0;
                double avgHeight = 0;

                int rowCount = visitDetails.Rows.Count;

                if (rowCount > 0)
                {
                    double totalBloodPressure_dia = 0;
                    double totalBloodPressure_sys = 0;
                    double totalTemperature = 0;
                    double totalHeartRate = 0;
                    double totalWeight = 0;
                    double totalHeight = 0;



                    foreach (DataRow row in visitDetails.Rows)
                    {
                        totalBloodPressure_dia += Convert.ToDouble(row["bloodPressure_dia"]);
                        totalBloodPressure_sys += Convert.ToDouble(row["bloodPressure_sys"]);
                        totalTemperature += Convert.ToDouble(row["temperature"]);
                        totalHeartRate += Convert.ToDouble(row["heartRate"]);
                        totalWeight += Convert.ToDouble(row["weight"]);
                        totalHeight += Convert.ToDouble(row["height"]);
                    }

                    avgBloodPressure_dia = totalBloodPressure_dia / rowCount;
                    avgBloodPressure_sys = totalBloodPressure_sys / rowCount;
                    avgTemperature = totalTemperature / rowCount;
                    avgHeartRate = totalHeartRate / rowCount;
                    avgWeight = totalWeight / rowCount;
                    avgHeight = totalHeight / rowCount;
                }

                TableCell avgBloodPressurediaCell = new TableCell();
                avgBloodPressurediaCell.Text = avgBloodPressure_dia.ToString("F1");
                avgRow.Cells.Add(avgBloodPressurediaCell);

                TableCell avgBloodPressuresysCell = new TableCell();
                avgBloodPressuresysCell.Text = avgBloodPressure_sys.ToString("F1");
                avgRow.Cells.Add(avgBloodPressuresysCell);

                TableCell avgTemperatureCell = new TableCell();
                avgTemperatureCell.Text = avgTemperature.ToString("F1");
                avgRow.Cells.Add(avgTemperatureCell);

                TableCell avgHeartRateCell = new TableCell();
                avgHeartRateCell.Text = avgHeartRate.ToString("F1");
                avgRow.Cells.Add(avgHeartRateCell);

                TableCell avgHeightCell = new TableCell();
                avgHeightCell.Text = avgHeight.ToString("F1");
                avgRow.Cells.Add(avgHeightCell);

                TableCell avgWeightCell = new TableCell();
                avgWeightCell.Text = avgWeight.ToString("F1");
                avgRow.Cells.Add(avgWeightCell);

                avgRow.Attributes.Add("class", "average-row-class"); // Add class attribute for styling
                reportTable.Rows.Add(avgRow);


                // Add the report table to the page

                reportDiv.Controls.Add(reportTable);

                if (graphBox.Checked)
                {
                    // Generate the chart
                    generateChart(visitDetails, start, end);
                }
            }
            catch
            {
                //
            }

        }

        protected void generateChart(DataTable visitDetails, DateTime start, DateTime end)
        {
            // Set the chart properties
            Chart chart = new Chart();
            chart.Width = 500;
            chart.Height = 300;
            chart.Titles.Add("Blood Pressure");
            ChartArea chartArea = new ChartArea();
            chart.BackColor = Color.Transparent;
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisY.Minimum = 50; // Set the minimum value for the Y-axis
            chartArea.AxisY.Maximum = 150; // Set the maximum value for the Y-axis
            chartArea.AxisY.Interval = 20; // Set the interval for the Y-axis
            chart.ChartAreas.Add(chartArea);

            Legend legend = new Legend();
            legend.BackColor = Color.Transparent;
            chart.Legends.Add(legend);

            // Set the chart properties
            Chart chart2 = new Chart();
            chart2.Width = 500;
            chart2.Height = 300;
            chart2.Titles.Add("Temperature");
            ChartArea chartArea2 = new ChartArea();
            chart2.BackColor = Color.Transparent;
            chartArea2.BackColor = Color.Transparent;
            chartArea2.AxisY.Minimum = 80; // Set the minimum value for the Y-axis
            chartArea2.AxisY.Maximum = 120; // Set the maximum value for the Y-axis
            chartArea2.AxisY.Interval = 10; // Set the interval for the Y-axis
            chart2.ChartAreas.Add(chartArea2);

            Legend legend2 = new Legend();
            legend2.BackColor = Color.Transparent;
            chart2.Legends.Add(legend2);

            // Set the chart properties
            Chart chart3 = new Chart();
            chart3.Width = 500;
            chart3.Height = 300;
            chart3.Titles.Add("Heart Rate");
            ChartArea chartArea3 = new ChartArea();
            chart3.BackColor = Color.Transparent;
            chartArea3.BackColor = Color.Transparent;
            chartArea3.AxisY.Minimum = 60; // Set the minimum value for the Y-axis
            chartArea3.AxisY.Maximum = 120; // Set the maximum value for the Y-axis
            chartArea3.AxisY.Interval = 10; // Set the interval for the Y-axis
            chart3.ChartAreas.Add(chartArea3);

            Legend legend3 = new Legend();
            legend3.BackColor = Color.Transparent;
            chart3.Legends.Add(legend3);

            // Set the chart properties
            Chart chart4 = new Chart();
            chart4.Width = 500;
            chart4.Height = 300;
            chart4.Titles.Add("Weight");
            ChartArea chartArea4 = new ChartArea();
            chart4.BackColor = Color.Transparent;
            chartArea4.BackColor = Color.Transparent;
            chartArea4.AxisY.Minimum = 100; // Set the minimum value for the Y-axis
            chartArea4.AxisY.Maximum = 200; // Set the maximum value for the Y-axis
            chartArea4.AxisY.Interval = 20; // Set the interval for the Y-axis
            chart4.ChartAreas.Add(chartArea4);

            Legend legend4 = new Legend();
            legend4.BackColor = Color.Transparent;
            chart4.Legends.Add(legend4);

            // Set the chart properties
            Chart chart5 = new Chart();
            chart5.Width = 500;
            chart5.Height = 300;
            chart5.Titles.Add("Height");
            ChartArea chartArea5 = new ChartArea();
            chart5.BackColor = Color.Transparent;
            chartArea5.BackColor = Color.Transparent;
            chart5.ChartAreas.Add(chartArea5);

            Legend legend5 = new Legend();
            legend5.BackColor = Color.Transparent;
            chart5.Legends.Add(legend5);

            // Set the chart series
            Series bloodPressureSeries_dia = new Series();
            bloodPressureSeries_dia.Name = "BP diastolic(mmHg)";
            bloodPressureSeries_dia.ChartType = SeriesChartType.Line;
            bloodPressureSeries_dia.BorderWidth = 3;
            chart.Series.Add(bloodPressureSeries_dia);

            Series bloodPressureSeries_sys = new Series();
            bloodPressureSeries_sys.Name = "BP systolic(mmHg)";
            bloodPressureSeries_sys.ChartType = SeriesChartType.Line;
            bloodPressureSeries_sys.BorderWidth = 3;
            chart.Series.Add(bloodPressureSeries_sys);

            Series temperatureSeries = new Series();
            temperatureSeries.Name = "Temperature(F)";
            temperatureSeries.ChartType = SeriesChartType.Line;
            temperatureSeries.BorderWidth = 3;
            chart2.Series.Add(temperatureSeries);

            Series heartRateSeries = new Series();
            heartRateSeries.Name = "Heart Rate(bpm)";
            heartRateSeries.ChartType = SeriesChartType.Line;
            heartRateSeries.BorderWidth = 3;
            chart3.Series.Add(heartRateSeries);


            Series WeightSeries = new Series();
            WeightSeries.Name = "Weight(lb)";
            WeightSeries.ChartType = SeriesChartType.Line;
            WeightSeries.BorderWidth = 3;
            chart4.Series.Add(WeightSeries);

            Series HeightSeries = new Series();
            HeightSeries.Name = "Height(in)";
            HeightSeries.ChartType = SeriesChartType.Line;
            HeightSeries.BorderWidth = 3;
            chart5.Series.Add(HeightSeries);

            // Add the data points to the chart series
            foreach (DataRow row in visitDetails.Rows)
            {
                DateTime date = (DateTime)row["AppointmentDate"];
                if (date >= start && date <= end)
                {
                    double bloodPressure_dia = Convert.ToDouble(row["bloodPressure_dia"]);
                    double bloodPressure_sys = Convert.ToDouble(row["bloodPressure_sys"]);
                    double temperature = Convert.ToDouble(row["temperature"]);
                    double heartRate = Convert.ToDouble(row["heartRate"]);
                    double weight = Convert.ToDouble(row["weight"]);
                    double height = Convert.ToDouble(row["height"]);

                    bloodPressureSeries_dia.Points.AddXY(date, bloodPressure_dia);
                    bloodPressureSeries_sys.Points.AddXY(date, bloodPressure_sys);
                    temperatureSeries.Points.AddXY(date, temperature);
                    heartRateSeries.Points.AddXY(date, heartRate);
                    WeightSeries.Points.AddXY(date, weight);
                    HeightSeries.Points.AddXY(date,height);
                }
            }
            //Console.WriteLine($"Blood Pressure Series: {bloodPressureSeries_dia.Points.Count} points");
            //Console.WriteLine($"Blood Pressure Series: {bloodPressureSeries_sys.Points.Count} points");
            //Console.WriteLine($"Temperature Series: {temperatureSeries.Points.Count} points");
            //Console.WriteLine($"Heart Rate Series: {heartRateSeries.Points.Count} points");
            //Console.WriteLine($"Weight Series: {WeightSeries.Points.Count} points");
            //Console.WriteLine($"Height Series: {HeightSeries.Points.Count} points");



            // Add the chart to the page
            chartPlaceholder.Controls.Add(chart);
            chartPlaceholder2.Controls.Add(chart2);
            chartPlaceholder3.Controls.Add(chart3);
            chartPlaceholder4.Controls.Add(chart4);
            //chartPlaceholder5.Controls.Add(chart5);

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int doctorID = Convert.ToInt32(Request.QueryString["doctorID"]);
            Response.Redirect("ProviderView.aspx?doctorID=" + doctorID);

        }
    }
}
