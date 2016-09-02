using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;
using System.Configuration;
using RBSR_AUFW.DB.IEventLog;

namespace _6MAR_WebApplication.utilities
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler2 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            AFWACsession session = context.Session["AFWACSESSION"] as AFWACsession;

            string action = context.Request.Params["action"];
            string csvfolder = context.Request.Params["csvfolder"];
            string csvfilename = context.Request.Params["csvfilename"];
            int startat = int.Parse(context.Request.Params["startat"]);
            int count = int.Parse(context.Request.Params["count"]);
            string howToHandleNonRegTCodes = context.Request.Params["handlenonregtc"];   // Will be either WARN or ERR

          
            context.Response.Write("<p>Processing a set of " + count.ToString() + " records starting at record #" + startat.ToString() + "...\n<pre>\n");
            context.Response.Flush();

            Queue RETmsgs = new Queue();

            DataTable dt = HELPERS.LoadCsv(csvfolder, csvfilename);

            System.Data.Odbc.OdbcConnection conn =

  new System.Data.Odbc.OdbcConnection(
                                      ConfigurationManager.AppSettings["DBconnstr"]);

            conn.Open();

            IEventLog LOGGER = new IEventLog(conn);

            DateTime NOW = DateTime.Now;



            if (action == "initiate")
            {
                // This is the filename on the original client side, the one
                // that would be recognized by the end user who initiated the upload.
                string origfilename = context.Request.Params["origfilename"];
                int baby = LOGGER.NewEventLog(NOW, session.idUser, session.strIPaddr, "UpSAPEnts", "");
                LOGGER.SetEventLog(baby, NOW, session.idUser, session.strIPaddr, "UpSAPEnts", 0, "Start", "Name of file uploaded to RAF server: " + origfilename, "");
                context.Response.Write("<script>window.location.href='UploadSAPEntitlementsViaChain.ashx?handlenonregtc=" + howToHandleNonRegTCodes + "&action=cont&now=" + NOW.Ticks.ToString() + "&startat=0"
                    + "&count=" + count + "&csvfolder=" + HttpUtility.UrlEncode(csvfolder) + "&csvfilename=" + HttpUtility.UrlEncode(csvfilename) + "';</script>\n");
                context.Response.Flush();
                return;
            }
            else
            {
                NOW = new DateTime(long.Parse(context.Request.Params["now"]));
            }


            if (action == "summary")
            {
                context.Response.Write("\n</pre><H3>Upload has completed.</H3>\n");
                context.Response.Write("<P>Complete set of messages is shown below.  Please review carefully.<hr/><pre>\n");
                returnListEventLog[] ret = LOGGER.ListEventLog_ForSpecificTimestamp(NOW, null);
                foreach (returnListEventLog amsg in ret) 
                {
                    switch (amsg.Detail1) {
                        case "Start":
                            continue;
                        case "Completion":
                            continue;
                        case "Warning":
                            context.Response.Write("Warning: " + amsg.Detail2 + "\n");
                            break;
                        case "Error":
                            context.Response.Write("ERROR: " + amsg.Detail2 + "\n");
                            break;
                               
                    }

                }
                    context.Response.Write("\n</pre>");
                    context.Response.Write("<hr/>To return to the RAF screen, <a href='../HOME.aspx'>click here</a>.\n");
                return;
            }





            bool processingCompleted = SAP_HELPERS.ImportSAPAuthFrameworkFromCSV
  (dt, session.idUser, session.idSubprocess, conn, RETmsgs,
     session.idWorkspace_SAP, startat, count, howToHandleNonRegTCodes);

            if (RETmsgs.Count > 0)
            {
                foreach (object objMsg in RETmsgs.ToArray())
                {
                    context.Response.Write("\n" + objMsg.ToString());
                    int baby = LOGGER.NewEventLog(NOW, session.idUser, session.strIPaddr, "UpSAPEnts", "");
                    LOGGER.SetEventLog(baby, NOW, session.idUser, session.strIPaddr, "UpSAPEnts", 0, "Warning", objMsg.ToString(), "");
                }
            }

            if (processingCompleted)
            {
                int baby = LOGGER.NewEventLog(NOW, session.idUser, session.strIPaddr, "UpSAPEnts", "");
                LOGGER.SetEventLog(baby, NOW, session.idUser, session.strIPaddr, "UpSAPEnts", 0, "Completion", "", "");
                context.Response.Write("<script>window.location.href='UploadSAPEntitlementsViaChain.ashx?action=summary&now=" + context.Request.Params["now"] + "&startat="
                    + (startat.ToString()) + "&count=" + count + "&csvfolder=" + HttpUtility.UrlEncode(csvfolder) + "&csvfilename=" + HttpUtility.UrlEncode(csvfilename) + "';</script>\n");
            }
            else
            {
                context.Response.Write("\n</pre><hr/>This process will <u>automatically</u> proceed to the next set of records.  Any messages shown above were logged and the entire set of messages will be re-displayed when this upload has completed.  Thank you for your patience.  DO NOT CLOSE THIS WINDOW and DO NOT HIT F5 and DO NOT HIT 'BACK'.\n");
                startat += count;
                context.Response.Write("<script>window.location.href='UploadSAPEntitlementsViaChain.ashx?action=cont&handlenonregtc=" + howToHandleNonRegTCodes + "&now=" + context.Request.Params["now"] + "&startat="
                    + (startat.ToString()) + "&count=" + count + "&csvfolder=" + HttpUtility.UrlEncode(csvfolder) + "&csvfilename=" + HttpUtility.UrlEncode(csvfilename) + "';</script>\n");

            }
        }








        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
