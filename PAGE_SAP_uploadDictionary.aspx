<%@ Page Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="PAGE_SAP_uploadDictionary.aspx.cs" Inherits="_6MAR_WebApplication.WebForm130" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class='ts-m' style='height:20px;color:#FFFFFF;padding-top:8px;margin-bottom:30px'>
 &nbsp; &nbsp; <a style='color:#FFFFFF' href='PAGE_DictTcodes.aspx'>&lt;&lt;&lt;  back to SAP dictionary</a>
</div>

<br/>

<center>
<asp:Panel ID="DIVlaunchpad" runat="server" Visible="True" Width="736px">

<div class='GUIbox'>
<DIV style='text-align:left;width:800px'>
<p>
    <strong>This control panel allows you to upload dictionaries.&nbsp; It is not used for
        uploading entitlements/mappings.</strong><div class='Careful'>
<p>
    2. Choose the type of upload: &nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server"
            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="-" Selected="True">(select one)</asp:ListItem>
            <asp:ListItem Value="AOCLASS">AuthObj Classes</asp:ListItem>
            <asp:ListItem Value="AOBJ">Auth Objects</asp:ListItem>
        <asp:ListItem Value="AFLD">Auth Fields</asp:ListItem>
            <asp:ListItem Value="TC">TCodes</asp:ListItem>
        </asp:DropDownList></p>
    <p>
        &nbsp; &nbsp; &nbsp; CSV columns:&nbsp;
        <asp:Literal ID="LITERALcsvspec" runat="server" Text="(Select an upload type...)"></asp:Literal>&nbsp;</p>
            <p>
                &nbsp;</p>
    <p>
    3. Locate the file to be uploaded, and launch the activity:<br />
        <asp:FileUpload ID="FileUpload2" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" /></p>
</div>
</div>
</div>
</asp:Panel>
</center>




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
