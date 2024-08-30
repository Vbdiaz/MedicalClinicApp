<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="success.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SUCCESS</title>
    <style>     

        .required {
        color: red;
         }

        </style>
	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />

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
    <h1 style="font-size: 26px">Response Saved Successfully! An email confirmation was sent with your new patientID code. Use that to complete the account set up below. 
</h1>
    <h1 style="font-size: 26px">We will reach out to you shortly with confirmation of your appointment.
</h1>
    <br />
    <br />
    <form id="form1" runat="server">
    <div class="form-container">
        <fieldset>
            <legend>Sign Up for Your New Account</legend>
        <div>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:<span class="required">*</span></asp:Label>
                
            <p>
            <p>
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Username_valid" runat="server"
                    ControlToValidate="UserName"
                    ErrorMessage="Please enter a valid username." style="color:Red;">
                </asp:RequiredFieldValidator>
            </p>
        </div>
        <div>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:<span class="required">*</span></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="password_valid" runat="server"
                    ControlToValidate="Password"
                    ErrorMessage="Please enter a valid password." style="color:Red;">
                </asp:RequiredFieldValidator>
            </p>
        </div>
        <div>
            <p>
                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:<span class="required">*</span></asp:Label>
            </p>
            <div>
                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="cpv" runat="server"
                    ControlToValidate="ConfirmPassword"
                    ErrorMessage="Please enter a valid password." style="color:Red;">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="Password" 
                    ControlToValidate="ConfirmPassword" 
                    ErrorMessage="Passwords do not match." 
                    style="color:Red;" Font-Bold="True"></asp:CompareValidator>
            </div>
        </div>
        <div>
            <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:<span class="required">*</span></asp:Label>
                
            </p>
            <div>
                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="emailvalid" runat="server"
                    ControlToValidate="Email"
                    ErrorMessage="Please enter a valid email." style="color:Red;">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div>
            <p>
                <asp:Label ID="PatientLabel" runat="server" AssociatedControlID="PatientID">PatientID:<span class="required">*</span></asp:Label>
            
            </p>
            <p>
                <asp:TextBox ID="PatientID" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="patientvalid" runat="server"
                    ControlToValidate="PatientID"
                    ErrorMessage="Please enter a valid patientID." style="color:Red;">
                </asp:RequiredFieldValidator>

            </p>
        </div>
        <div>
            <p align="left" colspan="2" style="color:Red;">
                <asp:Literal ID="ErrorMessage" runat="server" Text="Passwords must match"></asp:Literal>
            </p>
        </div>
        <asp:Button ID="SUBMIT" runat="server" OnClick="Button1_Click" Text="Submit" />
    </fieldset>
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


