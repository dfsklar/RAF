<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGE_DictTcodes.aspx.cs" Inherits="_6MAR_WebApplication.WebForm123" Title="Untitled Page" %>






<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <ComponentArt:Menu ID="GridTCodesContextMenu" SiteMapXmlFile="CTXMENU_PAGE_DictTcodes.xml" ExpandSlide="none"
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

    <ComponentArt:Menu ID="GridAuthClassContextMenu" 
        SiteMapXmlFile="CTXMENU_PAGE_DictTcodes_AuthClass.xml" ExpandSlide="none"
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

    <ComponentArt:Menu ID="GridAuthObjContextMenu" 
        SiteMapXmlFile="CTXMENU_PAGE_DictTcodes_AuthObj.xml" ExpandSlide="none"
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


<script type="text/javascript" src='PAGE_DictTcodes.js'></script>
<script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>





<B>This information's accessibility is NOT related to workspace existence or ownership.</B>
<br/>&nbsp;<br/>
<B><U>PLANNED</U> ACCESS CONTROL: Viewable by all, but Editable only by users of type 'admin'.</B>
<br/>&nbsp;



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

<DIV style='text-align:right;margin-bottom:7px'>
    <A href="javascript:BTNaddTcodeRow_Click();">Add New TCode</A> &nbsp;
    <A href="PAGE_SAP_uploadDictionary.aspx?type=TC">Upload</A> 
</DIV>




<!--
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
-->


<p style='color:red;background-color:yellow'><b>THIS GRID SHOWS ONLY 20 ITEMS.  Use the search feature to find particular TCodes.</b></p>

<ComponentArt:Grid ID="Grid1" runat="server" Width="600"


  ManualPaging="True"

  RunningMode="Server"

 ShowHeader="true"
 ShowSearchBox="true" 
 SearchOnKeyPress="false"
 HeaderHeight="18"
 HeaderCssClass="GridHeader" 
 SearchText="Search (hit Enter to refresh):"
 SearchTextCssClass="GridHeaderText"
 GroupByTextCssClass="GroupByText"
 GroupByCssClass="GroupByCell"
 GroupingNotificationTextCssClass="GridHeaderText"
 GroupingNotificationText=""
 

  ShowFooter="false"


  AutoCallBackOnUpdate="true"
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
	<ComponentArt:GridColumn DataField="c_u_TcodeID" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="TCode" />
	<ComponentArt:GridColumn DataField="c_u_Description"
				 Visible="true" AllowEditing="False" HeadingText="Description" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
        <ClientEvents>
            <ContextMenu EventHandler="GridTCodes_onContextMenu" />
            <ItemSelect  EventHandler="GridTCodes_onItemSelect" />
        </ClientEvents>
</ComponentArt:Grid>

</TD>


</ComponentArt:PageView>









<ComponentArt:PageView CssClass="PageContent" runat="server">

<DIV style='text-align:right;margin-bottom:7px'>
    <A href="javascript:LaunchNameDescrDlog_newfromscratch('AuthObj Class', GRID_authobjclasses);">Add New Class</A> &nbsp;
    <A href="PAGE_SAP_uploadDictionary.aspx?type=AOCLASS">Upload</A>
</DIV>

<ComponentArt:Grid ID="GRID_authobjclasses" runat="server" Width="600"

  DataSourceID="SQL_AuthObjClasses"
  ManualPaging="True"

  RunningMode="Client"

 ShowHeader="true"
 ShowSearchBox="true" 
 SearchOnKeyPress="false"
 HeaderHeight="18"
 HeaderCssClass="GridHeader" 
 SearchText="Search (hit Enter to refresh):"
 SearchTextCssClass="GridHeaderText"
 GroupByTextCssClass="GroupByText"
 GroupByCssClass="GroupByCell"
 GroupingNotificationTextCssClass="GridHeaderText"
 GroupingNotificationText=""

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
	<ComponentArt:GridColumn DataField="c_u_Name" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="Class" />
	<ComponentArt:GridColumn DataField="c_u_Description"
				 Visible="true" AllowEditing="False" HeadingText="Description" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
        <ClientEvents>
            <ContextMenu EventHandler="GridAuthClasses_onContextMenu" />
        </ClientEvents>
</ComponentArt:Grid>

</ComponentArt:PageView>





<ComponentArt:PageView CssClass="PageContent" runat="server">

<DIV style='text-align:right;margin-bottom:7px'>
    <A href="javascript:alert('To register new objects, visit the Classes tab and right-click on the relevant class.');">Add New Object</A> &nbsp; 
    <A href="PAGE_SAP_uploadDictionary.aspx?type=AOBJ">Upload Objects</A> &nbsp;
    <A href="PAGE_SAP_uploadDictionary.aspx?type=AFLD">Upload Fields</A> 
</DIV>

<ComponentArt:Grid ID="GRID_authobjects" runat="server" Width="600"

  DataSourceID="SQL_AuthObjects"
  ManualPaging="True"

  RunningMode="Client"

  AllowSorting="true"

 ShowHeader="true"
 ShowSearchBox="true" 
 SearchOnKeyPress="false"
 HeaderHeight="18"
 HeaderCssClass="GridHeader" 
 SearchText="Search (hit Enter to refresh):"
 SearchTextCssClass="GridHeaderText"
 GroupByTextCssClass="GroupByText"
 GroupByCssClass="GroupByCell"
 GroupingNotificationTextCssClass="GridHeaderText"
 GroupingNotificationText=""

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

  ImagesBaseUrl="images/" 

  AutoCallBackOnUpdate="true"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="True"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
      EditCellCssClass="EditDataCell"
      EditFieldCssClass="EditDataField"
      SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10"   >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="ClassName" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="Class" />
	<ComponentArt:GridColumn DataField="ObjName" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="Object" />
	<ComponentArt:GridColumn DataField="ObjDescr"
				 Visible="true" AllowEditing="False" HeadingText="Description" />
	<ComponentArt:GridColumn DataField="c_id" Width="30" fixedWidth="True"
	            DataCellClientTemplateId="TEMPLhovershowfield"
				 Visible="true" AllowEditing="False" HeadingText="Fields" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
<ClientTemplates>
            <ComponentArt:ClientTemplate ID="TEMPLhovershowfield">
	       ## "<IMG onmouseout='hoverhide()' onmouseover='hovershow(this,-1)' onclick='hovershow(this,\\"" +
		      DataItem.ClientId + "\\")' src='images/msg_unread.gif' border=0/>"
 ##
            </ComponentArt:ClientTemplate>
</ClientTemplates>
        <ClientEvents>
            <ContextMenu EventHandler="GridAuthObjs_onContextMenu" />
        </ClientEvents>
</ComponentArt:Grid>

</ComponentArt:PageView>







</ComponentArt:MultiPage>




    


<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_TcodeDictionary] ORDER BY [c_u_TcodeID]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SQL_AuthObjClasses" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_SAPauthClass] WHERE c_u_Status<>'X' ORDER BY [c_u_Name]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SQL_AuthObjects" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="
SELECT 
       OBJD.c_id,
       OBJD.c_u_Name as ObjName,
       OBJD.c_u_Description as ObjDescr,
       CLD.c_u_Name as ClassName,
       CLD.c_u_Description as ClassDescr
FROM [t_RBSR_AUFW_u_SAPauthObj] OBJD

LEFT OUTER JOIN [t_RBSR_AUFW_u_SAPauthClass] CLD 
   ON CLD.c_id = OBJD.c_r_SAPauthClass

WHERE OBJD.c_u_Status<>'X' AND CLD.c_u_Status<>'X' ORDER BY OBJD.c_u_Name">
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










<div id="JQDLGnamePlusDescr" class='JQdialog' title='Edit Name/Description'>

<form id='FORMJQ' name='FORMJQ' method='POST' action='self.htm' >

<table class='dlgcustomgrid'>


<tr id='JQDLGnamePlusDescr_sectionrow_class'>
<td>Class:</td>
<td><INPUT READONLY type='text' id="JQDLGnamePlusDescr_class"></INPUT></td>
</tr>

<tr>
<td>Name:</td>
<td><INPUT type='text' id="JQDLGnamePlusDescr_name"></INPUT></td>
</tr>

<tr>
<td>Description:</td>
<td>
<INPUT size="30" type='text' id="JQDLGnamePlusDescr_descr"></INPUT>
</td>
</tr>


</table>
</FORM>
</div>









<div id='hoverpopup' 
   style='font-size:9px;z-index:10000;text-align:left;visibility:hidden; position:absolute; top:0; left:0; width:180px;height:100px; padding:7px; background-color:#CCCCFF;filter:alpha(opacity=90)'>
  <span id='_hoverpopup'></span>
</div>


</asp:Content>
