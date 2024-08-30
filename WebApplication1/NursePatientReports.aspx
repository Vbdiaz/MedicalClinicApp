<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NursePatientReports.aspx.cs" Inherits="WebApplication1.NursePatientReports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/navbar.css" rel="stylesheet" type="text/css"/>
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />
    <style>
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
            width: 250px;
            height: 250px;
            border-radius: 10px;
        }
    
        /* On hover, darken the button */
        .custom-button:hover {
            background-color: darkgrey;
        }
    </style>
    <title>Admin Reports</title>
</head>
<body>
    <!-- Define the navigation bar at the top of the page -->
<form id="form1" runat="server">
    <div class="navbar">
      <a href="AboutUs.aspx">About Us</a>
      <a href="ContactUs.aspx">Contact Us</a>
      <a href="PatientLogin.aspx">Patient Login</a>
      <a href="ProviderLogin.aspx">Provider Login</a>
      <a href="HomePage.aspx">Home</a>
      <left><asp:LinkButton id="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton></left>
    </div>
    <br />
    <br />
    <h1 style="text-align: center">View Patient Reports</h1>
    <br />
    <br />

        <div style="display: flex; justify-content: center; align-items: center; height: 70vh;">
            <div style="display: flex; flex-direction: row;">
                <div>
                    <asp:Button ID="Report1" runat="server" Text="Patient Vitals History" OnClick="Report1_Click" CssClass="custom-button" style="background-size: 70% 70%; padding: 10px; background-position: center 20px; background-image: url(https://cdn.icon-icons.com/icons2/803/PNG/96/Doctor_Medical_Instrument_Stethoscope_icon-icons.com_65902.png); " />
                </div>

                <div>
                    <asp:Button ID="Report2" runat="server" Text="Patient Prescription History" OnClick="Report2_Click" CssClass="custom-button" style="background-size: 70% 70%; padding: 10px; background-position: center 20px; background-image: url(https://cdn.icon-icons.com/icons2/3299/PNG/96/prescription_document_x_ray_icon_208875.png);"/>
                </div>
            </div>
        </div>

        <br />
        <br />



            <!-- Adding scripts to use bootstrap -->
            <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"
                    integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
                    crossorigin="anonymous">
            </script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"
                    integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q"
                    crossorigin="anonymous">
            </script>
            <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
                    integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
                    crossorigin="anonymous">
            </script>

    </form>
    <div class="footer">
  <section class="contact">
    <p>Email: info@umamedicalgroup.com | Phone: (713)867-5309</p>
    <p>Dr. Uma's Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>
</body>
</html>
