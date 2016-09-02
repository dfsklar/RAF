<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Codebehind="TestWEBUIdatagrid.aspx.cs"
    Inherits="_6MAR_WebApplication.SandboxEditor" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<script type="text/javascript" src='gridediting.js'>
</script>


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




    <ComponentArt:Grid ID="Grid1" runat="server" 

	    RunningMode="Client"
       
       DataSourceID="SqlDataSource1"

 		 Debug="false"

       ShowHeader="true" Height="400" Width="900"
       HeaderCssClass="GridHeader" ShowSearchBox="true" SearchTextCssClass="GridHeaderText"


       SearchOnKeyPress="true" 


       GroupByCssClass="GroupByCell"

       AllowEditing="true"

        EditOnClickSelectedItem="false"

        AutoCallBackOnInsert="false"
        AutoCallBackOnUpdate="false"
        AutoCallBackOnDelete="false"
        AutoCallBackOnSelect="true"

        CallbackReloadTemplates="false"

        AutoPostBackOnInsert="true"
        AutoPostBackOnUpdate="true"
        AutoPostBackOnDelete="true"
        AutoPostBackOnSelect="false"

		  EnableViewState="true"

        GroupByTextCssClass="GroupByText" GroupingPageSize="5" PreExpandOnGroup="false"
        GroupingNotificationTextCssClass="GridHeaderText" GroupBySortAscendingImageUrl="group_asc.gif"
        GroupBySortDescendingImageUrl="group_desc.gif" GroupBySortImageWidth="10" GroupBySortImageHeight="10"

        CssClass="Grid" FooterCssClass="GridFooter" PagerStyle="Slider" PagerTextCssClass="GridFooterText"
        PagerButtonWidth="41" PagerButtonHeight="22" SliderHeight="15" SliderWidth="150"
        SliderGripWidth="9" SliderPopupOffsetX="20" SliderPopupClientTemplateId="SliderTemplate"
        PagerImagesFolderUrl="images/pager/" PageSize="20" 

		  ImagesBaseUrl="images/" 

        KeyboardEnabled="true">
        <ClientEvents>
            <ContextMenu EventHandler="Grid1_onContextMenu" />
            <ItemBeforeInsert EventHandler="Grid1_onItemBeforeInsert" />
            <ItemBeforeUpdate EventHandler="Grid1_onItemBeforeUpdate" />
            <ItemBeforeDelete EventHandler="Grid1_onItemBeforeDelete" />
        </ClientEvents>
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField" EditCommandClientTemplateId="EditCommandTemplate"
                InsertCommandClientTemplateId="InsertCommandTemplate">
                <Columns>
                    <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
                    <ComponentArt:GridColumn DataField="SIM" HeadingText="BusRoles" AllowEditing="False"
                        TextWrap="true" />
                    <ComponentArt:GridColumn DataField="c_u_StandardActivity" HeadingText="StdActivity" />
                    <ComponentArt:GridColumn DataField="c_u_RoleType" HeadingText="RoleType" />
                    <ComponentArt:GridColumn DataField="c_u_System" HeadingText="Sys" 
 EditControlType="Custom"
 EditCellServerTemplateId="ComboChooseSystem"
 CustomEditSetExpression="setCategory(DataItem)"
 CustomEditGetExpression="getCategory()"
 />
                    <ComponentArt:GridColumn DataField="c_u_Platform" HeadingText="Plat" />
                    <ComponentArt:GridColumn DataField="c_u_EntitlementName" HeadingText="EName" />
                    <ComponentArt:GridColumn DataField="c_u_EntitlementValue" HeadingText="EValue" />
                    <ComponentArt:GridColumn DataField="c_u_AuthObjValue" HeadingText="AuthObj" />
                    <ComponentArt:GridColumn DataField="c_u_FieldSecName" HeadingText="FieldSecN" />
                    <ComponentArt:GridColumn DataField="c_u_FieldSecValue" HeadingText="FieldSecV" />
                    <ComponentArt:GridColumn DataField="c_u_Commentary" HeadingText="Comments" TextWrap="true" />
                    <ComponentArt:GridColumn AllowSorting="false" HeadingText="Edit Command" DataCellClientTemplateId="EditTemplate"
                        EditControlType="EditCommand" Width="100" Align="Center" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>
        <ClientTemplates>
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
                        <td style="background-color: #CAC6D4; padding: 2px;" align="center">
                            <img src="images/## DataItem.GetMember('c_u_EntitlementValue').Value ##" width="12"
                                height="12" border="0"></td>
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
<asp:Label runat="server" Text='A' />
<asp:Label runat="server" Text='<%# DateTime.Now %>' />
<asp:Label runat="server" Text='<%# Container.DataItem["c_u_System"] %>' />
<asp:Label runat="server" Text='<%# Container.DataItem[1] %>' />


                    <ComponentArt:ComboBox ID="ComboBoxChooseSystem1" runat="server" CssClass="comboBox" 
						      TextBoxEnabled="false"
								DataSourceID="ListAllKnownSystems"
								DataTextField="c_u_System"
								DataValueField="c_u_System" 
                        HoverCssClass="comboBoxHover" RunningMode="CallBack" FocusedCssClass="comboBoxHover"
                        TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
                        DropImageUrl="images/drop.gif" DropDownWidth="250" Width="120" />
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

<P>CURRENTLY RUNNING IN "CLIENT" MODE because the search box doesn't work in Callback mode yet.

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT dbo.f_relatedBusRoles(T.c_id) as SIM, T.* FROM [t_RBSR_AUFW_u_WorkspaceEntitlement] T WHERE ([c_r_EditingWorkspace] = @c_r_EditingWorkspace)">
        <SelectParameters>
            <asp:QueryStringParameter Name="c_r_EditingWorkspace" QueryStringField="EWS" Type="Int32"
                DefaultValue="9" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ListAllKnownSystems" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="select distinct c_u_System FROM t_RBSR_AUFW_u_Entitlement order by c_u_System">
    </asp:SqlDataSource>
</asp:Content>
