<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListSAPWorkspaces.aspx.cs" Inherits="_6MAR_WebApplication.WebForm111" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT EW.c_id as THEID, c_u_tstamp, c_u_Commentary, t_RBSR_AUFW_u_User.c_u_Name FROM t_RBSR_AUFW_u_TcodeAssignmentSet EW INNER JOIN t_RBSR_AUFW_u_User ON   EW.c_r_User = t_RBSR_AUFW_u_User.c_id  WHERE EW.c_r_SubProcess = @c_r_SubProcess AND EW.c_u_Status='WORKSPACE' ORDER BY c_u_tstamp DESC">
                <SelectParameters>
            <asp:SessionParameter DefaultValue="-3" Name="c_r_SubProcess" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters>

    </asp:SqlDataSource>



<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
   BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor=Black GridLines=Vertical
        Width="859px" AutoGenerateSelectButton="False" DataKeyNames="THEID"  PageSize="8" 
>
        <Columns>
            <asp:BoundField DataField="c_u_tstamp" HeaderText="Initiated" SortExpression="c_u_TimeOfBirth" />
            <asp:BoundField DataField="c_u_Commentary" HeaderText="Commentary" SortExpression="c_u_Commentary" />
            <asp:BoundField DataField="c_u_Name" HeaderText="Owner" SortExpression="c_u_Name" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
       <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <br />




   <asp:Panel ID="PANELcond_NewSubpr" runat="server" Width="800px">
<div style='text-align:left'>
      This is a subprocess that does not have
any SAP role entitlements defined yet; work has not
      yet begun on this subprocess.
      <br />
      <br />
      To start work on this subprocess, 
      <asp:LinkButton ID="LinkButton1" onClick="ACTcreateBlankWorkspace" runat="server">create a blank workspace</asp:LinkButton>
      that will be owned by you and in which only you will be able to perform activities.

</div>
</asp:Panel>
    <br />



<!-- The page-load logic will choose one of these for display. -->
   <asp:Panel ID="PANELcond_InviteCreateWS" runat="server" Width="787px">
      There is currently no editing workspace active for the SAP role entitlements 
for this subprocess.
<a href="ACTcreateNewEntWorkspace.aspx?choose=latest">Click here to start editing</a> the current SAP role entitlement set for this subprocess.

   </asp:Panel>





   <asp:Panel ID="PANELcond_Locked" runat="server" Width="788px" Wrap="False">
      Currently another user is editing the SAP entitlement set for this subprocess.<BR>
If you wish, you may 
<a target="AFWACWkSpcWinSAP" href="SAPEntitlementWorkspace.aspx?ID=<%=this.idActiveWS%>">access it in read-only mode</A>.
</asp:Panel>




   <asp:Panel ID="PANELcond_IsOwnerOfCurrentWS" runat="server" Width="787px">
      If you have already opened this workspace in another window, please just move to
      that window.<br />
      <br />
      Otherwise, 
<a target="AFWACWkSpcWinSAP" href="SAPEntitlementWorkspace.aspx?ID=<%=this.idActiveWS%>">click here to launch a workspace window</a>.
      </asp:Panel>
 

</asp:Content>
