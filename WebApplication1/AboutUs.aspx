<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="WebApplication1.AboutUs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About Us</title>

     <style type="text/css">

		.btn {
			display: inline-block;
			padding: 10px 200px; 
			background-color: #117bff;
			color: white;
			text-decoration: none;
			border-radius: 4px;
			font-size: 18px;
			margin-bottom: 20px;
			margin-right: 10px;
		}
		.btn-container {
			display: flex;
			justify-content: center;
		}

		.btn:hover {
			background-color: #3e8e41;
		}
        body {
			font-family: 'Open Sans', sans-serif;
			background-color: #f5f5f5;
		}
		.container {
			margin: 0 auto;
			padding: 20px;			
		}
		h1 {
			font-size: 36px;
			text-align: center;
			margin-top: 0;
		}
		h2 {
			font-size: 24px;
			margin-top: 20px;
		}
		p {
			font-size: 16px;
			line-height: 1.5;
			margin-bottom: 20px;
		}
		ul {
			margin-left: 20px;
			margin-bottom: 20px;
		}
		li {
			margin-bottom: 10px;
		}
	  
    </style>
		<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar">
	  <div class="navbar-right">
		<a href="HomePage.aspx" class="nav-item">Home</a>
		<a href="#" class="nav-item">About Us</a>
		<a href="#" class="nav-item">Contact Us</a>
		<a href="PatientLogin.aspx" class="nav-item">Patient Login</a>
		<a href="ProviderLogin.aspx" class="nav-item">Provider Login</a>
	  </div>
	</div>
        <div class="container">
		<h1>&nbsp;</h1>
            <h1>About Us</h1>
		<p>Our medical clinic is dedicated to providing quality healthcare to our patients in multiple locations across the country. With a team of skilled doctors and healthcare professionals, we offer a wide range of services to help you and your family stay healthy.</p>
		<h2>Our Doctors</h2>
		<p>Our team of doctors includes specialists in a variety of fields, including:</p>
		<asp:Literal ID="specialtiesList" runat="server" />

		<p>Each doctor in our clinic is highly trained and experienced in their respective field. They work closely with our patients to provide personalized care and treatment plans.</p>
		<h2>Our Patients</h2>
		<p>At our clinic, we believe that every patient deserves the best possible care. That's why we work hard to create a welcoming and supportive environment for all of our patients. Whether you're here for a routine check-up or a more complex medical issue, we are here to help you.</p>
		<h2>Our Services</h2>
		<p>We offer a wide range of services to help you and your family stay healthy, including:</p>
		<ul>
			<li>Primary care</li>
			<li>Specialty care</li>
			<li>Diagnostic tests</li>
			<li>Imaging services</li>
			<li>Lab tests</li>
			<li>Vaccinations</li>
			<li>Wellness exams</li>
		</ul>
		<p>Our healthcare professionals are dedicated to providing high-quality care to each and every patient we see. We believe that prevention is key to maintaining good health, and we encourage our patients to take an active role in their own healthcare.</p>
		<h2>Appointments</h2>
		<p>To schedule an appointment, simply call the office closest to you, or use our online appointment booking system:</p>
		<div class="btn-container">
			<a href="ContactUs.aspx" class="btn">Contact Us</a> <a href="PatientLogin.aspx" class="btn">Book Appointment</a>
		</div>
        </div>
        <div>
        </div>
    </form>
 <div class="footer">
  <section class="contact">
    <p>Email: info@coogmedicalgroup.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>
</div>
</body>
</html>

