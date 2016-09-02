<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGElaunchBRoleEntitlementsUpload.aspx.cs" Inherits="_6MAR_WebApplication.WebForm121" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class='GUIbox'>
<DIV style='text-align:left;width:800px'>

<p>



<asp:Panel ID="PANELcond_AllowUpload" runat="server" Width="759px">


    <ComponentArt:TabStrip id="TabStrip1"
      CssClass="TopGroup"
      SiteMapXmlFile="PAGElaunchBRoleEntitlementUpload.sitemap.xml"
      DefaultItemLookId="DefaultTabLook"
      DefaultSelectedItemLookId="SelectedTabLook"
      DefaultDisabledItemLookId="DisabledTabLook"
      DefaultGroupTabSpacing="1"
      ImagesBaseUrl="images/"
      MultiPageId="MultiPage1"
      runat="server">
    <ItemLooks>
      <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="5" LabelPaddingBottom="4" LeftIconUrl="tab_left_icon.gif" RightIconUrl="tab_right_icon.gif" HoverLeftIconUrl="hover_tab_left_icon.gif" HoverRightIconUrl="hover_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
      <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="SelectedTab" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="4" LabelPaddingBottom="4" LeftIconUrl="selected_tab_left_icon.gif" RightIconUrl="selected_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
    </ItemLooks>
    </ComponentArt:TabStrip>


<ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server">


<ComponentArt:PageView CssClass="PageContent" runat="server">

      <p>
<A href="media/TemplateBroleEntitlementUpload.csv">Download a template</A> to use in preparing your data.  Note that the columns may be reordered but the column names/titles must be exactly as shown.
      </p>
      <p><i>This template differs from that used in the "B.Ents" section only in this way: this template requires the "Application" column, and this template allows for a "Business Role" column.</i></p>
      </p>
<div class='Careful'>
<p>
    1. Specify the business role OR leave blank if specified in the input rows:
      &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
<div style="margin-left:45px">
<ComponentArt:ComboBox 
  ID="COMBOXchooseBrole" runat="server" Width="350" 
  RunningMode="Client"
  AutoPostBack="false"
  DataSourceID="SQL_broleList"
  DataValueField="c_id"
  DataTextField="Name"
  TextBoxEnabled="False"  
  HoverCssClass="comboBoxHover"  FocusedCssClass="comboBoxHover"
 TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
 ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
 DropImageUrl="images/drop.gif">
 <ClientEvents>
 </ClientEvents>

</ComponentArt:ComboBox></div>

<asp:SqlDataSource 
      ID="SQL_broleList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT c_id, c_u_Name AS Name FROM [t_RBSR_AUFW_u_BusRole] WHERE c_r_SubProcess = @CURSUBPR ORDER BY Name ">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-3" Name="CURSUBPR" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>






<p>
2. Specify the action that should be done if an unregistered entitlement is encountered:
      &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
<div style="margin-left:45px">
<ComponentArt:ComboBox 
  ID="COMBOXchooseUnregEntAction" runat="server" Width="350" 
  RunningMode="Client"
  AutoPostBack="false"
  TextBoxEnabled="False"  
  HoverCssClass="comboBoxHover"  FocusedCssClass="comboBoxHover"
 TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown" ItemCssClass="comboItem"
 ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" DropHoverImageUrl="images/drop_hover.gif"
 DropImageUrl="images/drop.gif">
<Items>
  <ComponentArt:ComboBoxItem  Text="Reject that row, but continue processing" Value="REJECT" />
  <ComponentArt:ComboBoxItem  Text="Register as new entitlement already in status 'Active'" Value="ADDa" />
</Items>
<ClientEvents>
</ClientEvents>
</ComponentArt:ComboBox>
</div>


<p>
    3. Find the file to upload, and initiate the action:
<div style="margin-left:45px">
      <asp:FileUpload ID="FileUpload2" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" />
</div>

<p>
</div>
</div>

</ComponentArt:PageView>








<ComponentArt:PageView CssClass="PageContent" runat="server">

<p>This allows you to do BULK REMOVAL of entitlements.

<p>You can do bulk removal across all subprocesses with a single upload.

<p>The uploaded CSV must have two columns: RoleName,PrivString

<p>The subprocess of each role is auto-determined.  If the particular subprocess does not have an open workspace, the edit is NOT performed.

<p>If the subprocess has a workspace, it is used for the edit EVEN IF the workspace is owned by a different user (not you).

<p>
To launch: Find the file to upload, and initiate the action:
<div style="margin-left:45px">
      <asp:FileUpload ID="FileUpload_BulkRemove" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="BTNbulkremove" runat="server" OnClick="ONCLICK_BulkRemove" Text="Upload" />
</div>

</ComponentArt:PageView>




<ComponentArt:PageView   CssClass="PageContent" runat="server">

<p>
This allows you to add new business roles to the CURRENT SUBPROCESS (be sure to check the "scope" header before performing this action).
</p>

<p>
Each role should be listed on its own row of the CSV file.  The first three columns are MANDATORY; the last two columns can have empty cells.
</p>

<p>
The CSV file should start with a header row, with exactly these column names:
<br />
Role Name,Description,Role Type,Primary Approver,Primary Owner
</p>

<p>The "Role Type" must be a single letter:  A, E, or F
</p>

<p>If the role is already known to R-AF, then the only action will be updating the owner/approver information if provided.</p>

<p>
To launch: Find the file to upload, and initiate the action:
</p>
<div style="margin-left:45px">
      <asp:FileUpload ID="FileUpload_AddRoles" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Launch_AddRoles" runat="server" OnClick="ONCLICK_BulkUploadNewBusRoles" Text="Upload" />
</div>
</ComponentArt:PageView>





<ComponentArt:PageView  CssClass="PageContent" runat="server">

<p>This allows you to do BULK COMPLETE RESPECIFICATION of role owners, approvers, etc. across all subprocesses with a single upload.
</p>

<p>The uploaded CSV must have three columns: Role Name, Type, EID
</p>

<p>The "Type" value must be one of these:  Primary Approver, Primary Owner
</p>

<p>These edits take effect immediately, as they do not require sandboxing inside a workspace prior to effect.
</p>

<p>WARNING: The existing businessrole-to-personnel mappings will be ERASED.  Your file is expected to be have the complete set of mappings for the entire RAF system across all subprocesses!  We strongly suggest you first <a href='export/BusRolePersonnelDump.ashx'>download a backup of the entire role-to-personnel table</a> for safety.
</p>

<p>
To launch: Find the file to upload, and initiate the action:
</p>
<div style="margin-left:45px">
      <asp:FileUpload ID="FileUpload_PersonnelMappings" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Button_PersonnelMappings" runat="server" OnClick="ONCLICK_BulkUploadPersonnel" Text="Upload" />
</div>

</ComponentArt:PageView>




</ComponentArt:MultiPage>


</asp:Panel>





      <asp:Panel ID="PANELcond_AbortUpload" runat="server" Width="756px">
		<DIV style='text-align:left'>
         This activity is only available if there is a workspace that is open for this subprocess and owned by you.<br />
			</div>
      </asp:Panel>



      <asp:Panel ID="DIVimportFeeback" runat="server" Visible="False" Width="736px">
         <br />
         IMPORT COMPLETED.  Please peruse the generated messages:<br />
         <br />
         <asp:TextBox ID="TXTimportEngineMessages" runat="server" Height="154px" ReadOnly="True"
            TextMode="MultiLine" Width="900px"></asp:TextBox><br />
         <br />
      </asp:Panel>
      &nbsp;</p>
</div>


</asp:Content>
