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


namespace _6MAR_WebApplication
{
    public partial class SandboxEditor : System.Web.UI.Page
    {

        protected int IDworkspace = 9;  //hardwired for demo


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

        }


        private void Grid1_InsertCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            // NEVER INSERT USING SQLSERVER SHIT!
            // No way to get the primary key easily of the new row.
            // absolute shit

            // Use the RISE-genearated ODBC

            string connstr = @"Dsn=RISEauthframework";

                        System.Data.Odbc.OdbcConnection conn = 
                new System.Data.Odbc.OdbcConnection(connstr);

            conn.Open();

            IWorkspaceEntitlement Iwserows = new IWorkspaceEntitlement(conn);

            // It would be nice to verify that the permutation vector is indeed unique to this sandbox!!
            // LATER!


            Hashtable fields = new Hashtable();

            string SQLcolnames = "c_r_EditingWorkspace ";
            string SQLcolvalues = IDworkspace.ToString();


            int numParams = 0;

            for (int i = 0; i < e.Item.ToArray().Length; i++)
            {
                GridColumn col = Grid1.Levels[0].Columns[i];
                if (col.AllowEditing != InheritBool.False)
                {
                    if (col.Visible && (col.DataField != "") && (col.DataField != null))
                    {

                        SQLcolnames += " , ";
                        SQLcolvalues += " , ";

                        fields.Add(col.DataField, e.Item.ToArray()[i].ToString());

                        SQLcolnames += col.DataField;
                        SQLcolvalues += "@" + col.DataField;
                        numParams++;
                        SqlDataSource1.InsertParameters.Add(col.DataField, e.Item.ToArray()[i].ToString());
                    }
                }
            }


            // Here we only add the REQUIRED columns, then we'll do an UPDATE to fill in the non-required

            int IDnewEntVector =
                        Iwserows.NewWorkspaceEntitlement
                        (
                        fields["c_u_StandardActivity"] as string,
                        fields["c_u_RoleType"] as string ,
                        fields["c_u_System"] as string ,
                        fields["c_u_Platform"] as string ,
                        fields["c_u_EntitlementName"] as string ,
                        fields["c_u_EntitlementValue"] as string ,
                        IDworkspace);


            Iwserows.SetWorkspaceEntitlement(IDnewEntVector,
                        fields["c_u_StandardActivity"] as string,
                        fields["c_u_RoleType"] as string,
                        fields["c_u_System"] as string,
                        fields["c_u_Platform"] as string,
                        fields["c_u_EntitlementName"] as string,
                        fields["c_u_EntitlementValue"] as string,
                        fields["c_u_AuthObjName"] as string,
                        fields["c_u_AuthObjValue"] as string,
                        fields["c_u_FieldSecName"] as string,
                        fields["c_u_FieldSecValue"] as string,
                        null,
                        null,
                        fields["c_u_Commentary"] as string,
                        IDworkspace);

            conn.Close();

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


    }
}
