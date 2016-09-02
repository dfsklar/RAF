<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGElaunchSAPUpload.aspx.cs" Inherits="_6MAR_WebApplication.WebForm19" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<asp:Panel ID="DIVlaunchpad" runat="server" Visible="True" Width="736px">

<div class='GUIbox'>
<DIV style='text-align:left;width:800px'>
<p>

<A href="media/TemplateSAProlenames.csv">Download a template</A> to use in preparing your data.  The first column (role name) is required, but the description column may be left blank.

<div class='Careful'>
<p>
  
        1.
Check to ensure you are uploading a file that contains only SAP roles that are for your current subprocess scope.
<p>
    2. Locate the file to be uploaded, and launch the activity:<br />
        <asp:FileUpload ID="FileUpload2" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" /></p>
</div>
</div>
</div>
</asp:Panel>




      <asp:Panel ID="DIVimportFeeback" runat="server" Visible="False" Width="736px">
         <br />
         IMPORT COMPLETED.  Please peruse the generated messages:<br />
         <br />
         <asp:TextBox ID="TXTimportEngineMessages" runat="server" Height="154px" ReadOnly="True"
            TextMode="MultiLine" Width="900px"></asp:TextBox><br />
         <br />
         You may move to another tab to perform another activity.
         <br />
      </asp:Panel>





</asp:Content>
