<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="WebApplication1.ContactUs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Contact Us</title>

    <style type="text/css">
    	.location-item {
            margin-bottom: 1rem;
            padding-bottom: 1rem;
            border-bottom: 1px solid #ccc;
        }

		.container {
			display: flex;
			align-items: center;
			justify-content: center;
			text-align: center;
		}
		h1 {
			font-size: 2rem;
			margin-bottom: 1rem;
		}

		p {
			font-size: 1.2rem;
			margin-bottom: 1rem;
		}

		.contact-info {
			max-width: 800px;
			margin: 0 auto;
			text-align: center;
		}

      .location {
  margin-bottom: 20px;
}
      ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
}

	  
    </style>
    		<link href="Content/navbar.css" rel="stylesheet" type="text/css"/>
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />
</head>
<body >
<form id="form1" runat="server">
    <div class="navbar">
	  <div class="navbar-right">
		<a href="HomePage.aspx" class="nav-item">Home</a>
		<a href="AboutUs.aspx" class="nav-item">About Us</a>
		<a href="ContactUs.aspx" class="nav-item">Contact Us</a>
		<a href="PatientLogin.aspx" class="nav-item">Patient Login</a>
		<a href="ProviderLogin.aspx" class="nav-item">Provider Login</a>
	  </div>
	</div>
    <div class="container">
                <div class="contact-info">
                    <h1>Contact Us</h1>
                    <p>Please select a state below for contact information:</p>
                    <asp:DropDownList ID="stateDropdown" runat="server" OnSelectedIndexChanged="stateDropdown_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Select a state" Value=""></asp:ListItem>
                        <asp:ListItem Text="Texas" Value="TX"></asp:ListItem>
                        <asp:ListItem Text="Florida" Value="FL"></asp:ListItem>
                    </asp:DropDownList>
	                <asp:Literal ID="locationInfoDiv" runat="server"></asp:Literal>
                    <p>If you are experiencing a medical emergency, please call 911 or go to the nearest emergency room.</p>
                </div>
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