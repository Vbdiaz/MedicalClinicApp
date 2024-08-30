<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPersonnel.aspx.cs" Inherits="WebApplication1.NewPersonnel" %>
<!-- Set the page language to C#, and specify the code-behind file -->

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>
<!-- Register the asp prefix and define the namespace and assembly to be used -->

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Include the jQuery library -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <!-- Include the navigation bar and personnel stylesheets -->
    <link href="Content/navbar.css" rel="stylesheet" type="text/css"/>
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css"/>

    <style>
    </style>

    <!-- Set the page title -->
    <title>Add Personnel</title>
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

    <!-- Define the form to add a new personnel -->
    <form id="form1" runat="server">
        <div>
            <h1>Add New Personnel</h1>
            <p>Please fill out where indicated</p>

            <!-- Define the dropdown list to select personnel type -->
            <div class="select-type">
                <br />
                <label>Select a personnel type <span class="required">*</span></label>
                <label for="jobtype"></label>

                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>Doctor</asp:ListItem>
                    <asp:ListItem>Nurse</asp:ListItem>
                    <asp:ListItem>Staff</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
            </div>

            <!-- Define the fieldset to enter contact information -->
            <div class="form-container">

                    <!-- Define the textbox to enter first name -->
                    <div class="form-group">
                        <label for="first-name">First Name <span class="required">*</span></label>
                        <asp:TextBox ID="fname" runat="server"></asp:TextBox>
                    </div>

                    <!-- Define the textbox to enter middle initial -->
                    <div class="form-group">
                        <label for="middle-initial">Middle Initial </label>
                        <asp:TextBox ID="mi" runat="server"></asp:TextBox>
                    </div>

                    <!-- Define the textbox to enter last name -->
                    <div class="form-group">
                        <label for="last-name">Last Name <span class="required">*</span></label>
                        <asp:TextBox ID="lname" runat="server"></asp:TextBox>
                    </div>

                    <!-- Define the textbox to enter date of birth -->
                    <div class="form-group">
                        <label for="date-of-birth">Date of Birth <span class="required">*</span></label>
                        <asp:TextBox ID="dob" runat="server" TextMode="Date" DataFormatString="{yyyy/MM/dd}"></asp:TextBox>
                    </div>

                    <!-- Create a form field for Phone Number -->
                    <div class="form-group">
                        <label for="phone">Phone Number <span class="required">*</span></label>
                        <asp:TextBox ID="phone_num" runat="server" placeholder="1234567890" MaxLength="10" />
                        <asp:RegularExpressionValidator ID="phoneValidator" runat="server" ControlToValidate="phone_num" ValidationExpression="^\d{10}$" ErrorMessage="Please enter a valid phone number" style="color: red; font-weight: normal;" />
                    </div>

                    <!-- Create a form field for Email -->
                    <div class="form-group">
                        <label for="email">Email <span class="required">*</span></label>
                        <asp:TextBox ID="email" runat="server" placeholder="you@example.com"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="emailValidator" runat="server" ControlToValidate="email" ErrorMessage="Please enter a valid email address." ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" style="color: red; font-weight: normal;"/>
                    </div>

                    <!-- Create a form field for Specialty (if adding a doctor) -->
                    <div class="form-group">
                        <label for="specialty">(If adding a doctor.) Specialty </label>
                        <asp:TextBox ID="specialty" runat="server"></asp:TextBox>
                    </div>

                    <!-- Create a form field for Role (if adding staff) -->
                    <div class="form-group">
                        <label for="role">(If adding staff.) Role </label>
                        <asp:TextBox ID="role" runat="server"></asp:TextBox>
                    </div>

                    <!-- Create a form field for Office ID (if adding staff or nurse) -->
                    <div class="form-group">
                        <label for="office">(If adding staff or nurse.) Office ID </label>
                        <asp:TextBox ID="office" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        Salary<label for="email"><span class="required">*</span></label>
                        <asp:TextBox ID="Salary" runat="server"></asp:TextBox>
                    </div>
                    <p style="font-weight: normal;"><span class="required">* </span>Required information</p>
                    <!-- This is a button to clear the form fields -->
                    <asp:Button ID="clear" runat="server" OnClick="ButtonClear_Click" Text="Clear Entries"/>
                    
                    <!-- This is the submit button for the form -->
                    <asp:Button ID="SUBMIT" runat="server" Text="SUBMIT" OnClick="SUBMIT_Click_addPer"/>
            </div>
            <div>
                <!-- This is a button to return to the admin page -->
                <asp:Button ID="exit" runat="server" OnClick="ButtonExit_Click" Text="Return to Admin Page"/>
            </div>


            <script>
                // This is a jQuery script that attaches an event handler to the submit button
                //Client-side jQuery to cancel form submission if required fields empty
                $(function () {
                    // Attach event handler to form submit button click
                    $('#<%=SUBMIT.ClientID %>').on('click', function () {
                        // Check if required fields are filled out
                        if ($('#<%=fname.ClientID %>').val() === '' || $('#<%=lname.ClientID %>').val() === '' || $('#<%=email.ClientID %>').val() === ''
                            || $('#<%=phone_num.ClientID %>').val() === '') {
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