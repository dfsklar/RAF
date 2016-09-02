using System;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using RBSR_AUFW.DB.IProcess;
using RBSR_AUFW.DB.ISubProcess;
using ComponentArt.Web.UI;

using Ionic.Zip;



namespace _6MAR_WebApplication
{
  public partial class WebForm120 : AFWACpage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      if (MEGATREE.Nodes.Count == 0)
        {
          LoadProcesses();
        }
    }





    private void LoadProcesses()
    {
      IProcess engineProcess = new IProcess(HELPERS.NewOdbcConn());
      returnListProcess[] allprocs = engineProcess.ListProcess(null, "", new string[] { }, "c_u_Name asc");
      foreach (returnListProcess cur in allprocs)
        {
          if (cur.Name == "-")
            {
              continue;
            }
          if (cur.Name.ToLower() == "(internal use)")
            {
              continue;
            }
          ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
          rootNode.Text = cur.Name;
          rootNode.Expanded = false;
          rootNode.ImageUrl = "folder.gif";
          rootNode.ShowCheckBox = false;
          MEGATREE.Nodes.Add(rootNode);

          LoadSubProcesses(cur.ID, rootNode);
        }
    }



    private void LoadSubProcesses(int idProc, TreeViewNode nodeProc)
    {
      ISubProcess engineProcess = new ISubProcess(HELPERS.NewOdbcConn());
      returnListSubProcessByProcess[] allprocs = engineProcess.ListSubProcessByProcess
        (null, "\"Status\" = ?", new string[] { "Active" }, "c_u_Name asc", idProc);
      foreach (returnListSubProcessByProcess cur in allprocs)
        {
          ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
          rootNode.Text = cur.Name;
          rootNode.Expanded = false;
          rootNode.ImageUrl = "folder.gif";
          rootNode.ShowCheckBox = true;
          rootNode.ID = "SP/" + cur.ID;
          rootNode.ContentCallbackUrl = "XMLtree_RolesInSubprocess.ashx?subproc=" + cur.ID;
              
          nodeProc.Nodes.Add(rootNode);

        }
    }



    struct exportinfo
    {
      public int idWS;
      public ArrayList ARRidNode;
      public string strName;
      public bool isWorkspace;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
      TreeViewNode[] checkednodes = MEGATREE.CheckedNodes;

      //Key is: subprocess ID
      //Value is: struct containing the workspaceID and the list of rolenums
      Hashtable selectedSubprocess = new Hashtable();

      foreach (TreeViewNode curnode in checkednodes)
        {
          if (curnode.ID.StartsWith("SP/"))
            {
              if (curnode.Checked)
                {
                  exportinfo baby = new exportinfo();
                  baby.ARRidNode = new ArrayList();
                  baby.idWS = -1;
                  baby.isWorkspace = false;
                  baby.strName = curnode.Text;
                  selectedSubprocess[int.Parse(curnode.ID.Substring(3))] = baby;
                }
            }
          string srchfor = "EntSet/ACT/";
          if (curnode.ID.StartsWith(srchfor))
            {
              int subpr = int.Parse(curnode.ID.Substring(srchfor.Length));
              try
                {
                  exportinfo x = (exportinfo)(selectedSubprocess[subpr]);
                  x.idWS = int.Parse(curnode.Value);
                  x.strName += " (ACTIVE)";
                  selectedSubprocess[subpr] = x;
                }
              catch (Exception eign) { }
            }
          srchfor = "EntSet/WS/";
          if (curnode.ID.StartsWith(srchfor))
            {
              int subpr = int.Parse(curnode.ID.Substring(srchfor.Length));
              try
                {
                  exportinfo x = (exportinfo)(selectedSubprocess[subpr]);
                  x.idWS = int.Parse(curnode.Value);
                  x.strName += " (Workspace)";
                  x.isWorkspace = true;
                  selectedSubprocess[subpr] = x;
                }
              catch (Exception eign) { }
            }
          srchfor = "EntSet/ARCHIVE/";
          if (curnode.ID.StartsWith(srchfor))
            {
              int subpr = int.Parse(curnode.ID.Substring(srchfor.Length));
              try
                {
                  exportinfo x = (exportinfo)(selectedSubprocess[subpr]);
                  x.idWS = int.Parse(curnode.Value);
                  x.strName += " (Archive: " + curnode.Text + ")";
                  x.isWorkspace = true;
                  selectedSubprocess[subpr] = x;
                }
              catch (Exception eign) { }
            }
          srchfor = "BR/";
          if (curnode.ID.StartsWith(srchfor))
            {
              // This node represents a business role.
                if (curnode.Checked)
                {
                    string[] parts = curnode.ID.Split(new char[] { '/' });

                    // Special value: parts[1] == "*ALL*" means all roles is implied, we do not have an explicit list.
                    // The ARRidNode array will thus be left empty and that signals "show all roles" to the export engine.
                    
                    {
                        int subpr = int.Parse(parts[1]);
                        int broleID = -1;  // -1 is special sentinel meaning all business roles *ALL*
                        if (parts[2] != "*ALL*")
                        {
                            broleID = int.Parse(parts[2]);
                        }

                        if (broleID >= 0)
                        {
                            try
                            {
                                exportinfo x = (exportinfo)(selectedSubprocess[subpr]);
                                x.ARRidNode.Add(broleID);
                                selectedSubprocess[subpr] = x;  //ESSENTIAL, or the change to the array is ignored!!
                            }
                            catch (Exception eign) { }
                        }
                    }
                }

            }

        }


      // NOW we are ready to launch.

      // If exactly one subprocess, then we can force the IFRAME to be the receipient
      if (selectedSubprocess.Count == 1)
        {
          IEnumerator enumKeys = selectedSubprocess.Keys.GetEnumerator();
          enumKeys.MoveNext();
          int thekey = (int)(enumKeys.Current);
          exportinfo guider = (exportinfo)(selectedSubprocess[thekey]);

          string curSubcontractorURL = GenUrl(thekey, ref guider);
          
          this.TheIframe.Attributes["src"] = curSubcontractorURL;
        }



      if (selectedSubprocess.Count > 1)
        {

            if (CHKforFullDetailCSV.Checked)
            {
                // We want all the subprocesses all merged into a single CSV
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition",
                                           "filename=export.csv;attachment");
                IEnumerator enumKeys = selectedSubprocess.Keys.GetEnumerator();
                while (enumKeys.MoveNext())
                {
                    int thekey = (int)(enumKeys.Current);
                    exportinfo guider = (exportinfo)(selectedSubprocess[thekey]);
                    string curSubcontractorURL = GenUrl(thekey, ref guider);

                    HttpWebRequest subcontract =
                      (HttpWebRequest)WebRequest.Create(curSubcontractorURL);
                    subcontract.Method = "GET"; //experiment: may not work!!
                    HttpWebResponse thereply = (HttpWebResponse)subcontract.GetResponse();
                    Stream replycontent = thereply.GetResponseStream();
                    StreamReader SRreplycontent = new StreamReader(replycontent);

                    Response.Write(SRreplycontent.ReadToEnd());
                }
                Response.End();

            }
            else
            {
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "filename=excelfilecollection.zip");

                ZipFile zipfileOut = new ZipFile();

                IEnumerator enumKeys = selectedSubprocess.Keys.GetEnumerator();
                while (enumKeys.MoveNext())
                {
                    int thekey = (int)(enumKeys.Current);
                    exportinfo guider = (exportinfo)(selectedSubprocess[thekey]);
                    string curSubcontractorURL = GenUrl(thekey, ref guider);

                    HttpWebRequest subcontract =
                      (HttpWebRequest)WebRequest.Create(curSubcontractorURL);
                    subcontract.Method = "GET"; //experiment: may not work!!
                    HttpWebResponse thereply = (HttpWebResponse)subcontract.GetResponse();
                    Stream replycontent = thereply.GetResponseStream();
                    StreamReader SRreplycontent = new StreamReader(replycontent);

                    zipfileOut.AddFileFromString(
                        guider.strName.Replace(':', '-').Replace('/', '-') + ".xls", "",
                                                 SRreplycontent.ReadToEnd());
                }
                zipfileOut.Save(Response.OutputStream);
                Response.End();
            }
        }

    
    
    }

      private string GenUrl(int IDsubprocess, ref exportinfo guider)
      {
	  // If we have -1 as the workspaceID, that means the *active* entset for this subpr is implied.
	  if (guider.idWS < 0)
	  {
	      AFWACsession fakesession = new AFWACsession(null);
	      fakesession.idSubprocess = IDsubprocess;
	      fakesession.idUser = -1;
	      fakesession.ObtainWorkspaceContext();
	      guider.idWS = fakesession.idActiveEAset;
	  }


          string rolelist = "";
          foreach (int x in guider.ARRidNode)
          {
              rolelist += "," + x.ToString();
          }
          if (rolelist.Length > 0)
          {
              rolelist = rolelist.Substring(1);  //Eliminate the leading comma
              rolelist = "&brol=" + rolelist;
          }

          string singlesheet = "&singlesheet=true";
          if (CheckBox1.Checked)
          {
              singlesheet = "&singlesheet=false";
          }

          string curSubcontractorURL =
            Request.Url.OriginalString.Substring(0, Request.Url.OriginalString.LastIndexOf('/')) +
            "/export/EntitlementsPerSubpr.ashx?id=" + guider.idWS + singlesheet + rolelist
            + (!guider.isWorkspace ? "&showstatus=false" : "") +
            (CHKshowOnlyNetEffect.Checked ? "&deltasonly=true" : "") +
            (CHKforFullDetailCSV.Checked ? "&FMTfulldetail=true" : "") +
            (CHKforIDMuploadCSV.Checked ? "&FMTidm3col=true" : "") +
            "&name=" + HttpUtility.HtmlEncode(guider.strName);


          return curSubcontractorURL;
      }







    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
    }          
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
    }

      protected void CHKforIDMuploadCSV_CheckedChanged(object sender, EventArgs e)
      {
          CHKforFullDetailCSV.Checked = false;
          CheckBox1.Checked = false;
      }

      protected void CHKforFullDetailCSV_CheckedChanged(object sender, EventArgs e)
      {
          CHKforIDMuploadCSV.Checked = false;
          CheckBox1.Checked = false;
      }

  }

}



/*
          string rolelist = "";
          foreach (int x in guider.ARRidNode)
            {
              rolelist += "," + x.ToString();
            }
          if (rolelist.Length > 0)
            {
              rolelist = rolelist.Substring(1);  //Eliminate the leading comma
              rolelist = "&brol=" + rolelist;
            }

          string singlesheet = "&singlesheet=true";
          if (CheckBox1.Checked)
            {
              singlesheet = "&singlesheet=false";
            }

          string curSubcontractorURL =
            Request.Url.OriginalString.Substring(0, Request.Url.OriginalString.LastIndexOf('/')) + 
            "/export/EntitlementsPerSubpr.ashx?id=" + guider.idWS + singlesheet + rolelist 
            + (!guider.isWorkspace ? "&showstatus=false" : "") +
            ( CHKshowOnlyNetEffect.Checked ? "&deltasonly=true" : "" );
*/
