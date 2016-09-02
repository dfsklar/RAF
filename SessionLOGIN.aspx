<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionLOGIN.aspx.cs" Inherits="_6MAR_WebApplication.WebForm2" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>R-AF Login</title>
    <link rel="stylesheet" type="text/css" href="GRIDcustom.css" />
    <link rel="stylesheet" type="text/css" href="menuStyle.css" />
    <link rel="stylesheet" type="text/css" href="afwac.css" />
    <link rel="stylesheet" type="text/css" href="JQ/jQueryAlertDialogs/jquery.alerts.css" />
    <link type="text/css" href="JQ/css/custom-theme/jquery-ui-1.7.2.custom.css" rel="stylesheet" />

    <script type="text/javascript" src="JQ/js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="JQ/js/jquery-ui-1.7.2.custom.min.js"></script>
    <script type="text/javascript">
	    $JQ = jQuery.noConflict();
    </script>
    <script type="text/javascript" src='prototype.js'></script>
    <script type="text/javascript" src='SessionLOGIN.js'></script>
    <script type="text/javascript" src='JQ/jQueryAlertDialogs/jquery.alerts.js'></script>

<script language="javascript" type="text/javascript">
// <!CDATA[

function Button1_onclick() {


}

// ]]>
</script>
</head>
<body id="MYBODY" style="background:#F8CDCD url(images/bg.png) repeat-x scroll 0 0" onload="if($('PanelUserArea')){Dialog1.Show();Dialog2.Show();}">
    <form id="form1" runat="server">

<!--
<img src='images/RAFlogo_justwords_whiteONtransparent.png' border='0'/>
<BR>
<img src='images/RAFtextname_whiteONtransparent.png' border='0'/>
-->

<img src='Variant_CCR/images/CCR-RAF-logo-upperleft.png' border='0' />



    
    
    
                <ComponentArt:Dialog 

ModalMaskImage="images/CAdlog/alpha.gif" 
HeaderCssClass="headerCss" Icon="pencil.gif"  Value="Sample Dialog Content" HeaderClientTemplateId="header" Title="Admin Login"  FooterClientTemplateId="footer" AllowDrag="true" OffsetX=20 OffsetY=80 ID="Dialog1" runat="server" 

Width="370" 

 Alignment="TopLeft">

	<ClientTemplates>

                <ComponentArt:ClientTemplate id="header"><table cellpadding="0" cellspacing="0" 

width="380"

onmousedown="Dialog1.StartDrag(event);">
                          <tr>
                          <td width="5"><img style="display:block;" src="images/top_left.gif"/></td>
                          <td style="background-image:url(images/top.gif);padding:10px;"><table width="100%" cellpadding="0" cellspacing="0"><tr><td valign="middle" style="color:White;font-size:15px;font-family:Arial;font-weight:bold;">## Parent.Title ##</td><td align="right"> </td></tr></table></td>
                          <td width="5"><img style="display:block;" src="images/top_right.gif"/></td>
                          </tr>
                          </table>
      </ComponentArt:ClientTemplate>

      <ComponentArt:ClientTemplate id="footer"><table cellpadding="0" cellspacing="0" 
width="380"
>
                          <tr>
                          <td width="5"><img style="display:block;" src="images/CAdlog/bottom_left.gif"/></td>
                          <td style="background-image:url(images/CAdlog/bottom.gif);background-color:#F0F0F0;"><img style="display:block;" src="images/CAdlog/spacer.gif" height="4" 
width="370"
 /></td>
                          <td width="5"><img style="display:block;" src="images/CAdlog/bottom_right.gif"/></td>
                          </tr>
                          </table>
                          </ComponentArt:ClientTemplate></ClientTemplates>

<Content>

    <ComponentArt:Menu ID="GridContextMenu" SiteMapXmlFile="CTXMENU_SessionLOGIN.xml" ExpandSlide="none"
        ExpandTransition="fade" ExpandDelay="250" CollapseSlide="none" CollapseTransition="fade"
        Orientation="Vertical" CssClass="MenuGroup" DefaultGroupCssClass="MenuGroup"
        DefaultItemLookId="DefaultItemLook" DefaultGroupItemSpacing="1" ImagesBaseUrl="images/"
        EnableViewState="false" ContextMenu="Custom" runat="server">
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="MenuItem" HoverCssClass="MenuItemHover"
                LabelPaddingLeft="15" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="3" />
            <ComponentArt:ItemLook LookId="BreakItem" ImageUrl="break.gif" CssClass="MenuBreak"
                ImageHeight="1" ImageWidth="100%" />
        </ItemLooks>
    </ComponentArt:Menu>


<table cellpadding="0" cellspacing="0" 
width="380"
>
<TR>

<td style="background-image:url(images/CAdlog/left.gif);" width="5"></td>



<td style="background-color:white;font-size:12px;font-family:Arial;padding:10px;">

<CENTER>
<TABLE>
<TR>
<TD style="text-align:right">User:</TD>
<TD style="text-align:left">
        <asp:DropDownList ID="CHOOSEusername" runat="server" DataSourceID="DSloginusers" DataTextField="c_u_Name"
            DataValueField="c_id" Width="192px">
        </asp:DropDownList>
</TD>
</TR>
<TR>
<TD valign="top">Password:</TD>
<TD style="text-align:left">
        <asp:TextBox ID="TXTpassword" TextMode="password" runat="server" Width="183px"></asp:TextBox>  
             <asp:Button ID="Button2" runat="server" Text="Login" />
</TD>
</TR>
</TABLE>



<DIV style='height:50px;margin-top:20px;color:red'>
        <asp:Panel ID="PanelLoginFailMsg" runat="server">
		  <p>
		  <B>Login failed.  Please try again or contact support for assistance.</B>
		  </p>
        </asp:Panel>
</DIV>


</td>


<td style="background-image:url(images/CAdlog/right.gif);" width="5"></td>


</TR>
</TABLE>


      </Content>

        </ComponentArt:Dialog>














    
    
    
                <ComponentArt:Dialog 

ModalMaskImage="images/CAdlog/alpha.gif" 
HeaderCssClass="headerCss" Icon="pencil.gif"  Value="Sample Dialog Content" HeaderClientTemplateId="header2" Title="Read-only Access"  FooterClientTemplateId="footer2" AllowDrag="true" OffsetX=500 OffsetY=80 ID="Dialog2" runat="server" 

Width="370" 

 Alignment="TopLeft">

	<ClientTemplates>

                <ComponentArt:ClientTemplate id="header2"><table cellpadding="0" cellspacing="0" 

width="380"

onmousedown="Dialog2.StartDrag(event);">
                          <tr>
                          <td width="5"><img style="display:block;" src="images/top_left.gif"/></td>
                          <td style="background-image:url(images/top.gif);padding:10px;"><table width="100%" cellpadding="0" cellspacing="0"><tr><td valign="middle" style="color:White;font-size:15px;font-family:Arial;font-weight:bold;">## Parent.Title ##</td><td align="right"> </td></tr></table></td>
                          <td width="5"><img style="display:block;" src="images/top_right.gif"/></td>
                          </tr>
                          </table>
      </ComponentArt:ClientTemplate>


      <ComponentArt:ClientTemplate id="footer2"><table cellpadding="0" cellspacing="0" 
width="380"
>
                          <tr>
                          <td width="5"><img style="display:block;" src="images/CAdlog/bottom_left.gif"/></td>
                          <td style="background-image:url(images/CAdlog/bottom.gif);background-color:#F0F0F0;"><img style="display:block;" src="images/CAdlog/spacer.gif" height="4" 
width="370"
 /></td>
                          <td width="5"><img style="display:block;" src="images/CAdlog/bottom_right.gif"/></td>
                          </tr>
                          </table>
                          </ComponentArt:ClientTemplate>



</ClientTemplates>

<Content>


<table cellpadding="0" cellspacing="0" 
width="380"
>
<TR>

<td style="background-image:url(images/CAdlog/left.gif);" width="5"></td>

<td style="background-color:white;font-size:12px;font-family:Arial;padding:10px;">
<CENTER>

Your EID: <asp:TextBox ID="TXTbusownereid" runat="server" Width="100px"></asp:TextBox>
&nbsp; &nbsp;
<asp:Button ID="BTNloginReadOnly" runat="server" Text="Login" OnClick="BTNloginReadOnly_Click"/>
<BR>
<DIV style='height:50px;margin-top:20px;color:red'>
        <asp:Panel ID="PanelReadOnlyLoginFailMsg" runat="server">
		  <p>
		  <B>Login failed.  Please try again or use the anonymous login.</B>
		  </p>
        </asp:Panel>
</DIV>

<BR>
<HR>

<A href='viewer/home.aspx'>Anonymous access</A>

</CENTER>
</td>


<td style="background-image:url(images/CAdlog/right.gif);" width="5"></td>


</TR>
</TABLE>


      </Content>

        </ComponentArt:Dialog>












        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
            SelectCommand="SELECT [c_u_Name], [c_id] FROM [t_RBSR_AUFW_u_Process] ORDER BY [c_u_Name]">
        </asp:SqlDataSource>



        <asp:Panel ID="PanelUserArea" runat="server">
<!--
        Choose your username:&nbsp;&nbsp;
        <asp:DropDownList ID="CHOOSEusernameOLD" runat="server" DataSourceID="DSloginusers" DataTextField="c_u_Name"
            DataValueField="c_id" Width="192px">
        </asp:DropDownList><br />
        <br />
        Type your password and then hit the 'Enter' key:&nbsp;
        <asp:TextBox ID="TXTpasswordOLD" runat="server" Width="183px"></asp:TextBox><br />
        <br />
-->
        </asp:Panel>


        <asp:SqlDataSource ID="DSloginusers" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
            SelectCommand="SELECT [c_u_Name], [c_id] FROM [t_RBSR_AUFW_u_User] WHERE c_u_PrivilegeLevel='admin' ORDER BY [c_u_Name]">
        </asp:SqlDataSource>
    




        <asp:Panel ID="PanelProcessArea" runat="server" class="CLASSpanelprocessarea">

<TABLE class="TBLchooseOrCreateSubpr">

<TR>

<TD><B>Login to an existing subprocess:</B></TD>

<TD>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
            SelectCommand="

SELECT

SUBPR.c_id, SUBPR.c_u_Name AS NameOfSubprocess, 
SUBPR.c_u_Status AS StatusOfSubprocess, SUBPR.c_r_Process, 
t_RBSR_AUFW_u_Process.c_u_Name AS NameOfProcess, 
t_RBSR_AUFW_u_Process.c_id AS IdOfProcess,
t_RBSR_AUFW_u_Process.c_u_Description AS DescrOfProcess,
EASET.c_id as IdOfWorkspace,
USERR.c_u_Name as WSowner,
SAPUSERR.c_u_Name as SAPWSowner


FROM 

t_RBSR_AUFW_u_SubProcess SUBPR

LEFT OUTER JOIN t_RBSR_AUFW_u_Process ON SUBPR.c_r_Process = t_RBSR_AUFW_u_Process.c_id  

LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet EASET
   ON EASET.c_r_SubProcess = SUBPR.c_id AND EASET.c_u_Status='WORKSPACE'
LEFT OUTER JOIN t_RBSR_AUFW_u_User USERR ON USERR.c_id = EASET.c_r_User

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeAssignmentSet TEASET
   ON TEASET.c_r_SubProcess = SUBPR.c_id AND TEASET.c_u_Status='WORKSPACE'
LEFT OUTER JOIN t_RBSR_AUFW_u_User SAPUSERR ON SAPUSERR.c_id = TEASET.c_r_User


WHERE SUBPR.c_u_Status IN ('Active') AND SUBPR.c_u_Name NOT LIKE '(%' ORDER BY NameOfProcess, NameOfSubprocess">
        </asp:SqlDataSource>




<ComponentArt:Grid ID="Grid1" runat="server"

        PagerStyle="Slider"        PagerTextCssClass="GridFooterText"
        SliderHeight="20"

        ManualPaging = "true"


	ItemDraggingEnabled="False"
	ExternalDropTargets="Grid1"




        SliderWidth="150"
        SliderGripWidth="9"
        SliderPopupOffsetX="35"
        PagerImagesFolderUrl="images/pager/"

EnableViewState="true"

RunningMode="Client"


AutoCallBackOnInsert="true"
AutoCallBackOnUpdate="true"


        KeyboardEnabled="false"
        CallbackReloadTemplates="false"

Width="600"
Height="200"


DataSourceID="SqlDataSource1"  

>
        <ClientEvents>
           <ContextMenu EventHandler="Grid1_onContextMenu" />
	   <ItemExternalDrop EventHandler="Grid1_ondrop"/>
        </ClientEvents>
        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow"
				    SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10"
					  EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField"
                >
                <Columns>


 <ComponentArt:GridColumn DataField="IdOfProcess" Visible="false" AllowEditing="False" />
 <ComponentArt:GridColumn DataField="StatusOfSubprocess" Visible="false" AllowEditing="False" />
 <ComponentArt:GridColumn DataField="DescrOfProcess" Visible="false" AllowEditing="False" />

 <ComponentArt:GridColumn DataField="NameOfProcess" HeadingText="Process" AllowEditing="False"/>

 <ComponentArt:GridColumn DataField="NameOfSubprocess" HeadingText="SubProcess" DataCellClientTemplateId="TEMPLlinkToSubprocess" />

 <ComponentArt:GridColumn DataField="WSowner" HeadingText="Business Lock" />
 <ComponentArt:GridColumn DataField="SAPWSowner" HeadingText="SAP Lock" />

 <ComponentArt:GridColumn DataField="c_id" Visible="false"/>

		</Columns>
            </ComponentArt:GridLevel>
        </Levels>
        <ClientTemplates>


<ComponentArt:ClientTemplate ID="TEMPLdesignLinkage">
   <a href="javascript:REACTrenameSubprocess(Grid1.getItemFromClientId('## DataItem.ClientId ##'));">Rename</a> | 
   <a href="javascript:REACTdeleteSubprocess(Grid1.getItemFromClientId('## DataItem.ClientId ##'));">Delete</a>  
</ComponentArt:ClientTemplate>

<ComponentArt:ClientTemplate ID="TEMPLlinkToSubprocess">
   <a href="SessionLOGIN.aspx?subprid=##DataItem.getMember('c_id').Value##&subprname=##escape(DataItem.getMember('NameOfSubprocess').Value)##&prname=##escape(DataItem.getMember('NameOfProcess').Value)##">##DataItem.getMember('NameOfSubprocess').Value##</a>
</ComponentArt:ClientTemplate>

        </ClientTemplates>

    </ComponentArt:Grid>


</TD>

</TR>

<TR>
<TD><BR>&nbsp;<B>OR<BR>&nbsp;</B></TD>
</TR>

<TR>
<TD>
<B>Create a new subprocess:</B>
</TD>
<TD>
        <table style="width: 506px">
        <tr>
        <td style="text-align: right; width: 19px;" valign="top">
        Process:</td><td style="width: 300px">


     <ComponentArt:ComboBox
      ID="ChooserProcess" runat="server"
      TextBoxEnabled="true"
      RunningMode="Client"
      DataSourceID="SqlDataSource2" DataTextField="c_u_Name"
      DataValueField="c_id" Width="192px"
      CssClass="comboBox"
      FocusedCssClass="comboBoxHover" TextBoxCssClass="comboTextBox" DropDownCssClass="comboDropDown"
      ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
      DropHoverImageUrl="images/drop_hover.gif" DropImageUrl="images/drop.gif" DropDownWidth="250"
/>
<I style='font-size:10px'>Select a process from the dropdown... <BR/>OR: to create a new process, just enter the name<br/>into the above box.</I><br/>&nbsp;

<br/>&nbsp;

            <br />
        </td></tr>
        <tr>
        <td style="text-align: right; width: 19px;" valign="top">
        Subprocess:</td><td style="width: 300px">
            <asp:TextBox ID="TextBox_NewSubprocess" runat="server" Width="202px"></asp:TextBox><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create" /></td></tr>
        </table>
</TD>
</TR>
</TABLE>
        </asp:Panel>


    </form>
</body>
</html>
