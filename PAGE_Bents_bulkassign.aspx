<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PAGE_Bents_bulkassign.aspx.cs" Inherits="_6MAR_WebApplication.WebForm126" Title="Untitled Page" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src='PAGE_Bents_bulkassign.js'></script>



<TABLE style='width:100%'>
<TR>
<TD style='text-align:left;vertical-align:top'>
<B><A href='PAGE_BEnts_Vectors.aspx'>&lt;&lt;&lt; </A></B> &nbsp; <B><%= this.THEent.Application %></B> <BR>
<%= this.THEent.StandardActivity %> 
&bull;
<%= this.THEent.RoleType %> 
&bull;
<%= this.THEent.System %> 
&bull;
<%= this.THEent.Platform %> 
&bull;
<%= this.THEent.EntitlementName %> 
<%= this.THEent.EntitlementValue %> 
&bull;
<%= this.THEent.AuthObjName %> 
<%= this.THEent.AuthObjValue %> 
&bull;
<%= this.THEent.FieldSecName %> 
<%= this.THEent.FieldSecValue %> 
&bull;
<%= this.THEent.Level4SecName %> 
<%= this.THEent.Level4SecValue %> 
&bull;
 &nbsp;

</TD>
<TD style='text-align:right;vertical-align:top'>

<asp:Panel ID="PANELcond_readonly" runat="Server">
<SPAN style='color:blue; font-weight:bold'>READ-ONLY VIEW</SPAN> &nbsp; &nbsp;</asp:Panel>

<asp:Panel ID="PANELcond_changesExist" runat="Server">
<SPAN style='color:red; font-weight:bold' ID='SPANalertUnsavedChanges'>UNSAVED CHANGES EXIST</SPAN>
<INPUT type='Button' id="BTNsubmitChgs" Value="Submit" onClick="javascript:Submit();" />
<INPUT type='Button' id="BTNcancelChgs" Value="Cancel" 
    onClick="javascript:window.location='PAGE_Bents_Vectors.aspx';"/>
</asp:Panel>

<asp:Panel ID="PANELcond_goback" runat="Server">
</asp:Panel>

</TD>
</TR>
</TABLE>





    <ComponentArt:Grid 
    ID="Grid1" runat="server" 
    RunningMode="Client"
    ManualPaging="true"
    FillContainer="true"

TreeLineImagesFolderUrl="images/lines/"
TreeLineImageWidth="19"
TreeLineImageHeight="20"
IndentCellWidth="16"
GroupingMode="ConstantRows"
PreExpandOnGroup="false"


    Width="2500"
    Height="440"

    ShowSearchBox="true" 
    SearchOnKeyPress="false"

AllowEditing="true" EditOnClickSelectedItem="false" AutoCallBackOnInsert="false"
        AutoCallBackOnUpdate="false" AutoCallBackOnDelete="false" CallbackReloadTemplates="false"
        AutoPostBackOnInsert="false" AutoPostBackOnUpdate="false" AutoPostBackOnDelete="false"
        AutoPostBackOnSelect="false" EnableViewState="true"


    ColumnResizeDistributeWidth='false'

    DataSourceID="SQL_busrolelist"

    Debug="false" ShowHeader="true" HeaderHeight="18"
    HeaderCssClass="GridHeader" 

  SearchTextCssClass="GridHeaderText"
  GroupByTextCssClass="GroupByText"
  GroupByCssClass="GroupByCell"
  GroupBySortAscendingImageUrl="group_asc.gif" GroupBySortDescendingImageUrl="group_desc.gif"
  GroupBySortImageWidth="10" GroupBySortImageHeight="10" GroupingNotificationTextCssClass="GridHeaderText"
  CssClass="Grid" FooterCssClass="GridFooter"

        PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="41"
        PagerButtonHeight="22" SliderHeight="15" SliderWidth="150" SliderGripWidth="9"
        SliderPopupOffsetX="20" SliderPopupClientTemplateId="SliderTemplate" ScrollPopupClientTemplateId="ScrollPopupTemplate"
        PagerImagesFolderUrl="images/pager/"  ImagesBaseUrl="images/" KeyboardEnabled="true"

    >

        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField" EditCommandClientTemplateId="EditCommandTemplate"
                InsertCommandClientTemplateId="InsertCommandTemplate">

                <Columns>

		<ComponentArt:GridColumn DataField="c_id" Visible="false" AllowEditing="False" />
		<ComponentArt:GridColumn DataField="INTeditStatus" HeadingText="Chg" Visible="true" AllowEditing="False"
		   DataCellClientTemplateId="TEMPLchgstatusByIcon"
			FixedWidth="True"
			Width="35"
			 />
		<ComponentArt:GridColumn DataField="InUse"
		   DataType='System.Boolean' ColumnType='CheckBox'
			FixedWidth="True"
			Width="45"
					 />
		<ComponentArt:GridColumn DataField="DELTA" HeadingText=""
		   	FixedWidth="True" Width="45"  AllowEditing='False'
					 />
		<ComponentArt:GridColumn DataField="NamePr" HeadingText="Scope" AllowEditing="False" />
		<ComponentArt:GridColumn DataField="c_u_Name" HeadingText="Role" AllowEditing="False" />
		<ComponentArt:GridColumn DataField="c_u_Description" HeadingText="Description" AllowEditing="False" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>

		  <ClientEvents>
		  <ItemCheckChange EventHandler="RespondToCheckboxChangeNATIVE" />
		  <Load EventHandler="REACTgridDoneLoading" />
		  <CallbackComplete EventHandler="REACTgridDoneCallback" />
		  </ClientEvents>

        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="TEMPLchgstatusByIcon">
	          <div style="font-family:verdana;">## RENDERchgstatus(DataItem) ##</div>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>


    </ComponentArt:Grid>


<asp:SqlDataSource ID="SQL_busrolelist" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
      SelectCommand="

SELECT 

0 as INTeditStatus,
BR.*,
(PR.c_u_Name + ' &bull; ' + SUBPR.c_u_Name) as NamePr,
SUBPR.c_id as IDSubPr,
CASE WHEN (EA.c_id IS NOT NULL OR WSEA.c_id IS NOT NULL) THEN 'True' ELSE 'False' END as InUse,
CASE WHEN (WSEASET.c_id IS NOT NULL AND
              ( ((WSEA.c_id IS NULL) AND (EA.c_id IS NOT NULL) )
                       OR
                ((EA.c_id IS NULL) AND (WSEA.c_id IS NOT NULL) )
              ) ) THEN 'Pending' ELSE '' END as DELTA,
WSEASET.c_id as IDworkspace

FROM t_RBSR_AUFW_u_BusRole BR


LEFT OUTER JOIN t_RBSR_AUFW_u_SubProcess SUBPR 
   ON SUBPR.c_id = BR.c_r_SubProcess AND SUBPR.c_u_Status='Active'

LEFT OUTER JOIN t_RBSR_AUFW_u_Process PR 
   ON PR.c_id = SUBPR.c_r_Process


LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet EASET
   ON EASET.c_r_SubProcess = SUBPR.c_id AND EASET.c_u_Status='ACTIVE'

LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignment EA
   ON EA.c_r_EntAssignmentSet = EASET.c_id AND EA.c_u_Status IN ('A','N')
    AND EA.c_r_BusRole=BR.c_id AND
    	EA.c_r_Entitlement = @CONTEXTENT


LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet WSEASET
   ON WSEASET.c_r_SubProcess = SUBPR.c_id AND WSEASET.c_u_Status='WORKSPACE'

LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignment WSEA
   ON WSEA.c_r_EntAssignmentSet = WSEASET.c_id AND WSEA.c_u_Status IN ('A','N')
    AND WSEA.c_r_BusRole=BR.c_id AND
    	WSEA.c_r_Entitlement = @CONTEXTENT

WHERE 
SUBPR.c_u_Status='Active' AND PR.c_u_Name NOT LIKE '(internal use)'

ORDER BY InUse DESC, PR.c_u_Name, SUBPR.c_u_Name, BR.c_u_Name

">
            <SelectParameters>
                <asp:QueryStringParameter Name="CONTEXTENT" QueryStringField="entid" Type="Int32"
                    DefaultValue="64622" />
            </SelectParameters>

</asp:SqlDataSource>


<!-- JQUERY-UI DIALOG -->
<!-- See the corresponding JS file for the initialization code -->
<div id="JQDLGmsgs" class='JQdialog' title='Important Messages'>
<PRE style='width:650px;height:250px' ID="IFRAMEmsgs"></PRE>
</div>
<!--
	// How to populate:
	//$('IFRAMEmsgs').innerHTML = "FJIEOWJINNERjfiwe<BR>";
	//for (i=0; i<200;i++) {
	//$('IFRAMEmsgs').innerHTML += "FJIEOWJINNER<BR>";
	//}
-->
    <asp:TextBox ID="TXTBOXmessagesFromServer" runat="server" Width="383px"></asp:TextBox><br />
    <asp:Label ID="Label1" runat="server" Text="Label" Width="186px"></asp:Label><br />


</asp:Content>
