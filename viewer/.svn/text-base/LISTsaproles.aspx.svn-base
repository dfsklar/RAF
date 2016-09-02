<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="LISTsaproles.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WebForm14" 
Title="RBSR Authorization Framework - Bus Roles by SAP Role"
%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &bull;  &nbsp; <b>Business Role Selection via SAP Role</b></asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Selection by SAP Role
				</div>
				<div id="PageDescription">
					<i>description of this page forthcoming...</i>
				</div>
			</div>



<asp:panel ID="PNLsearch" defaultbutton="BTNtry" runat="server" Width="765px">
&nbsp;<BR>
<HR>
<B>Enter any portion of the SAP role's name:</B>
<asp:textbox id="TXTrolenamesrch" runat="server" OnTextChanged="TXTrolenamesrch_TextChanged" Width="261px"/>
<asp:button id="BTNtry" runat="server" Text="Search" /></asp:panel>

<HR>

			<p />
			<div style="text-align:left;">
			<table class="process">
			<%= this.RENDER() %>
			</table>
			</div>


</asp:Content>


