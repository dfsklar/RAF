<%@ Page Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="DLOGmaintainListOfRelatedFields.aspx.cs" Inherits="_6MAR_WebApplication.WebForm134" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class='ts-m' style='height:20px;color:#FFFFFF;padding-top:8px;margin-bottom:30px'>
 &nbsp; &nbsp; <a style='color:#FFFFFF' href='<%=Request.Params["origin"]%>'>&lt;&lt;&lt;  go back with no effect</a>
</div>

<input type='hidden' id='returnurl' value='<%=Request.Params["origin"]%>'>
<input type='hidden' id='RoleID' value='<%=Request.Params["RoleID"]%>'>

<P>

<script type="text/javascript" src='SAP1251.js'></script>
<script type="text/javascript" src='SAP1251_modaldialog.js'></script>
<script type="text/javascript" src='PAGE_DictTcodes.js'></script>



<H2>THIS IS A WORK IN PROGRESS.  IT IS NOT FUNCTIONAL AT THIS TIME.</H2>


<TABLE>


<TR>


<TD>

<ComponentArt:Grid ID="GRID_authfields" runat="server" Width="600"

  DataSourceID="SQL_AuthFields"
  ManualPaging="True"

  RunningMode="Client"

  AllowMultipleSelect="false"

 ShowHeader="true"
 ShowSearchBox="true" 
 SearchOnKeyPress="false"
 HeaderHeight="18"
 HeaderCssClass="GridHeader" 
 SearchText="Search (hit Enter to refresh):"
 SearchTextCssClass="GridHeaderText"
 GroupByTextCssClass="GroupByText"
 GroupByCssClass="GroupByCell"
 GroupingNotificationTextCssClass="GridHeaderText"
 GroupingNotificationText=""

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

  AutoCallBackOnUpdate="true"
>
<Levels>
  <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" 
      DataCellCssClass="DataCell" AllowSorting="False"
      RowCssClass="Row" SelectedRowCssClass="SelectedRow" 
      EditCellCssClass="EditDataCell"
      EditFieldCssClass="EditDataField"
  >
       <Columns>
        <ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />

        <ComponentArt:GridColumn DataField="LinkageExists" Visible="true" AllowEditing="False" />

	<ComponentArt:GridColumn DataField="FieldName" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="Field" />

	<ComponentArt:GridColumn DataField="FieldDescr"
				 Visible="true" AllowEditing="False" HeadingText="Description" />

       </Columns>
  </ComponentArt:GridLevel>
</Levels>
<ClientTemplates>
            <ComponentArt:ClientTemplate ID="TEMPLhovershowfield">
	       ## "<IMG onmouseout='hoverhide()' onmouseover='hovershow(this,\\"" +
		      DataItem.ClientId + "\\")' src='images/msg_unread.gif' border=0/>"
 ##
            </ComponentArt:ClientTemplate>
</ClientTemplates>
        <ClientEvents>
        </ClientEvents>
</ComponentArt:Grid>

</TD>


<TD style='font-size:16px;padding-left:20px'>

Click any field's row to toggle its<br/>
inclusion status for this object.
<br/>
<br/>
<INPUT type='button' value='Submit all changes' onclick='javascript:ProcessSelectedFields();'>


</TD>

</TR>


</TABLE>






<asp:SqlDataSource ID="SQL_AuthFields" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="
SELECT 
       FIELD.c_id,
       (CASE WHEN LINKAGE.c_id IS NOT NULL THEN 'Y' ELSE '' END) as LinkageExists,
       FIELD.c_u_Name as FieldName,
       FIELD.c_u_Description as FieldDescr
FROM t_RBSR_AUFW_u_SAPauthField FIELD
LEFT OUTER JOIN t_RBSR_AUFW_r_SAPauthObjSAPauthField LINKAGE
   ON     LINKAGE.c_r_SAPauthField = FIELD.c_id
      AND LINKAGE.c_r_SAPauthObj   = @CONTEXTOBJ
WHERE FIELD.c_u_Status<>'X'  ORDER BY LinkageExists DESC, FIELD.c_u_Name">
  <SelectParameters>
    <asp:QueryStringParameter Name='CONTEXTOBJ' QueryStringField='contextobj' Type="Int32" DefaultValue="1"/>
  </SelectParameters>
</asp:SqlDataSource>





<div id='hoverpopup' 
   style='font-size:9px;z-index:10000;text-align:left;visibility:hidden; position:absolute; top:0; left:0; width:180px;height:100px; padding:7px; background-color:#CCCCFF;filter:alpha(opacity=90)'>
  <span id='_hoverpopup'></span>
</div>

</asp:Content>
