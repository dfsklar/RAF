<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGElaunchSAPentitlementsUpload.aspx.cs" Inherits="_6MAR_WebApplication.WebForm112" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">





<div class='GUIbox'>
<div style='text-align:left;width:800px'>

<p>



<asp:Panel ID="PANELcond_AllowUpload" runat="server" Width="759px">

    <ComponentArt:TabStrip id="TabStrip1"
      CssClass="TopGroup"
      SiteMapXmlFile="PAGElaunchSAPentUpload.sitemap.xml"
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

<div class='Careful'>

      <p>
<A href="media/TemplateSAPuploadEntsMar2010.csv">Download a self-documenting template</A> to use in preparing your data.  Note that the columns may be reordered and the sheet may include other columns (which will be ignored) &mdash; but the column names/titles must as shown.
      </p>



<p>
1. Note that each row's "Role Name" and "Platform" values will determine the recipient of the entitlement.
Rows with not-yet-known role+platform permutations will generate error messages.
<br/>


<p>
2. Tell us how rows with not-yet-registered TCodes should be handled:
    <asp:RadioButtonList RepeatDirection="Horizontal" ID="RADIOhowHandleNonregTcodes" runat="server">
    <asp:ListItem Value='ERR'>Error</asp:ListItem>
    <asp:ListItem Value='WARN' Selected=True>Warning</asp:ListItem>    
    </asp:RadioButtonList>
    
<br/>


<p>
3. Note that the entitlement assignments are being placed in <I>this</I>
workspace, thus scoped to the currently active subprocess.


<p>4. 
<span style='color:#FF0000'>[NEW 01-APR-2010] Note that existing assignments will be automatically deleted as needed to ensure that a particular TCode is assigned to a particular SAP role no more than once.</span>

<p>
    5. Add entitlements to the current workspace by uploading an "SAP Entitlements" (SAProle-to-TCode/Org/AuthObj) CSV file:<br />
        <asp:FileUpload ID="FileUpload2" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" /></p>



<p>
    6. You will be automatically brought to the page that allows you to launch a workspace window.
        &nbsp;</p>


<!-- End of div class=Careful -->
</div>

</ComponentArt:PageView>











<ComponentArt:PageView CssClass="PageContent" runat="server">
<div class='Careful'>
      <p>Column headings expected:<br/>&nbsp;&nbsp;&nbsp;&nbsp;<tt style='font-size:13px;font-weight:bold'>AGR_NAME,VARBL,LOW,HIGH</tt>
<br/>
Note that the columns may be reordered and the sheet may include other columns (which will be ignored).
      </p>



<p>
1. The AGR_NAME column must be the name of an SAP role in THIS subprocess.
Rows not meeting that criteria generate error messages (but processing continues).
<br/>

<p>
2. The org-value variable names and low/high values are NOT verified in any way.

<p>
3. Uploaded vectors that are already known are silently ignored.

<p>
4. WARNING: Existing vectors that are not in the uploaded data are silently retained!  No vectors are ever "marked for deletion" via this upload.  <b>This upload can only be used to ADD, not to revise or repair!</b>

<p>
5. Locate the CSV file then click "Upload" to add its org-value mappings to the current workspace:<br/>
   <asp:FileUpload ID="FileUpload2A" runat="server" Width="377px" />&nbsp; &nbsp;<asp:Button ID="Button1A" runat="server" OnClick="Button1A_Click" Text="Upload" /></p>


<!-- End of div class=Careful -->
</div>

</ComponentArt:PageView>



<ComponentArt:PageView CssClass="PageContent" runat="server">
COMING SOON.
</ComponentArt:PageView>


</ComponentArt:MultiPage>



<!-- This is the end of the PANEL that is displayed if there is indeed a workspace open. -->
</asp:Panel>






<asp:Panel ID="PANELcond_AbortUpload" runat="server" Width="756px">
		<DIV style='text-align:left'>
         This activity is only available if there is a SAP workspace that is open for this subprocess and owned by you.<br />
			</div>
</asp:Panel>






      <asp:Panel ID="DIVimportFeeback" runat="server" Visible="False" Width="736px">
         <br />
         IMPORT COMPLETED.  Please peruse the generated messages:<br />
         <br />
         <asp:TextBox ID="TXTimportEngineMessages" runat="server" Height="154px" ReadOnly="True"
            TextMode="MultiLine" Width="900px"></asp:TextBox><br />
         <br />
         <a href="ListSAPRoles.aspx?WSID=<%=session.idWorkspace_SAP%>">Visit the workspace</a> to work with the uploaded entitlements.
         <br />
      </asp:Panel>
      &nbsp;</p>


</div>



<asp:SqlDataSource 
      ID="SQL_saproleList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="SELECT c_id, c_u_Name+' ('+c_u_Platform+')' AS Name FROM [t_RBSR_AUFW_u_SAProle] WHERE c_r_SubProcess = @CURSUBPR ORDER BY Name ">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-3" Name="CURSUBPR" SessionField="UUIDSUBPROCESS"
                Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>


</asp:Content>
