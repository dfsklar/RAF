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

using System.Data.Odbc;
using RBSR_AUFW.DB.IProcess;
using RBSR_AUFW.DB.ISubProcess;
using RBSR_AUFW.DB.IUser;


namespace _6MAR_WebApplication
{
  public partial class WebForm2 : System.Web.UI.Page
  {



    // This is still in use, even with the new GUI, as of Oct2009
    private void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
        
      int idSubpr = int.Parse(e.Item["c_id"] as string);
      int idProcess = int.Parse(e.Item["IdOfProcess"] as string);
      string nameSubpr = e.Item["NameOfSubprocess"] as string;
      string namePr = e.Item["NameOfProcess"] as string;
      string statusSubpr = e.Item["StatusOfSubprocess"] as string;
      string descrPr = e.Item["DescrOfProcess"] as string;

      IProcess engineProcess = new IProcess(HELPERS.NewOdbcConn());
      ISubProcess engineSubProcess = new ISubProcess(HELPERS.NewOdbcConn());

      engineProcess.SetProcess(idProcess, namePr, descrPr);
      engineSubProcess.SetSubProcess(idSubpr, nameSubpr, idProcess, statusSubpr);
         
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      Response.Cache.SetNoStore();
      Response.Cache.SetExpires(DateTime.MinValue);

      if (!AlreadyLoggedIn())
        {
          this.ChooserProcess.DataBind();
          if (Page.IsPostBack)
            {
              this.PanelLoginFailMsg.Visible = false;
              this.PanelReadOnlyLoginFailMsg.Visible = false;
              AuthCheck();
            }
          else
            {
              this.PanelLoginFailMsg.Visible = false;
              this.PanelReadOnlyLoginFailMsg.Visible = false;
            }
        }
      else 
        {
          if (Request.Params["subprid"] != null)
            {
              LogInToSubprocess(
                                int.Parse(Request.Params["subprid"]),
                                Request.Params["prname"],
                                Request.Params["subprname"]);
              Response.Redirect("HOME.aspx");
            }
        }

      Grid1.UpdateCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
    }



    private bool AlreadyLoggedIn()
    {
      if (Session["AFWACSESSION"] == null)
        {
          this.PanelProcessArea.Visible = false;
          return false;
        }
      else
        {
          this.sessnew = Session["AFWACSESSION"] as AFWACsession;
          this.PanelUserArea.Visible = false;
          this.PanelProcessArea.Visible = true;
          this.PanelLoginFailMsg.Visible = false;
          this.PanelReadOnlyLoginFailMsg.Visible = true;
          return true;
        }
    }




    // This is called if someone selects an existing subprocess:
    protected void OnSelect(object sender, EventArgs e)
    {
      GridView gv = sender as GridView;
      int idsubpr = (int)gv.SelectedDataKey.Value;

      if (!AuthCheck())
        {
          Server.Transfer("AuthFail.html");
          return;
        }

      LogInToSubprocess(idsubpr,
                        gv.Rows[gv.SelectedIndex].Cells[1].Text,
                        gv.Rows[gv.SelectedIndex].Cells[2].Text);


      Response.Redirect("HOME.aspx");

    }



    public AFWACsession sessnew = null;


    private bool AuthCheck()
    {
      if (AlreadyLoggedIn())
        return true;


      int IDuser = int.Parse(this.CHOOSEusername.SelectedValue);
      string STRpassword = this.TXTpassword.Text.Trim();

      IUser userfactory = new IUser(HELPERS.NewOdbcConn());
      returnGetUser userinfo = userfactory.GetUser(IDuser);
      if
        (userinfo.EID.ToLower() ==
         STRpassword.ToLower())
        {
          // 
          // LOGIN GOOD !
          //
          sessnew = new AFWACsession(this.Request);
          sessnew.username = userinfo.Name;
          sessnew.idUser = IDuser;

          // this extra session var UUIDSUBPROCESS is needed so data sources can use
          // it as parameters for scoping results.
          Session["AFWACSESSION"] = sessnew;
          Session["AFWACUSERID"] = IDuser;

          Response.Redirect("SessionLOGIN.aspx");

          return true;
        }
      else
        {
          this.PanelLoginFailMsg.Visible = true;
          return false;
        }

    }






    protected void LogInToSubprocess
      (int idsubpr,
       string nameprocess, string namesubprocess)
    {

      sessnew.idSubprocess = idsubpr;
      sessnew.nameProcess = nameprocess;
      sessnew.nameSubprocess = namesubprocess;

      // this extra session var UUIDSUBPROCESS is needed so data sources can use
      // it as parameters for scoping results.
      Session["UUIDSUBPROCESS"] = idsubpr;

      // Get rid of any sticky session information that is subprocess-dependent
      Session["INTcurWS"] = "";
      Session["INTcurWS_SAP"] = "";

      sessnew.ObtainWorkspaceContext();
    }




    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void Button2_Click(object sender, EventArgs e)
    {
      int x = 3;
    }




    protected void BTNloginReadOnly_Click(object sender, EventArgs e)
    {
      string eid = this.TXTbusownereid.Text.Trim();
      IUser engineuser = new IUser(HELPERS.NewOdbcConn());
      returnListUser[] ret =
        engineuser.ListUser(null, "\"EID\" like ?", new string[] { eid }, "");
      if (ret.Length != 1)
        {
          this.PanelReadOnlyLoginFailMsg.Visible = true;
          return;
        }
      if (ret.Length == 1)
        {
          Session["RAFLOGINbusOwnerEID"] = eid;
          Session["RAFLOGINbusOwnerUserID"] = ret[0].ID;
          Session["RAFLOGINbusOwnerNameFirst"] = ret[0].NameFirst;
          Response.Redirect("viewer/home.aspx");
          return;
        }
      throw new Exception("Database internal error code SESLG_230");
    }




    // User wants to create a new subprocess
    protected void Button1_Click(object sender, EventArgs e)
    {

      if (!AuthCheck())
        {
          Server.Transfer("AuthFail.html");
          return;
        }


      int IDprocess;
      OdbcConnection conn = HELPERS.NewOdbcConn();
      string newname;
      string processname;

      // New process or existing process?
      if (ChooserProcess.SelectedItem != null)
        {
          // Existing process!
          IDprocess = int.Parse(ChooserProcess.SelectedItem.Value);
          processname = ChooserProcess.SelectedItem.Text;
        }
      else
        {
          IProcess Iprc = new IProcess(conn);
          newname = ChooserProcess.Text.Trim();
          processname = newname;
          if (newname.Length < 2)
            {
              throw new Exception("Name for new process is empty or too brief.");
            }
          IDprocess = Iprc.NewProcess(newname);
        }

      // Create the new subprocess

      ISubProcess Isprc = new ISubProcess(conn);
      newname = this.TextBox_NewSubprocess.Text.Trim();
      if (newname.Length < 2)
        {
          throw new Exception("Name for new subprocess is empty or too brief.");
        }
      int IDsubprocess = Isprc.NewSubProcess(newname, IDprocess);
      Isprc.SetSubProcess(IDsubprocess, newname, IDprocess, "Active");

      // New subprocess now created.  Record this with the session.
      LogInToSubprocess(IDsubprocess,
                        processname, newname);

      Response.Redirect("HOME.aspx");
    }



    // protected voideGRIDVIEW_chooseSubprocess.
  }
}
