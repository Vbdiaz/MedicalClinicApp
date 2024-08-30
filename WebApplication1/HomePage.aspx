<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

  <meta charset="utf-8"/>
  <title>DR. UMA'S HEALTH CLINIC</title>
 <style>
      html, body {
          margin: 0;
          padding: 0;
      }
      body {
          background-color: white;
          font-family: 'Roboto', sans-serif;
          font-weight: 100;
          background-color: white;
          letter-spacing: .5px;
          background-image: url("https://plus.unsplash.com/premium_photo-1661740940614-e8cac9342154?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1632&q=80");
          background-size: cover;
          background-position: center;

          z-index:0;
          /*filter: blur(2px);
          -webkit-filter: blur(2px);*/
      }
      .body::before{
          backdrop-filter:blur(10px);
          -webkit-filter:blur(2px);
      }
      .container {
          margin: 0 auto;
          max-width: 1000px;
          text-align: center;
          margin-bottom:200px;
      }

      .header {
          position: relative;
          height:10%;
          text-align: center;
          color: black;
          font-family: monospace;
          font-size: 40px;
          z-index: 1;
        }

      .header::before {
            content: '';
            display: block;
            position: absolute;
            top: 0;
            left: 20%;
            width: 60%;
            height: 100%;
            padding:10px;
            padding-bottom:30px;
            z-index: -1;
            background-color:transparent;
            border-radius:20px;
            box-shadow:25px;
      }

      .btn {
          color: white;
          background-color:black;
          padding: 10px 40px;
          text-decoration: none;
          transition: background .5s;
          font-size: 25px;
          font-family: serif;
          border-radius: 15px;
          box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1);
          font-weight: bold;
          display:block;
          float:center;
          margin:20px auto;
          width:fit-content;
      }
              /* Navigation styles */
       .navbar {
        background-color: black;
        overflow: hidden;
                height: 48px;
            }
      .navbar a {
        float: right;
        color: white;
        text-align: center;
        padding: 14px 16px;
        text-decoration: none;
        font-size: 17px;
      }
            .navbar left {
        float: left;
        color: white;
        text-align: left;
        padding: 14px 16px;
        text-decoration: none;
        font-size: 17px;
      }
      .navbar a:hover {
        background-color: grey;
        color: white;
      }
      .btn:hover {
          background-color: gray;
          cursor: pointer;
          transition: background .5s;
      }
      .nav ul li:hover {
          background: #000;
          cursor: pointer;
          transition: background .5s;
      }
      .footer{
          position:relative;
          left:0;
          bottom:0;
          width:100%;
          background-color: transparent;
          color: black;
          text-align:left;
          font-size: 12px;
          padding: 10px 15px;
      }
      .contact {
  color: black;
}
    .location-box {
      background-color: #FFF9D9;
      color: #C8102E;
      padding: 1rem;
      width: 350px;
      position: absolute;
      bottom: 20%;
      right: 10%;
      border-radius: 15px;
      box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1);
    }
    </style>
</head>


<body>
  <form id="form1" runat="server">
      	<div class="navbar">
      <a href="AboutUs.aspx">About Us</a>
      <a href="ContactUs.aspx">Contact Us</a>
      <a href="PatientLogin.aspx">Patient Login</a>
      <a href="ProviderLogin.aspx">Provider Login</a>
      <a href="HomePage.aspx">Home</a>
      <left>Coog Medical Clinic</left>
    </div>
  <div class="header">
    <div class="container">
      <h1>Coog Clinic </h1>
      <p>Your Health Journey Starts Here!</p>
      <a class="btn" href="NewPatient.aspx">Click Here to Schedule Your Appointment Today</a>
        <a class ="btn" href="PatientLogin.aspx"> Click Here to Make a Payment</a>
    </div>
  </div>
</br>
 <div class="footer">
  <section class="contact">
    <p>Email: info@coogmedicalgroup.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>

      


</form>
</body>


</html>