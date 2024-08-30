<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NurseView.aspx.cs" Inherits="WebApplication1.NurseView" %>
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


    <title>Nurse Portal</title>
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
        <p1 style="font-size: 18px" id ="officeHeader" runat ="server"></p1>

    <br />
    <br />

        <div>
            <h1>Upcoming Appointments</h1>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="nurseID" OnRowCommand="GridView1_RowCommand"
                 BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="appointmentID" HeaderText="AppointmentID" />
                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
                    <asp:BoundField DataField="docName" HeaderText="Doctor" />
                    <asp:BoundField DataField="Specialty" HeaderText="Specialty" />
                    <asp:BoundField DataField="Approval" HeaderText="Approval Status" />
                    <asp:BoundField DataField="Time" HeaderText="Time" />
                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="nurseID" HeaderText="nurseID" Visible="false"/>
                    <asp:BoundField DataField="Nurse" HeaderText="Nurse" NullDisplayText="Not Assigned" />
                    <asp:ButtonField ButtonType="button" Text="ACCEPT" CommandName="assignApp" HeaderText="Assignment"/>
                    <asp:ButtonField ButtonType="button" Text="GENERATE" CommandName="GENERATE" HeaderText="REPORT" ControlStyle-BackColor="" >
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
                    <asp:BoundField DataField="docName" HeaderText="Doctor" />
                    <asp:BoundField DataField="Specialty" HeaderText="Specialty" />
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
                        
        <div>
            <h1>Medical Testing</h1>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="TestID"
                OnRowCommand="GridView2_RowCommand" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="TestID" HeaderText="TestID"/>
                    <asp:BoundField DataField="Test" HeaderText="Test Ordered" />
                    <asp:BoundField DataField="patname" HeaderText="Patient" />
                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}" NullDisplayText="Awaiting Patient" />
                    <asp:BoundField DataField="Time" HeaderText="Time" NullDisplayText="Awaiting Patient" />
                    <asp:BoundField DataField="Nurse" HeaderText="Nurse" NullDisplayText="Not Assigned"/>
                    <asp:ButtonField ButtonType="button" Text="ACCEPT" CommandName="assign" HeaderText="Assignment" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </div>

        <script>
            $(document).ready(function () {
                $('#GridView1').on('click', 'input[type="button"]', function (e) {
                    e.preventDefault();
                    var row = $(this).closest('tr');
                    var nurseCell = row.find('td:eq(7)'); 
                    var $button = $(this);
                    var buttonText = $button.val();
                    if (buttonText === 'ACCEPT') {
                        if (nurseCell.text() !== 'Not Assigned') {
                            alert("Cannot accept. A nurse is already assigned to this task.");
                        } else {
                            // Perform the 'Accept' action if the nurse is not assigned
                        }
                    }
                });
            });

            $(document).ready(function () {
                $('#GridView2').on('click', 'input[type="button"]', function (e) {
                    e.preventDefault();
                    var row = $(this).closest('tr');
                    var nurseCell = row.find('td:eq(5)');
                    var $button = $(this);
                    var buttonText = $button.val();
                    if (buttonText === 'ACCEPT') {
                        if (nurseCell.text() !== 'Not Assigned') {
                            alert("Cannot accept. A nurse is already assigned to this task.");
                        } else {
                            // Perform the 'Accept' action if the nurse is not assigned
                        }
                    }
                });
            });

            $(document).ready(function () {
                $('#GridView1').on('click', 'input[type="button"]', function (e) {
                    e.preventDefault();
                    var row = $(this).closest('tr');
                    var appointmentDateCell = row.find('td:eq(6)');
                    var appointmentDate = new Date(appointmentDateCell.text());
                    var today = new Date();

                    today.setHours(0, 0, 0, 0);
                    appointmentDate.setHours(0, 0, 0, 0);

                    var $button = $(this);
                    var buttonText = $button.val();

                    if (buttonText === 'GENERATE') {
                        if (appointmentDate.getTime() !== today.getTime()) {
                            alert("You can't generate a report until the appointment has happened.");
                        } else {
                            // Perform the 'Generate' action if the appointment date is today
                        }
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

