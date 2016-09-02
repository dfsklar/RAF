<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_6MAR_WebApplication.WebForm129" Title="Untitled Page" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	     <script type="text/javascript" src="PAGEreportLaunchpad.js">
	     </script>


<table>

<tr>
<td style='text-align:right;vertical-align:top;padding-right:15px'>

<b>Choose subprocesses to be processed:</b>

<p>
<span class='linklookalike' onclick='MEGATREE.checkAll();'>Checkmark all &gt;</span>

<p>
<span class='linklookalike' onclick='MEGATREE.unCheckAll();'>Un-check all &gt;</span>


</td>

<td style='text-align:left'>


      <ComponentArt:TreeView id="MEGATREE" Height="150" Width="500"
        DragAndDropEnabled="false"
	ExpandNodeOnSelect="false"
	CollapseNodeOnSelect="false"
        NodeEditingEnabled="false"
        KeyboardEnabled="true"
        CssClass="TreeView"
        NodeCssClass="TreeNode"
        SelectedNodeCssClass="SelectedTreeNode"
        HoverNodeCssClass="HoverTreeNode"
        NodeEditCssClass="NodeEdit"
        LineImageWidth="19"
        LineImageHeight="20"
        DefaultImageWidth="16"
        DefaultImageHeight="16"
        ItemSpacing="0"
        ImagesBaseUrl="images/"
        NodeLabelPadding="3"
        ParentNodeImageUrl="folder.gif"
        ExpandedParentNodeImageUrl="folder_open.gif"
        LeafNodeImageUrl="file.gif"
        ShowLines="true"
        LineImagesFolderUrl="images/lines/"
        EnableViewState="true"
        runat="server" >
	<ClientEvents>
	  <NodeCheckChange EventHandler="OnCheckboxChange"/>
	  <NodeExpand      EventHandler="OnNodeExpand"/>
	  <NodeBeforeExpand      EventHandler="OnBeforeNodeExpand"/>
	  <Load            EventHandler="OnWebLoadComplete"/>
	</ClientEvents>
      </ComponentArt:TreeView>
     <span style="font-size: 8pt">Note: only active entitlement sets will be loaded.<br /></span>
</td>
</tr>




<tr>
<td colspan=2><hr/></td>
</tr>



<tr>
<td style='text-align:right;vertical-align:top;padding-right:15px'>
<b>Choose options:</b>
</td>

<td style='text-align:left'>
    <asp:CheckBox ID="CHKsaveToHistory" runat="server" />
       Save for analysis and history
</td>
</tr>



<tr>
<td colspan=2><hr/></td>
</tr>


<tr> 
<td style='text-align:right;vertical-align:top;padding-right:15px'>
<b>Launch a Comparison:</b>
</td>
<td>
    Select the SAP dump to upload:
    <asp:FileUpload ID="FileUpload1" runat="server" Width="463px" /><br />
       <span style="font-size: 7pt">CSV format, 4 columns: RoleName,Platform,Value,ComparisonType</span><br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Launch" OnClick="Button1_Click" /></p>
</td>
</tr>




<tr>
<td colspan=2><hr/></td>
</tr>


<tr> 
<td style='text-align:right;vertical-align:top;padding-right:15px'>
<b>Export to facilitate your own comparison (via Access, or Excel, etc.):</b>
</td>
<td>
    <asp:Button ID="Button2" runat="server" Text="Export" OnClick="ButtonExport_Click" /></p>
</td>
</tr>

</table> 


</asp:Content>
