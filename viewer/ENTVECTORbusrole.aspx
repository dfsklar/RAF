<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="WebForm1.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WEBFORMentvectorbusrole" 
Title="RBSR Authorization Framework" %>

<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &gt;  &nbsp; <A href='LISTbusroles.aspx<%=Request.Url.Query%>'>Business Role Selection</A> &nbsp; &gt; &nbsp; <A href='DETAILbusrole.aspx<%=Request.Url.Query%>'>Business Role Detail</A> &nbsp; &gt; &nbsp; <B>Technical Authorization Framework</B> </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Detail
				</div>
				<div id="PageDescription">
					Process: <%= this.nameOfProcess() %>
					<br />
					Subprocess: <%= this.nameOfSubProcess() %>
					<br />
					Role Name: <%= this.nameOfRole() %>
					<br />
					Description:  <%= this.descrOfRole() %>
					<br />
					Type: <%= this.theBR.RoleType_Displayable %>
				</div>
			</div>
			<p />
The technical authorization framework describes the detailed privilege strings that are assigned to the business role.  This detail is meant primarily for the Security Access Controls team and BIS teams supporting the roles and applications.

			<p />
			<table class='Print'>
					<tr>
						<th>Application Name</th>
						<th>Standard Activity</th>
						<th>Role Type</th>
						<th>System</th>
						<th>Platform</th>
						<th>Entitlement Name</th>
						<th>Entitlement Value</th>
						<th>Authorization Object</th>
						<th>Field Level Security Name</th>
						<th>Field Level Security Value</th>
						<th>4th Level Security Name</th>
						<th>4th Level Security Value</th>
					</tr>

<%= this.RENDER() %>

</table>
</asp:Content>
