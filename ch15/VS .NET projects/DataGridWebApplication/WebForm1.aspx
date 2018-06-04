<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="DataGridWebApplication.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id=DataGrid1 style="Z-INDEX: 101; LEFT: 11px; POSITION: absolute; TOP: 11px" runat="server" AutoGenerateColumns="False" BorderColor="Blue" Font-Bold="True" Font-Names="Arial" ForeColor="Black" BackColor="White" AllowPaging="True" PageSize="5" ShowFooter="True" DataMember="Products" AllowSorting="True" DataSource="<%# dataSet11 %>" Height="333px" Width="352px" OnItemCommand="AddToCart">
				<Columns>
					<asp:BoundColumn DataField="ProductID" HeaderText="ProductID"></asp:BoundColumn>
					<asp:BoundColumn DataField="ProductName" HeaderText="ProductName"></asp:BoundColumn>
					<asp:BoundColumn DataField="QuantityPerUnit" HeaderText="QuantityPerUnit"></asp:BoundColumn>
					<asp:BoundColumn DataField="UnitPrice" SortExpression="UnitPrice" HeaderText="UnitPrice" DataFormatString="{0:$##.00}"></asp:BoundColumn>
					<asp:ButtonColumn Text="Buy" ButtonType="PushButton" CommandName="AddToCart"></asp:ButtonColumn>
				</Columns>
			</asp:datagrid>
			<asp:DataGrid id="ShoppingCart" style="Z-INDEX: 102; LEFT: 445px; POSITION: absolute; TOP: 15px" runat="server"></asp:DataGrid></form>
	</body>
</HTML>
