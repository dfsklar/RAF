<%@ Page 
Title="RBSR Authorization Framework" 
Language="C#" 
MasterPageFile="~/viewer/viewer.Master" AutoEventWireup="true" 
CodeBehind="home.aspx.cs" Inherits="_6MAR_WebApplication.viewer.WebForm1"  %>


<asp:Content ID="ContentNAV" ContentPlaceHolderID="CONTENTnav" runat="server">
<script type="text/javascript" src='home.js'></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
			<div id="PageHeader">
				<div id="PageTitle">
					Business Role Information for IDM Users
				</div>
				<div id="PageDescription">
					Business role information and assigned entitlements.
				</div>
			</div>


<p />
This website provides information to IDM users on the business roles designed and assigned based on the Role-Based Security Redesign (RBSR) project.

    <br />
    <br />



<p/>

<b>1. Search for a ROLE:</b> &nbsp;
<br/>
<asp:panel ID="PNLsearch" defaultbutton="BTNtry"  runat="server" Width="684px">
    Type of role: &nbsp;<asp:DropDownList ID="CBOXroletype" runat="server">
        <asp:ListItem Value="IDM">Business/IDM role</asp:ListItem>
        <asp:ListItem Value="SAP">SAP role</asp:ListItem>
    </asp:DropDownList>
    &nbsp; &nbsp; &nbsp; &nbsp;
    <br />
Enter any portion of the role name:
<asp:textbox id="TXTrolenamesrch" runat="server"/>
<asp:button id="BTNtry" runat="server" Text="Search" OnClick="BTNtry_Click" /></asp:panel>





<p /><b>OR:</b><br />
    <br />
<b>    2. Search for a TCODE</b><br/>

    <asp:Panel ID="Panel1" runat="server" defaultbutton="BTNtryByTcode" Width="681px">
        Enter any portion of the TCode:
        <asp:TextBox ID="TXTtcode" runat="server"></asp:TextBox>&nbsp;
        <asp:button id="BTNtryByTcode" runat="server" Text="Search" OnClick="BTNtryByTcode_Click" /></asp:Panel>
    <br />
    


<p /><b>OR:<br />
    <br />
    3. Search by APPLICATION:<br /></b>
    <asp:Panel ID="Panel2" runat="server" defaultbutton="BTNsearchByAppName" Width="670px">
        Enter any portion of the Application's name:&nbsp;
        <asp:TextBox ID="TXTappname" runat="server" Width="234px"></asp:TextBox>
        <asp:Button ID="BTNsearchByAppName" runat="server" OnClick="BTNsearchByAppName_Click"
            Text="Search" /></asp:Panel>
            
            
<p></p>    
    
    
    <b>OR:</b><br/>
    <br />
    <b>4. REVIEW all roles controlled by an employee:</b>
<br/>
<asp:panel ID="PNLsearchByOwner" defaultbutton="BTNtryByOwner" runat="server" Width="677px">
Enter an EID:
<asp:textbox id="TXTeid" runat="server"/>
<asp:button id="BTNtryByOwner" runat="server" Text="Search" OnClick="BTNtryByOwner_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp; <a href='../export/BusRolePersonnelDump.ashx' style='font-size:9px'>Click here for the complete role-to-personnel table (CSV format)</a></asp:panel>


    <br />

<b>OR:</b>
<br/>
<br/>
<b>5. <a href="LISTbusroles.aspx">Click here to BROWSE ALL business ROLES</a>.</b>

<br/>

    <br />

<b>OR:</b>
<br/>
<br/>
<b>6. <a href="LISTallApplsInScope.aspx">Click here to BROWSE ALL APPLICATIONS in scope</a>.</b>

<!--
<B>3. <a href="LISTbusroles_byOwner.aspx?mode=owner&srch=.">Click here to REVIEW the business roles you control.</a></asp:Panel>
-->



    &nbsp;</asp:Content>
