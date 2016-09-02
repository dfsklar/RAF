<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Codebehind="PAGE_BRoles_LaunchNewWorkspace.aspx.cs"
    Inherits="_6MAR_WebApplication.WebForm15" Title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<p>Enter commentary to attach to the new workspace:</p>

        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" OnTextChanged="TextBox1_TextChanged"
            Width="321px" Height="64px"></asp:TextBox>
    <br />
    <br />
             <asp:Button ID="Button1" runat="server" Text="Launch" OnClick="Button1_Click1" /><br />
            After you press this button, please be patient and wait a minute for
    the action to complete...<br />
    <br />
            
    

    
    
    <asp:SqlDataSource ID="DSfindActiveEASetToClone" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT * FROM [t_RBSR_AUFW_u_EntAssignmentSet] WHERE (([c_u_BOOLisActive] = @c_u_BOOLisActive) AND ([c_r_SubProcess] = @c_r_SubProcess))">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="c_u_BOOLisActive" Type="Byte" />
            <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="DSfindLatestEASetToClone" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT [c_id] FROM [t_RBSR_AUFW_u_EntAssignmentSet]  WHERE ([c_r_SubProcess] = @c_r_SubProcess) ORDER BY [c_u_DATETIMElock] DESC">
                <SelectParameters>
      
            <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters></asp:SqlDataSource>
</asp:Content>
