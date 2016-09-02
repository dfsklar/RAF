using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Web.Script.Serialization;
using RBSR_AUFW.DB.IEventLog;
using System.Web.SessionState;


namespace _6MAR_WebApplication.GuidedEditor
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class RecordEntitlementMod : IHttpHandler, IReadOnlySessionState
  {





    public void ProcessRequest(HttpContext context)
    {
      AFWACsession session = context.Session["AFWACSESSION"] as AFWACsession;

      context.Response.ContentType = "text/plain";

      System.Web.Script.Serialization.JavaScriptSerializer UTIL =
        new System.Web.Script.Serialization.JavaScriptSerializer();

      string JSONidsOfSelRows = (context.Request.Params["wserowids"]);
      string JSONnewval = (context.Request.Params["newvector"]);
      string JSONoldval = (context.Request.Params["oldvector"]);

      Array ARRmultiselOldEditStat =
        UTIL.DeserializeObject
        (context.Request.Params["multiselOldEditStat"]) as Array;

      // will be either ADD or DEL
      string rolelinksIncrementalUpdateType = (context.Request.Params["mode"]);

      // edittype will be either EDIT or CLONE or NEWFRESH
      string edittype = (context.Request.Params["edittype"]);

      Object OBJarrofids = UTIL.DeserializeObject(JSONidsOfSelRows);
      Array arrofids = OBJarrofids as Array;

      // This will be used for any situation where there is only one row
      // being worked on (the most common situation).
      int IDwserow = -1;
      if (edittype != "NEWFRESH")
        {
          IDwserow = int.Parse(arrofids.GetValue(0) as string);
        }
      // arrofids.GetLength()   .GetValue(idx)   etc.


      /////////////////////////////////////
      //
      // MAKE CHANGES TO THE FIELDS OF THE VECTOR
      //

      if (JSONnewval != "--@--NOCHANGE--@--")
        {
          //
          // There was a change in the entitlements
          //

          // In this case, there is only one row that is being worked on.
          // ASSERT(arrofids.GetLength() == 1



          Object deserresult = UTIL.DeserializeObject(JSONnewval);
          System.Collections.Generic.Dictionary<string, object> THERESULT =
            deserresult as System.Collections.Generic.Dictionary<string, object>;


          // Check to ensure nothing else in workspace has very same vector

          bool changeWasOnlyCosmetic = false;

          string targetchecksum =
            HELPERS.ENTCHECKSUM
            (
             THERESULT["StandardActivity"] as string,
             THERESULT["RoleType"] as string,
             THERESULT["Application"] as string,
             THERESULT["System"] as string,
             THERESULT["Platform"] as string,
             THERESULT["EntitlementName"] as string,
             THERESULT["EntitlementValue"] as string,
             "",
             THERESULT["AuthObjValue"] as string,
             THERESULT["FieldSecName"] as string,
             THERESULT["FieldSecValue"] as string,
             THERESULT["Level4SecName"] as string,
             THERESULT["Level4SecValue"] as string);

            
          int existingID =
            HELPERS.FindEntitlementByChecksum(targetchecksum);


          // The retval is -1 if nothing found with that exact vector
          if (existingID >= 0)
            {
              // If we get here, an existing row was found with exact same checksum.
              // If it is the same row as this one being edited, then this
              // means that only non-significant fields were changed, e.g. commentary.

              if ((existingID == IDwserow) && (edittype == "EDIT"))
                {
                  changeWasOnlyCosmetic = true;
                }
              else
                {
                  // Whoops, something else has the same vector.
                  // LOGIC ERROR HERE: Assumes checksum for two different vectors will never be identical.
                  context.Response.Write("An entitlement with that exact set of characteristics already exists.");
                  context.Response.StatusCode = 500;
                  return;
                }
            }


          switch (edittype)
            {

            case "EDIT":
              Object deserresultOLD = UTIL.DeserializeObject(JSONoldval);
              System.Collections.Generic.Dictionary<string, object> THEOLDRESULT =
                deserresultOLD as System.Collections.Generic.Dictionary<string, object>;
                         
              HELPERS.EntitlementVectorUpdate
                (IDwserow, THERESULT, THEOLDRESULT, changeWasOnlyCosmetic,
                 session.idUser, context.Request.ServerVariables["REMOTE_ADDR"], UTIL
                 );
                                  
              break;

            case "CLONE":
            case "NEWFRESH":
              IDwserow = HELPERS.EntitlementCreate(THERESULT, targetchecksum);
              context.Response.Write(IDwserow.ToString());
              break;
            }
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
