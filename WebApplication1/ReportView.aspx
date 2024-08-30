<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="WebApplication1.ReportView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient Form</title>
    <style type="text/css">
        #patientName {
            width: 521px;
        }
    </style>
    	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="patientForm" runat="server">
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
        <div style="font-weight: bold">
            <!--<h1>Report for spiderman on 3/28/23</h1> --> <!-- placeholder header -->
            <h1 id="reportHeader" runat="server"> </h1>
            <p id ="reportBy" runat="server"></p>
            <hr style="color: #000000" />
            <label for="patientName" style="font-weight: bold">
            <br />
            Patient Name:</label> <br />
            <asp:TextBox ID="patientNameBox" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" MaxLength="100"  ReadOnly="True" Width="362px"></asp:TextBox>

            <label for= "patientAge">
            <br />
            </label>
            <label for="patientGender">
            <br />
            Doctor Name:</label><br />

            <asp:TextBox ID="doctorNameBox" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px" MaxLength="10"  ReadOnly="True" Width="370px"></asp:TextBox>
            <br />

            <label for="patientCondition">
            <br />
            Patient Condition/Diagnosis: 
             <br />
            </label>
            <asp:TextBox ID="diagnosisBox" runat="server" BackColor="#CCCCCC" BorderWidth="2px" Height="72px" MaxLength="200" TextMode="MultiLine" Width="428px" ReadOnly="True"></asp:TextBox>
            
            <label for="patientSymptoms"></label>
            <br />
            <br />
Temperature(F)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BP sys(mmHg)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BP dia(mmHg)&nbsp;&nbsp;&nbsp;&nbsp;<br />
                &nbsp;<div style="display:inline">
                  <asp:TextBox ID="temperatureBox" runat="server" Width="86px" style="margin-right: 40px" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px"></asp:TextBox>
                 <asp:TextBox ID="p_sys" runat="server" Width="97px" style="margin-right: 40px" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px"></asp:TextBox>
                 <asp:TextBox ID="p_dia" runat="server" Width="98px" style="margin-right: 40px" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px"></asp:TextBox>
             </div>
                           <br />
                    <br />
&nbsp; Height(in)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Weight(lb)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Heart Rate(bpm)<br />
&nbsp;<div style="display:inline">
                 <asp:TextBox ID="heightBox" runat="server" Width="86px" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:TextBox ID="weightBox" runat="server" Width="86px" style="margin-right: 40px" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px"></asp:TextBox>
                 <asp:TextBox ID="heartrateBox" runat="server" Width="86px" style="margin-right: 40px" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px"></asp:TextBox>

             </div>
            
            <br />
            <br />
            Patient Symptoms:<br />
            <asp:TextBox ID="symptomsBox" runat="server" BackColor="#CCCCCC" BorderWidth="2px" Height="57px" MaxLength="200" TextMode="MultiLine" Width="420px" ReadOnly="True"></asp:TextBox>
&nbsp;<br />
            Patient Prescription:<br />
            <asp:TextBox ID="prescriptionBox" runat="server" BackColor="#CCCCCC" BorderWidth="2px" Height="57px" MaxLength="200" TextMode="MultiLine" Width="420px" ReadOnly="True"></asp:TextBox>
            <br />
            <br />

            Follow-up evaluation:<br />
                  <asp:TextBox ID="evalBox" runat="server" Width="137px" style="margin-right: 40px" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px" ReadOnly="True"></asp:TextBox>

            <br />
            <br />
            Appointment Total:
            <br />
            <asp:TextBox ID="apptTotalBox" runat="server" BackColor="#CCCCCC" BorderWidth="2px" Height="19px" Width="70px" Wrap="False" ReadOnly="True"></asp:TextBox>
            <br />
            <br />
            Insurance Coverage:<br />
            <asp:TextBox ID="insuranceCovBox" runat="server" BackColor="#CCCCCC" BorderWidth="2px" Height="16px" Width="79px" ReadOnly="True"></asp:TextBox>
            <br />
            <br />
            Co-Payment:<br />
            <asp:TextBox ID="copayBox" runat="server" BackColor="#CCCCCC" BorderWidth="2px" Height="16px" Width="79px"></asp:TextBox>
            <br />
            &nbsp;</div>
        <p>
            <asp:Button ID="returnBttn" runat="server" BackColor="#003300" BorderColor="#009933" BorderWidth="2px" Font-Bold="True" ForeColor="White" Height="37px" OnClick="returnBttn_Click" Text="Return" Width="251px" />
        </p>
    </form>
     <div class="footer">
  <section class="contact">
    <p>Email: info@coogmedicalgroup.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p>Email: info@coogclinic.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>
</body>
</html>