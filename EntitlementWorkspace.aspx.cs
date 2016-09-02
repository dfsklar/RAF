using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ComponentArt.Web.UI;

using RBSR_AUFW.DB.IWorkspaceEntitlement;
using RBSR_AUFW.DB.IEditingWorkspace;

namespace _6MAR_WebApplication
{
  public partial class WebForm18 : AFWACpage
  {

	 public int IDworkspace = -1;

	 override protected void OnInit(EventArgs e)
	 {
		//
		// CODEGEN: This call is required by the ASP.NET Web Form Designer.
		//
		InitializeComponent();
		base.OnInit(e);
	 }


	 private void InitializeComponent()
	 {
		this.Load += new System.EventHandler(this.Page_Load);
		/*
		  Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(OnNeedRebind);
		  Grid1.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(OnNeedDataSource);
   		  Grid1.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(OnPageChanged);
		  Grid1.SortCommand += new ComponentArt.Web.UI.Grid.SortCommandEventHandler(OnSort);
		*/
		Grid1.UpdateCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
		Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
		Grid1.InsertCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_InsertCommand);
		Grid1.SelectCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_SelectCommand);
		Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(this.OnNeedRebind);


	 }


	 protected void Page_Load(object sender, EventArgs e)
	 {

		/*  EXample of how to avoid multiple init:
			 if (!Page.IsPostBack && (!Grid1.CausedCallback)) {
			 Grid1.DataBind();
			 }
		*/
		base.Page_Load(sender, e);

		this.Page.Title = "Business Entitlements - " + session.nameProcess + "/" + session.nameSubprocess;

		if (Request.QueryString.Get("ID") != null)
		  {
			 IDworkspace = int.Parse(Request.QueryString.Get("ID"));
		  }
		else
		  {
			 IDworkspace = 10;
		  }

		IEditingWorkspace engine = new IEditingWorkspace(HELPERS.NewOdbcConn());
		returnGetEditingWorkspace ret = engine.GetEditingWorkspace(IDworkspace);

		if (ret.ID != IDworkspace)
		  {
			 // There is no workspace with this ID!
			 Context.Response.Redirect("ListEWorkspaces.aspx");
			 return;
		  }

		this.HIDDENidWS.Value = IDworkspace.ToString();

		if (GridCfg_Height.Text == "")
		  {
			 GridCfg_RowCnt.Text = "15";
			 GridCfg_Height.Text = "400";
			 //Grid1.GroupBy = "c_u_System";
		  }

		SetGridViewMode_Scrolling();

		Site1 THEMASTER = this.Master as Site1;
		Page whatisthis = THEMASTER.Page;
	 }



	 protected void SetGridViewMode_Scrolling()
	 {


		return;

		/*
		  switch (GridCfg_Mode.tEXT) {
		  case "Group":
		  break;
		  case "Sort":
		  throw new Exception("Not implemented yet - use the widget demo to illustrate");
		  break;
		  }
		  * */


		/*
		Grid1.GroupingMode = GridGroupingMode.ConstantRows;
		Grid1.PreExpandOnGroup = false;


		Grid1.ScrollBar = GridScrollBarMode.Auto;

		Grid1.ScrollTopBottomImageHeight = 2;
		Grid1.ScrollTopBottomImageWidth = 16;
		Grid1.ScrollTopBottomImagesEnabled = true;
		Grid1.ScrollButtonWidth = 16;
		Grid1.ScrollButtonHeight = 17;
		Grid1.ScrollImagesFolderUrl = "images/scroller/";
		Grid1.ScrollBarWidth = 16;
		Grid1.ScrollBarCssClass = "ScrollBar";
		Grid1.ScrollGripCssClass = "ScrollGrip";

		Grid1.ShowFooter = false;
		Grid1.ShowHeader = true;

		//            Grid1.Height = int.Parse(GridCfg_Height.Text);

		Grid1.ManualPaging = true;
		//            Grid1.PageSize = int.Parse(GridCfg_RowCnt.Text);
		Grid1.GroupingPageSize = int.Parse(GridCfg_RowCnt.Text);

		Grid1.AllowVerticalScrolling = true;
		Grid1.AllowHorizontalScrolling = true;
		Grid1.AllowPaging = false;

		Grid1.TreeLineImageHeight = 19;
		Grid1.TreeLineImageWidth = 22;
		Grid1.TreeLineImagesFolderUrl = "images/lines/";
		*/

	 }




	 private void Grid1_InsertCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
	 {
		/* THIS WAS A HUGE MISTAKE AND HAS BEEN KILLED ON 27 MAR 2009 */

	 }



	 private void _SqlDataSourceStatusEventHandler(object sender, SqlDataSourceStatusEventArgs e)
	 {
		int xfew = 332;
	 }



	 private void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
	 {

		// UpdateCommand needs to be set to a SQL string with params
		// UpdateParameters needs to be...

		// e.Item.ToArray()[0] is the WSentitlementID number to be updated

		//Grid1.Levels[0].Columns[

		/*
		  SqlDataSource1.UpdateCommand =
		  @"
		  UPDATE t_RBSR_AUFW_u_WorkspaceEntitlement
		  * */

		if (((e.Item.ToArray()[0] as string) == "") || (e.Item.ToArray()[0] == null))
		  {
			 Grid1_InsertCommand(sender, e);
		  }
		else
		  {
			 _Grid_Update(e.Item.ToArray());
		  }
	 }


	 private void _Grid_Update(object[] x)
	 {

		string SQL = "UPDATE t_RBSR_AUFW_u_WorkspaceEntitlement SET ";
		int numParams = 0;

		for (int i = 0; i < x.Length; i++)
		  {
			 GridColumn col = Grid1.Levels[0].Columns[i];
			 if (col.AllowEditing != InheritBool.False)
				{
				  if (col.Visible && (col.DataField != "") && (col.DataField != null))
					 {
						if (numParams > 0)
						  {
							 SQL += " , ";
						  }

						SQL += col.DataField + " = @" + col.DataField + "  ";
						numParams++;
						SqlDataSource1.UpdateParameters.Add(col.DataField, x[i].ToString());
					 }
				}
		  }
		SQL += " WHERE c_id = " + x[0];

		SqlDataSource1.UpdateCommand = SQL;

		SqlDataSource1.Update();


		//UpdateDb(e.Item, "UPDATE");
	 }







	 private void Grid1_SelectCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
	 {
		int x = 5;

	 }




	 private void Grid1_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
	 {
		int x = 5;
		//  UpdateDb(e.Item, "DELETE");
	 }


	 public void OnNeedRebind(object sender, EventArgs oArgs)
	 {
		Grid1.DataBind();
	 }

	 protected void Button3_Click(object sender, EventArgs e)
	 {
		SetGridViewMode_Scrolling();

	 }

	 protected void GridCfg_RowCnt_TextChanged(object sender, EventArgs e)
	 {

	 }


  }
}
