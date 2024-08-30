<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPrescription.aspx.cs" Inherits="WebApplication1.OrderPrescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>NEW PRESCRIPTION</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script> 
    	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />>
</head>
<body>
       <form id="form1" runat="server">
    <div class="navbar">
      <a href="AboutUs.aspx">About Us</a>
      <a href="ContactUs.aspx">Contact Us</a>
      <a href="PatientLogin.aspx">Patient Login</a>
      <a href="ProviderLogin.aspx">Provider Login</a>
      <a href="HomePage.aspx">Home</a>
      <left><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton></left>
    </div>


     <h1 style ="font-size: 32px; width: 907px;" runat="server">
            &nbsp;</h1>


    <h1 style = "text-align: center">Order New Prescription</h1>
	<p style = "text-align: center">Please fill out where indicated</p>

		<div class="form-container">
			<fieldset>
			<legend>Prescription Details</legend>
            <div class="form-group">
				<label for="Pat">
                Patient Name </label>
                <asp:TextBox ReadOnly="true" ID="patientName" runat="server"></asp:TextBox>
			
			    <br />
                <br />
			</div>
                 <div class="form-group">
				<label for="Pat">
                Drug Class: </label>
                <asp:TextBox ID="drugclass" runat="server"></asp:TextBox>
			
			    <br />
                <br />
			</div>
            <div class="form-group">
                Drug Name: <span class="required"> * </span> <asp:TextBox ID="drugname" runat="server"></asp:TextBox>

			    <br />
                <br />
			</div>
            <div class="form-group">
                NDC: <span class="required"> * </span> <asp:TextBox ID="NDC" runat="server"></asp:TextBox>

			    <br />
                <br />
			</div>
         
			<div class="form-group">
				<label for="dosage">Dosage: <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="dosage" runat="server" ></asp:TextBox>
			
			
			<br />
            <br />
            </div>
            <div class="form-group">
                Refills: <span class="required"> * </span> <asp:TextBox ID="refills" runat="server"></asp:TextBox>

			    <br />
                <br />
			</div>

          <div class="form-group">
                Instructive Notes: <span class="required"> * </span> <asp:TextBox ID="notes" TextMode="MultiLine" runat="server"></asp:TextBox>

			    <br />
                <br />
			</div>
         
			<p><span class="required">*</span> Required information</p>
            <p>
                <asp:Button ID="SUBMIT" runat="server" Text="SUBMIT" OnClick="SUBMIT_Click" />
            </p>
                <p>
                    &nbsp;</p>
	</fieldset>
    
     </div>    
     

            <script>
                //Client-side jQuery to cancel form submission if required fields empty 
                $(function () {
                    // Attach event handler to form submit button click
                    $('#<%=SUBMIT.ClientID %>').on('click', function () {
                        // Check if required fields are filled out
                        if ($('#<%=NDC.ClientID %>').val() === "" || $('#<%=dosage.ClientID %>').val() === "" || $('#<%=refills.ClientID %>').val() === "" || $('#<%=drugname.ClientID %>').val() === "") {
                            // Display dialog box
                            alert('Please fill out all required fields.');
                            return false; // Cancel form submission
                        }
                    });
                });
            </script>
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
