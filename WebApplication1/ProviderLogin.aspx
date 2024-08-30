<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderLogin.aspx.cs" Inherits="WebApplication1.ProviderLoginLanding" %>

<!DOCTYPE html>
<html>
<head>
	<title>Provider Login</title>
	<!-- Add background image and styles -->
	<style>
		body {
			background-image: url(https://img.freepik.com/free-photo/blur-hospital_1203-7972.jpg?w=740&t=st=1680371876~exp=1680372476~hmac=b046d30cd6532065742f9ff17312d9ab44b28023c50553b2ee18c66f64c1cb86);
			background-repeat: no-repeat;
			background-size: cover;
		}

		.container {
			display: flex;
			justify-content: center;
			align-items: center;
			height: 100vh;
		}

		.square {
			display: flex;
			flex-direction: column;
			justify-content: center;
			align-items: center;
			position: relative;
			width: 478px;
			height: 257px;
			overflow: hidden;
			margin: 20px;
		}

		.square img {
			width: 100%;
			height: 100%;
			object-fit: cover;
			transition: transform 0.5s ease;
			/* Align image to the right and adjust margin */
			margin-left: 0px;
			margin-right: 13px;
			margin-top: 1px;
		}

		.square:hover img {
			transform: scale(1.1);
		}

		.overlay {
			position: absolute;
			top: 50%;
			left: 50%;
			transform: translate(-50%, -50%);
			background-color: rgba(0, 0, 0, 0.5);
			color: white;
			font-size: 24px;
			padding: 20px;
			text-align: center;
			transition: opacity 0.5s ease;
			opacity: 0;
			height: 225px;
			width: 572px;
			font-family: 'Open Sans', sans-serif;
			display: flex;
			justify-content: center;
			align-items: center;
		}

		.square:hover .overlay {
			opacity: 1;
		}
	</style>
	<!-- Add navigation bar styles -->
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
	<!-- Add container to center the squares and images -->
	<div class="container">
		<!-- Add links to nurse and doctor login pages -->
		<a href="NurseLogin.aspx">
			<div class="square">
				<img src="https://imageio.forbes.com/specials-images/imageserve/619ea056b05de2285c025b70/Any-updates-on-this-patient-/960x0.jpg?format=jpg&width=960" alt="Nurse">
            		<div class="overlay">
						<h2>NURSE LOGIN</h2>
					</div>
				</div>
			</a>
            <a href="DoctorLogin.aspx">
				<div class="square">
					        <img src="https://health.gov/sites/default/files/styles/topic_card_image/public/2020-01/regular-checkups_banner.jpg?itok=2HIjcA3h" alt="Doctor">
					<div class="overlay">
						<h2>DOCTOR LOGIN</h2>
					</div>
				</div>
			</a>
		</div>
	 <div class="footer">
  <section class="contact">
    <p>Email: info@coogmedicalgroup.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>
</body>
</html>
