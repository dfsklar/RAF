<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_6MAR_WebApplication.WebForm115" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='PAGEroleDesAppList.js'></script>

<input type='hidden' name='IDrole' id='IDrole' value='<%=this.brole.ID%>'/>

<TABLE>
<TR>
<TD style='text-align:left'>
<B><A href='ListBRoles.aspx'>&lt;&lt;&lt; </A></B> &nbsp;
<b><%= this.brole.Name %></b>  &nbsp;
</TD>
</TR>


<TR>

<TD style='text-align:left'>

<ComponentArt:Grid 
  ID="Grid1" 

  ManualPaging="true"

  Width="200px"

        ShowFooter="false"

        ScrollBar="Auto"
        ScrollTopBottomImagesEnabled="true"
        ScrollTopBottomImageHeight="2"
        ScrollTopBottomImageWidth="16"
        ScrollImagesFolderUrl="images/scroller/"
        ScrollButtonWidth="16"
        ScrollButtonHeight="17"
        ScrollBarCssClass="ScrollBar"
        ScrollGripCssClass="ScrollGrip"
        ScrollBarWidth="16"


  AllowHorizontalScrolling="false" 
  
  runat="server" RunningMode="Client"


  PageSize="18" FillContainer="false"  
  AllowTextSelection="true"

  DataSourceID="SQL_applicationList" Debug="false" ShowHeader="false" HeaderHeight="18"
        HeaderCssClass="GridHeader" ShowSearchBox="true" SearchTextCssClass="GridHeaderText"
        SearchOnKeyPress="true" AllowEditing="true" EditOnClickSelectedItem="false" AutoCallBackOnInsert="false"
        AutoCallBackOnUpdate="false" AutoCallBackOnDelete="false" CallbackReloadTemplates="false"
        AutoPostBackOnInsert="true" AutoPostBackOnUpdate="true" AutoPostBackOnDelete="true"
        AutoPostBackOnSelect="false" EnableViewState="true" GroupByTextCssClass="GroupByText"
        GroupByCssClass="GroupByCell"
	     GroupBySortAscendingImageUrl="group_asc.gif" GroupBySortDescendingImageUrl="group_desc.gif"
        GroupBySortImageWidth="10" GroupBySortImageHeight="10" GroupingNotificationTextCssClass="GridHeaderText"
        GroupingPageSize="1" PreExpandOnGroup="true" CssClass="Grid" FooterCssClass="GridFooter"

	ImagesBaseUrl="images/" KeyboardEnabled="true"

	>
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField" EditCommandClientTemplateId="EditCommandTemplate"
                InsertCommandClientTemplateId="InsertCommandTemplate">

<ConditionalFormats>
	<ComponentArt:GridConditionalFormat ClientFilter="DataItem.GetMember('KOUNT').Value < 1"
	 				    RowCssClass="GreyBGRow"  
	 				    SelectedRowCssClass="SelectedRow" />
</ConditionalFormats>


                <Columns>

		<ComponentArt:GridColumn DataField="c_id" AllowEditing="False" HeadingText=" "
		DataCellClientTemplateId="TEMPLdesignLinkage" Width='10' FixedWidth='True'
		 />

		<ComponentArt:GridColumn DataField="FANid" AllowEditing="False" Visible="False"/>

		<ComponentArt:GridColumn DataField="c_u_Name" HeadingText="Name" Visible="true" AllowEditing="False"
			FixedWidth="True"
			Width="200"
			 />

		<ComponentArt:GridColumn DataField="FANtext" HeadingText="Notes" Visible="true" AllowEditing="False" Align='left'
					 Width='100' FixedWidth="True"
			 />

		<ComponentArt:GridColumn DataField="KOUNT" HeadingText="Ent#" Visible="true" AllowEditing="False" Align='right'
					 Width='40' FixedWidth="True"
			 />

			                 </Columns>
            </ComponentArt:GridLevel>
        </Levels>

        <ClientTemplates>

            <ComponentArt:ClientTemplate ID="TEMPLdesignLinkage">
	          <div style="font-family:verdana;"><A href='PAGEroleDesigner.aspx?AppID=## DataItem.GetMember("c_id").Value ##&RoleID=##$('IDrole').value##'>&gt;</A></div>
            </ComponentArt:ClientTemplate>





            <ComponentArt:ClientTemplate ID="SliderTemplate">
                <table class="SliderPopup" cellspacing="0" cellpadding="0" border="0" style="background-color: white">
                    <tr>
                        <td valign="top" style="padding: 5px;">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td width="25" align="center" valign="top" style="padding-top: 3px;">
                                        <br>
                                    </td>
                                    <td>
                                        <table cellspacing="0" cellpadding="2" border="0" style="width: 255px;">
                                            <tr>
                                                <td colspan="2">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="230" colspan="2" style="font-family: verdana; font-size: 11px; font-weight: bold;">
                                                                <div style="text-overflow: ellipsis; overflow: hidden; width: 250px;">
                                                                    <nobr>## DataItem.GetMember('c_u_Name').Value ##</nobr>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 14px; background-color: #757598;">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td style="padding-left: 5px; color: white; font-family: verdana; font-size: 10px;">
                                        Page <b>## DataItem.PageIndex + 1 ##</b> of <b>## Grid1.PageCount ##</b>
                                    </td>
                                    <td style="padding-right: 5px; color: white; font-family: verdana; font-size: 10px;"
                                        align="right">
                                        Row <b>## DataItem.Index + 1 ##</b> of <b>## Grid1.RecordCount ##</b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>


        </ClientTemplates>

 <ClientEvents>
    <ItemSelect EventHandler="EVT_Grid_RowSelect" />
 </ClientEvents>


</ComponentArt:Grid>

</TD>

<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>

<TD style='text-align:left;background-color:#FFEEEE;padding:10px'>

Functional Application Entitlement
<br/>
design notes for this role,
<br/>
for the application selected at left:<br/>
<TEXTAREA ID="NOTESeditarea" rows='13' cols='50'>
</TEXTAREA>
<BR>
<INPUT TYPE='BUTTON' VALUE="Submit Changes" onclick='javascript:AJAXsendCommentToServer();'>

</TD>

</TR>

</TABLE>




<asp:SqlDataSource ID="SQL_applicationList" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="

SELECT APP.c_id, APP.c_u_Name, 
       FAN.c_id as FANid, FAN.c_u_Comment as FANtext,

(
SELECT COUNT(*) FROM t_RBSR_AUFW_u_EntAssignment EA
LEFT OUTER JOIN t_RBSR_AUFW_u_Entitlement ENT
   ON EA.c_r_Entitlement = ENT.c_id
WHERE
ENT.c_u_Application = APP.c_u_Name
AND
EA.c_r_BusRole = @IDBUSROLE
AND
EA.c_r_EntAssignmentSet = @IDWS
AND
EA.c_u_Status NOT IN ('X')
) as KOUNT

FROM [t_RBSR_AUFW_u_Application] APP 

LEFT OUTER JOIN t_RBSR_AUFW_u_FuncApplNotes FAN
   ON    FAN.c_u_REFapplication = APP.c_id
      AND
         FAN.c_r_BusRole = @IDBUSROLE

ORDER BY [c_u_Name] 

" >

<SelectParameters>
      <asp:SessionParameter Name="IDWS"  SessionField="INTcurWS" Type="Int32" DefaultValue="-32" />
      <asp:SessionParameter Name="IDBUSROLE"  SessionField="INTcurBUSROLE" Type="Int32" DefaultValue="-32" />
</SelectParameters>

</asp:SqlDataSource>

</asp:Content>
