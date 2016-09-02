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
using RBSR_AUFW.DB.ITcodeDictionary;
using ComponentArt.Web.UI;

namespace _6MAR_WebApplication
{
  public partial class WebForm123 : AFWACpage
  {


    private void InitializeComponent()
    {
      this.Load += new System.EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);
            
      Grid1.UpdateCommand +=
        new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);

      CALLBACK_GRID_orgvalue.Callback +=
	new ComponentArt.Web.UI.CallBack.CallbackEventHandler(Callback1_Callback);
     }




      private string idCurrentOrgAxis = "-32432";

    private void Callback1_Callback(object sender,
				    ComponentArt.Web.UI.CallBackEventArgs e)
    {
       //this.SQL_OrgValue.SelectParameters.Add("FILTERorgaxis", "1");
       this.idCurrentOrgAxis = e.Parameter;
       this.GRID_orgvalue.DataSource =
           (DataView)
           this.SQL_OrgValue.Select(DataSourceSelectArguments.Empty);
       this.GRID_orgvalue.DataBind();
        GRID_orgvalue.RenderControl(e.Output);
    }


      protected void INTERCEPTsqldatasource_sqlorgvalue_select
          (object sender, SqlDataSourceSelectingEventArgs e)
      {
          e.Command.Parameters[0].Value = this.idCurrentOrgAxis;
      }



    private void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      
      GridItem theitem = e.Item;
      int idRow = int.Parse(theitem["c_id"] as string);

      ITcodeDictionary engine = new ITcodeDictionary(HELPERS.NewOdbcConn());
      engine.SetTcodeDescription(idRow, theitem["c_u_Description"] as string);     
    }


    public string HumanReadableRelatedOrgValueList(string income)
    {
      return "FJIEOWFEW";
    }

  }



}
