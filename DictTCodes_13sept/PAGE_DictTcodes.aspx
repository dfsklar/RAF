<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGE_DictTcodes.aspx.cs" Inherits="_6MAR_WebApplication.WebForm123" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<script type="text/javascript" src='PAGE_DictTcodes.js'></script>


    <ComponentArt:TabStrip id="TabStrip1"
      CssClass="TopGroup"
      SiteMapXmlFile="PAGE_DictTcodes.sitemap.xml"
      DefaultItemLookId="DefaultTabLook"
      DefaultSelectedItemLookId="SelectedTabLook"
      DefaultDisabledItemLookId="DisabledTabLook"
      DefaultGroupTabSpacing="1"
      ImagesBaseUrl="images/"
      MultiPageId="MultiPage1"
      runat="server">
    <ItemLooks>
      <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="5" LabelPaddingBottom="4" LeftIconUrl="tab_left_icon.gif" RightIconUrl="tab_right_icon.gif" HoverLeftIconUrl="hover_tab_left_icon.gif" HoverRightIconUrl="hover_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
      <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="SelectedTab" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="4" LabelPaddingBottom="4" LeftIconUrl="selected_tab_left_icon.gif" RightIconUrl="selected_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
    </ItemLooks>
    </ComponentArt:TabStrip>









<ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server">





<ComponentArt:PageView CssClass="PageContent" runat="server">

<I>To edit description: select row, click on description, make your edit, hit Enter.</I>
<ComponentArt:Grid ID="Grid1" runat="server" Width="700"

  DataSourceID="SqlDataSource1"
  ManualPaging="True"

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

  AutoCallBackOnUpdate="true"

  HeaderCssClass="GridHeader"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
      EditCellCssClass="EditDataCell"
      EditFieldCssClass="EditDataField"
  >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="c_u_TCodeID" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="TCode" />
	<ComponentArt:GridColumn DataField="c_u_Description"
				 Visible="true" AllowEditing="True" HeadingText="Description" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
</ComponentArt:Grid>
    &nbsp;
    <INPUT type="Button" ID="BTNaddTcodeRow" OnClick="javascript:BTNaddTcodeRow_Click();" Value="Add"/></TD>


</ComponentArt:PageView>







<ComponentArt:PageView CssClass="PageContent" runat="server">

<TABLE class='verttop'><TR><TD>

<ComponentArt:Grid ID="GRID_orgcat" runat="server" Width="330"

  DataSourceID="SQL_OrgCat"

  ManualPaging="True"

    AllowEditing="False"

  RunningMode="Callback"
  EnableViewState="true"

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

  HeaderCssClass="GridHeader"
>
        <ClientEvents>
            <ContextMenu EventHandler="GridOrgCat_onContextMenu" />
            <ItemSelect  EventHandler="GridOrgCat_onItemSelect" />
        </ClientEvents>

<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
  >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="c_u_SAP_Name" Width="80"
				 Visible="true" AllowEditing="False" HeadingText="SAP Name" />
	<ComponentArt:GridColumn DataField="c_u_English_Name"
				 Visible="true"  HeadingText="Description" />
       </Columns>
  </ComponentArt:GridLevel>

</Levels>
<ServerTemplates>
    <ComponentArt:GridServerTemplate Id="TEMPLlistRelatedOrgValues">
       <template>X  <%# this.HumanReadableRelatedOrgValueList(Container.DataItem["c_id"].ToString())%>     </template>
    </ComponentArt:GridServerTemplate>
</ServerTemplates>
</ComponentArt:Grid>

<!--
	<ComponentArt:GridColumn DataField="c_id"
				 HeadingText="Value Range"
				 DataCellServerTemplateId="TEMPLlistRelatedOrgValues" />
-->

</TD>
<TD>
<ComponentArt:CallBack id="CALLBACK_GRID_orgvalue" runat="server">
<Content ID="CALLBACK_GRID_orgvalue">
<ComponentArt:Grid ID="GRID_orgvalue" runat="server" Width="180"

  DataSourceID="SQL_OrgValue"
  ManualPaging="True"

  RunningMode="Callback"

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

  AutoCallBackOnUpdate="true"

  HeaderCssClass="GridHeader"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
      EditCellCssClass="EditDataCell"
      EditFieldCssClass="EditDataField"
  >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="c_u_Name" Width="80"
				 Visible="true" AllowEditing="False" HeadingText="Value" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
</ComponentArt:Grid>
</Content>
</ComponentArt:CallBack>

</TD>

</TR>

</TABLE>

    &nbsp;
    <INPUT type="Button" OnClick="javascript:MENUACT_orgaxis_newfromscratch();" Value="Add"/></TD>
</ComponentArt:PageView>






<ComponentArt:PageView CssClass="PageContent" runat="server">
<SPAN style='color:red'><B><I>NOT YET SUPPORTING EDITING/ADDING</I></B></SPAN>
    &nbsp;
    <INPUT type="Button"  OnClick="javascript:BTNaddOrgValue_Click();" Value="Add"/></TD>
</ComponentArt:PageView>







</ComponentArt:MultiPage>




    


<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_TcodeDictionary] ORDER BY [c_u_TcodeID]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SQL_OrgCat" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="SELECT *, 0 as dummy FROM [t_RBSR_AUFW_u_SAPsecurityOrgAxis] ORDER BY [c_u_SAP_Name]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SQL_OrgValue" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  OnSelecting="INTERCEPTsqldatasource_sqlorgvalue_select"
  SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_SAPsecurityOrg] WHERE c_r_SAPsecurityOrgAxis = @FILTERorgaxis  ORDER BY [c_u_Name]">
   <SelectParameters>
     <asp:Parameter Name="FILTERorgaxis" Type="Int32" DefaultValue="-342"/>
   </SelectParameters>
</asp:SqlDataSource>



<ComponentArt:Menu ID="GridOrgAxisContextMenu" SiteMapXmlFile="PAGE_DictTcodes_OrgAxis_MenuData.xml" ExpandSlide="none"
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






<div id="JQDLGeditOrgAxis" class='JQdialog' title='Org Category'>

<form id='FORMJQ' name='FORMJQ' method='POST' action='self.htm' >

<table class='dlgcustomgrid'>

<tr>
<td>SAP Name:</td>
<td><INPUT type='text' id="JQDLGeditOrgAxis_sapname"></INPUT></td>
</tr>

<tr>
<td>Description:</td>
<td>
<INPUT type='text' id="JQDLGeditOrgAxis_descr"></INPUT>
</td>
</tr>

<tr>
<td>Legal values:<BR><I>comma-separated</I></td>
<td>
<INPUT type='text' id="JQDLGeditOrgAxis_legalvals"></INPUT>
</td>
</tr>

</table>
</FORM>
</div>



</asp:Content>
