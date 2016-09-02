<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="LISTsaproles_byTcode.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WebForm15" 
Title="RBSR Authorization Framework - Bus Roles by TCode"
%>


<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &bull;  &nbsp; <b>Business Role Selection by TCode</b></asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Selection by TCode
				</div>
				<div id="PageDescription">
					<i>description of this page forthcoming...</i>
				</div>
			</div>



<asp:panel ID="PNLsearch" defaultbutton="BTNtry" runat="server">
&nbsp;<BR>
<HR>
<B>Enter any portion of the TCode:</B>
<asp:textbox id="TXTtcodesrch" runat="server" OnTextChanged="TXTrolenamesrch_TextChanged"/>
<asp:button id="BTNtry" runat="server" Text="Search" /></asp:panel>

<hr/>

			<p />
			<div style="text-align:left;">
			<table class="process">
			<%= this.RENDER() %>
			</table>
			</div>


</asp:Content>


