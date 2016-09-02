<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGE_idmreconcile.aspx.cs" Inherits="_6MAR_WebApplication.WebForm125" Title="Untitled Page" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	     <script type="text/javascript" src="PAGEreportLaunchpad.js">
	     </script>

       <asp:Panel ID="Panel1" runat="server" Visible="False">
           DOWNLOAD FOR DANIEL<p>
       <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Click here to download </asp:LinkButton>
       a CSV to use for Daniel's manual reconciliation process.</p>
   <p>Includes all active entitlement sets for all subprocesses except "TEST" ones.</p>
       </asp:Panel>


<table>

<tr>
<td style='text-align:right;vertical-align:top;padding-right:15px'>

<b>Choose roles to be processed:</b>

<p>
<span class='linklookalike' onclick='MEGATREE.checkAll();'>Checkmark all &gt;</span>
</p>

<p>
<span class='linklookalike' onclick='MEGATREE.unCheckAll();'>Un-check all &gt;</span>
</p>

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
<td colspan="2"><hr/></td>
</tr>



<tr>
<td style='text-align:right;vertical-align:top;padding-right:15px'>
<b>Choose options:</b>
</td>

<td style='text-align:left'>
    <asp:CheckBox ID="CHKincludeInternalRoles" runat="server" />
       Include list of IDM "ELE_INT_" and "INT_" roles.
    <br />
    <asp:CheckBox ID="CHKsaveToHistory" runat="server" />
       Save for analysis and history
</td>
</tr>



<tr>
<td colspan="2"><hr/></td>
</tr>


<tr> 
<td style='text-align:right;vertical-align:top;padding-right:15px'>
<b>Launch:</b>
</td>
<td>
    Select the IDM file to upload:
    <asp:FileUpload ID="FileUpload1" runat="server" Width="463px" /><br />

<div style="padding:10px;background-color:#FFFFAA;text-align:left;font-size: 8pt">
CSV format, 3 columns: RoleName, Entitlement, ComparisonObject
<br />
Legal comparison objects:<br/>
<div style='padding-left:15px'>
RoleDescription, Entitlement, PrimaryApprover
</div>

<div style='color:#008800;margin-top:10px'>
<b>NEW (19-Mar):</b>
Deltas in the PrimaryApprover information are now being reported, but they
do not contribute (yet) to any counters shown on the metadata page.
</div>
</div>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Launch" OnClick="Button1_Click" Enabled="True" />
  &lt;&lt; Click only <u>once</u>... and then be patient while awaiting the download popup...
</td>
</tr>

</table>


    <iframe runat="server" id="TheIframe" src='blank.htm' height='5px' width='5px' /> 


</asp:Content>
