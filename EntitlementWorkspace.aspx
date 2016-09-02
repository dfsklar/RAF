<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Codebehind="EntitlementWorkspace.aspx.cs"
    Inherits="_6MAR_WebApplication.WebForm18" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src='prototype.js'></script>
    <script type="text/javascript" src='json2.js'></script>

    <script type="text/javascript" src='gridediting.js'></script>

    <script type="text/javascript" src="DHTMLX/dhtmlxCombo/codebase/dhtmlxcommon.js"></script>

    <script type="text/javascript" src="DHTMLX/dhtmlxCombo/codebase/dhtmlxcombo.js"></script>

    <script type="text/javascript" src="DHTMLX/dhtmlxCombo/codebase/ext/dhtmlxcombo_group.js"></script>

    <ComponentArt:Menu ID="GridContextMenu" SiteMapXmlFile="menuData.xml" ExpandSlide="none"
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
    <ComponentArt:Menu ID="GridContextMenuNOEDIT" SiteMapXmlFile="menuDataNOEDIT.xml"
        ExpandSlide="none" ExpandTransition="fade" ExpandDelay="250" CollapseSlide="none"
        CollapseTransition="fade" Orientation="Vertical" CssClass="MenuGroup" DefaultGroupCssClass="MenuGroup"
        DefaultItemLookId="DefaultItemLook" DefaultGroupItemSpacing="1" ImagesBaseUrl="images/"
        EnableViewState="false" ContextMenu="Custom" runat="server">
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="MenuItem" HoverCssClass="MenuItemHover"
                LabelPaddingLeft="15" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="3" />
            <ComponentArt:ItemLook LookId="BreakItem" ImageUrl="break.gif" CssClass="MenuBreak"
                ImageHeight="1" ImageWidth="100%" />
        </ItemLooks>
    </ComponentArt:Menu>
  

<!-- We are removing these from the gri:

AutoAdjustPageSize="false"

-->
   <div style="xxxheight:300px; width:100%" id="containerforgrid">
    <ComponentArt:Grid ID="Grid1" AllowHorizontalScrolling="false" runat="server" RunningMode="Client"
	 	  PageSize="15"					   FillContainer="true"
		  AllowTextSelection="true"
        DataSourceID="SqlDataSource1" Debug="false" ShowHeader="true" HeaderHeight="18"
        HeaderCssClass="GridHeader" ShowSearchBox="true" SearchTextCssClass="GridHeaderText"
        SearchOnKeyPress="true" AllowEditing="true" EditOnClickSelectedItem="false" AutoCallBackOnInsert="false"
        AutoCallBackOnUpdate="false" AutoCallBackOnDelete="false" CallbackReloadTemplates="false"
        AutoPostBackOnInsert="true" AutoPostBackOnUpdate="true" AutoPostBackOnDelete="true"
        AutoPostBackOnSelect="false" EnableViewState="true" GroupByTextCssClass="GroupByText"
        GroupByCssClass="GroupByCell"
	GroupBySortAscendingImageUrl="group_asc.gif" GroupBySortDescendingImageUrl="group_desc.gif"
        GroupBySortImageWidth="10" GroupBySortImageHeight="10" GroupingNotificationTextCssClass="GridHeaderText"
        GroupingPageSize="1" PreExpandOnGroup="true" CssClass="Grid" FooterCssClass="GridFooter"
        PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="41"
        PagerButtonHeight="22" SliderHeight="15" SliderWidth="150" SliderGripWidth="9"
        SliderPopupOffsetX="20" SliderPopupClientTemplateId="SliderTemplate" ScrollPopupClientTemplateId="ScrollPopupTemplate"
        PagerImagesFolderUrl="images/pager/"  ImagesBaseUrl="images/" KeyboardEnabled="true">
        <ClientEvents>
            <ContextMenu EventHandler="Grid1_onContextMenu" />
            <ItemSelect  EventHandler="Grid1_onItemSelect" />
        </ClientEvents>
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField" EditCommandClientTemplateId="EditCommandTemplate"
                InsertCommandClientTemplateId="InsertCommandTemplate">
                <ConditionalFormats>
                    <ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_EditStatus').Value=='N'"
                        RowCssClass="BlueRow" SelectedRowCssClass="SelectedRow" />
                    <ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_EditStatus').Value=='M'"
                        RowCssClass="RedRow" SelectedRowCssClass="SelectedRow" />
                </ConditionalFormats>
                <Columns>

<ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />

<ComponentArt:GridColumn DataField="c_u_EditStatus" HeadingText="Changes" Visible="true" AllowEditing="False"
         DataCellClientTemplateId="TEMPLeditstatusByIcon"
			FixedWidth="True"
			Width="50"
 />

                    <ComponentArt:GridColumn DataField="BusRolesLinked" Visible="false" AllowEditing="False" />

<ComponentArt:GridColumn DataField="FORDISP_BusRolesLinked" HeadingText="BusRoles" AllowEditing="False"
			FixedWidth="True"
			Width="70"
 />

                    <ComponentArt:GridColumn DataField="c_u_StandardActivity" HeadingText="StdActivity" />

                    <ComponentArt:GridColumn DataField="c_u_RoleType" HeadingText="RoleType" />

<ComponentArt:GridColumn DataField="c_u_Application" HeadingText="Appl" 
			FixedWidth="True"
			Width="50"
/>

                    <ComponentArt:GridColumn DataField="c_u_System" HeadingText="System" />

                    <ComponentArt:GridColumn DataField="c_u_Platform" HeadingText="Plat" />
                    <ComponentArt:GridColumn DataField="c_u_EntitlementName" HeadingText="EName" />
                    <ComponentArt:GridColumn DataField="c_u_EntitlementValue" HeadingText="EValue" />
                    <ComponentArt:GridColumn DataField="c_u_AuthObjValue" HeadingText="AuthObj" />
                    <ComponentArt:GridColumn DataField="c_u_FieldSecName" HeadingText="FieldSecN" />
                    <ComponentArt:GridColumn DataField="c_u_FieldSecValue" HeadingText="FieldSecV" />
                    <ComponentArt:GridColumn DataField="c_u_Commentary" HeadingText="Comments" TextWrap="true" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>
        <ClientTemplates>
        
            <ComponentArt:ClientTemplate ID="TEMPLeditstatusByIcon">
      <div style="font-family:verdana;">## RENDEReditstatus(DataItem) ##</div>
            </ComponentArt:ClientTemplate>


            <ComponentArt:ClientTemplate ID="EditTemplate">
                <a href="javascript:editGrid('## DataItem.ClientId ##');">Edit</a> | <a href="javascript:deleteRow('## DataItem.ClientId ##')">
                    Delete</a>
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
            <ComponentArt:GridServerTemplate ID="PickerTemplate">
                <Template>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td onmouseup="Button_OnMouseUp()">
                                <ComponentArt:Calendar ID="Picker1" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                    ControlType="Picker" SelectedDate="2005-9-13" ClientSideOnSelectionChanged="Picker1_OnDateChange"
                                    PickerCssClass="picker" />
                            </td>
                            <td style="font-size: 10px;">
                                &nbsp;</td>
                            <td>
                                <img id="calendar_from_button" alt="" onclick="Button_OnClick(this)" onmouseup="Button_OnMouseUp()"
                                    class="calendar_button" src="images/btn_calendar.gif" /></td>
                        </tr>
                    </table>
                </Template>
            </ComponentArt:GridServerTemplate>
        </ServerTemplates>
    </ComponentArt:Grid>
   </div>

<HR>

Preview of manifest string for current selection: 
<DIV ID='PreviewManifestString'>...</DIV>

<HR>


<asp:HiddenField ID="HIDDENidWS" runat="server" />

<!--
    <span style="font-size: 7pt">Height: </span>
    <asp:TextBox ID="GridCfg_Height" runat="server" Width="34px"></asp:TextBox>px <span
        style="font-size: 7pt">&nbsp;&nbsp; Num of visible rows: </span>
    <asp:TextBox ID="GridCfg_RowCnt" runat="server" OnTextChanged="GridCfg_RowCnt_TextChanged"
        Width="32px" Wrap="False"></asp:TextBox><span style="font-size: 7pt"> </span>
-->


<!--
    <asp:DropDownList ID="GridCfg_Mode" runat="server">
        <asp:ListItem Value="Group">Group Multi-Col</asp:ListItem>
        <asp:ListItem Value="Sort">Sort Multi-Col</asp:ListItem>
    </asp:DropDownList><span style="font-size: 7pt">&nbsp; Nav: </span>
    <asp:DropDownList ID="GridCfg_Nav" runat="server">
        <asp:ListItem>Pager</asp:ListItem>
        <asp:ListItem Selected="True">Scroller</asp:ListItem>
    </asp:DropDownList><span style="font-size: 7pt"> </span>
-->

<!--
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Redraw" />
-->
<!--
    <input id="Button6" type="button" value="New Row" onclick="MENUACTnewblankentitlement();"/>
-->

<!--
<a href="javascript:alert('Not yet implemented');">
   Submit for review by role owners</a> 

&bull; 
<a href="javascript:alert('Not yet implemented');">Publish</a> 
&bull; 
-->

<a href="export/workspace.ashx?id=<%= this.IDworkspace %>">Export to spreadsheet</a> 

&bull; 
<a onclick="javascript:return confirm('Are you sure you want to DELETE this workspace?');" href="utilities/CleaningCrew.ashx?cmd=WSdel&arg1=<%= this.IDworkspace %>">Delete</a>

&nbsp;<asp:SqlDataSource
            ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
            SelectCommand="SELECT dbo.f_relatedBusRoles(T.c_id) as BusRolesLinked, dbo.f_relatedBusRolesFmtOrdList(T.c_id) as FORDISP_BusRolesLinked, T.* FROM [t_RBSR_AUFW_u_WorkspaceEntitlement] T WHERE ([c_r_EditingWorkspace] = @c_r_EditingWorkspace)">
            <SelectParameters>
                <asp:QueryStringParameter Name="c_r_EditingWorkspace" QueryStringField="ID" Type="Int32"
                    DefaultValue="10" />
            </SelectParameters>
        </asp:SqlDataSource>
    <asp:SqlDataSource ID="ListAllKnownSystems" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="select distinct c_u_System FROM t_RBSR_AUFW_u_Entitlement order by c_u_System">
    </asp:SqlDataSource>
    <!--
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    DIALOG    D L G e d i t R o w
    
    
    -->
    <ComponentArt:Dialog FocusOnClick="false" ID="DLGeditRow" runat="server" Value=""
        Title="Entitlement Editor" ShowTransition="Fade" CloseTransition="Fade" TransitionDuration="5"
        ContentCssClass="contentCss" FooterCssClass="footerCss" HeaderCssClass="headerCss"
        CssClass="dialogCss" ModalMaskCssClass="modalMaskCssClass" HeaderClientTemplateId="header"
        ContentClientTemplateId="content" FooterClientTemplateId="footer" AllowDrag="true"
        AllowTextSelection="true" Alignment="MiddleCentre" Height="350" Width="500">
        <ClientEvents>
            <OnShow EventHandler="onShow_DLGeditRow" />
            <OnClose EventHandler="dialogclose" />
            <OnDrag EventHandler="dialogdrag" />
            <OnDrop EventHandler="dialogdrop" />
            <OnFocus EventHandler="dialogfocus" />
        </ClientEvents>
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="header">
                <table class="DLGEDITheader" onmousedown="DLGeditRow.StartDrag(event);">
                    <tr>
                        <td style="background-image: url(images/top.gif); padding: 10px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="middle" style="color: White; font-size: 15px; font-family: Arial; font-weight: bold;">
                                        ## Parent.Title ##</td>
                                    <td align="right">
                                        <img src="images/close.gif" onclick="DLGeditRow.Close('Close click');" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="footer">
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="content">
                <div id="DIVeditdlgcontent" class="DIVeditdlgcontentCLASS">
                    &nbsp;<table class="DIVeditdlgcontent">
                        <tr>
                            <td class="rightjust">
                                Std Activity:
                            </td>
                            <td>
                                <div id="DIVchooseSTDACT" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td rowspan="4" style='background-color: #FFFFFF'>
                                <div id="DIVmultichooseBRole">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                Role Type:
                            </td>
                            <td>
                                <div id="DIVchooseROLETYPE" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                Application/OS:
                            </td>
                            <td>
                                <div id="DIVchooseAPPL" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                System:
                            </td>
                            <td>
                                <div id="DIVchooseSYSTEM" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                Platform:
                            </td>
                            <td>
                                <div id="DIVchoosePLATFORM" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                Entitlement:
                            </td>
                            <td>
                                <div id="DIVchooseENAME" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                                <div id="DIVchooseEVALUE" class="HOLDERcombobox">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                AuthObj:
                            </td>
                            <td>
                                <div id="DIVchooseAUTHOBJ" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                Field Sec:
                            </td>
                            <td>
                                <div id="DIVchooseFLDSECN" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                                <div id="DIVchooseFLDSECV" class="HOLDERcombobox">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightjust">
                                Comments:
                            </td>
                            <td colspan="2">
                                <textarea id="DIVcommentary" rows='2' cols='30'></textarea>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr>
                    <center>
                        <input id="Button1" type="button" value="Submit" onclick="DLGEDITsubmit()" />
                        <input id="Button2" type="button" value="Cancel" onclick="DLGEDITcancel();" />
                    </center>
                </div>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
    </ComponentArt:Dialog>
    
    <!--
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   
    
    
    D L G m a i n t R o l e A s s i g n
    
    -->
    <ComponentArt:Dialog FocusOnClick="false" ID="DLGmaintRoleAssign" runat="server"
        Value="" Title="Modify current role assignment" ShowTransition="Fade" CloseTransition="Fade"
        TransitionDuration="5" ContentCssClass="contentCss" FooterCssClass="footerCss"
        HeaderCssClass="headerCss" CssClass="dialogCss" ModalMaskCssClass="modalMaskCssClass"
        HeaderClientTemplateId="ClientTemplate1" ContentClientTemplateId="ClientTemplate3"
        FooterClientTemplateId="ClientTemplate2" AllowDrag="true" Alignment="MiddleCentre"
        Height="250" Width="300">
        <ClientEvents>
            <OnShow EventHandler="onShow_DLGmaintRoleAssigns" />
            <OnClose EventHandler="dialogclose" />
            <OnDrag EventHandler="dialogdrag" />
            <OnDrop EventHandler="dialogdrop" />
            <OnFocus EventHandler="dialogfocus" />
        </ClientEvents>
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="ClientTemplate1">
                <table class="DLGEDITheader" onmousedown="DLGmaintRoleAssign.StartDrag(event);">
                    <tr>
                        <td style="background-image: url(images/top.gif); padding: 10px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="middle" style="color: White; font-size: 15px; font-family: Arial; font-weight: bold;">
                                        ## Parent.Title ##</td>
                                    <td align="right">
                                        <img src="images/close.gif" onclick="DLGmaintRoleAssign.Close('Close click');" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="ClientTemplate2">
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="ClientTemplate3">
                <div id="DIV1" class="DIVmaintroleassignContentCLASS">
                <p id="TXTexplainmaintrolecontrols">
                ....
                </p>
                    <div id="DIVmultichooseBRole2">
                    </div>
                    <br />
                    <hr />
                    <center>
                        <input id="Button4" type="button" value="Submit" onclick="DLGROLEMAINTsubmit()" />
                        <input id="Button5" type="button" value="Cancel" onclick="DLGROLEMAINTcancel();" />
                    </center>
                </div>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
    </ComponentArt:Dialog>
    <!--
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    UNUSED:
   
    -->
    
    <ComponentArt:Dialog FocusOnClick="false" ID="DLGassignRoles" runat="server" Value=""
        Title="Role Assigner" ShowTransition="Fade" CloseTransition="Fade" TransitionDuration="5"
        ContentCssClass="contentCss" FooterCssClass="footerCss" HeaderCssClass="headerCss"
        CssClass="dialogCss" ModalMaskCssClass="modalMaskCssClass" HeaderClientTemplateId="header2"
        FooterClientTemplateId="footer2" AllowDrag="true" Alignment="MiddleLeft" Height="130"
        Width="400" Style="left: 36px; top: 71px">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="header2">
                <table class="DLGEDITheader" style="width: 400" onmousedown="DLGassignRoles.StartDrag(event);">
                    <tr>
                        <td width="5">
                            <img style="display: block;" src="images/top_left.gif" /></td>
                        <td style="background-image: url(images/top.gif); padding: 10px;">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="middle" style="color: White; font-size: 15px; font-family: Arial; font-weight: bold;">
                                        ## Parent.Title ##</td>
                                    <td align="right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="5">
                            <img style="display: block;" src="images/top_right.gif" /></td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="footer2">
                <table class="DLGEDITfooter">
                    <tr>
                        <td width="5">
                            <img style="display: block;" src="images/bottom_left.gif" /></td>
                        <td style="background-image: url(images/bottom.gif); background-color: #F0F0F0;">
                            <img style="display: block;" src="images/spacer.gif" height="4" width="290" /></td>
                        <td width="5">
                            <img style="display: block;" src="images/bottom_right.gif" /></td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <Content>
            <div id="DIVdlgeditcontent" class="DIVdlgbrolesCLASS">
                WARNING: This action will impact ALL currently selected entitlement rows on the
                current webpage!
                <br />
                <ComponentArt:Grid ID="GRIDbusroles" runat="server" RunningMode="Client" DataSourceID="DSRCbusroles"
                    AllowPaging="false" Debug="true" ShowHeader="false" Height="200" Width="100%"
                    SearchTextCssClass="GridHeaderText" AllowEditing="false" EditOnClickSelectedItem="false"
                    AutoCallBackOnInsert="false" AutoCallBackOnUpdate="false" AutoCallBackOnDelete="false"
                    CallbackReloadTemplates="false" AutoPostBackOnInsert="true" AutoPostBackOnUpdate="false"
                    AutoPostBackOnDelete="true" AutoPostBackOnSelect="false" EnableViewState="true"
                    CssClass="Grid" FooterCssClass="GridFooter" ImagesBaseUrl="images/" KeyboardEnabled="true">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                            SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell">
                            <Columns>
                                <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
                                <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="Name" />
                                <ComponentArt:GridColumn DataField="c_u_Abbrev" HeadingText="Abbrev." />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                </ComponentArt:Grid>
                <hr />
                <center>
                    <input id="Button11" type="button" value="Submit" onclick="alert('Submit not implemented yet.');DLGassignRoles.Close();" />
                    <input id="Button22" type="button" value="Cancel" onclick="DLGassignRoles.Close();" />
                </center>
            </div>
        </Content>
    </ComponentArt:Dialog>
    <!--
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    D A T A S R C
    
    -->
    <asp:SqlDataSource ID="DSRCbusroles" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_BusRole] WHERE ([c_r_SubProcess] = @c_r_SubProcess)">
        <SelectParameters>
            <asp:SessionParameter Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
