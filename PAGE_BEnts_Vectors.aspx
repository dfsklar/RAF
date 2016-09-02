<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_6MAR_WebApplication.WebForm117" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src='json2.js'></script>
    <script type="text/javascript" src='PAGE_BEnts_Vectors.js'></script>
    <script type="text/javascript" src='gridediting.js'></script>
    <script type="text/javascript" src="DHTMLX/dhtmlxCombo/codebase/dhtmlxcommon.js"></script>
    <script type="text/javascript" src="DHTMLX/dhtmlxCombo/codebase/dhtmlxcombo.js"></script>
    <script type="text/javascript" src="DHTMLX/dhtmlxCombo/codebase/ext/dhtmlxcombo_group.js"></script>



    <ComponentArt:Menu ID="GridContextMenu" SiteMapXmlFile="menuData_PAGE_BEnts_Vectors.xml" ExpandSlide="none"
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



<!-- WARNING: the only reason this use of a sessionparam works is because
     a POSTback is being done each time a new application is selected! 
     A CALLback will not work: the new session param value will not fully be
     recognized by this same page. -->
<asp:SqlDataSource
    ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    SelectCommand="

SELECT ENTIT.*,
   
  (SELECT COUNT(*) FROM t_RBSR_AUFW_u_EntAssignment EA 
LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet EASET
   ON EA.c_r_EntAssignmentSet = EASET.c_id
   WHERE
   EASET.c_u_Status IN ('ACTIVE')
   AND
   EA.c_u_Status NOT IN ('X') 
   AND
   EA.c_r_Entitlement = ENTIT.c_id
  ) as UsageKount

 FROM t_RBSR_AUFW_u_Entitlement ENTIT WHERE c_u_Application = @FILTERAPP

">
            <SelectParameters>
                <asp:SessionParameter Name="FILTERAPP"    SessionField="STRcurAppScope" Type="String" DefaultValue=".." />
            </SelectParameters>
</asp:SqlDataSource>



    <!--
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    DIALOG    D L G e d i t R o w
    
    
    -->
    <ComponentArt:Dialog FocusOnClick="false" ID="DLGeditRow" runat="server" Value=""
        Title="Entitlement Editor" ShowTransition="Fade" 
        ContentCssClass="contentCss" FooterCssClass="footerCss" HeaderCssClass="headerCss"
        CssClass="dialogCss" ModalMaskCssClass="modalMaskCssClass" HeaderClientTemplateId="header"
        ContentClientTemplateId="content"  AllowDrag="true"
        AllowTextSelection="true" Alignment="MiddleCentre" Height="380" Width="500">
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
                                Std Activity:
                            </td>
                            <td>
                                <div id="DIVchooseSTDACT" class="HOLDERcombobox">
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
                                Level 4:
                            </td>
                            <td>
                                <div id="DIVchooseL4N" class="HOLDERcombobox">
                                </div>
                            </td>
                            <td>
                                <div id="DIVchooseL4V" class="HOLDERcombobox">
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
<hr/>
<A href='javascript:RefreshManifestVerificationString();'>(Re)compute privilege</A> &gt;&gt;
<span id="DLGentitelementEditor_manifestverif"></span>

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
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    DIALOG    D L G c h g S t a t u s
    
    
    -->
    <ComponentArt:Dialog FocusOnClick="false" ID="DLGchgStatus" runat="server" Value=""
        Title="Entitlement Editor" ShowTransition="Fade" 
        ContentCssClass="contentCss" FooterCssClass="footerCss" HeaderCssClass="headerCss"
        CssClass="dialogCss" ModalMaskCssClass="modalMaskCssClass" HeaderClientTemplateId="headerChgstat"
        ContentClientTemplateId="contentChgstat"  AllowDrag="true"
        AllowTextSelection="true" Alignment="MiddleCentre" Height="150" Width="600">
        <ClientEvents>
            <OnShow EventHandler="onShow_DLGchgStatus" />
            <OnClose EventHandler="dialogclose" />
            <OnDrag EventHandler="dialogdrag" />
            <OnDrop EventHandler="dialogdrop" />
            <OnFocus EventHandler="dialogfocus" />
        </ClientEvents>
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="headerChgstat">
                <table class="DLGEDITheader" onmousedown="DLGchgStatus.StartDrag(event);">
                    <tr>
                        <td style="background-image: url(images/top.gif); padding: 10px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="middle" style="color: White; font-size: 15px; font-family: Arial; font-weight: bold;">
                                        Change Status</td>
                                    <td align="right"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="footerChgstat">
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="contentChgstat">
                <div class="DIVeditchgstatcontentCLASS">
<p>
 You will be changing the status value for all currently selected rows.</p>
<p>
 The number of rows that will be affected: <b><span id="FILLINnumrowsaffected">...</span></b>
<p>
<p>
CHANGE STATUS FROM 
&nbsp; &nbsp; 
<b><span id="FILLINoldstatus"></span></B>
&nbsp; &nbsp; 

TO 

&nbsp; &nbsp; 

<b>
<select id="CHOOSEnewstatus">
   <option value='P'>P (pending)</option>
   <option value='A'>A (active)</option>
   <option value='I'>I (inactive)</option>
   <option value='X'>X (deleted)</option>
</select>

		<p>
                        <input id="Button1" type="button" value="Submit" onclick="DLGCHGSTATsubmit()" />
                        <input id="Button2" type="button" value="Cancel" onclick="DLGCHGSTATcancel();" />
                    </center>
                </div>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
    </ComponentArt:Dialog>



<CENTER>
<table>
<tr>
<td style='text-align:right'>Select an application to browse its entitlements:</td>
<td>
<ComponentArt:ComboBox 
  ID="COMBOXchooseApp" runat="server" Width="250" 
  OnSelectedIndexChanged="COMBOXchooseApp_SelectedIndexChanged"
  RunningMode="Callback"
  AutoPostBack="true"
  DataSourceID="SQL_applicationList"
  DataValueField="c_id"
  DataTextField="c_u_Name"
  TextBoxEnabled="False"  
  HoverCssClass="comboBoxHover"  FocusedCssClass="comboBoxHover"
 TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
 ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
 DropImageUrl="images/drop.gif">
 <ClientEvents>
 </ClientEvents>

</ComponentArt:ComboBox>
</td>
</tr>
</table>
</CENTER>




<asp:SqlDataSource ID="SQL_applicationList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT c_id, c_u_Name FROM [t_RBSR_AUFW_u_Application] ORDER BY [c_u_Name]">
</asp:SqlDataSource>





<asp:Panel ID="PANELcond_ZeroRows" runat="server" Wrap="False">
<P><B>No entitlements have been uploaded or entered for the currently selected application.<BR>Use the combobox to select another application.</B>
</asp:Panel>




<asp:Panel ID="PANELgrid" runat="server" style="xxxheight:300px; width:100%; xxbackground-color:yellow">



<!-- HINT: only by adding EditControlType="..." to a GridColumn
     can you PREVENT the autoselection that occurs normally when you click on a cell in a row -->

<ComponentArt:Grid ID="Grid1" AllowHorizontalScrolling="true" runat="server" 

AutoCallBackOnDelete="true"

TreeLineImagesFolderUrl="images/lines/"
TreeLineImageWidth="19"
TreeLineImageHeight="20"
IndentCellWidth="16"

GroupingMode="ConstantRows"
PreExpandOnGroup="false"

FillContainer="true"

AllowTextSelection="false"

CallbackCacheSize="50"
CallbackCacheLookAhead="15"
CallbackCachingEnabled="True"

RunningMode="Client"
EnableViewState="true"

EditOnClickSelectedItem="false"
AllowEditing="false"

AllowSorting="true"

PageSize="20"

Width="2500"
Height="440"

        DataSourceID="SqlDataSource1" Debug="false" ShowHeader="true" HeaderHeight="18"
        HeaderCssClass="GridHeader" 


ShowSearchBox="true" 
SearchOnKeyPress="false"

CallbackReloadTemplates="false"

SearchText="Search (hit Enter to refresh):"

ColumnResizeDistributeWidth='false'

	SearchTextCssClass="GridHeaderText"

		  GroupByTextCssClass="GroupByText"
        GroupByCssClass="GroupByCell"
		  GroupBySortAscendingImageUrl="group_asc.gif" GroupBySortDescendingImageUrl="group_desc.gif"
        GroupBySortImageWidth="10" GroupBySortImageHeight="10" GroupingNotificationTextCssClass="GridHeaderText"
CssClass="Grid" FooterCssClass="GridFooter"
        PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="41"
        PagerButtonHeight="22" SliderHeight="15" SliderWidth="150" SliderGripWidth="9"
        SliderPopupOffsetX="20" SliderPopupClientTemplateId="SliderTemplate" ScrollPopupClientTemplateId="ScrollPopupTemplate"
        PagerImagesFolderUrl="images/pager/"  ImagesBaseUrl="images/" KeyboardEnabled="true">


        <ClientEvents>
            <ContextMenu EventHandler="Grid1_onContextMenu" />
        </ClientEvents>


        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField" EditCommandClientTemplateId="EditCommandTemplate"
                InsertCommandClientTemplateId="InsertCommandTemplate">



<ConditionalFormats>
	<ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_Status').Value=='P'"
	 				    RowCssClass="BlueRow"  
	 				    SelectedRowCssClass="SelectedRow" />
</ConditionalFormats>


<Columns>

<ComponentArt:GridColumn DataField="c_id" HeadingText="S" Visible="true" AllowEditing="False" />
<ComponentArt:GridColumn DataField="c_u_Application" Visible="false" AllowEditing="False" />

<ComponentArt:GridColumn 
EditControlType="EditCommand"
AllowEditing="false"
AllowHtmlContent="true"
HeadingText="St"
DataField="c_u_Status" 
DataCellClientTemplateId="TEMPLlaunchStatusChangeDialog" Width="20"
Visible="true" FixedWidth="True"
 />

<ComponentArt:GridColumn DataField="c_u_Commentary" HeadingText="Ct" Width="15" FixedWidth="True" 
      DataCellClientTemplateId="TEMPLcommenticon" />

<ComponentArt:GridColumn DataField="c_u_StandardActivity" HeadingText="StdActivity" Width="80" />

<ComponentArt:GridColumn DataField="c_u_RoleType" HeadingText="RoleType" Width="80" />

<ComponentArt:GridColumn DataField="c_u_System" HeadingText="System" Width="90"/>

<ComponentArt:GridColumn DataField="c_u_Platform" HeadingText="Plat" Width="75" />

<ComponentArt:GridColumn DataField="c_u_EntitlementName" HeadingText="EName" Width="80" />
<ComponentArt:GridColumn DataField="c_u_EntitlementValue" HeadingText="EValue" Width="80" />
<ComponentArt:GridColumn DataField="c_u_AuthObjValue" HeadingText="AuthObj" Width="80"/>



                    <ComponentArt:GridColumn DataField="c_u_FieldSecName" HeadingText="FieldSecN"  Width="80"/>
                    <ComponentArt:GridColumn DataField="c_u_FieldSecValue" HeadingText="FieldSecV" Width="80" />




    <ComponentArt:GridColumn DataField="c_u_Level4SecName" HeadingText="Level4N" Width="80" />
    <ComponentArt:GridColumn DataField="c_u_Level4SecValue" HeadingText="Level4V" Width="80" />
    <ComponentArt:GridColumn DataField="UsageKount" HeadingText="Usage" Width="80" />



</Columns>
            </ComponentArt:GridLevel>
        </Levels>
        <ClientTemplates>


            <ComponentArt:ClientTemplate ID="TEMPLlaunchStatusChangeDialog">
             ## RENDERHTMLlaunchStatusChangeDialog(DataItem) ##
            </ComponentArt:ClientTemplate>

            <ComponentArt:ClientTemplate ID="TEMPLcommenticon">
	       ## (DataItem.GetMember("c_u_Commentary").Value) ?
	       	  "<IMG onmouseout='hoverhide()' onmouseover='hovershow(this,\\"" +
		      DataItem.ClientId + "\\")' src='images/msg_unread.gif' border=0/>"
		  :
		  "" ##
            </ComponentArt:ClientTemplate>

            <ComponentArt:ClientTemplate ID="EditTemplate">
	    <DIV style='width:80'> ## RENDERHTMLlaunchVectorManagement(DataItem) ## </DIV>
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
</asp:Panel>

<P style='text-align:left'>STATUS KEY</P>
<UL style='font-size:9px;text-align:left'>
<LI>P = Pending (not available in role designs yet).
<LI>A = Available / Active
<LI>I = Inactive (no longer available in role designer, but may be in legacy use)
<LI>X = Deleted (no longer in use in any way but retained in DB for historical presentation)
</UL>

<iframe id="TheIframe" src='blank.htm' height='1px' width='1px'></iframe>



<!-- JQUERY-UI DIALOG -->
<!-- See the corresponding JS file for the initialization code -->
<div id="JQDLGmsgs" class='JQdialog' title='Important Messages'>
<PRE style='width:650px;height:250px' ID="IFRAMEmsgs"></PRE>
</div>
<!--
	// How to populate:
	//$('IFRAMEmsgs').innerHTML = "FJIEOWJINNERjfiwe<BR>";
	//for (i=0; i<200;i++) {
	//$('IFRAMEmsgs').innerHTML += "FJIEOWJINNER<BR>";
	//}
-->




<!-- JQUERY-UI DIALOG -->
<!-- See the corresponding JS file for the initialization code -->
<div id="JQDLGviewreadonly" class='JQdialog' title='Details'>

<TABLE class='viewreadonlyTABLE'>
<TR class='viewreadonlyROW' id='viewRO_Application'><TD>Application:</TD><TD id='viewRO_TD_Application'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_StandardActivity'><TD>StandardActivity:</TD><TD id='viewRO_TD_StandardActivity'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_RoleType'><TD>RoleType:</TD><TD id='viewRO_TD_RoleType'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_System'><TD>System:</TD><TD id='viewRO_TD_System'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_Platform'><TD>Platform:</TD><TD id='viewRO_TD_Platform'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_EntitlementName'><TD>EntitlementName:</TD><TD id='viewRO_TD_EntitlementName'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_EntitlementValue'><TD>EntitlementValue:</TD><TD id='viewRO_TD_EntitlementValue'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_AuthObjValue'><TD>AuthObjValue:</TD><TD id='viewRO_TD_AuthObjValue'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_FieldSecName'><TD>FieldSecName:</TD><TD id='viewRO_TD_FieldSecName'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_FieldSecValue'><TD>FieldSecValue:</TD><TD id='viewRO_TD_FieldSecValue'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_Level4SecName'><TD>Level4Name:</TD><TD id='viewRO_TD_Level4SecName'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_Level4SecValue'><TD>Level4Value:</TD><TD id='viewRO_TD_Level4SecValue'></TD></TR>
<TR class='viewreadonlyROW' id='viewRO_Commentary'><TD>Commentary:</TD><TD id='viewRO_TD_Commentary'></TD></TR>
</TABLE>
<HR>
<SPAN id='viewRO_ManifestPrivString'></SPAN>

<P>

</div>



<div id='hoverpopup' 
   style='text-align:left;visibility:hidden; position:absolute; top:0; left:0; width:180px;height:100px; padding:17px; background-color:#CCCCFF;filter:alpha(opacity=90)'>
  <span id='_hoverpopup'></span>
</div>




</asp:Content>
