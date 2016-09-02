<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="DETAILbusrole.aspx.cs" Inherits="_6MAR_WebApplication.viewer.UIPAGE_DETAILbusrole" 
Title="RBSR Authorization Framework - Bus Role Detail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &gt;  &nbsp; 
<a href='LISTbusroles.aspx<%=Request.Url.Query%>'>Business Role Selection</a> &nbsp; &gt; &nbsp;
 <b>Business Role Detail</b></asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
			<h3>Role Owners or Approvers</h3>
						<table class='subprocess'>
					<tr>
						<th>Type</th>
						<th>Name</th>
						<th>EID</th>
					</tr>
					<%= this.rowsOwnersApprovers() %>
			</table>
			<br />
<hr/>
			<h3>Business Role Summary</h3>
			The currently assigned application entitlements to this business role are listed.
			<br />Business Process Activities <br />
			<img src="../images/IsHead_false__IsKeyPoint_true.gif" /> Key Control Points 
			<p />
			<table class='subprocess'>
					<tr>
						<th>Activities</th>
						<th>Performed By</th>
						<th>System Mapping</th>
					</tr>
					<%= this.ActiveBusinessEntitlements() %>

			</table>
			<p />
			The technical authorization framework is available for detailed viewing:  
<a href="ENTVECTORbusrole.aspx<%=Request.Url.Query%>">Printable</a> &bull; 
<a href="ENTVECTORbusrole_GRID.aspx<%=Request.Url.Query%>">Interactive</a>


			<p />
<a name='ANCHORsaproles'></a><br/><hr/>
			<h3 style='margin-top:14px'>SAP Roles</h3>
    
        <asp:Panel ID="PANELsapdesignnote" runat="server" Width="748px">
            Design note:<br />
            <asp:TextBox ID="STATICTXTsapfuncappdesignnote" runat="server" CssClass="indentedbox" ReadOnly="True" TextMode="MultiLine"></asp:TextBox></asp:Panel>
       
       <p> 
        
        
        
        
           <asp:Panel ID="PANEL_tcodelistingmode_HtmlTable" runat="server">
           	The SAP roles / TCodes currently assigned to this business role are listed below:
            <table class='subprocess'>
					<tr>
						<th>Role Name</th>
						<th>Platform</th>
						<th>Tcode</th>
						<th>Description</th>
						<th>Act. Folder</th>
						<th>Access</th>
					</tr>
                 <%= this.ROWStcodes() %>    
</table>
  
   
           </asp:Panel>
        
       
       

           <asp:Panel ID="PANEL_tcodelistingmode_interactive" runat="server">
              
              This SAP roles /TCodes currently assigned to this business role are shown in the interactive table below.
              
              <p>(<a href='DETAILbusrole.aspx?<%= this.Request.QueryString.ToString().Replace("sapview","ssignore") %>&sapview=H#ANCHORsaproles'>Click here</a> to see this table as an HTML page suitable for printing.)</p>
        
        <!-- THIS IS WHERE THE NEW GRID SHOWING ALL THE TCODES WILL GO -->
        
        <ComponentArt:Grid ID="GRIDsapdetails" runat="server" Width="800"

  ManualPaging="True"

  RunningMode="Client"

 ShowHeader="true"
 ShowSearchBox="true" 
 SearchOnKeyPress="false"
 HeaderHeight="18"
 HeaderCssClass="GridHeader" 
 SearchText="Search (hit Enter to refresh):"
 SearchTextCssClass="GridHeaderText"
 GroupByTextCssClass="GroupByText"
 GroupByCssClass="GroupByCell"
 GroupingNotificationTextCssClass="GridHeaderText"
 GroupingNotificationText=""
 

  ShowFooter="false"

  ScrollBar="Auto"
  ScrollTopBottomImagesEnabled="true"
  ScrollTopBottomImageHeight="2"
  ScrollTopBottomImageWidth="16"
  ScrollImagesFolderUrl="../images/scroller/"
  ScrollButtonWidth="16"
  ScrollButtonHeight="17"
  ScrollBarCssClass="ScrollBar"
  ScrollGripCssClass="ScrollGrip"
  ScrollBarWidth="16"

  AutoCallBackOnUpdate="true"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="True"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
      EditCellCssClass="EditDataCell"
      EditFieldCssClass="EditDataField"
  >
<Columns>
    <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="RoleName" Width="200" AllowEditing="False" HeadingText="Role Name" />
	<ComponentArt:GridColumn DataField="Platform" Width="70" AllowEditing="False" HeadingText="Platform" />
	<ComponentArt:GridColumn DataField="TCode" Width="150" AllowEditing="False" HeadingText="TCode" />
	<ComponentArt:GridColumn DataField="Description" Width="180" AllowEditing="False" HeadingText="Description" />
	<ComponentArt:GridColumn DataField="ActiveFolder" Width="180" AllowEditing="False" HeadingText="Active Folder" />
	<ComponentArt:GridColumn DataField="Access" Width="70" AllowEditing="False" HeadingText="Access" />
</Columns>
  </ComponentArt:GridLevel>
</Levels>
        <ClientEvents>
        
        </ClientEvents>
</ComponentArt:Grid>


</asp:Panel>








        
        
        
        

   


			<p />
<hr/>			<h3>Available Exception Access</h3>
<p>When an exception role is requested, it must be approved by the Segregation of Duties team, the user's Manager and the role owner.  Once the exception role  is approved, the role may take up to four days to be provisioned in the requested system.
</p>
				<table class='subprocess'>
					<tr>
						<th>Type</th>
						<th>Name</th>
						<th>Description</th>
					</tr>

					<%= this.ROWSadditionalBusinessRoles() %>
<!--
					<tr>
						<td>Elevated</td>
						<td><a href="business_user_role_detail.html">ELE_Audio_Conferencing</a></td>
						<td>Elevated - Audio Conferencing</td>
					</tr>
					<tr>
						<td>Extra</td>
						<td><a href="business_user_role_detail.html">APP_VPN_Access</a></td>
						<td>Application Access - VPN Access</td>
					</tr>
-->
			</table>

<p>&nbsp;</p>
<hr/>
</asp:Content>
