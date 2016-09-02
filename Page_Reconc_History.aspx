<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Page_Reconc_History.aspx.cs" Inherits="_6MAR_WebApplication.WebForm127" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='Page_Reconc_History.js'></script>
<script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>



<iframe id="DumpFrame" src='blank.htm' style="height:2px;width:2px">
</iframe>



    <ComponentArt:Grid ID="Grid1"

PageSize="10"

PagerStyle="Slider"        PagerTextCssClass="GridFooterText"
        SliderHeight="20"
        SliderWidth="150"
        SliderGripWidth="9"
        SliderPopupOffsetX="35"
        PagerImagesFolderUrl="images/pager/"

AllowTextSelection="false"

        KeyboardEnabled="false"

RunningMode="Client"

AllowEditing="false"

CallbackReloadTemplates="false"
runat="server" Width="770" DataSourceID="SqlDataSource1"  
>
        <ClientEvents>
	 <ContextMenu EventHandler="Grid_onContextMenu" />
	 <ItemSelect  EventHandler="Grid_onItemSelect" />
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

    <ComponentArt:GridColumn DataField="c_id" HeadingText=" " Visible="true" AllowEditing="False" 
           DataCellClientTemplateId="TEMPLaction"
    />

    <ComponentArt:GridColumn DataField="c_u_Domain" HeadingText="Scope" Width='70'  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="User" Width='70'  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="CreationTime" 
        HeadingText="Birth"  Width='90' AllowEditing="False" />

    <ComponentArt:GridColumn DataField="KountTotal" HeadingText="#Diff"  AllowEditing="False" />
    <ComponentArt:GridColumn DataField="KountResolved" HeadingText="#Rslvd"  AllowEditing="False" />
    <ComponentArt:GridColumn DataField="c_id"
    			     DataCellClientTemplateId="CalcPerc"
    			     HeadingText="%Rslvd"  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_Comment" HeadingText="Comment"  AllowEditing="False" />

		</Columns>
            </ComponentArt:GridLevel>
        </Levels>


        <ClientTemplates>

            <ComponentArt:ClientTemplate ID="CalcPerc">
	       ##(DataItem.GetMember("KountTotal").Value == 0)?
	       	   "?" : 
		       (100 * (DataItem.GetMember("KountResolved").Value / 
		              DataItem.GetMember("KountTotal").Value)).toFixed(1) ## %
            </ComponentArt:ClientTemplate>
 
            <ComponentArt:ClientTemplate ID="TEMPLaction">
	          <A href='Page_Reconc_Analysis.aspx?id=## DataItem.GetMember("c_id").Value ##'><img src='images/arrow_blue_tiny_right.gif' border=0/></A>
            </ComponentArt:ClientTemplate>

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



<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="


SELECT
   RR.*, USR.c_u_Name, RR.c_id as IDreport,

   DATENAME(day, RR.c_u_CreationTime) + '-' +
   LEFT(DATENAME(month, RR.c_u_CreationTime),3) + ' ' +
   DATENAME(hour, RR.c_u_CreationTime) + ':' +
   DATENAME(minute, RR.c_u_CreationTime)
   as CreationTime,

   (SELECT COUNT(*) FROM t_RBSR_AUFW_u_ReconcDiffItem WHERE c_r_ReconcReport=RR.c_id) as KountTotal,

   (SELECT COUNT(*) FROM t_RBSR_AUFW_u_ReconcDiffItem WHERE c_r_ReconcReport=RR.c_id AND c_u_Status<>'P') as KountResolved,

   '?' as PercResolved

FROM
   t_RBSR_AUFW_u_ReconcReport RR
LEFT OUTER JOIN
   t_RBSR_AUFW_u_User USR
ON
   USR.c_id = RR.c_r_User
ORDER BY RR.c_u_CreationTime DESC


">
        <SelectParameters>
            <asp:SessionParameter Name="CURSUBPR" SessionField="UUIDSUBPROCESS" Type="Int32" />
        </SelectParameters>

</asp:SqlDataSource>



<ComponentArt:Menu ID="ContextMenu" SiteMapXmlFile="CTXMENU_GRID_reconchistory.xml" ExpandSlide="none"
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




</asp:Content>
