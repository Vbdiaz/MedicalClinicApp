<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="WebApplication1.AdminView" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/navbar.css" rel="stylesheet" type="text/css"/>
    <title>Admin View</title>
    <style>
        body {
            background-image: url(https://img.freepik.com/free-photo/blur-hospital_1203-7972.jpg?w=740&t=st=1680371876~exp=1680372476~hmac=b046d30cd6532065742f9ff17312d9ab44b28023c50553b2ee18c66f64c1cb86);
            background-repeat: no-repeat;
            background-size: cover;
        }
        h1{
            font-family: 'Open Sans', sans-serif;
            margin: 50px 50px;
        }
        .custom-button {
            background-repeat: no-repeat;
            
            background-color: #f2f2f2;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
            border: 1px solid black;
            border-radius: 10px;

            background-position: center top;
            color: black;
            text-align: center;
            text-decoration: none;
            display: flex;
            font-size: 10px;
            font-weight: bold;
            flex-direction: column;
            justify-content: flex-end;
            align-items: center;
            font-size: 16px;
            margin: 20px 10px;
            cursor: pointer;
            width: 200px;
            height: 200px;
            border-radius: 10px;
        }
    
        /* On hover, darken the button */
        .custom-button:hover {
            background-color: darkgrey;
        }
        
    </style>
    </head>
<body>

    <!-- Define the navigation bar at the top of the page -->
    <div class="navbar">
	  <div class="navbar-right">
		<a href="HomePage.aspx" class="nav-item">Home</a>
		<a href="#" class="nav-item">About Us</a>
		<a href="#" class="nav-item">Contact Us</a>
		<a href="PatientLogin.aspx" class="nav-item">Patient Login</a>
		<a href="ProviderLogin.aspx" class="nav-item">Provider Login</a>
	  </div>
	</div>
    <br />
    <form id="form1" runat="server">
        <h1>Welcome, Administrator!</h1>

        <div style="display: flex; justify-content: center; align-items: center; height: 70vh;">
            <div style="display: flex; flex-direction: row;">
                <div>
                    <asp:Button ID="ButtonAP" runat="server" Text="Add Personnel" OnClick="ButtonAP_Click" CssClass="custom-button" style="background-size: 80% 80%; padding: 10px; background-image: url(https://cdn.icon-icons.com/icons2/1572/PNG/512/3592854-add-user-business-man-employee-general-human-member-office_107767.png); " />
                    <asp:Button ID="ButtonAO" runat="server" Text="Add Office" OnClick="ButtonAO_Click" CssClass="custom-button"  style="background-size: 80% 80%; padding: 10px; background-image: url(https://cdn-icons-png.flaticon.com/512/15/15735.png?w=740&t=st=1680571821~exp=1680572421~hmac=d4771d192ae9b4f18c929417ca097612ef912ad30f2fb9be3307b82fe6a636b0);"/>
                </div>

                <div>
                    <asp:Button ID="ButtonRP" runat="server" Text="Remove Personnel" OnClick="ButtonRP_Click" CssClass="custom-button" style="background-size: 80% 80%; padding: 10px; background-image: url(https://cdn.icon-icons.com/icons2/1572/PNG/512/3592826-employee-human-member-office-remove-remove-user-user_107791.png);"/>
                    <asp:Button ID="ButtonRO" runat="server" Text="Remove Office" OnClick="ButtonRO_Click" CssClass="custom-button"  style="background-size: 80% 80%; padding: 10px; background-image: url(https://cdn-icons-png.flaticon.com/512/15/15762.png?w=740&t=st=1680571689~exp=1680572289~hmac=b8c84a454135ecb86dec775625e8e383e30e91314cccf9814d2687b27cfc7821);"/>
                </div>
                <div>
                   <asp:Button ID="login" runat="server" Text="Create Login" OnClick="login_click" CssClass="custom-button"  style="background-size: 80% 80%; padding: 10px; background-position: center 20px; background-image: url(https://cdn.icon-icons.com/icons2/2248/PNG/512/login_icon_137429.png)"/>
                   <asp:Button ID="Report" runat="server" Text="Generate Report" OnClick="Report_Click" CssClass="custom-button"  style="background-size: 80% 80%; padding: 10px; background-position: center 10px; background-image: url(https://cdn-icons-png.flaticon.com/512/38/38817.png?w=740&t=st=1680915029~exp=1680915629~hmac=670e5463286b79d12c9af5e1f3f83f203df3d06b45a11f0abb303a52aa1a107d)"/>
                </div>
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