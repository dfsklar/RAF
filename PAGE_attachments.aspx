<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGE_attachments.aspx.cs" Inherits="_6MAR_WebApplication.WebForm124" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<script type="text/javascript" src='PAGE_attachments.js'></script>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_EASfileAttachment] WHERE c_r_EntAssignmentSet = @EASETID  ORDER BY [c_u_UploadDate]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-3" Name="EASETID" SessionField="INTcurWS"
                Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>


<B>All files attached to this workspace</B>

<ComponentArt:Grid ID="GRIDlistAttachments" runat="server" Width="800"
  DataSourceID="SqlDataSource1"
  ManualPaging="True"

  ShowFooter="False"

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
<ClientEvents>
 <ContextMenu EventHandler="Grid_onContextMenu" />
 <ItemSelect  EventHandler="Grid_onItemSelect" />
</ClientEvents>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
      EditCellCssClass="EditDataCell"
      EditFieldCssClass="EditDataField"
  >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
	<ComponentArt:GridColumn DataField="c_u_Filename" Width="230"
				 Visible="true" AllowEditing="False" HeadingText="Filename" />
	<ComponentArt:GridColumn DataField="c_u_UploadDate"
				 Visible="true" AllowEditing="False" HeadingText="Timestamp" />
	<ComponentArt:GridColumn DataField="c_u_Comment"
				 Visible="true" AllowEditing="False" HeadingText="Comment" />
       </Columns>
  </ComponentArt:GridLevel>
</Levels>
</ComponentArt:Grid>



    <br />
    <br />
<HR>
<iframe id="TheIframe" src='blank.htm' height='1px' width='1px'></iframe>
    <br />
    <B>Use this form to attach a file to this workspace.</B>
    <br />
    Commentary<br />
    &nbsp;<asp:TextBox ID="TXTcomment" runat="server" Height="50px" TextMode="MultiLine"
        Width="409px"></asp:TextBox><br />
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" Width="422px" />
    &nbsp;&nbsp;<br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />


<ComponentArt:Menu ID="ContextMenu" SiteMapXmlFile="CTXMENU_GRID_attachments.xml" ExpandSlide="none"
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



