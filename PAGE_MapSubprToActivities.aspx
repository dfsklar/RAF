<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGE_MapSubprToActivities.aspx.cs" Inherits="_6MAR_WebApplication.WebForm122" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='PAGE_MapSubprToActivities.js'>
function Checkbox1_onclick() {

}

function Button1_onclick() {

}

</script>


<I>
<%= (this.session.isWorkspaceOwner) ? "Note: you can re-order items via drag/drop." : "<B>READ ONLY VIEW</B><BR>" %></I>
<br/>

<ComponentArt:Grid ID="GridMain" runat="server" 

    ShowFooter='false'

    RunningMode="Callback"
    EnableViewState="true"

    Width="880"

    PageSize='40'

    DataSourceId="SqlDataSource1"

    AllowTextSelection="false"

    AllowEditing="False"

    ItemDraggingEnabled="True"

    ItemDraggingClientTemplateId="DragTemplate"

    ExternalDropTargets="GridMain"

    EditOnClickSelectedItem="false"

    >


<Levels>

<ComponentArt:GridLevel
 AllowSorting="False"
 DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
 HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
 RowCssClass="Row" SelectedRowCssClass="SelectedRow"
>

                <ConditionalFormats>
                    <ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('c_u_NodeType').Value=='HEAD'"
                        RowCssClass="ActivityTableHEADERROW" 
			SelectedRowCssClass="ActivityTableHEADERROW_SelectedRow" />
                </ConditionalFormats>



   <Columns>

      <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
      <ComponentArt:GridColumn DataField="c_u_NodeType" Visible="false" AllowEditing="False" />
      <ComponentArt:GridColumn DataField="c_u_Sequence" Visible="false" AllowEditing="False" />

      <ComponentArt:GridColumn DataField="c_u_BOOLisKeyPoint" HeadingText="Key" Visible="false" Width="20"
      			       DataType="System.Boolean" ColumnType="CheckBox" />

      <ComponentArt:GridColumn DataField="c_u_Text" 
      DataCellClientTemplateId="TEMPLActivityTitle"
      HeadingText="Activity" Visible="true" AllowEditing="False" 
      TextWrap="True"
          Width="250"/>


      <ComponentArt:GridColumn DataField="c_u_ListIdsApps"
          DataCellServerTemplateId="TEMPLlistApps"
	  TextWrap="True"
	  HeadingText="Applications" />

      
      <ComponentArt:GridColumn AllowEditing="false"
      DataField="c_u_ListIdsBusRoles"
          DataCellServerTemplateId="TEMPLlistBusRoles"
	  TextWrap="True"
          HeadingText="Roles" />

   </Columns>


</ComponentArt:GridLevel>

</Levels>

<ClientEvents>
   <Load EventHandler="EVTHNDL_GridDoneLoading"/>
</ClientEvents>



 <ClientTemplates>

    <ComponentArt:ClientTemplate Id="DragTemplate">
       <DIV style='padding:5px;margin-left:12px;background-color:#BBBBBB'>##DataItem.GetMember('c_u_Text').Value##</DIV>
    </ComponentArt:ClientTemplate>

    <ComponentArt:ClientTemplate Id="TEMPLActivityTitle">
       <TABLE><TR>
       <TD><img src="images/IsHead_##DataItem.GetMember('c_u_NodeType').Value=='HEAD'##__IsKeyPoint_## DataItem.GetMember('c_u_BOOLisKeyPoint').Value ##.gif"/></TD><TD>## DataItem.GetMember('c_u_Text').Value ##</TD>
       </TR></TABLE>
    </ComponentArt:ClientTemplate>
 </ClientTemplates>


 <ClientEvents>
    <ItemSelect EventHandler="EVT_GridMain_ItemSelect" />
    <ItemExternalDrop EventHandler="EVT_GridMain_Resequence"/>
 </ClientEvents>

 <ServerTemplates>
    <ComponentArt:GridServerTemplate Id="TEMPLlistBusRoles">
       <template><%# _6MAR_WebApplication.HELPERS.HumanReadableRoleList(Container.DataItem["c_u_ListIdsBusRoles"].ToString()," &bull; ","") %>&nbsp;
       </template>
    </ComponentArt:GridServerTemplate>
    <ComponentArt:GridServerTemplate Id="TEMPLlistApps">
       <template><%# _6MAR_WebApplication.HELPERS.HumanReadableAppList(Container.DataItem["c_u_ListIdsApps"].ToString()," &bull; ","") %>&nbsp;
       </template>
    </ComponentArt:GridServerTemplate>
 </ServerTemplates>


</ComponentArt:Grid>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_METADATA_SubprToActivityList] WHERE ([c_r_SubProcess] = @c_r_SubProcess) ORDER BY [c_u_Sequence]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SQL_AppList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT [c_id], [c_u_Name] FROM [t_RBSR_AUFW_u_Application] ORDER BY [c_u_Name]">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SQL_BusRoles" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        OnSelecting="INTERCEPTparamsubprocess"
        SelectCommand="SELECT [c_id], [c_u_Name] FROM [t_RBSR_AUFW_u_BusRole] WHERE ([c_r_SubProcess] = @c_r_SubProcess) ORDER BY [c_u_Name]">
        <SelectParameters>
            <asp:Parameter Name="c_r_SubProcess" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>



<input type='hidden' ID="HIDDEN_idSubPr" value='<%=this.idSubPr%>'/>
<input type='hidden' ID="HIDDEN_canedit" value='<%=this.session.isWorkspaceOwner%>'/>


<div id="JQDLGeditRow" class='JQdialog' title='Row Editor'>

    <select id="SELnodetype">
        <option value="ACT" selected="selected">Activity</option>
        <option value="HEAD">Heading</option>
    </select>
    <br />

<INPUT type='text' id="TXTactivityname" style="width: 410px"></INPUT>
    &nbsp; &nbsp; &nbsp;
    <input id="CHKBOXisKey" type="checkbox" />Key
    Control Point<br />

<HR>

<span style="color:#EE3300;font-weight:bold">TIP: Hold down CTRL key to ensure your clicks affect only one item.</span>

<DIV ID="WRAPPERtableDeployed">
</DIV>

</DIV>





<DIV ID="HIDDEN_CLOSET" style='visibility:hidden;height:3px;overflow:hidden'>
<TABLE ID="GridTable" class='dlgcustomgrid'>
<TR>
<TD>




<ComponentArt:Grid ID="GRID_SelectApplications" runat="server" Width="300"
      DataSourceId="SQL_AppList"

        RunningMode="Client"
        ShowFooter="false"
        ScrollBar="Auto"
        ScrollTopBottomImagesEnabled="true"
        ScrollTopBottomImageHeight="2"
        ScrollTopBottomImageWidth="16"
        ScrollImagesFolderUrl="images/scroller/"
        ScrollButtonWidth="16"
        ScrollButtonHeight="17"
        ScrollBarCssClass="ScrollBar"
        ScrollGripCssClass="ScrollGrip"
        ScrollBarWidth="16"

    >
    <Levels>
    <ComponentArt:GridLevel
       AllowSorting="False"

        DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow"
				    SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10"
					  EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField"
				       EditCommandClientTemplateId="EditCommandTemplate"
            InsertCommandClientTemplateId="EditCommandTemplate"
                >
       <Columns>
          <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
          <ComponentArt:GridColumn DataField="c_u_Name"  HeadingText="Applications" Visible="true" AllowEditing="False" />
       </Columns>
    </ComponentArt:GridLevel>
    </Levels>
    </ComponentArt:Grid>
</TD>
<TD>



<ComponentArt:Grid ID="GRID_SelectBusRoles" runat="server" Width="300"
      DataSourceId="SQL_BusRoles"

        RunningMode="Client"
        ShowFooter="false"
        ScrollBar="Auto"
        ScrollTopBottomImagesEnabled="true"
        ScrollTopBottomImageHeight="2"
        ScrollTopBottomImageWidth="16"
        ScrollImagesFolderUrl="images/scroller/"
        ScrollButtonWidth="16"
        ScrollButtonHeight="17"
        ScrollBarCssClass="ScrollBar"
        ScrollGripCssClass="ScrollGrip"
        ScrollBarWidth="16"

    >
    <Levels>
    <ComponentArt:GridLevel
       AllowSorting="False"
        DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow"
				    SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10"
					  EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField"
				       EditCommandClientTemplateId="EditCommandTemplate"
            InsertCommandClientTemplateId="EditCommandTemplate"
                >
       <Columns>
          <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
          <ComponentArt:GridColumn DataField="c_u_Name" HeadingText="Roles" Visible="true" AllowEditing="False" />
       </Columns>
    </ComponentArt:GridLevel>
    </Levels>
    </ComponentArt:Grid>
</TD>
</TR>
</TABLE>
</DIV>





</asp:Content>
