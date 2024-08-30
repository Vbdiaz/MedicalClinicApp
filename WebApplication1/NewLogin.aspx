<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewLogin.aspx.cs" Inherits="WebApplication1.NewLogin" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css"/>
    <link href="Content/navbar.css" rel="stylesheet" type="text/css" />


    <title>Add Logn</title>
</head>
<body>
    <!-- Define the navigation bar at the top of the page -->
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
            <div style="text-align:center">
                <h1>Add New Login</h1>
                <p>Please fill out where indicated</p>
               <div class="form-group" style="text-align:center">
                            <br />
                           <label style="text-align:center"> Select a personnel type <span class="required">*</span></label>&nbsp;
                            <label for="jobtype">Choose Personnel Type:</label>&nbsp;
                            <asp:DropDownList ID="DropDownList1" runat="server" style ="text-align:center">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Doctor</asp:ListItem>
                                <asp:ListItem>Nurse</asp:ListItem>
                                <asp:ListItem>Admin</asp:ListItem>
                                <asp:ListItem>Patient</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                        </div>
                        <div class="form-container">
                                <fieldset>
                                    <legend>Login Information</legend>

                                    <div class="form-group">
                                        <label for="username">Username <span class="required">*</span></label>&nbsp;
                                        <asp:TextBox ID="username" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="password">Password<span class="required">*</span></label>&nbsp;
                                        <asp:TextBox ID="password" runat="server"></asp:TextBox>
                                    </div>
                                  <div class="form-group">
                                        <label for="id">ID<span class="required">*</span></label>&nbsp;
                                        <asp:TextBox ID="id" runat="server"></asp:TextBox>
                                    </div>
                                    </fieldset>

                        <p style="font-weight: normal;"><span class="required">*</span> Required information</p>
                        <asp:Button ID="clear" runat="server" OnClick="ButtonClear_Click" Text="Clear Entries"/>
                        <asp:Button ID="SUBMIT" runat="server" Text="SUBMIT" OnClick="SUBMIT_Click_AddPer" />
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
                            if ($('#<%=username.ClientID %>').val() === '' || $('#<%=password.ClientID %>').val() === '') {
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
