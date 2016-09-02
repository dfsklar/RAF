<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_6MAR_WebApplication.WebForm116" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<div class='GUIbox'>

<p>
   <asp:Panel ID="PANELcond_AllowUpload" runat="server" Width="759px">
<DIV style='text-align:left'>
      <p>
<A href="media/TemplateEntitlementUpload.csv">Download a template</A> to use in preparing your data.<BR> Note that the columns may be reordered but the significant column names/titles must be exactly as shown.&nbsp; Any columns with unrecognized names will be ignored silently.</p>
   <p>
      &nbsp;</p>


   <p>
<asp:Panel runat="Server" ID="PANELchooseApp" style='width:400px;background-color:white;padding-top:10px;padding-bottom:10px'>
      1. Specify the application:


&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
<div style="margin-left:45px">
<ComponentArt:ComboBox 
  ID="COMBOXchooseApp" runat="server" Width="250" 
  RunningMode="Client"
  AutoPostBack="false"
  DataSourceID="SQL_applicationList"
  DataValueField="c_id"
  DataTextField="c_u_Name"
  TextBoxEnabled="False"  
  HoverCssClass="comboBoxHover"  FocusedCssClass="comboBoxHover"
 TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
 ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
 DropImageUrl="images/drop.gif">
 <ClientEvents>
 </ClientEvents>

</ComponentArt:ComboBox>
<I style='font-size:9px'>Note: SAP entitlements cannot be uploaded on this screen.</I>
</div>



</asp:Panel>


<asp:SqlDataSource ID="SQL_applicationList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT c_id, c_u_Name FROM [t_RBSR_AUFW_u_Application] WHERE c_u_Name not like 'SAP'  ORDER BY [c_u_Name]">
</asp:SqlDataSource>



   <p>
      2. Specify the initial status to be given any new entitlement created by this upload:<br />
      &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
      <asp:DropDownList ID="COMBOXinitStatus" runat="server">
         <asp:ListItem Value="P">Pending</asp:ListItem>
         <asp:ListItem Selected="True" Value="A">Available</asp:ListItem>
         <asp:ListItem Value="I">Inactive</asp:ListItem>
      </asp:DropDownList></p>
<div class='Careful'>
<p>
   3. Select the CSV file from your local file system:<br />
      &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:FileUpload ID="FileUpload2" runat="server" Width="607px"/></p>
   <p>
      3. Launch:&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" /></p>
</div></div>
   </asp:Panel>
   &nbsp;</p>
   <p>
      
            <asp:Panel ID="DIVimportFeeback" runat="server" Visible="False" Width="736px">
         <br />
			IMPORT COMPLETED with these messages:<br/>
         <br />
         <asp:TextBox ID="TXTimportEngineMessages" runat="server" Height="154px" ReadOnly="True"
            TextMode="MultiLine" Width="900px"></asp:TextBox><br />
         <br />
      </asp:Panel>

      &nbsp;</p>
</div>


</asp:Content>
