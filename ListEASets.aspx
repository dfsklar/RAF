<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListEASets.aspx.cs" Inherits="_6MAR_WebApplication.WebForm1" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT t_RBSR_AUFW_u_EntAssignmentSet.c_id, t_RBSR_AUFW_u_EntAssignmentSet.c_u_BOOLisActive, t_RBSR_AUFW_u_EntAssignmentSet.c_u_DATETIMElock, t_RBSR_AUFW_u_EntAssignmentSet.c_u_Commentary, t_RBSR_AUFW_u_EntAssignmentSet.c_r_SubProcess, t_RBSR_AUFW_u_EntAssignmentSet.c_r_User, t_RBSR_AUFW_u_User.c_u_Name, t_RBSR_AUFW_u_User.c_u_EID FROM t_RBSR_AUFW_u_EntAssignmentSet INNER JOIN t_RBSR_AUFW_u_User ON t_RBSR_AUFW_u_EntAssignmentSet.c_r_User = t_RBSR_AUFW_u_User.c_id  WHERE (t_RBSR_AUFW_u_EntAssignmentSet.c_r_SubProcess = @c_r_SubProcess) ORDER BY t_RBSR_AUFW_u_EntAssignmentSet.c_u_DATETIMElock DESC">
                <SelectParameters>
            <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />THIS WILL BE POPULATED BY THE FORTHCOMING PUBLISHING FEATURES.

    &nbsp;
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
        Width="802px" AutoGenerateSelectButton="True" DataKeyNames="c_id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="8" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <Columns>
            <asp:BoundField DataField="c_u_BOOLisActive" HeaderText="Active?" SortExpression="c_u_BOOLisActive" />
            <asp:BoundField DataField="c_u_DATETIMElock" HeaderText="FreezeTime" SortExpression="c_u_DATETIMElock" />
            <asp:BoundField DataField="c_u_Commentary" HeaderText="Commentary" SortExpression="c_u_Commentary" />
            <asp:BoundField DataField="c_u_Name" HeaderText="User" SortExpression="c_u_Name" />
            <asp:BoundField DataField="c_u_EID" HeaderText="EID" SortExpression="c_u_EID" />
        </Columns>
        <RowStyle BackColor="White" ForeColor="#003399" />
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
    </asp:GridView>
    &nbsp;&nbsp;<br />
    <br />
</asp:Content>
