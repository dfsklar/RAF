<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListBRoles.aspx.cs" Inherits="_6MAR_WebApplication.WebForm16" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='ListBRoles.js'></script>
<script type="text/javascript" src='LookupEID.js'></script>
<script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>

<script type="text/javascript">
   var GLOBALpageIsEditable = <%= ((this.session.idWorkspace==int.Parse(Session["INTcurWS"].ToString())) && (this.session.isWorkspaceOwner)) ? 1 : 0 %>;
   var GLOBALidworkspace = <%= Session["INTcurWS"] %>;
</script>





<asp:Panel ID="PANELcond_Editable" runat="server" Width="788px" Wrap="False">

<INPUT type='hidden' id='commentOnThisWorkspace' value='<%=this.session.commentWorkspace%>'/>

<B>YOU ARE LOGGED INTO A WORKSPACE.</B> 
<BR/>
It is safe for you to perform role design (entitlement allocation) without affecting "live" entitlements.
<br />
<p style='text-align:right;margin-bottom:4px'>
<a target="AFWACexport" href="export/EntitlementsPerSubpr.ashx?id=<%=Session["INTcurWS"]%>">Export</a>
&nbsp;&nbsp;
<a href="javascript:LaunchJQdialog(null);">Add role</a>
&nbsp;&nbsp;
<a href="javascript:PublishWorkspace(<%=this.session.idWorkspace%>);">Publish</a>
&nbsp;&nbsp;
<a href="javascript:LaunchChangeMgmt(<%=this.session.idWorkspace%>);">ChangeMgmt</a>
&nbsp;&nbsp;
<a href="PAGE_attachments.aspx">Attachments</a>

</p>
</asp:Panel>




<asp:Panel ID="PANELcond_ViewOtherUserWorkspace" runat="server" Width="788px" Wrap="False">
<B>THIS IS A READ-ONLY VIEW OF WORKSPACE BELONGING TO <U><%= this.session.nameUserWorkspaceOwner %></U></B>
<BR/>
You will be able to view the design-in-progress but you cannot make modifications.
<BR/>
For more information about this workspace, visit the History pane.
<a target="AFWACexport" href="export/EntitlementsPerSubpr.ashx?id=<%=Session["INTcurWS"]%>">Export</a>
</asp:Panel>



<asp:Panel ID="PANELcond_ViewActiveReadOnly" runat="server" Width="788px" Wrap="False">
<B>THIS IS A READ-ONLY VIEW OF THE CURRENTLY ACTIVE ENTITLEMENTS.</B>
<BR/>
You will be able to view but not make modifications.
<BR/>
For more information on the history of this subprocess, visit the History pane.
<a target="AFWACexport" href="export/EntitlementsPerSubpr.ashx?id=<%=Session["INTcurWS"]%>">Export</a>
</asp:Panel>



<asp:Panel ID="PANELcond_ViewHistoricalReadOnly" runat="server" Width="788px" Wrap="False">
<B>THIS IS A READ-ONLY VIEW OF A HISTORICAL SNAPSHOT.</B>
<BR/>
You will be able to view but not make modifications.
<BR/>
For more information about the history of this subprocess, visit the History pane.
<a target="AFWACexport" href="export/EntitlementsPerSubpr.ashx?id=<%=Session["INTcurWS"]%>">Export</a>
</asp:Panel>



<asp:Panel ID="PANELcond_ReadOnly" runat="server" Width="788px" Wrap="False">
<b>NO WORKSPACE IS OPEN FOR THIS SUBPROCESS.</b>
<BR/>
To perform viewing, visit the "History" pane and click on "Visit".
<BR/>
To perform maintenance, visit the "History" pane and initiate a workspace.
</asp:Panel>






<ComponentArt:Menu ID="GridContextMenu" SiteMapXmlFile="menuData_ListBRoles.xml" ExpandSlide="none"
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
<ComponentArt:Menu ID="GridContextMenu_ReadOnly" SiteMapXmlFile="menuData_ListBRoles_ReadOnly.xml" ExpandSlide="none"
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







<asp:SqlDataSource ID="SQL_allKnownBusRoles" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
  SelectCommand="
SELECT *
FROM t_RBSR_AUFW_u_BusRole
WHERE c_u_Name NOT LIKE '%//DEL_%'
ORDER BY c_u_Name"
/>



<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
  SelectCommand="

SELECT BROLE.c_u_Name as BROLENAME, *, 
(
SELECT COUNT(*) FROM t_RBSR_AUFW_u_EntAssignment EA

LEFT OUTER JOIN t_RBSR_AUFW_u_Entitlement ENT
ON EA.c_r_Entitlement = ENT.c_id

WHERE
EA.c_r_EntAssignmentSet = @IDWS
AND
EA.c_u_Status NOT IN ('X')
AND
EA.c_r_BusRole = BROLE.c_id
) as KOUNT,

(
SELECT TOP 1 
  (THEUSER.c_u_NameSurname + ', ' + THEUSER.c_u_NameFirst)
FROM
t_RBSR_AUFW_u_BusRoleOwner BRO
LEFT OUTER JOIN t_RBSR_AUFW_u_User THEUSER
ON THEUSER.c_u_EID = BRO.c_u_EID
WHERE 
  BRO.c_u_Rank='OWNprim' AND
  BRO.c_r_BusRole = BROLE.c_id
) as UserName

,
(SELECT Displayable from DICT_BusRoleType where Abbrev=BROLE.c_u_RoleType) 
    as RoleTypeDispl

FROM t_RBSR_AUFW_u_BusRole BROLE

WHERE ([c_r_SubProcess] = @c_r_SubProcess) ORDER BY [BROLENAME]

">
 <SelectParameters>
     <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS" Type="Int32" />
     <asp:SessionParameter DefaultValue="-3" Name="IDWS" SessionField="INTcurWS" Type="Int32" />
 </SelectParameters>
</asp:SqlDataSource>






<ComponentArt:Grid ID="Grid1" runat="server"

ManualPaging="true"

EnableViewState="true"

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


        PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="41"
        PagerButtonHeight="22" SliderHeight="15" SliderWidth="150" SliderGripWidth="9"
        SliderPopupOffsetX="20" SliderPopupClientTemplateId="SliderTemplate" ScrollPopupClientTemplateId="ScrollPopupTemplate"
        PagerImagesFolderUrl="images/pager/"  

	ImagesBaseUrl="images/"

        AutoCallBackOnInsert="true"
        AutoCallBackOnUpdate="true"
        AutoCallBackOnDelete="true"
        KeyboardEnabled="false"
        CallbackReloadTemplates="false"
 Width="90%" DataSourceID="SqlDataSource1"  


AllowEditing="true" 
EditOnClickSelectedItem="false"

>
        <ClientEvents>
          <CallbackError EventHandler="Grid1_onCallbackError" />
          <ContextMenu EventHandler="Grid1_onContextMenu" />
        </ClientEvents>
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow"
				    SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10"
					  EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField"
				       EditCommandClientTemplateId="EditCommandTemplate"
            InsertCommandClientTemplateId="EditCommandTemplate"
                >
                <Columns>
                    <ComponentArt:GridColumn DataField="c_id" HeadingText="ID" Visible="false" AllowEditing="False" />
                    <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="Name" />
                    <ComponentArt:GridColumn DataField="c_u_Description" HeadingText="Description" TextWrap="false"/>
                    <ComponentArt:GridColumn DataField="c_u_RoleType" HeadingText="Type" TextWrap="false" Visible='false'/>
                    <ComponentArt:GridColumn DataField="RoleTypeDispl" HeadingText="Type" TextWrap="false"/>
                    <ComponentArt:GridColumn DataField="UserName" HeadingText="Owner" Width='70' TextWrap="false" AllowEditing="False"/>
                    <ComponentArt:GridColumn DataField="c_u_OwnerSecondaryEID" HeadingText="OwnerSec" Visible="false"/>
                    <ComponentArt:GridColumn DataField="c_u_DesignDetails" HeadingText="DesignDetails" Visible="false"/>
                    <ComponentArt:GridColumn DataField="KOUNT" HeadingText="Ent. count" DataCellCssClass='GridColumn_rightjust' 
		        Align='right' TextWrap="false" AllowEditing="False" Width='60'/>
		</Columns>
            </ComponentArt:GridLevel>
        </Levels>
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="TEMPLdesignLinkage">
               ##RenderEditArea(DataItem);##
            </ComponentArt:ClientTemplate>
          <ComponentArt:ClientTemplate Id="EditCommandTemplate">
            <a href="javascript:Grid1.editComplete();">Update</a> | <a href="javascript:Grid1.EditCancel();">Cancel</a>
          </ComponentArt:ClientTemplate>


            <ComponentArt:ClientTemplate ID="SliderTemplate">
                <table class="SliderPopup" cellspacing="0" cellpadding="0" border="0" style="background-color: white">
                    <tr>
                        <td valign="top" style="padding: 5px;">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td width="25" align="center" valign="top" style="padding-top: 3px;">
                                        <br>
                                    </td>
                                    <td>
                                        <table cellspacing="0" cellpadding="2" border="0" style="width: 255px;">
                                            <tr>
                                                <td colspan="2">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="230" colspan="2" style="font-family: verdana; font-size: 11px; font-weight: bold;">
                                                                <div style="text-overflow: ellipsis; overflow: hidden; width: 250px;">
                                                                    <nobr>## DataItem.GetMember('c_u_Name').Value ##</nobr>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 14px; background-color: #757598;">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td style="padding-left: 5px; color: white; font-family: verdana; font-size: 10px;">
                                        Page <b>## DataItem.PageIndex + 1 ##</b> of <b>## Grid1.PageCount ##</b>
                                    </td>
                                    <td style="padding-right: 5px; color: white; font-family: verdana; font-size: 10px;"
                                        align="right">
                                        Row <b>## DataItem.Index + 1 ##</b> of <b>## Grid1.RecordCount ##</b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>

    </ComponentArt:Grid>
    <br />












<!-- This is designed to inform the server that a change-mgmt popup
   is about to be called and thus it might be a good idea to
   make sure the ChangeManagmeent rows already exist for this
   workspace, and to construct if not -->
<ComponentArt:CallBack ID="CALLBACKinitChangeMgmt" runat="server">
 <ClientEvents>
    <CallbackComplete EventHandler="LaunchChangeMgmt_Continue" />
 </ClientEvents>
</ComponentArt:CallBack>

<div id="JQDLGchangeMgmt" class='JQdialog' title='Change Management'>
<DIV id="TARGETHOME_GRIDchangeMgmt">
</DIV>
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










<ComponentArt:CallBack ID="CALLBACKadditionalRoles" runat="server">
</ComponentArt:CallBack>

<div id="JQDLGsupplementalRoleList" class='JQdialog' title='Additional Roles Allowed for _____'>
<div id="spinnerImg" style="font-size:26pt;display: none; height:800pt;color:#BB0000">
Recording your modification... Please wait...
</div>
<DIV id="TARGETHOME_GRIDadditionalRoles">
</DIV>
</div>

<div id="JQDLGroleowners" class='JQdialog' title='Role Owners for _____'>
<DIV id="TARGETHOME_GRIDroleowners">
</DIV>
</div>





<div id="JQDLGbusroleProperties" class='JQdialog' title='Business Role Properties'>
   <form id='FORMJQ' name='FORMJQ' method='POST' action='self.htm' >
   <table class='dlgcustomgrid'>
   <tr>
   <td>Role Name:</td>
   <td>
   <input type='text' id='JQDLGbp_name' name='JQDLGbp_name' value='' size='50'></input>
   <input type='hidden' id='JQDLGbp_id' name='JQDLGbp_id'   value=''></input>
   </td>
   </tr>
   <tr>
   <td>Role Type:</td>
   <td>
      <SELECT id="JQDLGbp_roletype" name="JQDLGbp_roletype">
         <OPTION value="F">Functional</OPTION>
         <OPTION value="B">Base</OPTION>
         <OPTION value="E">Elevated</OPTION>
         <OPTION value="A">Application</OPTION>
      </SELECT>
   </td>
   </tr>
   <tr>
   <td>Role Description:</td>
   <td><input type='text' id='JQDLGbp_descr' name='JQDLGbp_descr' value='' size='50'></input></td>
   </tr>
   <tr>
   <td>Design notes:</td>
   <td>
	<TEXTAREA id='JQDLGbp_designdetails' name='JQDLGbp_designdetails' rows='6' cols='80'></TEXTAREA>
   </td>
   </tr>

   </table>

   </form>
   </div>





<!-- This is the version that shows ONLY the actually registered additional-bus-role entries. -->
<asp:SqlDataSource
    ID="SQL_additionalRoles_ORIG" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="

SELECT ABR.*, BR.c_u_Name AS NAME_AdditionalBusRole, 
CONVERT(char(10),ABR.c_u_RecertificationStartDate, 120) AS DATErecertstart,
CONVERT(char(10),ABR.c_u_ExpirationDate, 120) AS DATEexpir,
ABR.c_u_RecertificationInterval + ' ' + CONVERT(VARCHAR,ABR.c_u_RecertificationStartDate, 101) as FORDISP_Recertification,
CONVERT(VARCHAR,ABR.c_u_ExpirationDate, 101) as FORDISP_ExpirationDate
FROM t_RBSR_AUFW_u_AdditionalBusRole ABR 
LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole BR ON BR.c_id=ABR.c_u_idAdditionalBusRole  WHERE c_r_BusRole = @FILTERBROLE

">
            <SelectParameters>
                <asp:SessionParameter Name="FILTERBROLE"  SessionField="intFILTERBROLE" Type="Int32" DefaultValue="218"/>
            </SelectParameters>
 </asp:SqlDataSource>




<!-- This is the version that shows ALL busroles, with a checkbox marking those that are registered -->
<asp:SqlDataSource
    ID="SQL_additionalRoles" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="

SELECT 

BR.c_id as IDrole,
@FILTERBROLE as IDroleToWhichGranted,
ABR.*, 
CASE WHEN ABR.c_id IS NOT NULL THEN 'True' ELSE 'False' END as BoolRegisteredNow,
BR.c_u_Name AS NAME_AdditionalBusRole, 
CONVERT(char(10),ABR.c_u_RecertificationStartDate, 120) AS DATErecertstart,
CONVERT(char(10),ABR.c_u_ExpirationDate, 120) AS DATEexpir,
ABR.c_u_RecertificationInterval + ' ' + CONVERT(VARCHAR,ABR.c_u_RecertificationStartDate, 101) as FORDISP_Recertification,
CONVERT(VARCHAR,ABR.c_u_ExpirationDate, 101) as FORDISP_ExpirationDate

FROM t_RBSR_AUFW_u_BusRole BR

LEFT OUTER JOIN t_RBSR_AUFW_u_AdditionalBusRole ABR 
    ON BR.c_id=ABR.c_u_idAdditionalBusRole  AND ABR.c_r_BusRole = @FILTERBROLE

WHERE BR.c_u_Name NOT LIKE '%//DEL_%'

ORDER BY BoolRegisteredNow DESC, BR.c_u_Name ASC

">
            <SelectParameters>
                <asp:SessionParameter Name="FILTERBROLE"  SessionField="intFILTERBROLE" Type="Int32" DefaultValue="218"/>
            </SelectParameters>
 </asp:SqlDataSource>






<asp:SqlDataSource
    ID="SQL_roleowners" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="

SELECT 
BRO.*, ROT.ForDisplay as RankName, ROT.Abbrev as RankCode,
USR.c_u_NameSurname + ', ' + USR.c_u_NameFirst AS FORDISP_fullname
FROM t_RBSR_AUFW_u_BusRoleOwner BRO

LEFT OUTER JOIN t_RBSR_AUFW_u_User USR ON USR.c_u_EID=BRO.c_u_EID 
LEFT OUTER JOIN DICT_RoleOwnerType ROT ON ROT.Abbrev = BRO.c_u_Rank

WHERE c_r_BusRole = @FILTERBROLE
">
   <SelectParameters>
     <asp:SessionParameter Name="FILTERBROLE"  SessionField="intFILTERBROLE" Type="Int32" DefaultValue="218"/>
   </SelectParameters>
 </asp:SqlDataSource>









<asp:SqlDataSource
    ID="SQL_changeMgmt" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="SELECT * FROM t_RBSR_AUFW_u_ChangeManagementEvent CME WHERE c_r_EntAssignmentSet = @IDWS">
            <SelectParameters>
	        <asp:SessionParameter DefaultValue="-3" Name="IDWS" SessionField="INTcurWS" Type="Int32" />
            </SelectParameters>
</asp:SqlDataSource>









<!-- ****************************************** -->

<!-- DIALOG:  editing one specific additional-role row -->

<div id="JQDLGeditAdditionalRoleRow" class='JQdialog' title='Additional Role Management'>





<table class='dlgcustomgrid'>

<input type='hidden' id="editaddrole_HIDDEN_ID" name="editaddrole_HIDDEN_ID" value=''>

<tr>
<td>Role:</td>
<td ID="TD_editaddrole_INPUT_ROLE">
<!-- THE ComboBox WIDGET IS MOVED HERE PROGRAMMATICALLY -->
</td>
</tr>

<tr>
<td>Expiration:</td>
<td>
<INPUT type='text' id="editaddrole_INPUT_EXPIRDATE" name="editaddrole_INPUT_EXPIRDATE">
</td>
</tr>

<tr>
<td>Recertification:</td>
<td>
<SELECT id="editaddrole_SELECT_RECERTINTERVAL" name="editaddrole_SELECT_RECERTINTERVAL">
  <OPTION value='N/A'>N/A</OPTION>
  <OPTION value='Daily'>Daily</OPTION>
  <OPTION value='Weekly'>Weekly</OPTION>
  <OPTION value='Monthly'>Monthly</OPTION>
  <OPTION value='Semi-Annually'>Semi-Annually</OPTION>
  <OPTION value='Annually'>Annually</OPTION>
</SELECT>
<BR>
starting on: <INPUT type='text' id="editaddrole_INPUT_RECERTSTARTDATE" name="editaddrole_INPUT_RECERTSTARTDATE"></INPUT>
</td>
</tr>

<tr>
<td>Comments:</td>
<td><TEXTAREA rows='6' cols='50' type='text' id="editaddrole_TEXTAREA_COMMENTS" name="editaddrole_TEXTAREA_COMMENTS"></TEXTAREA></td>
</tr>

<tr ID="ROWdeleteme">
<TD>DELETE:</TD>
<TD>
<input type="checkbox" id="editaddrole_CHK_DEL" name="editaddrole_CHK_DEL">
Check this box to request deletion of this data record.
</TD>
</tr>


</table>


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
  <OPTION value='OWNprim'>Primary Owner</OPTION>
  <OPTION value='OWNdele'>Delegate Owner</OPTION>
  <OPTION value='appr'>Primary Approver</OPTION>
  <OPTION value='delegate'>Delegate Approver</OPTION>
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













<!-- <A href="javascript:JQdeclareDialogs();">Declare dialogs</A> -->




<DIV ID="HIDDEN_CLOSET" style='visibility:hidden'>

<DIV ID="WRAPPER_GRIDadditionalRoles">
<ComponentArt:Grid 
ID="GRIDadditionalRoles" runat="server"
DataSourceID="SQL_additionalRoles"

RunningMode="Client"

Width="870"

ShowFooter="False"

ShowHeader="True"
 ShowSearchBox="true" 
 SearchOnKeyPress="false"
 HeaderHeight="18"
 HeaderCssClass="GridHeader" 
 SearchText="Search (hit Enter to refresh):"
 SearchTextCssClass="GridHeaderText"

GroupingNotificationText=""

EnableViewState="True"

ManualPaging="True"

ScrollBar="Auto"
ScrollTopBottomImagesEnabled="true"
ScrollTopBottomImageHeight="2"
ScrollTopBottomImageWidth="12"
ScrollImagesFolderUrl="images/scroller/"
ScrollBarCssClass="ScrollBar"
ScrollGripCssClass="ScrollGrip"
ScrollBarWidth="16"
ScrollButtonWidth="12"
ScrollButtonHeight="12"

>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
  >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
        <ComponentArt:GridColumn DataField="IDrole" Visible="false" AllowEditing="False" />
        <ComponentArt:GridColumn DataField="IDroleToWhichGranted" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn 
				 DataField="BoolRegisteredNow" AllowEditing="True" HeadingText="."
 				 DataType='System.Boolean'
                    ColumnType='CheckBox' Width='35' FixedWidth='True' Visible="true"  />
	<ComponentArt:GridColumn DataField="c_u_idAdditionalBusRole" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="NAME_AdditionalBusRole" Visible="true" AllowEditing="False" HeadingText="Role" />
        <ComponentArt:GridColumn DataField="DATEexpir" Visible="false" AllowEditing="False" HeadingText="Expiration"/>
	<ComponentArt:GridColumn DataField="FORDISP_ExpirationDate" Visible="true" AllowEditing="False" HeadingText="Expiration" />
	<ComponentArt:GridColumn DataField="FORDISP_Recertification" Visible="true" AllowEditing="False" HeadingText="Recertification" />
        <ComponentArt:GridColumn DataField="c_u_RecertificationInterval" Visible="false" AllowEditing="False"/>
        <ComponentArt:GridColumn DataField="DATErecertstart" Visible="false" AllowEditing="False"/>
	<ComponentArt:GridColumn DataField="c_u_Comment" Visible="true" AllowEditing="False" HeadingText="Comments" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
 <ClientEvents>
    <ItemSelect EventHandler="EVT_GridSupplRoles_RowSelect" />
    <ItemCheckChange EventHandler="RespondToCheckboxChangeNATIVE" />
 </ClientEvents>
</ComponentArt:Grid>
</DIV>














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






<DIV ID="WRAPPER_combobox_rolechooser">
<ComponentArt:ComboBox 
  ID="ComboBoxChooseBRole" runat="server" CssClass="comboBox"
  RunningMode="Client"
  TextBoxEnabled="false"
  DataSourceID="SQL_allKnownBusRoles" DataTextField="c_u_Name" DataValueField="c_id" 
  HoverCssClass="comboBoxHover" 
  FocusedCssClass="comboBoxHover" TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown"
  ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
  DropHoverImageUrl="images/drop_hover.gif" DropImageUrl="images/drop.gif" 
  DropDownWidth="315" Width="315"
/>
</DIV>








<DIV ID="WRAPPER_GRIDroleowners">
<ComponentArt:Grid ID="GRIDroleowners" runat="server"
RunningMode="Client"
PageSize="5"
Width="650"
ShowHeader="False"
EnableViewState="True"
ShowFooter="False"
DataSourceID="SQL_roleowners"

ManualPaging="True"

ScrollBar="Auto"
ScrollTopBottomImagesEnabled="true"
ScrollTopBottomImageHeight="2"
ScrollTopBottomImageWidth="12"
ScrollBarCssClass="ScrollBar"
ScrollGripCssClass="ScrollGrip"
ScrollBarWidth="12"
ScrollButtonWidth="12"
ScrollButtonHeight="12"
ScrollImagesFolderUrl="images/scroller/"
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
	<ComponentArt:GridColumn DataField="RankName" Visible="true" AllowEditing="False" HeadingText="Type" />
	<ComponentArt:GridColumn DataField="RankCode" Visible="false" AllowEditing="False" />
        <ComponentArt:GridColumn DataField="c_u_Geography" Visible="true" AllowEditing="False" HeadingText="Geography"/>
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
 <ClientEvents>
    <ItemSelect EventHandler="EVT_GRIDroleowners_RowSelect" />
 </ClientEvents>
</ComponentArt:Grid>
</DIV>

</DIV>

</asp:Content>
