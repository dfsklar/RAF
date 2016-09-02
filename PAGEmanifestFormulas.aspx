<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGEmanifestFormulas.aspx.cs" Inherits="_6MAR_WebApplication.WebForm110" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='PAGEmanifestFormulas.js'></script>
<script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>


<ComponentArt:Menu ID="GridContextMenu" SiteMapXmlFile="CTXMENU_PAGEmanifestFormulas.xml" ExpandSlide="none"
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



<P style='text-align:right'>
<INPUT type='Button' value='Register New...' onclick='javascript:RegNewApp();'/>

<BR>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="

SELECT 
   APP.*, MV.c_u_Formula, MV.c_id AS MVFID, APP.c_u_Name as origname
FROM 
   t_RBSR_AUFW_u_Application APP
LEFT OUTER JOIN 
   t_RBSR_AUFW_u_MVFormula MV
ON
   APP.c_u_Name = MV.c_u_KEYapplication
ORDER BY 
   APP.c_u_Name
">
        </asp:SqlDataSource>
        
        
        
<!-- <INPUT type='Button' value='Add' onclick="javascript:Grid1.Table.AddRow();"/> -->
        
    <ComponentArt:Grid ID="Grid1"

ManualPaging="true"

        AutoCallBackOnInsert="true"
        AutoCallBackOnUpdate="true"
        AutoCallBackOnDelete="true"

		  PageSize="15"

        PagerStyle="Slider"        PagerTextCssClass="GridFooterText"
        SliderHeight="20"
        SliderWidth="150"
        SliderGripWidth="9"
        SliderPopupOffsetX="35"
        PagerImagesFolderUrl="images/pager/"

		  AllowTextSelection="true"
        KeyboardEnabled="false"
        RunningMode="Client"

AllowEditing="true" 
EditOnClickSelectedItem="false"

        CallbackReloadTemplates="false"
		   runat="server" Width="90%" DataSourceID="SqlDataSource1"  
>
        <ClientEvents>
          <CallbackError EventHandler="Grid1_onCallbackError" />
          <ContextMenu EventHandler="Grid1_onContextMenu" />
        </ClientEvents>
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"

 EditCommandClientTemplateId="EditCommandTemplate"
 InsertCommandClientTemplateId="InsertCommandTemplate"

                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField"
                >





<Columns>
 <ComponentArt:GridColumn DataField="c_id" HeadingText="ID" Visible="false" AllowEditing="False" />
 <ComponentArt:GridColumn DataField="MVFID" HeadingText="MVFID" Visible="false" AllowEditing="False" />
 <ComponentArt:GridColumn DataField="origname" Visible="False" AllowEditing="False" />
 <ComponentArt:GridColumn Width='300' FixedWidth='True' DataField="c_u_Name" HeadingText="Application/OS" AllowEditing="True" />
 <ComponentArt:GridColumn Width='30' FixedWidth='True' DataField="c_u_BOOLneedsLevel4" HeadingText="L4?" Visible="false" AllowEditing="True" />
 <ComponentArt:GridColumn DataField="c_u_Formula" HeadingText="Manifest-generation Formula" />
</Columns>


            </ComponentArt:GridLevel>
        </Levels>


        <ClientTemplates>
          <ComponentArt:ClientTemplate Id="EditTemplate">
            <a href="javascript:editGrid('## DataItem.ClientId ##');">Edit</a>
          </ComponentArt:ClientTemplate>
          <ComponentArt:ClientTemplate Id="EditCommandTemplate">
            <a href="javascript:editRow();">Update</a> | <a href="javascript:Grid1.EditCancel();">Cancel</a>
          </ComponentArt:ClientTemplate>
          <ComponentArt:ClientTemplate Id="InsertCommandTemplate">
            <a href="javascript:insertRow();">Insert</a> | <a href="javascript:Grid1.EditCancel();">Cancel</a>
          </ComponentArt:ClientTemplate>
          <ComponentArt:ClientTemplate Id="LoadingFeedbackTemplate">
          <table cellspacing="0" cellpadding="0" border="0">
          <tr>
            <td style="font-size:10px;">Loading...&nbsp;</td>
            <td><img src="images/spinner.gif" width="16" height="16" border="0"></td>
          </tr>
          </table>
          </ComponentArt:ClientTemplate>
        </ClientTemplates>


    </ComponentArt:Grid>

<HR>
<DIV class='helptext'>
<P>Formulas are composed by adding together (with the plus + sign) static text and field references.
<BR>Example for PAT: <U>"PRIV:PAT_PRODUCTION:"+FieldSecValue+"&"+EntitlementValue</U>
<UL>
<LI>For static text, use double-quotes.
<LI>For field references, use these names (case-insensitive):
<UL>
 <LI>system</LI>
 <LI>platform</LI>
 <LI>entitlementname</LI>
 <LI>entitlementvalue</LI>
 <LI>authobjname</LI>
 <LI>authobjvalue</LI>
 <LI>fieldsecname</LI>
 <LI>fieldsecvalue</LI>
 <LI>level4secname</LI>
 <LI>level4secvalue</LI>
 <LI>standardactivity</LI>
 <LI>roletype</LI>
</UL>
<LI>
A large number of string and numeric manipulation functions are available as well, and new ones can be custom-crafted to meet any spec.  Contact Sklar for details.
</UL>






<!-- JQUERY-UI DIALOG -->
<!-- See the corresponding JS file for the initialization code -->
<div id="JQDLGeditFormula" class='JQdialog' title='Edit Formula'>

<P>
Application: <SPAN ID='TXTappname' width="880"></SPAN>

<P>
Formula: <INPUT TYPE='text' ID='TXTEDformula' size='110'/>
<P>
<HR>
<A href='javascript:TestFormula()'>Refresh&gt;&gt;</A>
<DIV style="margin-top:8px" CLASS='ScrollBothAxes' ID="TXTformulaTest"></DIV>

</div>



</asp:Content>
