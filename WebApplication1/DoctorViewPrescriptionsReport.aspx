<%@ Page Language="C#" CodeBehind="DoctorViewPrescriptionsReport.aspx.cs" Inherits="WebApplication1.DoctorViewPrescriptionsReport" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prescription Report</title>
        <style>

        .leftPanel {
            float: left;
            margin-right: 20px;
            margin-left: 5px;
        }

        .rightPanel {
            margin-left: 5px;
            margin-right: 20px;
            float: left;
        }

        /* Clear floats after the panels */
        .clearfix::after {
            content: "";
            display: table;
            clear: both;
        }

            .table-class {
        border: 1px solid black;
        border-collapse: collapse;
    }

    .header-row-class {
        background-color: lightblue;
    }

    .data-row-class:nth-child(even) {
        background-color: lightblue;
    }

    .average-row-class {
        font-weight: bold;

    }
    table {
        border-collapse: collapse;
        width: 100%;
    }
    th, td {
        text-align: left;
        padding: 8px;
        border-bottom: 1px solid #ddd;
    }
    td:not(:last-child) {
        border-right: 1px solid #ddd;
    }
    th {
        background-color: #f2f2f2;
    }
    </style>
    	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
            <div class="navbar">
      <a href="AboutUs.aspx">About Us</a>
      <a href="ContactUs.aspx">Contact Us</a>
      <a href="PatientLogin.aspx">Patient Login</a>
      <a href="ProviderLogin.aspx">Provider Login</a>
      <a href="HomePage.aspx">Home</a>
      <left><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton></left>
    </div>
             <br />
     <br />
 <div class="form-container">


            <h1 style="text-align:center">Patient Prescription History</h1>
            <p style="text-align:center">Please select a patient and an appropriate date range</p>
              <br />
            <div>
                <label for="startDate">Start Date:</label>
                <asp:TextBox ID="startDate" runat="server" TextMode="Date" DataFormatString="yyyy-MM-dd"></asp:TextBox>

                <label for="endDate">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; End Date:</label>
                <asp:TextBox ID="endDate" runat="server" TextMode="Date" DataFormatString="yyyy-MM-dd"></asp:TextBox>
            </div>
            <div>
                <label for="patientName">
                <br />
                Patient Name:</label>
                <asp:DropDownList ID="patientName" runat="server">
                </asp:DropDownList>
                <br />
                <br />
                <br />
            </div>
            <br />
            <asp:Button ID="generateReport" runat="server" Text="Generate Report" OnClick="Generate_Click" />
              </div>

            <br />
            <br />
            <br />
                <div id="reportDiv" runat="server" method="post">
                </div>
            <br />
            <br />
</form>
     <div class="footer">
  <section class="contact">
    <p>Email: info@coogmedicalgroup.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>
</body>
</html>
