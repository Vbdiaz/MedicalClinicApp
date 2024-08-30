<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientLogin.aspx.cs" Inherits="WebApplication1.WebForm5" %>

<!-- Define the HTML document type -->
<!DOCTYPE html>

<!-- Define the HTML document -->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient-Login</title>

    <link href="Content/LoginStyle.css" rel="stylesheet" type="text/css" />
	<link href="Content/navbar.css" rel="stylesheet" type="text/css"/>
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
	<form id="form1" runat="server">
	<div class="login-container">
		<table class="login-table">
			<!-- Table Row for Sign In Title -->
			<tr>
				<td align="center" colspan="2" style="font-weight: bold;">Patient Login</td>
			</tr>
			<!-- Table Row for Username -->
			<tr>
				<td class="auto-style1">
					<div class="input-container">
						<asp:TextBox ID="UserName" runat="server" placeholder="Username"></asp:TextBox>
					</div>
				</td>
			</tr>
			<!-- Table Row for Password -->
			<tr>
				<td class="auto-style1">
					<div class="input-container">
						<asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
					</div>
				</td>
			</tr>
			<!-- Table Row for Error Message -->
			<tr>
				<td class="error-message">
					<asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
				</td>
			</tr>

			<!-- Button for Submitting Form -->
			<tr>
				<div class="auto-style1">
					<td class="submit-button">
						<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit"/>
					</td>
				</div>
			</tr>
			<!-- Link to Register -->
			<tr>
				<td class="auto-style1">
					<div style="padding: 10px">
						Are you a new patient? <a href="NewPatient.aspx">Register here.</a>
					</div>
				</td>
			</tr>
		</table>
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