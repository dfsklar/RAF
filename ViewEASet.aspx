<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewEASet.aspx.cs" Inherits="_6MAR_WebApplication.WebForm11" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
        SelectCommand="SELECT [IDentAssignment], [Bus. Role] AS broleName, [c_u_Abbrev], [c_u_StandardActivity], [c_u_RoleType], [c_u_System], [c_u_Platform], [c_u_EntitlementName], [c_u_EntitlementValue], [IDentAssSet], [IDentAssignment], [IDentitlement], [c_u_Commentary] FROM [DETAILSentitlementAssignmentSet] WHERE ([IDentAssSet] = @IDentAssSet)">
        <SelectParameters>
            <asp:QueryStringParameter Name="IDentAssSet" QueryStringField="IDeas" Type="Int32" DefaultValue="2" />
        </SelectParameters>
    </asp:SqlDataSource>

    <br />
    <br />THIS WILL BE POPULATED BY THE FORTHCOMING PUBLISHING FEATURES.

	 
	 </H2>

</asp:Content>
