<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListBRoles.aspx.cs" Inherits="_6MAR_WebApplication.WebForm16" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='ListBRoles.js'></script>

<script type="text/javascript">
   var GLOBALpageIsEditable = <%= ((this.session.idWorkspace==int.Parse(Session["INTcurWS"].ToString())) && (this.session.isWorkspaceOwner)) ? 1 : 0 %>;
   var GLOBALidworkspace = <%= Session["INTcurWS"] %>;
</script>





<asp:Panel ID="PANELcond_Editable" runat="server" Width="788px" Wrap="False">

<B>YOU ARE LOGGED INTO A WORKSPACE.</B>
<BR/>
It is safe for you to perform role design (entitlement allocation) without affecting "live" entitlements.
<br />
<p style='text-align:right;margin-bottom:4px'>
<a target="AFWACexport" href="export/EntitlementsPerSubpr.ashx?id=<%=Session["INTcurWS"]%>">Export</a>
&nbsp;&nbsp;
<a href="javascript:Grid1.Table.AddRow();">Add row</a>
&nbsp;&nbsp;
<a href="javascript:PublishWorkspace(<%=this.session.idWorkspace%>);">Publish</a>
&nbsp;&nbsp;
<a href="javascript:LaunchChangeMgmt(<%=this.session.idWorkspace%>);">ChangeMgmt</a>

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
<b>THERE IS NO WORKSPACE FOR THIS SUBPROCESS.</b>
<BR/>
This subprocess has not been given any role designs yet.<BR/>
To start designing, please visit the "History" pane and initiate a workspace.
</asp:Panel>





<asp:SqlDataSource ID="SQL_allKnownBusRoles" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
  SelectCommand="
SELECT *
FROM t_RBSR_AUFW_u_BusRole
ORDER BY c_u_Name"
/>



<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
  SelectCommand="

SELECT *,
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
) as KOUNT
FROM t_RBSR_AUFW_u_BusRole BROLE WHERE ([c_r_SubProcess] = @c_r_SubProcess) ORDER BY [c_u_Name]
">
 <SelectParameters>
     <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS" Type="Int32" />
     <asp:SessionParameter DefaultValue="-3" Name="IDWS" SessionField="INTcurWS" Type="Int32" />
 </SelectParameters>
</asp:SqlDataSource>






<ComponentArt:Grid ID="Grid1" runat="server"

EnableViewState="true"

RunningMode="Client"


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
                    <ComponentArt:GridColumn DataField="c_id" HeadingText="Action" DataCellClientTemplateId="TEMPLdesignLinkage"
						  									EditControlType="EditCommand" 
						  									Width='138' FixedWidth='True'/>
                    <ComponentArt:GridColumn DataField="c_id" HeadingText="ID" Visible="false" AllowEditing="False" />
                    <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="Name" />
                    <ComponentArt:GridColumn DataField="c_u_Description" HeadingText="Description" TextWrap="true"/>
                    <ComponentArt:GridColumn DataField="c_u_OwnerPrimaryEID" HeadingText="Owner" Width='32' TextWrap="true" AllowEditing="False"/>
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
	 <ComponentArt:GridColumn DataField="c_u_EventType" Visible="true" AllowEditing="False"
	 			  HeadingText="Event" />
	 <ComponentArt:GridColumn DataField="c_u_Who" Visible="true" AllowEditing="False" 
	 			  HeadingText="Who"/>
	 <ComponentArt:GridColumn DataField="c_u_TimeStamp" Visible="true" AllowEditing="False" 
	 			  FormatString="yyyy-MMM-dd"
	 			  HeadingText="When"/>
	 <ComponentArt:GridColumn DataField="c_u_Commentary" Visible="true" AllowEditing="False"
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

</div>




    





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

<asp:HiddenField runat="server" id="HIDDENcurbrole" value="218"/>


<div id="JQDLGsupplementalRoleList" class='JQdialog' title='Additional Roles Allowed for _____'>
<A href='javascript:GRIDeditaddrole_LaunchJQdialog_ADDNEW();'>Add</A>
</div>




<ComponentArt:Grid ID="GRIDadditionalRoles" runat="server"
RunningMode="Client"
EnableViewState="true"
PageSize="5"
AllowEditing="false"
Width="900"
Debug="false"
ShowHeader="False"
ShowFooter="False"
DataSourceID="SQL_additionalRoles"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
  >
       <Columns>
         <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	 <ComponentArt:GridColumn DataField="c_u_idAdditionalBusRole" Visible="false" AllowEditing="False" />
	 <ComponentArt:GridColumn DataField="NAME_AdditionalBusRole" Visible="true" AllowEditing="False" HeadingText="Role" />
	 <ComponentArt:GridColumn DataField="c_u_ExpirationDate" Visible="true" AllowEditing="False" HeadingText="Expiration" />
	 <ComponentArt:GridColumn DataField="FORDISP_Recertification" Visible="true" AllowEditing="False" HeadingText="Recertification" />
	 <ComponentArt:GridColumn DataField="c_u_Comment" Visible="true" AllowEditing="False" HeadingText="Comments" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
</ComponentArt:Grid>




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
   <td>Role Description:</td>
   <td><input type='text' id='JQDLGbp_descr' name='JQDLGbp_descr' value='' size='50'></input></td>
   </tr>
   <tr>
   <td>Primary Owner:</td>
   <td>
	EID: <input type='text' id='JQDLGbp_primown_eid' name='JQDLGbp_primown_eid' value=''></input> 
        <span class='fakelink' onclick="javascript:LookupEID('JQDLGbp_primown_eid','JQDLGbp_primown_name');">Lookup</span>
	<BR>
	Surname, First: <input type='text' id='JQDLGbp_primown_namelast' name='JQDLGbp_primown_namelast' value=''></input> , 
		 <input type='text' id='JQDLGbp_primown_namefirst' name='JQDLGbp_primown_namefirst' value=''></input>
   </td>
   </tr>
   <tr>
   <td>Secondary Owner:</td>
   <td>
	EID: <input type='text' id='JQDLGbp_secown_eid' name='JQDLGbp_secown_eid' value=''></input>
        <span class='fakelink' onclick="javascript:LookupEID('JQDLGbp_secown_eid','JQDLGbp_secown_name');">Lookup</span>
	<BR>
	Surname, First: <input type='text' id='JQDLGbp_secown_namelast' name='JQDLGbp_secown_namelast' value=''></input> , 
		 <input type='text' id='JQDLGbp_secown_namefirst' name='JQDLGbp_secown_namefirst' value=''></input>
   </td>
   </tr>

   <tr>
   <td>Design notes:</td>
   <td>
	<TEXTAREA id='JQDLGbp_designdetails' name='JQDLGbp_designdetails' rows='3' cols='80'></TEXTAREA>
   </td>
   </tr>

   <tr>
   <td colspan='2'>




   </td>
   </tr>

   </table>


   </form>
   </div>






<asp:SqlDataSource
    ID="SQL_additionalRoles" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="

SELECT ABR.*, BR.c_u_Name AS NAME_AdditionalBusRole, 
ABR.c_u_RecertificationInterval + ' ' + CONVERT(VARCHAR,ABR.c_u_RecertificationStartDate, 101) as FORDISP_Recertification
FROM t_RBSR_AUFW_u_AdditionalBusRole ABR 
LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole BR ON BR.c_id=ABR.c_u_idAdditionalBusRole  WHERE c_r_BusRole = @FILTERBROLE

">
            <SelectParameters>
                <asp:ControlParameter Name="FILTERBROLE"  Controlid="HIDDENcurbrole" PropertyName="value"/>
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
<!--
<A NAME="LOCdate" ID="LOCdate"></A>
onClick="CALP.select($('editaddrole_INPUT_EXPIRDATE'),'LOCdate','yy-NNN-dd'); return false;"
-->
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

</table>
</div>


<!-- <A href="javascript:JQdeclareDialogs();">Declare dialogs</A> -->


<DIV ID="HIDDEN_CLOSET" style='visibility:hidden'>
<DIV ID="WRAPPER_combobox_rolechooser">
<ComponentArt:ComboBox 
  ID="ComboBoxChooseBRole" runat="server" CssClass="comboBox"
  RunningMode="Client"
  TextBoxEnabled="false"
  DataSourceID="SQL_allKnownBusRoles" DataTextField="c_u_Name" DataValueField="c_id" 
  HoverCssClass="comboBoxHover" 
  FocusedCssClass="comboBoxHover" TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown"
  ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
  DropHoverImageUrl="images/drop_hover.gif" DropImageUrl="images/drop.gif" DropDownWidth="250"
  Width="240"/>
</DIV>
</DIV>

</asp:Content>
