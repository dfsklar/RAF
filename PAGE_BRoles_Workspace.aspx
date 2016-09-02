<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGE_BRoles_Workspace.aspx.cs" Inherits="_6MAR_WebApplication.WebForm118" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='PAGE_BRoles_Workspace.js'></script>
<script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>


<asp:Panel ID="PANELcond_NewSubpr" runat="server" Width="788px">
      No role-design work has been performed yet for this subprocess.
      <br />
      To work on this subprocess, 
      <a href="PAGE_BRoles_LaunchNewWorkspace.aspx?choose=blank">create a new blank workspace</a> and then visit the Designer.
</asp:Panel>



<br />



<!-- The page-load logic will choose one of these for display. -->
   <asp:Panel ID="PANELcond_InviteCreateWS" runat="server" Width="787px">To perform maintenance,
<a href="javascript:LaunchWorkspace(<%=this.session.idSubprocess%>, <%=this.session.idUser%>);">open a workspace</a>.
   </asp:Panel>
   <br />


<asp:Panel ID="PANELcond_Locked" runat="server" Width="788px" Wrap="False">
      Currently another user has a workspace open for this subprocess.
<BR/>
Details are shown in the below history table.
<BR/>
You may visit the workspace to obtain a read-only view of its contents.
</asp:Panel>


   <br />


   <asp:Panel ID="PANELcond_IsOwnerOfCurrentWS" runat="server" Width="787px">
      The workspace for this subprocess belongs to YOU.
      <br />&nbsp;
      </asp:Panel>




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

    <ComponentArt:GridColumn DataField="c_id" HeadingText=" "
           Visible="true" AllowEditing="False" 
           DataCellClientTemplateId="TEMPLaction"
    />

    <ComponentArt:GridColumn DataField="c_u_Status" HeadingText="Status"  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="User"  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_DATETIMEbirth" HeadingText="Birth"  AllowEditing="False" />
    <ComponentArt:GridColumn DataField="c_u_DATETIMElock" HeadingText="Freeze"  AllowEditing="False" />

    <ComponentArt:GridColumn DataField="c_u_Commentary" HeadingText="Commentary"  AllowEditing="False" TextWrap="True" />

    <ComponentArt:GridColumn DataField="rawrowcount" HeadingText="(size)"  AllowEditing="False" TextWrap="False" />
    <ComponentArt:GridColumn DataField="Arowcount" HeadingText="A"  AllowEditing="False" TextWrap="False" />
    <ComponentArt:GridColumn DataField="Xrowcount" HeadingText="X"  AllowEditing="False" TextWrap="False" />
    <ComponentArt:GridColumn DataField="Nrowcount" HeadingText="N"  AllowEditing="False" TextWrap="False" />
    <ComponentArt:GridColumn DataField="Prowcount" HeadingText="P"  AllowEditing="False" TextWrap="False" />

		</Columns>
            </ComponentArt:GridLevel>
        </Levels>


        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="TEMPLaction">
	          <A href='ListBRoles.aspx?WSID=## DataItem.GetMember("c_id").Value ##'><img src='images/arrow_blue_tiny_right.gif' border=0/></A>
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
   (SELECT count(*) from t_RBSR_AUFW_u_EntAssignment where c_r_EntAssignmentSet=EAS.c_id) as rawrowcount,
   (SELECT count(*) from t_RBSR_AUFW_u_EntAssignment where c_r_EntAssignmentSet=EAS.c_id and c_u_Status='X') as Xrowcount,
   (SELECT count(*) from t_RBSR_AUFW_u_EntAssignment where c_r_EntAssignmentSet=EAS.c_id and c_u_Status='A') as Arowcount,
   (SELECT count(*) from t_RBSR_AUFW_u_EntAssignment where c_r_EntAssignmentSet=EAS.c_id and c_u_Status='N') as Nrowcount,
   (SELECT count(*) from t_RBSR_AUFW_u_EntAssignment where c_r_EntAssignmentSet=EAS.c_id and c_u_Status='P') as Prowcount

FROM
   t_RBSR_AUFW_u_EntAssignmentSet EAS
LEFT OUTER JOIN
   t_RBSR_AUFW_u_User USR
ON
   USR.c_id = EAS.c_r_User
WHERE
   EAS.c_r_SubProcess = @CURSUBPR
AND
   EAS.c_u_Status NOT IN ('deleted')
ORDER BY EAS.c_u_DATETIMEbirth DESC
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
