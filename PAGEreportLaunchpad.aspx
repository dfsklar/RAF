<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGEreportLaunchpad.aspx.cs" Inherits="_6MAR_WebApplication.WebForm120" Title="Untitled Page" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src="PAGEreportLaunchpad.js">
</script>



<table>

<tr>

<td>

<b>Choose roles to be processed:</b>

<p>
<span class='linklookalike' onclick='MEGATREE.checkAll();MEGATREE.expandAll();'>Checkmark all &gt;</span>
</p>

<p>
<span class='linklookalike' onclick='MEGATREE.unCheckAll();'>Un-check all &gt;</span>
</p>

</td>

<td style='width:30px'>&nbsp;</td>

<td>


      <ComponentArt:TreeView id="MEGATREE" Height="300" Width="500"
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
	</ClientEvents>
      </ComponentArt:TreeView>

</td>
<!--
<td>
<div style='text-align:left;width:230px;margin-bottom:30px;color:#FF0000'>
<B>TIP: To auto-checkmark all roles belonging to a subprocess:</B>
<OL style='font-weight:bold'>
<LI>Open the subprocess' tree node to expose the list of roles.
<br/>&nbsp;
<LI>Checkmark the subprocess itself &mdash; that will auto-checkmark all its roles.
<br/>&nbsp;
</OL>
(Future user interface improvements will simplify this process.)
</div>
</td>
-->
</tr>
</table>





    <asp:Panel ID="Panel2" runat="server">
    <div style='text-align:left;width:450px'>

    <asp:CheckBox ID="CheckBox1" runat="server" Text="Place each role on a different worksheet/tab" OnCheckedChanged="CheckBox1_CheckedChanged1"  />
    <br/>
    <asp:CheckBox ID="CHKforFullDetailCSV" runat="server" Text="CSV, full detail" AutoPostBack="True" OnCheckedChanged="CHKforFullDetailCSV_CheckedChanged"/>
    <br/>
    <asp:CheckBox ID="CHKforIDMuploadCSV" runat="server" Text="CSV, 3-column reconciliation format" AutoPostBack="True" OnCheckedChanged="CHKforIDMuploadCSV_CheckedChanged"/>
    <br/>
    <asp:CheckBox ID="CHKshowOnlyNetEffect" runat="server" Text="Show only net effect (for workspace contexts only)"
            ToolTip='For any subprocesses for which you have chosen a workspace context, this will have an effect.  For "active/live" context, this is ignored.'
             />

    </div>
    </asp:Panel>



    &nbsp;
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate Report" /><br />
<span style='color:red'>Click ONCE and then be patient; it may be a full minute before the download starts.</span>
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="41px" Width="287px">
    </asp:Panel>
    <iframe runat="server" id="TheIframe" src='blank.htm' height='5px' width='5px' /> 




</asp:Content>
