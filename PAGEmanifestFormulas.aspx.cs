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

using RBSR_AUFW.DB.IMVFormula;
using System.Text.RegularExpressions;
using RBSR_AUFW.DB.IApplication;




namespace _6MAR_WebApplication
{
  public partial class WebForm110 : AFWACpage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      if (Request.Params["add"] != null)
        {
          IApplication engine = new IApplication(HELPERS.NewOdbcConn());
          try
            {
              engine.NewApplication(Request.Params["add"]);
            }
          catch (Exception eee) { }
          // Failure would only occur if this name already present (e.g. if user doubleclicked)

          Response.Redirect("PAGEmanifestFormulas.aspx");
          return;
        }


      Grid1.UpdateCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
      Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
      Grid1.InsertCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_InsertCommand);
      Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(this.OnNeedRebind);
    }




    private void Grid1_InsertCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      UpdateDb(e.Item, "INSERT");
    }

    private void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      UpdateDb(e.Item, "UPDATE");
    }

    private void Grid1_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      UpdateDb(e.Item, "DELETE");
    }


    public void OnNeedRebind(object sender, EventArgs oArgs)
    {
      Grid1.DataBind();
    }





    private void UpdateDb(ComponentArt.Web.UI.GridItem item, string command)
    {
      IMVFormula engine = new IMVFormula(HELPERS.NewOdbcConn());
      IApplication engineApp = new IApplication(HELPERS.NewOdbcConn());

      switch (command)
        {

              
        case "INSERT":
          throw new Exception("Internal error PGMF#85; contact Sklar");
          break;


        case "UPDATE":

          //ADDED 3 JULY 2009: support for changing application name:
          string origname = item["origname"] as string;
          string newname = item["c_u_Name"] as string;
          string newL4 = item["c_u_BOOLneedsLevel4"] as string;
          newname = newname.Trim();

          if (origname != newname)
            {
              HELPERS.RenameApplication(HELPERS.NewOdbcConn(), origname, newname);
            }

            engineApp.SetApplication
                (int.Parse(item["c_id"] as string), newname, newL4);

          if ((item["MVFID"] as string) == "") {

            int newID = engine.NewMVFormula(item["c_u_Name"] as string);
            engine.SetMVFormula(
                                newID,
                                item["c_u_Name"] as string,
                                null, null, null, 
                                item["c_u_Formula"] as string);
          }
          else{
            engine.SetMVFormula(
                                int.Parse(item["MVFID"] as string),
                   
                                item["c_u_Name"] as string,
                                null, null, null,
                                item["c_u_Formula"] as string);
          }
          break;


        case "DELETE":
          throw new NotImplementedException();
          break;
        }
    }

  }


}
