<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatEdit.aspx.cs" Inherits="WebApplication1.PatAppEditaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
<title></title>
	<meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
	<link href="Content/AdminPages.css" rel="stylesheet" type ="text/css"/>
	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <style>
        .my-textbox {
            background-color: #999999;
            color: black;
        }

    </style>
</head>
<body>
       <form id="form2" runat="server">

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

     <h1 style = "text-align: center">Edit your Personal Information</h1>
            <p style = "text-align: center">Please fill out where indicated</p>

	<div class="form-container" style="margin: auto;">

		<fieldset >
		<legend>Contact Information</legend>
			<div class="form-group">
				<label for="first-name">
                <br />
                First Name <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="fname" runat="server" ></asp:TextBox>
			</div>

			<div class="form-group">
				<label for="middle-initial">Middle Initial</label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="mi" runat="server"></asp:TextBox>
			</div>

			<div class="form-group">
				<label for="last-name">Last Name <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="lname" runat="server"></asp:TextBox>
				</div>

            <div class="form-group">
                <label for="phone">Phone Number <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="phone_num" runat="server" placeholder="1234567890" MaxLength="10" />
                <asp:RegularExpressionValidator ID="phoneValidator" runat="server" ControlToValidate="phone_num"
                    ValidationExpression="^\d{10}$" ErrorMessage="Please enter a valid phone number" />
            </div>



			<div class="form-group">
				<label for="email">Email <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="email" runat="server" placeholder="you@example.com"></asp:TextBox>
                <asp:RegularExpressionValidator ID="emailValidator" runat="server" 
                ControlToValidate="email" ErrorMessage="Please enter a valid email address." 
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" />
			</div>

			<div class="form-group"> 
				<label for="address"> Address <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="address" runat="server"></asp:TextBox>
                <br />
                <br />
			</div>

			<div class="form-group">
				<label for="Emergency-Contact">Emergency Contact <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="emergency_contact" runat="server"></asp:TextBox>
			</div>
			<div class="form-group">
				<label for="ECRelation">Emergency Contact Relation <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="ECRelation" runat="server"></asp:TextBox>
			</div>
            <div class="form-group">
                <label for="ECphone">Emergency Contact Phone Number <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="True" ID="ECphone" runat="server" placeholder="1234567890" MaxLength="10" />
                <asp:RegularExpressionValidator ID="ecphonevalidator" runat="server" ControlToValidate="phone_num"
                    ValidationExpression="^\d{10}$" ErrorMessage="Please enter a valid phone number" />
            </div>

			<div class="form-group">
				<label for="ECemail">Emergency Contact Email <span class="required">*</span></label>&nbsp;
                <asp:TextBox CssClass="my-textbox" ReadOnly ="true" ID="ECemail" runat="server" placeholder="you@example.com"></asp:TextBox>
                <asp:RegularExpressionValidator ID="ecemailvalidator" runat="server" 
                ControlToValidate="ECemail" ErrorMessage="Please enter a valid email address." 
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" />
			</div>
            <asp:Button ID="SUBMIT" runat="server" Text="SAVE" OnClick="SUBMIT_Click" />
            <asp:Button ID="EDIT" style="margin-right:10px" runat="server" Text="EDIT" OnClick="EDIT_CLICK" />

			</fieldset>

            <script>
                //Client-side jQuery to cancel form submission if required fields empty 
                $(function () {
                    // Attach event handler to form submit button click
                    $('#<%=SUBMIT.ClientID %>').on('click', function () {
                        // Check if required fields are filled out
                        if ($('#<%=fname.ClientID %>').val() === '' || $('#<%=lname.ClientID %>').val() === '' || $('#<%=email.ClientID %>').val() === ''
                            || $('#<%=phone_num.ClientID %>').val() === '' || $('#<%=address.ClientID %>').val() === '') {
                            // Display dialog box
                            alert('Please fill out all required fields.');
                            return false; // Cancel form submission
                        }
                    });
                });
            </script>
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
