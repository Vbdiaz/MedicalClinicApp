<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatBilling.aspx.cs" Inherits="WebApplication1.PatBilling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>

        .bill-summary {
          font-size: 24px;
          font-weight: bold;
          margin-bottom: 10px;
        }
        .bill-title {
          font-size: 32px;
          font-weight: bold;
          margin-bottom: 10px;
        }

        .bill-item {
          margin-bottom: 20px;
        }
        .bill-total{
          font-size: 28px;
        }
        #btnPayNow, #btnPrintSummary{
            background-color: cornflowerblue;
            color: white;
            padding: 14px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
          }

        #btnPayNow:hover, #btnPrintSummary:hover{
            background-color: dodgerblue;
        }



    </style>
	<link href="Content/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Content/AdminPages.css" rel="stylesheet" type="text/css" />

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

    <asp:Panel ID="pnlBillSummary" runat="server" CssClass="bill-summary">
        <div>
            <span class="bill-title">Current Bill&nbsp; |&nbsp; Due on <asp:Label ID="lblDueDate" runat="server"></asp:Label>
            </span>
            <hr style="color: #000000" />

        </div>
        <div>
            <span class="bill-item">Total past due:</span> <asp:Label ID="lblPastDue" runat="server" Text="$0.00"></asp:Label>
            <br />
        </div>
        <hr style="color: #000000" />

        <div>
            <span class="bill-item">Bill amount:</span> <asp:Label ID="lblBillAmount" runat="server" Text="$0.00"></asp:Label>
            <br />
        </div>
        <hr style="color: #000000" />

        <div>
            <span class="bill-item">Insurance Adjustments:</span> <asp:Label ID="lblInsuranceAdjustments" runat="server" Text="-$0.00"></asp:Label>
            <br />
        </div>
        <hr style="color: #000000" />

        <div>
            <span class="bill-item">Co-Pays:</span> <asp:Label ID="lblpayments" runat="server" Text="-$0.00"></asp:Label>
            <br />
        </div>
        <hr style="color: #000000" />

        <div>
            <span class="bill-item">Payments:</span> <asp:Label ID="lblmanpay" runat="server" Text="-$0.00"></asp:Label>
            <br />
        </div>
        <hr style="color: #000000" />

        <div>
            <span class="bill-total" style ="color:darkred">Total due:</span> <asp:Label ID="lblTotalDue" runat="server" Text="$0.00" style= "color:darkred"></asp:Label>
            <br />
            <br />
        </div>
        <div>
            <asp:Button ID="btnPayNow" runat="server" Text="Pay now" CssClass="pay-now-btn" OnClick="btnPayNow_Click" Width="133px" />
            <asp:Button ID="btnPrintSummary" runat="server" Text="Print Bill Summary" CssClass="print-btn" OnClick="btnPrintSummary_Click" Width="168px" />
            <br />
            <br />
        </div>
    </asp:Panel>

        <h1>Invoice Summary</h1>
    <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" DataKeyNames= "reportID" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" style="margin-top: 0px">
        <Columns>
            <asp:BoundField DataField="invoiceID" HeaderText="Invoice ID" />
            <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:c}" />
            <asp:BoundField DataField="claim" HeaderText="Claim Amount" DataFormatString="{0:c}" />
            <asp:BoundField DataField="paid_amount" HeaderText="Paid Amount" DataFormatString="{0:c}" />
            <asp:BoundField DataField="due_date" HeaderText="Due Date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="reportID" HeaderText="Report ID" Visible="false" />
            <asp:ButtonField ButtonType ="button" HeaderText="Report" Text="VIEW" CommandName="viewReport" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>


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
