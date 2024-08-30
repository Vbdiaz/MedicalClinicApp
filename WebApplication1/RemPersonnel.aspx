<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemPersonnel.aspx.cs" Inherits="WebApplication1.RemPersonnel" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />

    <title>Remove Personnel</title>
</head>
<body>
    <div class="navbar">
      <a href="AboutUs.aspx">About Us</a>
      <a href="ContactUs.aspx">Contact Us</a>
      <a href="PatientLogin.aspx">Patient Login</a>
      <a href="ProviderLogin.aspx">Provider Login</a>
      <a href="HomePage.aspx">Home</a>
    </div>
    <br />

    <br />
    <br />

    <form id="form1" runat="server">
        <div>
            <h1>Remove Personnel</h1>
            <p>Please fill out where indicated</p>

            <!-- Define the dropdown list to select personnel type -->
            <div class="select-type">
                <br />
                <label>Select a personnel type <span class="required">*</span></label>
                <label for="jobtype"></label>

                <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem>Doctor</asp:ListItem>
                    <asp:ListItem>Nurse</asp:ListItem>
                    <asp:ListItem>Staff</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
            </div>

            <div class="form-container">
                <div class="form-group">
                    <label for="perID"> Personnel ID <span class="required">*</span></label>
                    <asp:TextBox ID="perID" runat="server"></asp:TextBox>
                </div>
                <p style="font-weight: normal;"><span class="required">* </span>Required information</p>
                <div>
                    <asp:Button ID="clear" runat="server" OnClick="ButtonClear_Click" Text="Clear Entries"/>

                    <asp:Button ID="SUBMIT" runat="server" Text="SUBMIT" OnClick="SUBMIT_Click_RemPer"/>
                </div>
            </div>
            <div>
                <asp:Button ID="exit" runat="server" OnClick="ButtonExit_Click" Text="Return to Admin Page" />
            </div>
            <script>
//Client-side jQuery to cancel form submission if required fields empty
    $(function () {
                        // Attach event handler to form submit button click
                        $('#<%=SUBMIT.ClientID %>').on('click', function () {
                            // Check if required fields are filled out
                            if ($('#<%=perID.ClientID %>').val() === '') {
                                // Display dialog box
                                alert('Please fill out all required fields.');
                                return false; // Cancel form submission
                            }
                        });
    });
            </script>
        </div>
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
