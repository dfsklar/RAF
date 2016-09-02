<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListSAPRoles.aspx.cs" Inherits="_6MAR_WebApplication.ListSAProles" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




<ComponentArt:Menu ID="GridContextMenu" SiteMapXmlFile="menuData_ListSAPRoles.xml" ExpandSlide="none"
        ExpandTransition="fade" ExpandDelay="250" CollapseSlide="none" CollapseTransition="fade"
        Orientation="Vertical" CssClass="MenuGroup" DefaultGroupCssClass="MenuGroup"
        DefaultItemLookId="DefaultItemLook" DefaultGroupItemSpacing="1" ImagesBaseUrl="images/"
        EnableViewState="false" ContextMenu="Custom" runat="server">
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="MenuItem" HoverCssClass="MenuItemHover"
                LabelPaddingLeft="15" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="3" />
            <ComponentArt:ItemLook LookId="BreakItem" ImageUrl="break.gif" CssClass="MenuBreak"
                ImageHeight="1" ImageWidth="100%" />
        </ItemLooks>
</ComponentArt:Menu>
<ComponentArt:Menu ID="GridContextMenu_ReadOnly" SiteMapXmlFile="menuData_ListSAPRoles_ReadOnly.xml" ExpandSlide="none"
        ExpandTransition="fade" ExpandDelay="250" CollapseSlide="none" CollapseTransition="fade"
        Orientation="Vertical" CssClass="MenuGroup" DefaultGroupCssClass="MenuGroup"
        DefaultItemLookId="DefaultItemLook" DefaultGroupItemSpacing="1" ImagesBaseUrl="images/"
        EnableViewState="false" ContextMenu="Custom" runat="server">
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="MenuItem" HoverCssClass="MenuItemHover"
                LabelPaddingLeft="15" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="3" />
            <ComponentArt:ItemLook LookId="BreakItem" ImageUrl="break.gif" CssClass="MenuBreak"
                ImageHeight="1" ImageWidth="100%" />
        </ItemLooks>
</ComponentArt:Menu>



<script type="text/javascript" src="ListSAPRoles.js"></script>
<script type="text/javascript" src="LookupEID.js"></script>


<script type="text/javascript">
   var GLOBALpageIsEditable = <%= ((this.session.idWorkspace_SAP==int.Parse(Session["INTcurWS_SAP"].ToString())) && (this.session.isWorkspaceOwner_SAP)) ? 1 : 0 %>;
   var GLOBALidworkspace = <%= Session["INTcurWS_SAP"] %>;
</script>

<!--
<p>
    <strong>SAP roles</strong> are automatically placed in this dictionary by all SAP-related upload activities.
    Additionally, you can maintain the SAP role dictionary here.
 </p>
-->


<asp:Panel ID="PANELcond_Editable" runat="server" Width="788px" Wrap="False">
<B>YOU ARE LOGGED INTO A WORKSPACE.</B>
<BR/>
It is safe for you to perform role design (entitlement allocation) without affecting "live" entitlements.
<br />
<p style='text-align:right;margin-bottom:4px'>
<a target="AFWACexport" href="export/SAPworkspace.ashx?id=<%=this.session.idWorkspace_SAP%>">Export</a> &nbsp;&nbsp;
<a href="javascript:AddNewRole();">Add row</a>
&nbsp;&nbsp;
<a href="javascript:PublishWorkspace(<%=this.session.idWorkspace_SAP%>);">Publish</a>
&nbsp;&nbsp;
<a href="javascript:LaunchChangeMgmt(<%=this.session.idWorkspace_SAP%>);">ChangeMgmt</a>
<!--
&nbsp;&nbsp;
<a onclick="javascript:return confirm('Are you sure you want to DELETE this workspace?');" href="utilities/CleaningCrew.ashx?cmd=SAPWSdel&arg1=<%= session.idWorkspace_SAP %>">Delete this workspace</a>
-->
&nbsp;

</p>
</asp:Panel>





<asp:Panel ID="PANELcond_ViewOtherUserWorkspace" runat="server" Width="788px" Wrap="False">
<B>THIS IS A READ-ONLY VIEW OF WORKSPACE BELONGING TO <U><%= this.session.nameUserWorkspaceOwner_SAP %></U></B>
<BR/>
You will be able to view the design-in-progress but you cannot make modifications.
<BR/>
For more information about this workspace, visit the History pane.
<p style='text-align:right;margin-bottom:4px'>
<a target="AFWACexport" href="export/SAPworkspace.ashx?id=<%=this.session.idWorkspace_SAP%>">Export</a> &nbsp;&nbsp;</asp:Panel>



<asp:Panel ID="PANELcond_ViewActiveReadOnly" runat="server" Width="788px" Wrap="False">
<B>THIS IS A READ-ONLY VIEW OF THE CURRENTLY ACTIVE SAP ENTITLEMENTS.</B>
<BR/>
You will be able to view but not make modifications.
<BR/>
For more information on the history of this subprocess, visit the History pane.
<a target="AFWACexport" href="export/SAPworkspace.ashx?id=<%=Session["INTcurWS_SAP"]%>">Export</a>
</asp:Panel>



<asp:Panel ID="PANELcond_ViewHistoricalReadOnly" runat="server" Width="788px" Wrap="False">
<B>THIS IS A READ-ONLY VIEW OF A HISTORICAL SNAPSHOT.</B>
<BR/>
You will be able to view but not make modifications.
<BR/>
For more information about the history of this subprocess, visit the History pane.
<a target="AFWACexport" href="export/SAPworkspace.ashx?id=<%=Session["INTcurWS_SAP"]%>">Export</a>
</asp:Panel>



<asp:Panel ID="PANELcond_ReadOnly" runat="server" Width="788px" Wrap="False">
<b>THERE IS NO WORKSPACE FOR THIS SUBPROCESS.</b>
<BR/>
To view historical data or start doing design work, please visit the "History" pane.
</asp:Panel>



 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="

SELECT 
   *, ' ' AS Blank,

(
SELECT
COUNT(*)
FROM
t_RBSR_AUFW_u_TcodeAssignment TASS
WHERE 
(TASS.c_r_TcodeAssignmentSet = @SCOPEtasset) AND
(TASS.c_r_SAProle = SAPR.c_id) AND
(TASS.c_u_EditStatus <> 6) AND (TASS.c_u_EditStatus <> 4)
)
as KOUNT


FROM [t_RBSR_AUFW_u_SAPRole] SAPR WHERE ([c_r_SubProcess] = @c_r_SubProcess) OR ([c_u_Name] LIKE '%-DSP%') ORDER BY [c_u_Name]

">
                <SelectParameters>
    
            <asp:SessionParameter Name="SCOPEtasset" SessionField="INTcurWS_SAP" Type="Int32" DefaultValue="15" />
            <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>













<ComponentArt:Grid ID="Grid1"

 RunningMode="Client"

 ManualPaging="True"

 EditOnClickSelectedItem="false"

 AutoCallBackOnInsert="false"
 AutoCallBackOnUpdate="false"

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

 ShowFooter="false"

  ScrollBar="Auto"
  ScrollTopBottomImagesEnabled="true"
  ScrollTopBottomImageHeight="2"
  ScrollTopBottomImageWidth="16"
  ScrollImagesFolderUrl="images/scroller/"
  ScrollButtonWidth="16"
  ScrollButtonHeight="17"
  ScrollBarCssClass="ScrollBar"
  ScrollGripCssClass="ScrollGrip"
  ScrollBarWidth="16"


        CallbackReloadTemplates="false"
		   runat="server" Width="90%" DataSourceID="SqlDataSource1"  
           AllowEditing="false"
	>
        <ClientEvents>
          <CallbackError EventHandler="Grid1_onCallbackError" />
          <ContextMenu EventHandler="Grid1_onContextMenu" />
        </ClientEvents>
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField"

		EditCommandClientTemplateId="EditCommandTemplate"
            InsertCommandClientTemplateId="EditCommandTemplate"

                >



<Columns>
 <ComponentArt:GridColumn 
     DataField="c_id" AllowEditing="false" Visible="false"/>
 <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="Name" />
 <ComponentArt:GridColumn DataField="c_u_Description" HeadingText="Description" />
 <ComponentArt:GridColumn DataField="c_u_System" HeadingText="System" />
 <ComponentArt:GridColumn DataField="c_u_Platform" HeadingText="Platform" />
 <ComponentArt:GridColumn DataField="c_u_RoleActivity" HeadingText="Role Activity" />
 <ComponentArt:GridColumn DataField="c_u_RoleType" HeadingText="Role Type" />
 <ComponentArt:GridColumn DataField="KOUNT" HeadingText="#Tcodes"
    Align='right' Width='54'

 />
</Columns>





</ComponentArt:GridLevel>
</Levels>




        <ClientTemplates>
          <ComponentArt:ClientTemplate Id="EditCommandTemplate">
	  <DIV style='padding:3px'>
               <a href="javascript:Grid1.editComplete();">OK</a> 
	    	 <BR>
	       <a href="javascript:Grid1.EditCancel();">Cancel</a>
	  </DIV>
          </ComponentArt:ClientTemplate>
        </ClientTemplates>

    </ComponentArt:Grid>


<asp:Panel ID="PANELcond_PublishWorkspace" runat="server">
<P style='text-align:right'>
<a onclick="javascript:return confirm('Are you sure you want to DELETE this workspace?');" href="utilities/CleaningCrew.ashx?cmd=SAPWSdel&arg1=<%= session.idWorkspace_SAP %>">Delete this workspace</a>
&nbsp;
<a target="AFWACexport" href="export/SAPworkspace.ashx?id=<%=this.session.idWorkspace_SAP%>">Export</a> &nbsp;&nbsp;</asp:Panel>




<DIV id="JQDLGchangeMgmt" class='JQdialog' title='Change Management'>
<DIV id="TARGETHOME_GRIDchangeMgmt">
</DIV>
</DIV>






<DIV ID="HIDDEN_CLOSET" style='visibility:hidden'>

<DIV ID="WRAPPER_GRIDchangeMgmt">
<ComponentArt:Grid ID="GRIDchangeMgmt" runat="server"
EnableViewState="true"
RunningMode="Client"
PageSize="6"
Width="880"
Height="80"
ShowHeader="False"
ShowFooter="False"
DataSourceID="SQL_changeMgmt"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell"  
  			  AllowSorting="False"
      DataCellCssClass="DataCell" 
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
  >
       <Columns>
         <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
         <ComponentArt:GridColumn 
	 	 HeadingText="" DataCellClientTemplateId="TEMPLgridchgmgmt_edit" AllowEditing="False"
                 Width='20' FixedWidth='True'/>
	 <ComponentArt:GridColumn DataField="c_u_EventType" Visible="true" AllowEditing="False" Width="207"
	 			  HeadingText="Event" />
	 <ComponentArt:GridColumn DataField="c_u_Who" Visible="true" AllowEditing="False"  Width="154"
	 			  HeadingText="Who"/>
	 <ComponentArt:GridColumn DataField="c_u_TimeStamp" Visible="true" AllowEditing="False" Width="94"
	 			  FormatString="yyyy-MMM-dd"
	 			  HeadingText="When"/>
	 <ComponentArt:GridColumn DataField="c_u_Commentary" Visible="true" AllowEditing="False" Width="375"
	 			  HeadingText="Comments"/>
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
<ClientTemplates>
  <ComponentArt:ClientTemplate ID="TEMPLgridchgmgmt_edit">
     ##GRIDchgmgmt_RenderEditArea(DataItem);##
  </ComponentArt:ClientTemplate>
</ClientTemplates>
</ComponentArt:Grid>
</DIV>




<div id="JQDLGeditChgMgmtRow" class='JQdialog' title='ChangeManagment'>

<form id='FORMJQ' name='FORMJQ' method='POST' action='self.htm' >

Event: <SPAN id="editchgmgmtrow_LABEL_WHAT"></SPAN>

<table class='dlgcustomgrid'>

<input type='hidden' id="editchgmgmtrow_HIDDEN_ID" name="editchgmgmtrow_HIDDEN_ID" value=''>

<tr>
<td>Who:</td>
<td><INPUT type='text' id="editchgmgmtrow_INPUT_WHO" name="editchgmgmtrow_INPUT_WHO"></INPUT></td>
</tr>

<tr>
<td>When:</td>
<td>
<INPUT type='text' id="editchgmgmtrow_INPUT_WHEN" name="editchgmgmtrow_INPUT_WHEN"></INPUT>
</td>
</tr>

<tr>
<td>Comments:</td>
<td><TEXTAREA rows='6' cols='50' type='text' id="editchgmgmtrow_TEXTAREA_COMMENTS" name="editchgmgmtrow_TEXTAREA_COMMENTS"></TEXTAREA></td>
</tr>

</table>
</FORM>
</div>






<DIV ID="WRAPPER_GRIDroleowners">

<ComponentArt:CallBack ID="CALLBACKselectCurRole" runat="server">
<ClientEvents>
    <CallbackComplete EventHandler="CALLBACKselectCurRole_Continue" />
</ClientEvents>
<Content>
<ComponentArt:Grid ID="GRIDroleowners" runat="server"
RunningMode="Client"
PageSize="5"
Width="650"
ShowHeader="False"
EnableViewState="True"
ShowFooter="False"
DataSourceID="SQL_roleowners"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
  >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="c_u_EID" Visible="true" AllowEditing="False" HeadingText="EID" />
	<ComponentArt:GridColumn DataField="FORDISP_fullname" Visible="true" AllowEditing="False" HeadingText="Name" />
	<ComponentArt:GridColumn DataField="c_u_Rank" Visible="true" AllowEditing="False" HeadingText="Type" />
        <ComponentArt:GridColumn DataField="c_u_Geography" Visible="true" AllowEditing="False" HeadingText="Geography"/>
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
 <ClientEvents>
    <ItemSelect EventHandler="EVT_GRIDroleowners_RowSelect" />
 </ClientEvents>
</ComponentArt:Grid>
</Content>
</ComponentArt:CallBack>

</DIV>



</DIV><!-- End of the hidden closet -->




<asp:SqlDataSource
    ID="SQL_changeMgmt" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="SELECT * FROM t_RBSR_AUFW_u_SAPChangeManagementEvent CME WHERE c_r_TcodeAssignmentSet = @IDWS">
            <SelectParameters>
	        <asp:SessionParameter DefaultValue="-3" Name="IDWS" SessionField="INTcurWS_SAP" Type="Int32" />
            </SelectParameters>
</asp:SqlDataSource>




<asp:SqlDataSource
    ID="SQL_roleowners" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"

    OnSelecting="INTERCEPTsqldatasource_roleowners_select"

    SelectCommand="

SELECT 
BRO.*, 
USR.c_u_NameSurname + ', ' + USR.c_u_NameFirst AS FORDISP_fullname
FROM t_RBSR_AUFW_u_SAPRoleOwner BRO
LEFT OUTER JOIN t_RBSR_AUFW_u_User USR ON USR.c_u_EID=BRO.c_u_EID WHERE c_r_SAProle = @FILTERSAPROLE
">
   <SelectParameters>
     <asp:Parameter Name="FILTERSAPROLE" Type="Int32" />
   </SelectParameters>
 </asp:SqlDataSource>




<asp:HiddenField ID="HIDFIELDcurRoleId" runat="server" onvaluechanged="EVTHNDL_hidfield_curroleid_changed"/>



<div id="JQDLGroleowners" class='JQdialog' title='Role Owners for _____'>
<DIV id="TARGETHOME_GRIDroleowners">
</DIV>
</div>







<!-- ****************************************** -->

<!-- DIALOG:  editing one role owner row -->

<div id="JQDLGeditRoleOwnerRow" class='JQdialog' title='Role Owner Management'>



<table class='dlgcustomgrid'>

<input type='hidden' id="editroleowner_HIDDEN_ID" name="editroleowner_HIDDEN_ID" value=''>

<tr>
<td>EID:</td>
<td>
<input type='text' ID="TD_editroleowner_INPUT_EID">
<span class='fakelink' onclick="javascript:LookupEID('TD_editroleowner_INPUT_EID','JQDLGbp_primown_name');">Lookup</span>
</td>
</tr>


<tr>
<td>Name:</td>
<td>
<input type='text' id='JQDLGbp_primown_namelast' name='JQDLGbp_primown_namelast' value=''></input> , 
<input type='text' id='JQDLGbp_primown_namefirst' name='JQDLGbp_primown_namefirst' value=''></input>
</td>
</tr>

<tr>
<td>Type:</td>
<td>
<SELECT id="TD_editroleowner_SELECT_RANK">
  <OPTION value='OWNprim'>Primary OWNER</OPTION>
  <OPTION value='OWNdele'>Delegate Owner</OPTION>
  <OPTION value='appr'>Approver</OPTION>
  <OPTION value='delegate'>Delegate</OPTION>
</SELECT>
</td>
</tr>

<tr>
<td>Geography:</td>
<td>
<input type='text' ID="TD_editroleowner_INPUT_GEO">
</td>
</tr>

<tr ID="ROW_editroleowner_CHK_DEL">
<TD>DELETE:</TD>
<TD>
<input type="checkbox" id="editroleowner_CHK_DEL">
Check this box to request deletion of this data record.
</TD>
</tr>


</table>


</div>






<div id="JQDLGsaproleProperties" class='JQdialog' title='SAP Role Properties'>
   <form id='FORMJQ' name='FORMJQ' method='POST' action='self.htm'>
   <table class='dlgcustomgrid'>
   <tr>
   <td>Role Name:</td>
   <td>
   <input type='text' id='JQDLGbp_name' name='JQDLGbp_name' value='' size='50'></input>
   <input type='hidden' id='JQDLGbp_id' name='JQDLGbp_id'   value=''></input>
   </td>
   </tr>
   <tr>
   <td>Role Description:</td>
   <td><input type='text' id='JQDLGbp_description' name='JQDLGbp_description' value='' size='50'></input></td>
   </tr>
   <tr>
   <td>System:</td>
   <td><input type='text' id='JQDLGbp_system' name='JQDLGbp_system' value='' size='50'></input></td>
   </tr>
   <tr>
   <td>Platform:</td>
   <td><input type='text' id='JQDLGbp_platform' name='JQDLGbp_platform' value='' size='50'></input></td>
   </tr>
   <tr>
   <td>Role activity:</td>
   <td><input type='text' id='JQDLGbp_roleactivity' name='JQDLGbp_roleactivity' value='' size='50'></input></td>
   </tr>
   <tr>
   <td>Role type:</td>
   <td><input type='text' id='JQDLGbp_roletype' name='JQDLGbp_roletype' value='' size='50'></input></td>
   </tr>

   </table>

   </form>
   </div>





</asp:Content>
