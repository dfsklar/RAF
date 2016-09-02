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

using RBSR_AUFW.DB.IEntitlement;




namespace _6MAR_WebApplication
{
  public partial class WebForm117 : AFWACpage
  {

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
      Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(OnNeedRebind);
      Grid1.FilterCommand += new ComponentArt.Web.UI.Grid.FilterCommandEventHandler(OnFilter);
      Grid1.GroupCommand += new ComponentArt.Web.UI.Grid.GroupCommandEventHandler(OnGroup);
      Grid1.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(OnNeedDataSource);
      Grid1.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(OnPageChanged);
      Grid1.SortCommand += new ComponentArt.Web.UI.Grid.SortCommandEventHandler(OnSort);

      Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);


      /*
        Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
        Grid1.InsertCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_InsertCommand);
        Grid1.SelectCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_SelectCommand);
      */
      Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(this.OnNeedRebind);
    }

    public void OnSort(object sender, ComponentArt.Web.UI.GridSortCommandEventArgs oArgs)
    {
      Grid1.Sort = oArgs.SortExpression;
    }

    public void OnFilter(object sender, ComponentArt.Web.UI.GridFilterCommandEventArgs oArgs)
    {
      Grid1.Filter = oArgs.FilterExpression;
    }

    public void OnGroup(object sender, ComponentArt.Web.UI.GridGroupCommandEventArgs oArgs)
    {
      Grid1.GroupBy = oArgs.GroupExpression;
    }

    public void OnPageChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs oArgs)
    {
      Grid1.CurrentPageIndex = oArgs.NewIndex;
    }


    public void OnNeedDataSource(object sender, EventArgs oArgs)
    {
      this.Grid1.DataBind();
    }


    public void COMBOXchooseApp_SelectedIndexChanged__PreSep2009(Object sender, EventArgs e)
    {
      if (COMBOXchooseApp.SelectedValue != null)
        {
          Session["STRcurAppScope"] = COMBOXchooseApp.SelectedItem.Text;
          Session["COMBOIDXcurAppScope"] = COMBOXchooseApp.SelectedIndex;
        }
      this.Grid1.DataBind();
      if (this.Grid1.RecordCount > 0)
        {
          this.PANELcond_ZeroRows.Visible = false;
          this.PANELgrid.Visible = true;
        }
      else
        {
          this.PANELcond_ZeroRows.Visible = true;
          this.PANELgrid.Visible = false;
        }

      // Special case: if SAP is the selected app, hide the column that regards editing.
        // Things have changed: there is no longer any edit-buttons column.
        // This was hiding the Commentary column, which may actually be correct behavior
        // for SAP by coincidence, but I'm not sure yet:
  //    Grid1.Levels[0].Columns[3].Visible = ( ! (Session["STRcurAppScope"].ToString() == "SAP"));
        
    }




      public void COMBOXchooseApp_SelectedIndexChanged(Object sender, EventArgs e)
      {
          if (COMBOXchooseApp.SelectedValue != null)
          {
              Session["STRcurAppScope"] = COMBOXchooseApp.SelectedItem.Text;
              Session["COMBOIDXcurAppScope"] = COMBOXchooseApp.SelectedIndex;
          }
          this.Response.Redirect("PAGE_Bents_vectors.aspx");
          return;

      }


    public void OnNeedRebind(object sender, EventArgs oArgs)
    {
      Grid1.DataBind();
    }



    public void Grid1_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      int idTassToDel = int.Parse(e.Item["c_id"].ToString());

      try
        {
          IEntitlement engine = new IEntitlement(HELPERS.NewOdbcConn());
          engine.DeleteEntitlement(idTassToDel);
        }
      catch (Exception exxx)
        {
          throw new Exception("Deletion failed - this entitlement is in use in an assignment in at least one workspace.");
        }
     
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      string appSelectedByURL = this.Request.QueryString["app"];
      if (appSelectedByURL != null)
      {
          Session["STRcurAppScope"] = appSelectedByURL;
          Session["COMBOIDXcurAppScope"] = 0;
      }

      // AT THIS POINT, THE GRID IS NOT YET POPULATED.
      // TOO EALRY?
        /*
      if (this.Grid1.RecordCount > 0)
      {
          this.PANELcond_ZeroRows.Visible = false;
          this.PANELgrid.Visible = true;
      }
      else
      {
          this.PANELcond_ZeroRows.Visible = true;
          this.PANELgrid.Visible = false;
      }
         */


      if (!this.IsPostBack)
        {
          try
            {
              COMBOXchooseApp.SelectedIndex = int.Parse(Session["COMBOIDXcurAppScope"].ToString());
            }
          catch (Exception e_ignore) { }

          // Stuart has requested that the start of a visit to this page clears out the
          // grid of the previous content from the last time this page was visited.
          // I.e. he does not want sticky...

          // However, I think this POST operation confuses callbacks and hurts the grid.
          //this.Session["STRcurAppScope"] = "";
          //COMBOXchooseApp_SelectedIndexChanged(sender, e);
        }
    }
  }
}
