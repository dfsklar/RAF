<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SAPEntitlementWorkspace.aspx.cs" Inherits="_6MAR_WebApplication.WebForm113" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script type="text/javascript" src='prototype.js'></script>
    <script type="text/javascript" src='json2.js'></script>

    <script type="text/javascript" src='SAPgridediting.js'></script>

    <script type="text/javascript">
	 function IsReadOnly()
	 {
		  return "EDIT" != ($('<%= HIDDENeditmode.ClientID %>').value);
    }
	 </script>


    <ComponentArt:Menu ID="GridContextMenu" SiteMapXmlFile="menuDataSAP.xml" ExpandSlide="none"
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
    
    
    <ComponentArt:Menu ID="GridContextMenuNOEDIT" SiteMapXmlFile="menuDataNOEDITSAP.xml"
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


<TABLE style='width:100%;margin-bottom:9px'>
<TR>
<TD style='text-align:left;vertical-align:top'>
<B><A href='ListSAPRoles.aspx'>&lt;&lt;&lt; </A></B> &nbsp;
<b><%= this.strSaproleName %> (<%= this.strSaprolePlatform %>)</b>
</TD>
<TD style='text-align:right;vertical-align:top'>
<A href="javascript:MENUACTnewblank();">Add new...</A>
</TD>
</TR>
</TABLE>


<!--

The bug where any page after page 1 is empty after a confirmed edit callback...
... NOT due to fillcontainer
... NOT due to CallbackReloadTemplates

AHA!  Due to missing this: ManualPaging="true"

-->  
   <div style="xxxxheight:400px; width:100%" id="containerforgrid">
    <ComponentArt:Grid ID="Grid1" 
        EnableViewState="true"
        EditOnClickSelectedItem="false"
        AllowEditing="true"
	ColumnResizeDistributeWidth='false'
        CssClass="Grid"
        KeyboardEnabled="false"
        FooterCssClass="GridFooter"
        RunningMode="Client"
        PagerStyle="Slider"        PagerTextCssClass="GridFooterText"
        SliderHeight="20"
        SliderWidth="150"
        SliderGripWidth="9"
        SliderPopupOffsetX="35"
        SliderPopupClientTemplateId="SliderTemplate"
        PagerImagesFolderUrl="images/pager/"


        PageSize="15"
        ImagesBaseUrl="images/"
        Height="370"
        runat="server"

		  ManualPaging = "true"

		  AllowTextSelection="true"

		  AutoPostBackOnInsert="false"
        AutoPostBackOnUpdate="false"
        AutoPostBackOnDelete="false"

        AutoCallBackOnInsert="true" AutoCallBackOnUpdate="true" AutoCallBackOnDelete="true"
        CallbackReloadTemplates="false"

        ShowHeader="True" HeaderHeight="18"
        HeaderCssClass="GridHeader" ShowSearchBox="true" SearchTextCssClass="GridHeaderText"
        SearchOnKeyPress="true" 

        GroupByTextCssClass="GroupByText"
        GroupByCssClass="GroupByCell"
		  GroupBySortAscendingImageUrl="group_asc.gif" GroupBySortDescendingImageUrl="group_desc.gif"
        GroupBySortImageWidth="10" GroupBySortImageHeight="10" GroupingNotificationTextCssClass="GridHeaderText"

		  FillContainer="true"
>

        <ClientEvents>
            <ContextMenu EventHandler="Grid1_onContextMenu" />
            <ItemSelect  EventHandler="Grid1_onItemSelect" />
        </ClientEvents>

        <Levels>
            <ComponentArt:GridLevel DataKeyField="IDtass" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField">

<ConditionalFormats>
	<ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_EditStatus').Value & 4"
	 				    RowCssClass="DeletedRow"
	 				    SelectedRowCssClass="SelectedRow" />
</ConditionalFormats>


                <Columns>
                    <ComponentArt:GridColumn DataField="IDtass" Visible="false" AllowEditing="False" />
                    <ComponentArt:GridColumn DataField="IDTcodeEnt" Visible="false" AllowEditing="False" />

                    <ComponentArt:GridColumn DataField="c_u_EditStatus" HeadingText="Stat" Visible="true" AllowEditing="False"
			FixedWidth="True"
			Width="50"
                    DataCellClientTemplateId="TEMPLeditstatusByIcon" />


                    <ComponentArt:GridColumn DataField="c_u_TCode" HeadingText="TCode"
			FixedWidth="True"
			Width="80"
		     />

                    <ComponentArt:GridColumn 
		        DataField="TCodeDescr" HeadingText="TCode Descr" 
			AllowEditing="True" TextWrap="false"
			Width="120"
 			EditControlType="Custom"
 			CustomEditSetExpression="JSFUNCinitTcodeDescr(DataItem)"
 			CustomEditGetExpression="sapnoop()"
			/>

                    <ComponentArt:GridColumn DataField="c_u_ActivityFolder" HeadingText="Act Folder"
			FixedWidth="True"
			Width="60"
		    />
                    <ComponentArt:GridColumn DataField="c_u_Type" HeadingText="Type"
			FixedWidth="True"
			Width="50"
	   	     />

                    <ComponentArt:GridColumn DataField="c_u_AccessLevel" HeadingText="AccLvl"
			FixedWidth="True"
			Width="100"
   		     />

                    <ComponentArt:GridColumn DataField="CommentOnEntitlement" HeadingText="Comment"
			FixedWidth="True"
			Width="100"
   		     />

                </Columns>
            </ComponentArt:GridLevel>
        </Levels>


        <ClientTemplates>

          <ComponentArt:ClientTemplate Id="SliderTemplate">
            <table class="SliderPopup" cellspacing="0" cellpadding="0" border="0">
            <tr>
              <td valign="top" style="padding:5px;">
              <table width="100%" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <table cellspacing="0" cellpadding="1" border="0" style="width:255px;">
                <tr>
                  <td colspan="2" style="font-family:verdana;font-size:11px;font-weight:bold;"><div style="overflow:hidden;width:250px;"><nobr>## DataItem.GetMember('c_u_TCode').Value ##</nobr></div></td>
                </tr>
                <tr>
                  <td style="font-family:verdana;font-size:11px;"><div style="overflow:hidden;width:235px;"><nobr>## DataItem.GetMember('c_u_ActivityFolder').Value ## </nobr></div></td>
                </tr>
                <tr>
                  <td style="font-family:verdana;font-size:11px;"><div style="overflow:hidden;width:235px;"><nobr>
                Page <b>## DataItem.PageIndex + 1 ##</b> of <b>## Grid1.PageCount ##</b>
							 </nobr></div></td>
                </tr>
                </table>
                </td>
              </tr>
              </table>
              </td>
            </tr>
            </table>
          </ComponentArt:ClientTemplate>

        
            <ComponentArt:ClientTemplate ID="TEMPLeditstatusByIcon">
      <div style="font-family:verdana;">## RENDEReditstatus_canonical(DataItem) ##</div>
            </ComponentArt:ClientTemplate>



        </ClientTemplates>




         <ServerTemplates>


            <ComponentArt:GridServerTemplate ID="TEMPLATEcomment">
                <Template>							
					 <textarea ID="TXTAREAcommentbox" rows=3 cols=30></textarea>
					 <br/>
    <input onclick="Grid1.editComplete();" type="button" value="OK" id="BTNconfirmedit"/>
    <input onclick="Grid1.editCancel();" type="button" value="Cancel" id="BTNcanceledit"/>
                </Template>
            </ComponentArt:GridServerTemplate>

            <ComponentArt:GridServerTemplate ID="TEMPLATEsaprole">
                <Template>
                    <ComponentArt:ComboBox ID="COMBOBOXsaprole" runat="server" CssClass="comboBox" TextBoxEnabled="false"
						  	   DataSourceID="SQL_valuemenu_SAProle" DataTextField="c_u_Name" DataMember="SAPROLE"
								DataValueField="c_id"
                        HoverCssClass="comboBoxHover"  FocusedCssClass="comboBoxHover"
                        TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
                        DropImageUrl="images/drop.gif" Width="150" />
                </Template>
            </ComponentArt:GridServerTemplate>

        </ServerTemplates>


    </ComponentArt:Grid>




<BR>
&nbsp;
<BR>


<asp:Panel ID="PanelMacroActionsEDIT" runat="server">

<!--
<a href="javascript:alert('Not yet implemented');">
   Submit for review by role owners</a> 

&bull; 
<a href="javascript:alert('Not yet implemented');">Publish</a> 


&bull; 

-->


</asp:Panel>

<asp:Panel ID="PanelEditButtons" runat="server">
<!--
    <input onclick="Grid1.editComplete();" type="button" value="Confirm edit" id="BTNconfirmedit"/>
    <input onclick="Grid1.editCancel();" type="button" value="Cancel edit" id="BTNcanceledit"/>
    <input onclick="Grid1.Table.AddRow();" type="button" value="ADD ROW" id="BTNaddrow"/>
    <input onclick="CloneRow(Grid1);" type="button" value="CLONE ROW" id="BTNaddrow"/>
-->
</asp:Panel>

   </DIV>




	<asp:HiddenField ID="HIDDENidWS" runat="server" />
	<asp:HiddenField ID="HIDDENeditmode" runat="server" />





   <asp:SqlDataSource ID="SQL_valuemenu_type" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT DISTINCT Abbrev, Name FROM [DICT_SAPentitlementType] ORDER BY Abbrev">
   </asp:SqlDataSource>



   <asp:SqlDataSource ID="SQL_valuemenu_accesslevel" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT DISTINCT Abbrev, Name FROM [DICT_SAPentitlementAccessLevel] ORDER BY Abbrev">
   </asp:SqlDataSource>




   <asp:SqlDataSource ID="SQL_valuemenu_SAProle" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT DISTINCT c_u_Name, c_id FROM [t_RBSR_AUFW_u_SAProle] WHERE c_r_SubProcess=@IDsubprocess ORDER BY [c_u_Name]">
            <SelectParameters>
                <asp:SessionParameter Name="IDsubprocess"  SessionField="UUIDSUBPROCESS" Type="Int32"
                    DefaultValue="15" />
            </SelectParameters>
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="SQL_WorkspaceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT c_r_User,c_u_Status,c_u_Commentary FROM [t_RBSR_AUFW_u_TcodeAssignmentSet] WHERE c_id=@SCOPEtasset">
            <SelectParameters>
                <asp:SessionParameter Name="SCOPEtasset" SessionField="INTcurWS_SAP" Type="Int32" DefaultValue="15" />
            </SelectParameters>
   </asp:SqlDataSource>



<asp:SqlDataSource
   ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
   SelectCommand="


SELECT 

T.c_id AS IDTcodeEnt, 
T.c_u_ActivityFolder,
T.c_u_Type,
T.c_u_AccessLevel,
T.c_u_Comment as CommentOnEntitlement,
TASS.c_u_Commentary as CommentOnAssignment, 
T.c_u_TCode, TDICT.c_u_Description AS TcodeDescr, 
TASS.c_id AS IDtass, TASS.c_u_EditStatus, SAPROLE.c_u_Name as SAProlename, 
TASS.c_r_TcodeAssignmentSet 

FROM t_RBSR_AUFW_u_TcodeAssignment TASS

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeEntitlement T 
ON T.c_id = TASS.c_r_TcodeEntitlement

LEFT OUTER JOIN t_RBSR_AUFW_u_SAProle SAPROLE 
ON TASS.c_r_SAProle = SAPROLE.c_id

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeDictionary AS TDICT 
ON TDICT.c_u_TcodeID = T.c_u_TCode 

WHERE 

(TASS.c_r_TcodeAssignmentSet = @SCOPEtasset) AND
(TASS.c_r_SAProle = @SCOPEsaprole) AND
(TASS.c_u_EditStatus <> 6)

"
OnSelecting="SqlDataSource1_Selecting"
>
            <SelectParameters>
                <asp:QueryStringParameter Name="SCOPEsaprole" QueryStringField="RoleID" Type="Int32" DefaultValue="15" />
                <asp:SessionParameter Name="SCOPEtasset" SessionField="INTcurWS_SAP" Type="Int32" DefaultValue="15" />
            </SelectParameters>
        </asp:SqlDataSource>
        
        
        
    <asp:SqlDataSource ID="ListAllKnownSystems" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="select distinct c_u_System FROM t_RBSR_AUFW_u_Entitlement order by c_u_System">
    </asp:SqlDataSource>
 
 
    
    



<div id="JQDLGeditRow" class='JQdialog' title='Entitlement Editor'>

<table class='dlgcustomgrid'>

<input type='hidden' id="HIDcurrowid"/>
<input type='hidden' id="HIDroleid" value="<%= this.idSaprole %>"/>
<input type='hidden' id="HIDuserid" value="<%= this.session.idUser %>"/>
<input type='hidden' id="HIDsapwsid" value="<%= this.session.idWorkspace_SAP %>"/>

<tr>
<td>TCode:</td>
<td>
<input type='text' ID="DLGCTL_c_u_TCode">
<span class='fakelink' onclick="javascript:LookupTcode();">Lookup</span>
<BR/><span ID="TXTtcodedescr">.....</span></div>
</td>
</tr>


<tr>
<td>Activity Folder:</td>
<td>
<input size='33' type='text' id='DLGCTL_c_u_ActivityFolder' value=''></input>
</td>
</tr>



<tr>
<td>Type:</td>
<td id="TARGET_COMBOBOXtype">
</td>
</td>
</tr>



<tr>
<td>Access Level:</td>
<td id="TARGET_COMBOBOXaccesslevel">
</td>
</tr>





<tr>
<td colspan='2'><hr/><br/>
Comment: <!-- (attached to entitlement, not assignment): -->
<br/>
<textarea cols='60' rows='6' id='DLGCTL_c_u_Comment'></textarea>
</td>
</tr>




</table>
</div>    










<DIV ID="HIDDEN_CLOSET" style='visibility:hidden'>

<DIV ID="WRAPPER_COMBOBOXtype">
<ComponentArt:ComboBox 
ID="COMBOBOXtype" runat="server" CssClass="comboBox" TextBoxEnabled="false"
DataSourceID="SQL_valuemenu_type" DataTextField="Name" DataMember="ORGAXIS"
DataValueField="Abbrev"
HoverCssClass="comboBoxHover"  FocusedCssClass="comboBoxHover"
TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
DropImageUrl="images/drop.gif" Width="120">
  <ClientEvents>	       
      <Change EventHandler="EVENT_SelChange_COMBOBOXtype"/>
  </ClientEvents>
</ComponentArt:ComboBox>
</DIV>



<DIV ID="WRAPPER_COMBOBOXaccesslevel">
<ComponentArt:ComboBox 
 ID="COMBOBOXaccesslevel" runat="server" CssClass="comboBox" TextBoxEnabled="false"
 DataSourceID="SQL_valuemenu_accesslevel" DataTextField="Name" DataMember="ORGVALUE"
 DataValueField="Abbrev"
 HoverCssClass="comboBoxHover" SelectedIndex="4" FocusedCssClass="comboBoxHover"
 TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
 ItemHoverCssClass="comboItemHover" 
 SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
 DropImageUrl="images/drop.gif" Width="70">
  <ClientEvents>
  </ClientEvents>
</ComponentArt:ComboBox>
</DIV>

</DIV>

</asp:Content>
