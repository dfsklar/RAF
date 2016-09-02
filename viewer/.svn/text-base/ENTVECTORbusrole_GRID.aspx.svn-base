<%@ Page Language="C#" MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="WebForm1.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WebForm12" 
Title="RBSR Authorization Framework" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">&nbsp;  &gt;  &nbsp; <A href='LISTbusroles.aspx<%=Request.Url.Query%>'>Business Role Selection</A> &nbsp; &gt; &nbsp; <A href='DETAILbusrole.aspx<%=Request.Url.Query%>'>Business Role Detail</A> &nbsp; &gt; &nbsp; <B>Technical Authorization Framework</B> </asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Detail
				</div>
				<div id="PageDescription">
					Process: <%= this.nameOfProcess() %>
					<br />
					Subprocess: <%= this.nameOfSubProcess() %>
					<br />
					Role Name: <%= this.nameOfRole() %>
					<br />
					Description:  <%= this.descrOfRole() %>
					<br />
					Type: <%= this.theBR.RoleType_Displayable %>
				</div>
			</div>
			<p />
The technical authorization framework describes the detailed privilege strings that are assigned to the business role.  This detail is meant primarily for the Security Access Controls team and BIS teams supporting the roles and applications.



<ComponentArt:Grid ID="Grid1" AllowHorizontalScrolling="true" runat="server" 

TreeLineImagesFolderUrl="../images/lines/"
TreeLineImageWidth="19"
TreeLineImageHeight="20"
IndentCellWidth="16"

GroupingMode="ConstantRows"
PreExpandOnGroup="false"

FillContainer="true"

AllowTextSelection="false"

CallbackCacheSize="50"
CallbackCacheLookAhead="15"
CallbackCachingEnabled="True"

RunningMode="Client"
EnableViewState="true"

EditOnClickSelectedItem="false"
AllowEditing="false"

AllowSorting="true"

PageSize="20"

Width="2500"
Height="440"

DataSourceID="SqlDataSource1" 


Debug="false" ShowHeader="true" HeaderHeight="18"
HeaderCssClass="GridHeader" 


ShowSearchBox="true" 
SearchOnKeyPress="false"

CallbackReloadTemplates="false"

SearchText="Search (hit Enter to refresh):"

ColumnResizeDistributeWidth='false'

	SearchTextCssClass="GridHeaderText"

		  GroupByTextCssClass="GroupByText"
        GroupByCssClass="GroupByCell"
		  GroupBySortAscendingImageUrl="group_asc.gif" GroupBySortDescendingImageUrl="group_desc.gif"
        GroupBySortImageWidth="10" GroupBySortImageHeight="10" GroupingNotificationTextCssClass="GridHeaderText"
CssClass="Grid" FooterCssClass="GridFooter"
        PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="41"
        PagerButtonHeight="22" SliderHeight="15" SliderWidth="150" SliderGripWidth="9"


        SliderPopupOffsetX="20" 
	SliderPopupClientTemplateId="SliderTemplate" 
	ScrollPopupClientTemplateId="ScrollPopupTemplate"

        PagerImagesFolderUrl="../images/pager/"  ImagesBaseUrl="../images/" KeyboardEnabled="true">


        <Levels>
            <ComponentArt:GridLevel DataKeyField="c_id" HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" DataCellCssClass="DataCell" GroupHeadingCssClass="GroupHeading"
                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="10" EditCellCssClass="EditDataCell"
                EditFieldCssClass="EditDataField" >





<Columns>

<ComponentArt:GridColumn DataField="c_id" HeadingText="S" Visible="false" AllowEditing="False" />

<ComponentArt:GridColumn DataField="c_u_Application" Visible="true" HeadingText="App" Width="100" />

<ComponentArt:GridColumn DataField="c_u_StandardActivity" HeadingText="StdActivity" Width="80" />

<ComponentArt:GridColumn DataField="c_u_RoleType" HeadingText="RoleType" Width="80" />

<ComponentArt:GridColumn DataField="c_u_System" HeadingText="System" Width="90"/>

<ComponentArt:GridColumn DataField="c_u_Platform" HeadingText="Plat" Width="75" />

<ComponentArt:GridColumn DataField="c_u_EntitlementName" HeadingText="EName" Width="80" />
<ComponentArt:GridColumn DataField="c_u_EntitlementValue" HeadingText="EValue" Width="80" />
<ComponentArt:GridColumn DataField="c_u_AuthObjValue" HeadingText="AuthObj" Width="80"/>



                    <ComponentArt:GridColumn DataField="c_u_FieldSecName" HeadingText="FieldSecN"  Width="80"/>
                    <ComponentArt:GridColumn DataField="c_u_FieldSecValue" HeadingText="FieldSecV" Width="80" />




    <ComponentArt:GridColumn DataField="c_u_Level4SecName" HeadingText="Level4N" Width="80" />
    <ComponentArt:GridColumn DataField="c_u_Level4SecValue" HeadingText="Level4V" Width="80" />



</Columns>
            </ComponentArt:GridLevel>
        </Levels>



        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="font-size: 10px;">
                            Loading...&nbsp;</td>
                        <td>
                            <img src="../images/spinner.gif" width="16" height="16" border="0"></td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="ScrollPopupTemplate">
                <table cellspacing="0" cellpadding="2" border="0" class="ScrollPopup">
                    <tr>
                        <td style="width: 125px; padding: 5px">
                            <div style="font-size: 10px; font-family: MS Sans Serif; text-overflow: ellipsis;
                                overflow: hidden;">
                                <nobr>## DataItem.GetMember("c_u_System").Value ##</nobr>
                            </div>
                        </td>
                        <td style="width: 125px; padding: 5px">
                            <div style="font-size: 10px; font-family: MS Sans Serif; text-overflow: ellipsis;
                                overflow: hidden;">
                                <nobr>## DataItem.GetMember("c_u_Platform").Value ##</nobr>
                            </div>
                        </td>
                        <td style="width: 125px; padding: 5px" align="right">
                            <div style="font-size: 10px; font-family: MS Sans Serif; text-overflow: ellipsis;
                                overflow: hidden;">
                                <nobr>##  DataItem.GetMember("c_u_EntitlementName").Value ##</nobr>
                            </div>
                        </td>
                    </tr>
                </table>
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
                                                <td style="font-family: verdana; font-size: 11px;">
                                                    <div style="overflow: hidden; width: 115px;">
                                                        <nobr>## DataItem.GetMember('c_u_System').Value ##</nobr>
                                                    </div>
                                                </td>
                                                <td style="font-family: verdana; font-size: 11px;">
                                                    <div style="overflow: hidden; width: 135px;">
                                                        <nobr>## DataItem.GetMember('c_u_Platform').Text ##</nobr>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="230" colspan="2" style="font-family: verdana; font-size: 11px; font-weight: bold;">
                                                                <div style="text-overflow: ellipsis; overflow: hidden; width: 250px;">
                                                                    <nobr>## DataItem.GetMember('c_u_EntitlementName').Value ##</nobr>
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
    </ComponentArt:Grid>



<asp:SqlDataSource
    ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:afwac_sv6ConnectionString %>"
    OnSelecting="INTERCEPTsqldatasource"
    SelectCommand="

SELECT 
    TENT.c_id,
    TENT.c_u_Application,
    TENT.c_u_StandardActivity,
    TENT.c_u_RoleType,
    TENT.c_u_System,
    TENT.c_u_Platform,
    TENT.c_u_EntitlementName,
    TENT.c_u_EntitlementValue,
    TENT.c_u_AuthObjValue,
    TENT.c_u_FieldSecName,
    TENT.c_u_FieldSecValue,
    TENT.c_u_Level4SecName,
    TENT.c_u_Level4SecValue

FROM 
   t_RBSR_AUFW_u_Entitlement TENT

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_EntAssignment TEASS 
    ON 
            TEASS.c_r_EntAssignmentSet = @ACTIVEEASETID
        AND TEASS.c_r_BusRole = @BUSROLEID
        AND TEASS.c_r_Entitlement = TENT.c_id

WHERE 

    TEASS.c_u_Status NOT IN ('X')

ORDER BY   

    TENT.c_u_Application,
    TENT.c_u_StandardActivity,
    TENT.c_u_RoleType,
    TENT.c_u_System,
    TENT.c_u_Platform,
    TENT.c_u_EntitlementName,
    TENT.c_u_EntitlementValue,
    TENT.c_u_AuthObjValue,
    TENT.c_u_FieldSecName,
    TENT.c_u_FieldSecValue,
    TENT.c_u_Level4SecName,
    TENT.c_u_Level4SecValue">
    <SelectParameters>
       <asp:Parameter Name="ACTIVEEASETID"  Type="Int32"/>
       <asp:QueryStringParameter Name="BUSROLEID" QueryStringField="idBR" Type="Int32"/>
    </SelectParameters>
</asp:SqlDataSource>

</asp:Content>
