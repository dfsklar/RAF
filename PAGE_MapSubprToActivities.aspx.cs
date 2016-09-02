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
using RBSR_AUFW.DB.IMETADATA_SubprToActivityList;
using System.Data.Odbc;

namespace _6MAR_WebApplication
{
  public partial class WebForm122 : AFWACpage
  {



    private void InitializeComponent()
    {
      this.Load += new System.EventHandler(this.Page_Load);
    }


    public int idSubPr;




    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);


      try
        {
          idSubPr = int.Parse(Request.Params["idsubr"]);
        }
      catch (Exception e2)
        {
          idSubPr = session.idSubprocess;
        }



      // Create lots of blank rows if not yet any rows for this subprocess.
      IMETADATA_SubprToActivityList engine = new IMETADATA_SubprToActivityList(HELPERS.NewOdbcConn());
      returnListMETADATA_SubprToActivityListBySubProcess[]
        retlist = engine.ListMETADATA_SubprToActivityListBySubProcess(null, idSubPr);
      if (retlist.Length == 0)
        {
          // AutoCreate lots of rows!
          for (int i = 0; i < 40; i++)
            {
              int idBaby = engine.NewMETADATA_SubprToActivityList
                ("ACT", idSubPr);
              engine.SetMETADATA_SubprToActivityList
                (idBaby, (i+1) * 1000, "ACT", false, "", "", "", idSubPr); 
            }
        }


      DataView dv =
        (DataView)
        SqlDataSource1.Select(DataSourceSelectArguments.Empty);

      GridMain.DataSource = dv;
    }


    protected void INTERCEPTparamsubprocess(object sender, SqlDataSourceSelectingEventArgs e) 
    {
        e.Command.Parameters[0].Value = session.idSubprocess;
    }



  }
}
