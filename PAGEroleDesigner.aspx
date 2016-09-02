<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGEroleDesigner.aspx.cs" Inherits="_6MAR_WebApplication.WebForm114" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src='PAGEroleDesigner.js'></script>

<TABLE style='width:100%'>
<TR>
<TD style='text-align:left;vertical-align:top'>
<B><A href='ListBRoles.aspx'>&lt;&lt;&lt; </A></B> &nbsp;
<b><%= this.brole.Name %></b>  &nbsp;
<B><A href='PAGEroleDesAppList.aspx'>&lt;&lt;&lt;</A></B> &nbsp;
<b><%= this.applDetails.Name %></b>
</TD>
<TD style='text-align:right;vertical-align:top'>

<asp:Panel ID="PANELcond_readonly" runat="Server">
<SPAN style='color:blue; font-weight:bold'>READ-ONLY VIEW</SPAN> &nbsp; &nbsp;
</asp:Panel>

<asp:Panel ID="PANELcond_changesExist" runat="Server">
<SPAN style='color:red; font-weight:bold' ID='SPANalertUnsavedChanges'>UNSAVED CHANGES EXIST</SPAN>
<INPUT type='Button' id="BTNsubmitChgs" Value="Submit" onClick="javascript:Submit();" />
<INPUT type='Button' id="BTNcancelChgs" Value="Cancel" 
    onClick="javascript:window.location='PAGEroleDesAppList.aspx';"/>
</asp:Panel>

<asp:Panel ID="PANELcond_goback" runat="Server">
</asp:Panel>

</TD>
</TR>
</TABLE>






   <div style="xxxheight:300px; width:100%" id="containerforgrid">


<ComponentArt:Grid ID="Grid1" AllowHorizontalScrolling="true" runat="server" 

RunningMode="Client"
ManualPaging="true"
FillContainer="true"

PageSize="15"

AllowTextSelection="true"


	
        DataSourceID="SQL_entitlementList" Debug="false" ShowHeader="true" HeaderHeight="18"
        HeaderCssClass="GridHeader" 


SearchText="Search (hit Enter to refresh):" SearchTextCssClass="GridHeaderText"
ShowSearchBox="true" 
SearchOnKeyPress="false"


AllowEditing="true" EditOnClickSelectedItem="false" AutoCallBackOnInsert="false"
        AutoCallBackOnUpdate="false" AutoCallBackOnDelete="false" CallbackReloadTemplates="false"
        AutoPostBackOnInsert="false" AutoPostBackOnUpdate="false" AutoPostBackOnDelete="false"
        AutoPostBackOnSelect="false" EnableViewState="true"



GroupBySectionCssClass="grp"
GroupBySectionSeparatorCssClass=""
GroupingNotificationTextCssClass="GridHeaderText"
GroupingNotificationText="Drag a column header to this area to group by it."

GroupByCssClass="GroupByCell"
GroupByTextCssClass="GroupByText"
GroupBySortAscendingImageUrl="group_asc.gif" GroupBySortDescendingImageUrl="group_desc.gif"
GroupBySortImageWidth="10" GroupBySortImageHeight="10" 

TreeLineImagesFolderUrl="images/lines/"
TreeLineImageWidth="19"
TreeLineImageHeight="20"
IndentCellWidth="16"
        
GroupingMode="ConstantRows"
PreExpandOnGroup="false" 


CssClass="Grid" FooterCssClass="GridFooter"
        PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="41"
        PagerButtonHeight="22" SliderHeight="15" SliderWidth="150" SliderGripWidth="9"


        SliderPopupOffsetX="20" SliderPopupClientTemplateId="SliderTemplate" ScrollPopupClientTemplateId="ScrollPopupTemplate"
        PagerImagesFolderUrl="images/pager/"  ImagesBaseUrl="images/" KeyboardEnabled="true">
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField" EditCommandClientTemplateId="EditCommandTemplate"
                InsertCommandClientTemplateId="InsertCommandTemplate">

                <Columns>

		<ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
		<ComponentArt:GridColumn DataField="TEassRowId" Visible="false" AllowEditing="False" />

		<ComponentArt:GridColumn DataField="TEassEditStatus"
		                       DataCellClientTemplateId="TEMPLeditstatusByIcon"
				       Visible="true" AllowEditing="False" HeadingText="Stat" />

		<ComponentArt:GridColumn DataField="INTeditStatus" HeadingText="Chg" Visible="true" AllowEditing="False"
		   DataCellClientTemplateId="TEMPLchgstatusByIcon"
			FixedWidth="True"
			Width="35"
			 />

		<ComponentArt:GridColumn DataField="BOOLallocatedForThisRole" HeadingText="Alloc" Visible="true"
		   DataType='System.Boolean' ColumnType='CheckBox'		
			FixedWidth="True"
			Width="35"
			 />

                    <ComponentArt:GridColumn DataField="c_u_StandardActivity" HeadingText="StdActivity" />

                    <ComponentArt:GridColumn DataField="c_u_RoleType" HeadingText="RoleType" />

                    <ComponentArt:GridColumn DataField="c_u_System" HeadingText="System" />

                    <ComponentArt:GridColumn DataField="c_u_Platform" HeadingText="Plat" />
                    <ComponentArt:GridColumn DataField="c_u_EntitlementName" HeadingText="EName" />
                    <ComponentArt:GridColumn DataField="c_u_EntitlementValue" HeadingText="EValue" />
                    <ComponentArt:GridColumn DataField="c_u_AuthObjValue" HeadingText="AuthObj" />
                    <ComponentArt:GridColumn DataField="c_u_FieldSecName" HeadingText="FieldSecN" />
                    <ComponentArt:GridColumn DataField="c_u_FieldSecValue" HeadingText="FieldSecV" />

                    <ComponentArt:GridColumn DataField="c_u_Commentary" HeadingText="Comments" TextWrap="true" />

		    <ComponentArt:GridColumn DataField="c_u_Level4SecName" HeadingText="Level4N" Width="80" />
		    <ComponentArt:GridColumn DataField="c_u_Level4SecValue" HeadingText="Level4V" Width="80" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>

		  <ClientEvents>
		  <ItemCheckChange EventHandler="RespondToCheckboxChangeNATIVE" />
		  <Load EventHandler="REACTgridDoneLoading" />
		  </ClientEvents>



        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="TEMPLchgstatusByIcon">
	          <div style="font-family:verdana;">## RENDERchgstatus(DataItem) ##</div>
            </ComponentArt:ClientTemplate>

        
            <ComponentArt:ClientTemplate ID="TEMPLeditstatusByIcon">
      <div style="font-family:verdana;">## RENDEReditstatus(DataItem) ##</div>
            </ComponentArt:ClientTemplate>



            <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                <a href="javascript:editRow();">Update</a> | <a href="javascript:Grid1.EditCancel();">
                    Cancel</a>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="InsertCommandTemplate">
                <a href="javascript:insertRow();">Insert</a> | <a href="javascript:Grid1.EditCancel();">
                    Cancel</a>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="font-size: 10px;">
                            Loading...&nbsp;</td>
                        <td>
                            <img src="images/spinner.gif" width="16" height="16" border="0"></td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="ScrollPopupTemplate">
                <table cellspacing="0" cellpadding="2" border="0" class="ScrollPopup">
                    <tr>
                        <td style="width: 125px; padding: 5px">
                            <div style="font-size: 10px; font-family: MS Sans Serif; text-overflow: ellipsis;
                                overflow: hidden;">
                                <nobr>## DataItem.GetMember("c_u_System").Value ##</nobr>
                            </div>
                        </td>
                        <td style="width: 125px; padding: 5px">
                            <div style="font-size: 10px; font-family: MS Sans Serif; text-overflow: ellipsis;
                                overflow: hidden;">
                                <nobr>## DataItem.GetMember("c_u_Platform").Value ##</nobr>
                            </div>
                        </td>
                        <td style="width: 125px; padding: 5px" align="right">
                            <div style="font-size: 10px; font-family: MS Sans Serif; text-overflow: ellipsis;
                                overflow: hidden;">
                                <nobr>##  DataItem.GetMember("c_u_EntitlementName").Value ##</nobr>
                            </div>
                        </td>
                    </tr>
                </table>
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
                                                <td style="font-family: verdana; font-size: 11px;">
                                                    <div style="overflow: hidden; width: 115px;">
                                                        <nobr>## DataItem.GetMember('c_u_System').Value ##</nobr>
                                                    </div>
                                                </td>
                                                <td style="font-family: verdana; font-size: 11px;">
                                                    <div style="overflow: hidden; width: 135px;">
                                                        <nobr>## DataItem.GetMember('c_u_Platform').Text ##</nobr>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="230" colspan="2" style="font-family: verdana; font-size: 11px; font-weight: bold;">
                                                                <div style="text-overflow: ellipsis; overflow: hidden; width: 250px;">
                                                                    <nobr>## DataItem.GetMember('c_u_EntitlementName').Value ##</nobr>
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
        <ServerTemplates>
            <ComponentArt:GridServerTemplate ID="ComboChooseSystem">
                <Template>
                    <ComponentArt:ComboBox ID="ComboBoxChooseSystem1" runat="server" CssClass="comboBox"
                        TextBoxEnabled="false" DataSourceID="ListAllKnownSystems" DataTextField="c_u_System"
                        DataValueField="c_u_System" HoverCssClass="comboBoxHover" RunningMode="CallBack"
                        FocusedCssClass="comboBoxHover" TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown"
                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                        DropHoverImageUrl="images/drop_hover.gif" DropImageUrl="images/drop.gif" DropDownWidth="250"
                        Width="120" />
                </Template>
            </ComponentArt:GridServerTemplate>
            <ComponentArt:GridServerTemplate ID="ComboBoxTemplate">
                <Template>
                    <ComponentArt:ComboBox ID="ComboBox1" runat="server" CssClass="comboBox" TextBoxEnabled="false"
                        HoverCssClass="comboBoxHover" SelectedIndex="4" FocusedCssClass="comboBoxHover"
                        TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
                        DropImageUrl="images/drop.gif" Width="120" />
                </Template>
            </ComponentArt:GridServerTemplate>
        </ServerTemplates>
    </ComponentArt:Grid>
   </div>


   <asp:SqlDataSource ID="SQL_applicationList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT c_id, c_u_Name FROM [t_RBSR_AUFW_u_Application] ORDER BY [c_u_Name]">
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="SQL_entitlementList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="
SELECT 
TENT.*, 
TEASS.c_id as TEassRowId, 
TEASS.c_u_Status as TEassEditStatus,
CASE WHEN (TEASS.c_id > 0) AND (TEASS.c_u_Status NOT IN ('X')) THEN 'True' ELSE 'False' END as BOOLallocatedForThisRole, 
0 as INTeditStatus 
FROM 
   t_RBSR_AUFW_u_Entitlement TENT
LEFT OUTER JOIN 
   t_RBSR_AUFW_u_EntAssignment TEASS 
    ON 
            TEASS.c_r_EntAssignmentSet = @FILTERWS
        AND TEASS.c_r_BusRole = @FILTERBROLE
        AND TEASS.c_r_Entitlement = TENT.c_id
WHERE TENT.c_u_Application = @FILTERAPP AND TENT.c_u_Status='A'
">
            <SelectParameters>
                <asp:SessionParameter Name="FILTERAPP"    SessionField="STRcurAppScope" Type="String" DefaultValue="NONONONONO" />
                <asp:SessionParameter Name="FILTERBROLE"  SessionField="STRcurBusRoleScope" Type="String" DefaultValue="-1" />
                <asp:SessionParameter Name="FILTERWS"     SessionField="INTcurWS" Type="String" DefaultValue="-1" />
            </SelectParameters>
   </asp:SqlDataSource>


</asp:Content>
