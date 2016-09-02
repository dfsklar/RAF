<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGElaunchUpload.aspx.cs" Inherits="_6MAR_WebApplication.PAGElaunchUpload" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class='GUIbox'>

<p>
   <asp:Panel ID="PANELcond_AllowUpload" runat="server" Width="759px">
<DIV style='text-align:left'>
      <p>
<A href="media/TemplateEntitlementUpload.csv">Download a template</A> to use in preparing your data.<BR> Note that the columns may be reordered but the column names/titles must be exactly as shown.
      </p>
<div class='Careful'>
<p>
    1. Verify the accuracy of your session's current subprocess scope (see upper-right corner of webpage).</p>
    <p>
        2.
        Choose type of operation:<br />
        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Value="WS-Upload" Selected="True">Create new workspace and initialize with this upload.</asp:ListItem>
            <asp:ListItem Enabled="false" Value="WS-Append">[NOT IMPL YET]Copy currently-live entitlements into new workspace and append with this upload file.</asp:ListItem>
        </asp:RadioButtonList></p>
<p>
    3. Perform the upload of an "Entitlements" CSV file:<br />
        <asp:FileUpload ID="FileUpload2" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" /></p>
    <p>
        4. You will be automatically brought to the workspace editor page for the new workspace.
        &nbsp;</p>
</div></div>
   </asp:Panel>
   &nbsp;</p>
   <p>
      <asp:Panel ID="PANELcond_AbortUpload" runat="server" Width="756px">
         There is currently a workspace open and active for this subprocess.<br />
         <br />
         Therefore, you are not allowed to perform upload activities at this time, since
         AFWAC does not allow the existence of two workspaces open simultaneously for a single
         subprocess.<br />
      </asp:Panel>
      
      
            <asp:Panel ID="DIVimportFeeback" runat="server" Visible="False" Width="736px">
         <br />
         IMPORT SUCCEEDED... But please note these messages:<br />
         <br />
         <asp:TextBox ID="TXTimportEngineMessages" runat="server" Height="154px" ReadOnly="True"
            TextMode="MultiLine" Width="900px"></asp:TextBox><br />
         <br />
         After you have read any messages shown above,
         <a href="ListEWorkspaces.aspx">continue...</a>
         <br />
      </asp:Panel>

      &nbsp;</p>
</div>


</asp:Content>
