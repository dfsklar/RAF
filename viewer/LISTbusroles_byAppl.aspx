<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="LISTbusroles_byAppl.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WebForm16" 
Title="RBSR Authorization Framework - Bus Roles by Appl" %>


<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &bull;  &nbsp; <b>Business Role Selection by Application</b></asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Selection by Application</div>
				<div id="PageDescription">
					<i>description of this page forthcoming...</i>
				</div>
			</div>



<asp:panel ID="PNLsearch" defaultbutton="BTNtry" runat="server" Width="820px">
&nbsp;<BR>
<HR>
<B>Enter any portion of the Application name:</B>
<asp:textbox id="TXTsrch" runat="server" OnTextChanged="TXTrolenamesrch_TextChanged"/>
<asp:button id="BTNtry" runat="server" Text="Search" /></asp:panel>

<hr/>

			<p />
			<div style="text-align:left;">
			<table class="process">
			<%= this.RENDER() %>
			</table>
			</div>


</asp:Content>


