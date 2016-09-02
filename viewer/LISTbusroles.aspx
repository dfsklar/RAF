<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="LISTbusroles.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WebForm11" 
Title="RBSR Authorization Framework" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &bull;  &nbsp; <B>Business Role Selection</b></asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Selection
				</div>
				<div id="PageDescription">
					To view a business role that lists you as an owner or approver, click on the available business role.  Only business roles that list you as an owner or approver are displayed.  If a business role that you are assigned as an owner or approver is not listed, contact the SAC Production Support through email at "SAC Production Support".
				</div>
			</div>


<TABLE style='text-align:left;margin-top:12px;margin-bottom:12px'>
<TR><TD><span style='font-size:18px;font-weight:bold'>PROCESS:</span></TD>
<TD>
<ComponentArt:ComboBox 
  ID="COMBOXchooseProcess" runat="server" Width="250" 
  OnSelectedIndexChanged="COMBOXchooseProcess_SelectedIndexChanged"
  RunningMode="Callback"
  AutoPostBack="true"
  DataValueField="c_id"
  DataTextField="c_u_Name"
  TextBoxEnabled="False"  
  HoverCssClass="comboBoxHover"  FocusedCssClass="comboBoxHover"
 TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
 ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="../images/drop_hover.gif"
 DropImageUrl="../images/drop.gif">
 <ClientEvents>
 </ClientEvents>
</ComponentArt:ComboBox>
</TD>
</TR>
</TABLE>

			<p />
			<div style="text-align:left;">
			<table class="process">
			<%= this.RENDER() %>
			</table>
			</div>


</asp:Content>
