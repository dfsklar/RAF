/* STRATEGY FOR EditStatus:
 * 
 * Numeric value:
 *    See the .rise file for the semantics of the bit vector.
 * 
 */


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

using RBSR_AUFW.DB.ITcodeEntitlement;
using RBSR_AUFW.DB.ITcodeAssignment;
using RBSR_AUFW.DB.IEventLog;


using ComponentArt.Web.UI;
using RBSR_AUFW.DB.ISAPsecurityOrgAxis;



namespace _6MAR_WebApplication
{
    public partial class WebForm113 : AFWACpage
    {

        public string strSaproleName;
	public string strSaprolePlatform;

        public int idSaprole;



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

            //Grid1.UpdateCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
            Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
            //Grid1.InsertCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_InsertCommand);
            Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(OnNeedRebind);

            /*
            COMBOBOXtype.DataRequested +=
                new ComponentArt.Web.UI.ComboBox.DataRequestedEventHandler(OnPopulate_COMBOBOXOrgValue);
            */


            /*
              Grid1.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(OnNeedDataSource);
              Grid1.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(OnPageChanged);
              Grid1.SortCommand += new ComponentArt.Web.UI.Grid.SortCommandEventHandler(OnSort);
              Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
              Grid1.SelectCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_SelectCommand);
            */
        }


        public void OnNeedRebind(object sender, EventArgs oArgs)
        {
            this.DBrefresh();
        }



        /*
        public void OnPopulate_COMBOBOXOrgValue(object sender, ComboBoxDataRequestedEventArgs args)
        {
            COMBOBOXOrgValue.Items.Clear();

            ISAPsecurityOrgAxis engine = new ISAPsecurityOrgAxis(HELPERS.NewOdbcConn());
            returnListSAPsecurityOrgAxis[] ret = 
              engine.ListSAPsecurityOrgAxis(null, "\"SAP_Name\" = ?", new string[]{args.Filter}, "");
            if (ret.Length == 1) {
                string[] legalvals = ret[0].LegalValues.Split(new char[]{','});
                foreach (string S in legalvals)
                {
                    int baby = COMBOBOXOrgValue.Items.Add(
                        new ComboBoxItem(S));
                    COMBOBOXOrgValue.Items[baby].Value = S;
                }
            }
            
        }
        */







        private void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            throw new Exception("MOPVED");
            if (e.Item["IDtass"] == null)
            {
                throw new Exception("MOVED: Grid1_InsertCommand(sender, e)");
                
            }

            int IDtass = Int32.Parse(e.Item["IDtass"].ToString());
            int IDentit = Int32.Parse(e.Item["IDTcodeEnt"].ToString());

            // RecordChangeToVector(IDtass, IDentit, e);

            // Now that we are scoped to a role, the only change regarding "role linkage"
            // is REMOVAL from a role's assignment.
            //RecordChangeToRoleLink(IDtass, IDentit, e);
        }




        private void RecordChangeToRoleLink(int IDtass, int IDentit, GridItemEventArgs e)
        {
            throw new Exception("NOT IMPLEMENTED");
        }
        private void _DEFUNCT_RecordChangeToRoleLink(int IDtass, int IDentit, GridItemEventArgs e)
        {
            ITcodeAssignment Itass
              = new ITcodeAssignment(HELPERS.NewOdbcConn());

            // Get the ID of the current SAP role linked via this TcodeAssignment obj
            returnGetTcodeAssignment prev = Itass.GetTcodeAssignment(IDtass);
            int prevID = prev.SAProleID;


            // NEW?
            int newID = int.Parse(SafeString(e.Item["SAProlename"]));

            if (prevID != newID)
            {
                /*
                IEventLog LOG = new IEventLog(HELPERS.NewOdbcConn());
                // 1. Log this change so we record the previous value.
                string action = "OrigTASSro";
                int IDlog = LOG.NewEventLog
                  (DateTime.Now,
                   this.session.idUser,
                   this.Request.ServerVariables["REMOTE_ADDR"],
                   action);
                LOG.SetEventLog
                  (IDlog, DateTime.Now,
                   this.session.idUser,
                   this.Request.ServerVariables["REMOTE_ADDR"],
                   action, IDtass, IDentit.ToString());
                */
                throw new Exception("NOT IMPLEMENTED");
                // I BLIEVE THIS IS NOT UJSED ANYHMORE?


                // 2A. Make the actual change.

                /*
                  returnGetTcodeAssignment theTass = Itass.GetTcodeAssignment(IDtass);
                  Itass.SetTcodeAssignment
                  (IDtass,
                  theTass.TcodeAssignmentSetID,
                  newID,
                  theTass.TcodeEntitlementID,
                  theTass.EditStatus | 8);
                */

                //                DBrefresh();

            }

        }











        private string SafeString(object p)
        {
            if (p == null)
            {
                return "";
            }
            else
            {
                return p.ToString();
            }
        }




        protected void SetGridViewMode_Scrolling()
        {

            /*
              switch (GridCfg_Mode.tEXT) {
              case "Group":
              break;
              case "Sort":
              throw new Exception("Not implemented yet - use the widget demo to illustrate");
              break;
              }
              * */

            // Grid1.GroupingMode = GridGroupingMode.ConstantRows;


            //          Grid1.ScrollBar = GridScrollBarMode.Auto;

            Grid1.ScrollTopBottomImageHeight = 2;
            Grid1.ScrollTopBottomImageWidth = 16;
            Grid1.ScrollTopBottomImagesEnabled = true;
            Grid1.ScrollButtonWidth = 16;
            Grid1.ScrollButtonHeight = 17;
            Grid1.ScrollImagesFolderUrl = "images/scroller/";
            Grid1.ScrollBarWidth = 16;
            Grid1.ScrollBarCssClass = "ScrollBar";
            Grid1.ScrollGripCssClass = "ScrollGrip";

            //Grid1.ShowFooter = false;
            Grid1.ShowHeader = true;

            //            Grid1.Height = int.Parse(GridCfg_Height.Text);

            // Grid1.ManualPaging = true;

            //            Grid1.PageSize = int.Parse(GridCfg_RowCnt.Text);
            // Grid1.GroupingPageSize = 2;

            //          Grid1.AllowVerticalScrolling = true;
            //          Grid1.AllowHorizontalScrolling = true;
            //Grid1.AllowPaging = false;

            Grid1.TreeLineImageHeight = 19;
            Grid1.TreeLineImageWidth = 22;
            Grid1.TreeLineImagesFolderUrl = "images/lines/";

        }




        protected void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);


            this.Page.Title = "SAP Entitlements - " + session.nameProcess + "/" + session.nameSubprocess;



            strSaproleName = Request.QueryString.Get("RoleName");
	    strSaprolePlatform = Request.QueryString.Get("RolePlatform");

            if (Request.QueryString.Get("RoleID") != null)
            {
                this.idSaprole = int.Parse(Request.QueryString.Get("RoleID"));
            }
            else
            {
                throw new Exception("No identification of the SAP role being edited.");
            }


            if (Page.IsPostBack)
            {
                return;
            }


            DBrefresh();
            SetGridViewMode_Scrolling();
        }



        private string mode = "UNK";


        private void DBrefresh()
        {

            ///////////////////
            // CREATE THE DATA SET PROGRAMMATICALLY
            // The use of SqlDataSource in the aspx file does not allow for 
            // a DataSet that contains multiple DataTables, which is needed
            // for some custom editing features of the grid.


            DataView DV =
              (DataView)(SqlDataSource1.Select(new DataSourceSelectArguments()));
            DataTable DVT = DV.ToTable("DefaultView");

            DataSet DSET = new DataSet();
            DSET.Tables.Add(DVT);

            DV = (DataView)this.SQL_valuemenu_type.Select(new DataSourceSelectArguments());
            DVT = DV.ToTable("SAPENTTYPE");
            DSET.Tables.Add(DVT);

            DV = (DataView)this.SQL_valuemenu_SAProle.Select(new DataSourceSelectArguments());
            DVT = DV.ToTable("SAPROLE");
            DSET.Tables.Add(DVT);

            DV = (DataView)this.SQL_valuemenu_accesslevel.Select(new DataSourceSelectArguments());
            DVT = DV.ToTable("SAPENTACCESSLEVEL");
            DSET.Tables.Add(DVT);


            // Some security checking
            DV = (DataView)this.SQL_WorkspaceDetails.Select(new DataSourceSelectArguments());
            DVT = DV.ToTable("WORKSPACE");

            // 1. This workspace still in "WORKSPACE" status?
            if ("WORKSPACE" != (DVT.Rows[0]["c_u_Status"] as string))
            {
                this.mode = "READONLY";
            }
            else
            {

                // 2. This workspace is owned by the current user?
                if (this.session.idUser !=
                    (int)(DVT.Rows[0]["c_r_User"]))
                {
                    // Whoa!  This user is not the owner!
                    this.mode = "READONLY";
                }
                else
                {
                    this.mode = "EDIT";
                }
            }

            this.HIDDENeditmode.Value = this.mode;




            // Now bind
            Grid1.DataSource = DSET;
            Grid1.DataBind();

            // Ensure proper edit-enabling
            if (this.mode == "READONLY")
            {
                Grid1.AllowEditing = false;
                Grid1.EditOnClickSelectedItem = false;
                this.PanelEditButtons.Visible = false;
                this.PanelMacroActionsEDIT.Visible = false;
            }
        }


        // Actually, this TOGGLES the deletion state!
        private void Grid1_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            int idTassToDel = int.Parse(e.Item["IDtass"].ToString());
            SAP_HELPERS.RecordDeletionOfEntitlementAssignmentRow(idTassToDel,'T');
        }



        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}
