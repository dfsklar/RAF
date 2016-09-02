<%@ Page Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="DLOGselectAuthObj.aspx.cs" Inherits="_6MAR_WebApplication.WebForm133" Title="Untitled Page" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class='ts-m' style='height:20px;color:#FFFFFF;padding-top:8px;margin-bottom:30px'>
 &nbsp; &nbsp; <a style='color:#FFFFFF' href='<%=Request.Params["origin"]%>'>&lt;&lt;&lt;  back to 1251 grid</a>
</div>

<input type='hidden' id='returnurl' value='<%=Request.Params["origin"]%>'>
<input type='hidden' id='RoleID' value='<%=Request.Params["RoleID"]%>'>

<P>

<script type="text/javascript" src='SAP1251.js'></script>
<script type="text/javascript" src='SAP1251_modaldialog.js'></script>
<script type="text/javascript" src='PAGE_DictTcodes.js'></script>


<TABLE>


<TR>


<TD>

<ComponentArt:Grid ID="GRID_authobjects" runat="server" Width="600"

  DataSourceID="SQL_AuthObjects"
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
	<ComponentArt:GridColumn DataField="ClassName" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="Class" />
	<ComponentArt:GridColumn DataField="ObjName" Width="130"
				 Visible="true" AllowEditing="False" HeadingText="Object" />
	<ComponentArt:GridColumn DataField="ObjDescr"
				 Visible="true" AllowEditing="False" HeadingText="Description" />
	<ComponentArt:GridColumn DataField="c_id" Width="30" fixedWidth="True"
	            DataCellClientTemplateId="TEMPLhovershowfield"
				 Visible="true" AllowEditing="False" HeadingText="Fields" />
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

Select an object from the list,<br/>
then click the "Add" button below.
<br/>
<br/>
A new 1251-entitlement row will be added <u>for each field</u><br/>
that is associated with that object type.
<br/>
<br/>
If this object is already represented in this workspace,
<br/>
the only action would be the addition of newly-registered
<br/>
fields (if any).
<br/>
<br/>
<INPUT type='button' value='ADD' onclick='javascript:ProcessSelectedObj();'>


</TD>

</TR>


</TABLE>







<asp:SqlDataSource ID="SQL_AuthObjects" runat="server" 
  ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>" 
  SelectCommand="
SELECT 
       OBJD.c_id,
       OBJD.c_u_Name as ObjName,
       OBJD.c_u_Description as ObjDescr,
       CLD.c_u_Name as ClassName,
       CLD.c_u_Description as ClassDescr
FROM [t_RBSR_AUFW_u_SAPauthObj] OBJD

LEFT OUTER JOIN [t_RBSR_AUFW_u_SAPauthClass] CLD 
   ON CLD.c_id = OBJD.c_r_SAPauthClass

WHERE OBJD.c_u_Status<>'X' AND CLD.c_u_Status<>'X' ORDER BY OBJD.c_u_Name">
</asp:SqlDataSource>





<div id='hoverpopup' 
   style='font-size:9px;z-index:10000;text-align:left;visibility:hidden; position:absolute; top:0; left:0; width:180px;height:100px; padding:7px; background-color:#CCCCFF;filter:alpha(opacity=90)'>
  <span id='_hoverpopup'></span>
</div>


</asp:Content>
