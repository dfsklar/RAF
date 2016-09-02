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
using System.Data.Odbc;

namespace _6MAR_WebApplication
{
  public partial class WebForm123 : AFWACpage
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
      Grid1.UpdateCommand +=
        new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
      Grid1.FilterCommand +=
        new ComponentArt.Web.UI.Grid.FilterCommandEventHandler(this.Grid1_FilterCommand);
      Grid1.NeedDataSource +=
        new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(this.Grid1_NeedDataSource);
      Grid1.PageIndexChanged +=
        new Grid.PageIndexChangedEventHandler(this.Grid1_PageIndexChanged);
      Grid1.NeedRebind +=
        new Grid.NeedRebindEventHandler(this.Grid1_NeedRebind);
    }



    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);
    }


      private string condition = null;

    public void Grid1_PageIndexChanged(object sender, GridPageIndexChangedEventArgs e)
    {
    }

    public void Grid1_NeedRebind(object sender, EventArgs e)
    {

      
      DataTable table = new DataTable();

      table.Columns.Add("c_id", typeof(Int32));
      table.Columns.Add("c_u_TcodeID", typeof(string));
      table.Columns.Add("c_u_Description", typeof(string));


        if (condition == null) 
            condition = " 1 = 1 ";

        OdbcDataReader dr = HELPERS.RunSqlSelect("SELECT TOP 20 * FROM t_RBSR_AUFW_u_TcodeDictionary WHERE " + condition);

        table.BeginLoadData();
        while (dr.Read())
        {
            string descr = "";
            try
            {
                descr = dr.GetString(2);
            }
            catch (Exception) { }
            table.LoadDataRow(new object[] { dr.GetInt32(0), dr.GetString(1), descr }, true);
        }


      table.EndLoadData();

      DataSet DSET = new DataSet();
      DSET.Tables.Add(table);

      Grid1.DataSource = DSET;
      Grid1.DataBind();
    }





    public void Grid1_FilterCommand(object sender, GridFilterCommandEventArgs e)
    {
      this.condition = e.FilterExpression;
    }




    public void Grid1_NeedDataSource(object sender, EventArgs e)
    {
          
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
