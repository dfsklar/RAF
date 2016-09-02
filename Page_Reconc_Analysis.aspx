<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Page_Reconc_Analysis.aspx.cs" Inherits="_6MAR_WebApplication.WebForm128" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='Page_Reconc_Analysis.js'></script>
<script type="text/javascript" src='LookupEID.js'></script>
<script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>


<asp:SqlDataSource ID="SQL_difflist"
  OnSelecting="INTERCEPTsqldatasource_IDreport"
    runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="

SELECT ITEM.*, USSR.c_u_NameSurname, USSR.c_u_NameFirst, USSR.c_u_EID
FROM t_RBSR_AUFW_u_ReconcDiffItem ITEM
LEFT OUTER JOIN t_RBSR_AUFW_u_User USSR ON USSR.c_u_EID = ITEM.c_u_AssignedUser
WHERE ITEM.c_r_ReconcReport = @IDREPORT
ORDER BY ITEM.c_id

    ">
   <SelectParameters>
     <asp:Parameter Name="IDREPORT" Type="Int32"/>
   </SelectParameters>
</asp:SqlDataSource>







<ComponentArt:Grid ID="Grid1" AllowHorizontalScrolling="true" runat="server" 

RunningMode="Client"
ManualPaging="true"


PageSize="15"

Width="1000" 

AllowTextSelection="false"

DataSourceID="SQL_difflist"
Debug="false" 
ShowHeader="true" HeaderHeight="18"
HeaderCssClass="GridHeader" 

SearchText="Search (hit Enter to refresh):" SearchTextCssClass="GridHeaderText"
ShowSearchBox="true" 
SearchOnKeyPress="false"


AllowEditing="false" EditOnClickSelectedItem="false" AutoCallBackOnInsert="false"
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
                EditFieldCssClass="EditDataField" 
                InsertCommandClientTemplateId="InsertCommandTemplate">


<ConditionalFormats>
	<ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_Status').Value=='P'"
	 				    RowCssClass="RedRow"  
	 				    SelectedRowCssClass="SelectedRow" />
	<ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_Status').Value=='O'"
	 				    RowCssClass="GreyRow"  
	 				    SelectedRowCssClass="SelectedRow" />
	<ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_Status').Value=='i'"
	 				    RowCssClass="BlueRow"  
	 				    SelectedRowCssClass="SelectedRow" />
	<ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_Status').Value=='r'"
	 				    RowCssClass="BlueRow"  
	 				    SelectedRowCssClass="SelectedRow" />
</ConditionalFormats>



<Columns>
 <ComponentArt:GridColumn DataField="c_id" Visible="false" HeadingText="Seq" AllowEditing="False" />
 <ComponentArt:GridColumn DataField="c_u_EID" Visible="false" HeadingText="-" AllowEditing="False" />
 <ComponentArt:GridColumn
     DataField="c_u_Status" Visible="true" AllowEditing="False" HeadingText="Status" Width='40'
         DataCellClientTemplateId="TEMPLstatus"
 />
 <ComponentArt:GridColumn
     DataField="c_u_RoleName" Visible="true" AllowEditing="False" HeadingText="Role"  />
 <ComponentArt:GridColumn
     DataField="c_u_DiffType" Visible="true" AllowEditing="False" HeadingText="Action" Width='50' />
 <ComponentArt:GridColumn
     DataField="c_u_DiffObject" Visible="true" AllowEditing="False" HeadingText="Object" Width='55' />
 <ComponentArt:GridColumn
     DataField="c_u_Detail" Visible="true" AllowEditing="False" HeadingText="Detail"  />
 <ComponentArt:GridColumn
     DataField="c_u_NameSurname" Visible="true" AllowEditing="False" HeadingText="Assignee"  />
 <ComponentArt:GridColumn
     DataField="c_u_Comment" Visible="true" AllowEditing="False" HeadingText="CMT" 
         DataCellClientTemplateId="TEMPLcomment"
 />
</Columns>



            </ComponentArt:GridLevel>
        </Levels>

		  <ClientEvents>
		      <ContextMenu EventHandler="Grid_onContextMenu" />
		  </ClientEvents>



        <ClientTemplates>

	    <ComponentArt:ClientTemplate ID="TEMPLcomment">   
	       ## (DataItem.GetMember("c_u_Comment").Value) ?
	       	  "<IMG onmouseout='hoverhide()' onmouseover='hovershow(this,\\"" +
		      DataItem.ClientId + "\\")' src='images/msg_unread.gif' border=0/>"
		  :
		  "" ##
	    </ComponentArt:ClientTemplate>


	    <ComponentArt:ClientTemplate ID="TEMPLstatus">   
	       <IMG src='images/reconcstatus_##(DataItem.GetMember("c_u_Status").Value).toLowerCase()##.gif' border=0/>
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





<ComponentArt:Menu ID="ContextMenu" SiteMapXmlFile="CTXMENU_GRID_reconcanalysis.xml" ExpandSlide="none"
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


<div id='hoverpopup' 
   style='text-align:left;visibility:hidden; position:absolute; top:0; left:0; width:180px;height:100px; padding:17px; background-color:#CCCCFF;filter:alpha(opacity=90)'>
  <span id='_hoverpopup'></span>
</div>






<!-- ****************************************** -->

<!-- DIALOG:  assignment of a human to a row -->

<div id="JQDLGassign" class='JQdialog' title='Assignment'>



<table class='dlgcustomgrid'>

<input type='hidden' id="reconcdiff_HIDDEN_ID" name="editroleowner_HIDDEN_ID" value=''>

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


<tr ID="ROW_editroleowner_CHK_DEL">
<TD>DELETE:</TD>
<TD>
<input type="checkbox" id="editroleowner_CHK_DEL">
Check this box to request deletion of this assignment.
</TD>
</tr>


</table>


</div>



</asp:Content>
