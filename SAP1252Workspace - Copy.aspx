<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_6MAR_WebApplication.WebForm131" Title="Untitled Page" %>



<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script type="text/javascript" src='prototype.js'></script>
    <script type="text/javascript" src='json2.js'></script>

    <script type="text/javascript" src='SAP1252.js'></script>

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
<b><%= this.strSaproleName %> <!--(<%= this.strSaprolePlatform %>)--></b>
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
        DataSourceId='SqlDataSource1'
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
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
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
                    <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />

                    <ComponentArt:GridColumn DataField="c_u_EditStatus" HeadingText="Stat" Visible="true" AllowEditing="False"
			FixedWidth="True"
			Width="50"
                    DataCellClientTemplateId="TEMPLeditstatusByIcon" />


                    <ComponentArt:GridColumn DataField="c_u_FieldName" HeadingText="Field"
			FixedWidth="True"
			Width="80"
		     />

                    <ComponentArt:GridColumn 
		        DataField="FieldDescr" HeadingText="Description" 
			AllowEditing="True" TextWrap="false"
			Width="120"
			/>

                    <ComponentArt:GridColumn DataField="c_u_RangeLow" HeadingText="Low"
			FixedWidth="True"
			Width="60"
		    />
                    <ComponentArt:GridColumn DataField="c_u_RangeHigh" HeadingText="High"
			FixedWidth="True"
			Width="60"
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
                  <td colspan="2" style="font-family:verdana;font-size:11px;font-weight:bold;"><div style="overflow:hidden;width:250px;"><nobr>## DataItem.GetMember('c_u_FieldName').Value ##</nobr></div></td>
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
      <div style="font-family:verdana;">## RENDEReditstatus(DataItem) ##</div>
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

T.c_id,
T.c_u_FieldName,
T.c_u_RangeLow,
T.c_u_RangeHigh,
T.c_u_EditStatus, SAPROLE.c_u_Name as SAProlename, 
T.c_r_TcodeAssignmentSet,
'comingsoon' as FieldDescr

FROM t_RBSR_AUFW_u_OrgValue1252 T

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_SAProle SAPROLE 
       ON T.c_r_SAProle = SAPROLE.c_id

WHERE 

(T.c_r_TcodeAssignmentSet = @SCOPEtasset) AND
(T.c_r_SAProle = @SCOPEsaprole) AND
(T.c_u_EditStatus <> 6)

ORDER BY T.c_u_FieldName, c_u_RangeLow, c_u_RangeHigh

"
OnSelecting="SqlDataSource1_Selecting"
>
            <SelectParameters>
                <asp:QueryStringParameter Name="SCOPEsaprole" QueryStringField="RoleID" Type="Int32" DefaultValue="15" />
                <asp:SessionParameter Name="SCOPEtasset" SessionField="INTcurWS_SAP" Type="Int32" DefaultValue="15" />
            </SelectParameters>
        </asp:SqlDataSource>
        
        
        




<div id="JQDLGeditRow" class='JQdialog' title='Org-Value Editor'>

<table class='dlgcustomgrid'>

<input type='hidden' id="HIDcurrowid"/>
<input type='hidden' id="HIDroleid" value="<%= this.idSaprole %>"/>
<input type='hidden' id="HIDuserid" value="<%= this.session.idUser %>"/>
<input type='hidden' id="HIDsapwsid" value="<%= this.session.idWorkspace_SAP %>"/>

<tr>
<td>TCode:</td>
<td>
<input type='text' ID="DLGCTL_c_u_Variable"></input>
</td>
</tr>


<tr>
<td>Low:</td>
<td>
<input size='33' type='text' id='DLGCTL_c_u_Low' value=''></input>
</td>
</tr>


<tr>
<td>High:</td>
<td>
<input size='33' type='text' id='DLGCTL_c_u_High' value=''></input>
</td>
</tr>


</table>
</div>    










<DIV ID="HIDDEN_CLOSET" style='visibility:hidden'>


</DIV>



</asp:Content>
