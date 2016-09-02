<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Page_SAP_History.aspx.cs" Inherits="_6MAR_WebApplication.WebForm119" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='Page_SAP_History.js'></script>
<script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>



<asp:Panel ID="PANELcond_NewSubpr" runat="server" Width="788px">
      No SAP design work has been performed yet for this subprocess.
      <br />
      To work on this subprocess, <asp:LinkButton ID="LinkButton1" runat="server" OnClick=LinkButton1_Click>create a new blank workspace</asp:LinkButton>
        and then visit the Designer or
      Upload tabs.</asp:Panel>


<!-- The page-load logic will choose one of these for display. -->
   <asp:Panel ID="PANELcond_InviteCreateWS" runat="server" Width="787px">
      There is currently no editing workspace active for this subprocess.
<br/>
      To make changes to the currently active set of entitlements, <asp:LinkButton ID="LinkButton2" runat="server" OnClick=LinkButton2_Click>create a new workspace</asp:LinkButton>.

   </asp:Panel>

    <asp:Panel ID="PANELcond_MultWorkspaces" runat="server">
    <p class='RedAlert'><b>ACTION NEEDED!</b>
    <br /> <br />
    There are multiple workspaces open.  Examine the timestamps and '# of edits' information for the two workspaces, and use that info to determine which to delete.
    <br /> <br />
    To delete a workspace, right-click its row and use the context popup menu.
    <br /> <br />
    NOTE: Until you delete one of the two workspaces, some SAP-related functionality will be off-limits and will redirect you to this page.</p>
    </asp:Panel>
    
    
<asp:Panel ID="PANELcond_Locked" runat="server" Width="788px" Wrap="False">
      Currently another user has a workspace open for this subprocess.
<BR/>
Details are shown in the below history table.
<BR/>
You may visit the workspace to obtain a read-only view of its contents.
</asp:Panel>


<asp:Panel ID="PANELcond_IsOwnerOfCurrentWS" runat="server" Width="787px">
      This workspace is active and belongs to you.
      <br />
      Use the Upload and Designer tabs to perform activities in this workspace.
      </asp:Panel>





<DIV style='margin-top:8px'></DIV>

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
		   runat="server" Width="90%" DataSourceID="SqlDataSource1"  
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

    <ComponentArt:GridColumn DataField="c_id" HeadingText="Action" Visible="true" AllowEditing="False" 
           DataCellClientTemplateId="TEMPLaction"
    />

    <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="User"  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_Status" HeadingText="Status"  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_tstamp" HeadingText="Date"  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_Commentary" HeadingText="Commentary"  AllowEditing="False" />
    
    <ComponentArt:GridColumn DataField="CountOfEdited" HeadingText="#Edits(TC)"  AllowEditing="False" Align=Right Width='120'/>
    

		</Columns>
            </ComponentArt:GridLevel>
        </Levels>


        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="TEMPLaction">
	          <A href='ListSAPRoles.aspx?WSID=## DataItem.GetMember("c_id").Value ##'>Visit</A>
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
   EAS.*, USR.c_u_Name,
   (SELECT COUNT(c_id) 
     FROM t_RBSR_AUFW_u_TcodeAssignment TASS
     WHERE TASS.c_r_TcodeAssignmentSet = EAS.c_id
     AND TASS.c_u_EditStatus <> 0
   ) as CountOfEdited
FROM
   t_RBSR_AUFW_u_TcodeAssignmentSet EAS
LEFT OUTER JOIN
   t_RBSR_AUFW_u_User USR
ON
   USR.c_id = EAS.c_r_User
WHERE
   EAS.c_r_SubProcess = @CURSUBPR AND EAS.c_u_Status NOT LIKE 'deleted'
ORDER BY EAS.c_u_tstamp DESC
">
        <SelectParameters>
            <asp:SessionParameter Name="CURSUBPR" SessionField="UUIDSUBPROCESS" Type="Int32" />
        </SelectParameters>

</asp:SqlDataSource>



<ComponentArt:Menu ID="ContextMenu" SiteMapXmlFile="CTXMENU_GRID_broleEntsets.xml" ExpandSlide="none"
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

