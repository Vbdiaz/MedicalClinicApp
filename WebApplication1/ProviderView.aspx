<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderView.aspx.cs" Inherits="WebApplication1.ProviderView" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <style>   
        .header-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

    </style>
	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />


    <title>Doctor Portal</title>
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
  <div class="header-container">

   <h1 id="welcomeHeader" runat="server"> </h1>
    <asp:Button ID="Button1" BorderStyle="Double" BackColor="LightGray" runat="server" OnClick="Button1_Click" Text="Patients View" Height="51px" Width="169px" Font-Bold="True" Font-Size="Medium" Font-Strikeout="False" />

      </div>
                        <p style="font-size:18px" id="officeLocale" runat="server"></p>

    <br />
    <br />

        <div>
            <h1>Upcoming Appointments</h1>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="AppointmentID"
                OnRowCommand="GridView1_RowCommand" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="appointmentID" HeaderText="AppointmentID" />
                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
                    <asp:BoundField DataField="OfficeLocation" HeaderText="Office Location" />
                    <asp:BoundField DataField="Approval" HeaderText="Approval Status" />
                    <asp:BoundField DataField="Time" HeaderText="Time"/>
                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}"/>
                    <asp:ButtonField ButtonType = "button" Text="APPROVE" CommandName="ApproveAppointment" HeaderText="Approval" ControlStyle-BackColor="">
<ControlStyle BackColor=""></ControlStyle>
                    </asp:ButtonField>
                    <asp:ButtonField ButtonType="button" Text="DENY" CommandName="DenyAppointment" HeaderText="Denial" ControlStyle-BackColor="" >


<ControlStyle BackColor=""></ControlStyle>
                    </asp:ButtonField>


                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#808080" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#383838" />
            </asp:GridView>
                    </div>
            <br />
 


        <div>
            <h1>Referral Review</h1>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="AppointmentID2"
                OnRowCommand="GridView2_RowCommand" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="appointmentID2" HeaderText="AppointmentID" />
                    <asp:BoundField DataField="PatientName2" HeaderText="Patient Name" />
                    <asp:BoundField DataField="Specialist" HeaderText="Specialty" />
                    <asp:BoundField DataField="Referral" HeaderText="Referral Status" />
                    <asp:BoundField DataField="Date2" HeaderText="Date" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="Doctor" HeaderText="Doctor" />
                    <asp:ButtonField ButtonType="button" Text="APPROVE" CommandName="ApproveAppointment2" HeaderText="Approval" ControlStyle-BackColor="">
<ControlStyle BackColor=""></ControlStyle>
                    </asp:ButtonField>
                    <asp:ButtonField ButtonType="button" Text="DENY" CommandName="DenyAppointment2" HeaderText="Denial" ControlStyle-BackColor="" >
<ControlStyle BackColor=""></ControlStyle>
                    </asp:ButtonField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#808080" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#383838" />
            </asp:GridView>
        </div>
         <div>
            <h1>Previous Appointments</h1>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="AppointmentID"
                OnRowCommand="GridView3_RowCommand" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="appointmentID" HeaderText="AppointmentID" />
                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
                    <asp:BoundField DataField="OfficeLocation" HeaderText="Office Location" />
                    <asp:BoundField DataField="Time" HeaderText="Time" />
                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}"/>
                    <asp:ButtonField ButtonType="button" Text="VIEW" CommandName="VIEW" HeaderText="REPORT" ControlStyle-BackColor="">
<ControlStyle BackColor=""></ControlStyle>
                    </asp:ButtonField>



                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#808080" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#383838" />
            </asp:GridView>
        </div>
        <br />
                <div>
            <h1>Prescription Requests</h1>
                            <asp:GridView ID="prescriptionGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="patientID"
                OnRowCommand="prescriptions_RowCommand" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="appointmentID" HeaderText="AppointmentID" />
                    <asp:BoundField DataField="patientID" HeaderText="patientID" Visible="false" />
                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
                    <asp:BoundField DataField="Date" HeaderText="Appointment Date" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="DrugClass" HeaderText="Drug Class" />
                    <asp:ButtonField ButtonType = "button" Text="ORDER" CommandName="create_prescription" HeaderText="Prescription" ControlStyle-BackColor="">
<ControlStyle BackColor=""></ControlStyle>
                    </asp:ButtonField>


                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#808080" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#383838" />
            </asp:GridView>
        </div>
        <br />

 
        <script>
            $(document).ready(function () {
                // Attach click event handler to the approve and deny buttons
                $("input[type='button'][value='APPROVE'], input[type='button'][value='DENY']").click(function (e) {
                    // Prevent the button from performing its default action
                    e.preventDefault();
                    // Open the dialog box
                    var message = "ARE YOU SURE YOU WANT TO " + $(this).val() + " THIS APPOINTMENT? PRESS OK TO EMAIL THE PATIENT";
                    if (confirm(message)) {
                        // If the user confirms, submit the form
                        $(this).closest("form").submit();
                    }
                });
            });
        </script>

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
