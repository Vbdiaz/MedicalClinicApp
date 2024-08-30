 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPatient.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
<title>NEW PATIENT FORM</title>
	<meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
	<style>


      

    </style>
	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <div class="navbar">
      <a href="AboutUs.aspx">About Us</a>
      <a href="ContactUs.aspx">Contact Us</a>
      <a href="PatientLogin.aspx">Patient Login</a>
      <a href="ProviderLogin.aspx">Provider Login</a>
      <a href="HomePage.aspx">Home</a>
    </div>
    <h1 style = "text-align: center">&nbsp;</h1>
    <h1 style = "text-align: center">Schedule your appointment today</h1>
	<p style = "text-align: center">Please fill out where indicated</p>

	<div class="form-container">
		<form id="form2" runat="server">
		<fieldset>
		<legend>Contact Information</legend>
			<div class="form-group">
				<label for="first-name">
                <br />
                First Name <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="fname" runat="server" ></asp:TextBox>
			</div>

			<div class="form-group">
				<label for="middle-initial">Middle Initial</label>&nbsp;
                <asp:TextBox ID="mi" runat="server"></asp:TextBox>
			</div>

			<div class="form-group">
				<label for="last-name">Last Name <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="lname" runat="server"></asp:TextBox>
				</div>

			<div class="form-group">
				<label for="date-of-birth">Date of Birth <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="dob" runat="server" TextMode="Date" DataFormatString="{yyyy/MM/dd}"></asp:TextBox>
			</div>
           <div class="form-group">
				<label for="officelabel">
                <br />
                Gender <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="gender_list" runat="server" >
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>

			</div>
            <div class="form-group">
                <label for="phone">Phone Number <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="phone_num" runat="server" placeholder="1234567890" MaxLength="10" />
                <span class="required"><asp:RegularExpressionValidator ID="phoneValidator" runat="server" ControlToValidate="phone_num"
                    ValidationExpression="^\d{10}$" ErrorMessage="Please enter a valid phone number" /></span>
            </div>



			<div class="form-group">
				<label for="email">Email <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="email" runat="server" placeholder="you@example.com"></asp:TextBox>
                <span class="required"><asp:RegularExpressionValidator ID="emailValidator" runat="server" 
                ControlToValidate="email" ErrorMessage="Please enter a valid email address." 
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" /></span>
			</div>

			<div class="form-group"> 
				<label for="address"> Address <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="address" runat="server"></asp:TextBox>
                <br />
                <br />
			</div>

			<div class="form-group">
				<label for="Emergency-Contact">Emergency Contact <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="emergency_contact" runat="server"></asp:TextBox>
			</div>
			<div class="form-group">
				<label for="ECRelation">Emergency Contact Relation <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="ECRelation" runat="server"></asp:TextBox>
			</div>
            <div class="form-group">
                <label for="ECphone">Emergency Contact Phone Number <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="ECphone" runat="server" placeholder="1234567890" MaxLength="10" />
               <span class="required"> <asp:RegularExpressionValidator ID="ecphonevalidator" runat="server" ControlToValidate="phone_num"
                    ValidationExpression="^\d{10}$" ErrorMessage="Please enter a valid phone number" /></span>
            </div>

			<div class="form-group">
				<label for="ECemail">Emergency Contact Email <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="ECemail" runat="server" placeholder="you@example.com"></asp:TextBox>
                <span class="required"><asp:RegularExpressionValidator ID="ecemailvalidator" runat="server" 
                ControlToValidate="ECemail" ErrorMessage="Please enter a valid email address." 
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" /></span>
			</div>
			</fieldset>
 
            <br />
            <br />
			<fieldset>
			<legend>Appointment Details</legend>
                <br />
            <div class="form-group">
				<label for="date-requested">Date Requested <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="Date" runat="server" TextMode="Date" DataFormatString="yyyy-MM-dd" AutoPostBack="True" OnTextChanged="Date_TextChanged"></asp:TextBox>
			
			   <span class="required"> <asp:Literal ID="ErrorMessage_date2" EnableViewState="False"  runat="server"></asp:Literal></span>
			
			</div>

            <div class="form-group">
				<label for="officelabel">
                <br />
                Select a primary office location <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server" Width="140px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
			    <br />
                <br />
			</div>

            

            <div class="form-group">
				<label for="Pcplabel">
                <br />
                Select a Primary Care Physician <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="primary" runat="server" Width="140px" AutoPostBack="True" ></asp:DropDownList>
			    <br />
                <br />
			</div>

            

			<div class="form-group">
				<label for="time-requested">
                <br />
                Time Requested <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="ddlTimeRequested" runat="server" Width="140px" AutoPostBack="True"></asp:DropDownList>
			    <span class="required"><asp:Literal ID="ErrorMessage_date" runat="server" ></asp:Literal></span>
			    <br />
			</div>
                <br />
                <br />


                            <div class="form-group">
				<label for="reason">Reason for your visit:</label>
                                <asp:TextBox runat="server" TextMode="MultiLine"></asp:TextBox>
			</div>
			<br />
                <br />

			<div class="form-group">
				<label for="insurance">Do you have insurance?<span class="required">*</span></label>
                <asp:CheckBox ID="ins" runat="server" />
                <label for="insurance">Yes
                </label>
                <asp:CheckBox ID="no_ins" runat="server" />
                <label for="insurance">No<br />
                </label>
			</div>
<br />


			<div class="form-group">
				<label for="insurance-name">Insurance Name</label>
				<asp:Textbox id="insurance_name" runat="server"></asp:Textbox>

				<label for="insurance-type">Insurance Group No.</label>
				<asp:Textbox id="insurance_group" runat="server"></asp:Textbox>
			</div>
                <br />
            <div class="form-group">
				<label for="pharmacy-name">Preferred Pharmacy</label><span class="required">*</span>
				<asp:Textbox id="pharmacy_name" runat="server"></asp:Textbox>

				<label for="pharmacy address">Pharmacy Address</label><span class="required">*</span>
				<asp:Textbox id="pharmacy_address" runat="server"></asp:Textbox>
			</div>
			<p><span class="required">*</span> Required information</p>
            <p>
                <asp:Button ID="SUBMIT" runat="server" Text="SUBMIT" OnClick="SUBMIT_Click" />
            </p>
			</fieldset>
			&nbsp;</form>
	</div>
            <script>
            //Client-side jQuery to cancel form submission if required fields empty 
                $(function () {
                    // Attach event handler to form submit button click
                    $('#<%=SUBMIT.ClientID %>').on('click', function () {
                        // Check if required fields are filled out
                        if ($('#<%=fname.ClientID %>').val() === '' || $('#<%=lname.ClientID %>').val() === '' || $('#<%=email.ClientID %>').val() === ''
                            || $('#<%=phone_num.ClientID %>').val() === '' || $('#<%=address.ClientID %>').val() === '' || $('#<%=DropDownList1.ClientID %>').val() === ""
                            || $('#<%=ddlTimeRequested.ClientID %>').val() === "" || $('#<%=primary.ClientID %>').val() === "" || $('#<%=Date.ClientID %>').val() === "" || $('#<%=gender_list.ClientID %>').val() === "" || $('#<%=pharmacy_name.ClientID %>').val() === "" || $('#<%=pharmacy_address.ClientID %>').val() === ""){
                            // Display dialog box
                            alert('Please fill out all required fields.');
                            return false; // Cancel form submission
                        }
                    });
                });
            </script>
    <div class="footer">
  <section class="contact">
    <p>Email: info@coogmedicalgroup.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>
</body>
</html>
