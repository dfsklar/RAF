<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="LISTbusroles_byOwner.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WebForm13" 
Title="RBSR Authorization Framework" 
%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &bull;  &nbsp; <b>Business Role Selection</b></asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Selection
				</div>
				<div id="PageDescription">
					To view a business role that lists you as an owner or approver, click on the available business role.  Only business roles that list you as an owner or approver are displayed.  If a business role that you are assigned as an owner or approver is not listed, contact the SAC Production Support through email at "SAC Production Support".
				</div>
			</div>



<asp:panel ID="PNLsearch" defaultbutton="BTNtry" runat="server">
&nbsp;<br/>
<hr/
<B>Enter any portion of the role name:</B>
<asp:textbox id="TXTrolenamesrch" runat="server" OnTextChanged="TXTrolenamesrch_TextChanged"/>
<asp:button id="BTNtry" runat="server" Text="Search" /></asp:panel>

<hr/>

			<p />
			<div style="text-align:left;">
			<table class="process">
			<%= this.RENDER() %>
			</table>
			</div>


</asp:Content>
