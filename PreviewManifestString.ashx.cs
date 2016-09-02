using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using RBSR_AUFW.DB.IMVFormula;
using RBSR_AUFW.DB.IEntitlement;
using System.Data.Odbc;

using Eval3;



namespace _6MAR_WebApplication
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler2 : IHttpHandler
    {

        returnGetEntitlement OBJwsent;


        public void ProcessRequest(HttpContext context)
        {
            OdbcConnection conn = HELPERS.NewOdbcConn();

            int IDwsentrow = int.Parse(context.Request.Params["IDwsent"]);


            context.Response.ContentType = "text/plain";


            IMVFormula ENGINEmanif = new IMVFormula(conn);
            IEntitlement ENGINEwsent = new IEntitlement(conn);

            OBJwsent =
                    ENGINEwsent.GetEntitlement(IDwsentrow);

            string appname = OBJwsent.Application;

            returnListMVFormula[] LISTformulas = 
            ENGINEmanif.ListMVFormula(null, "\"KEYapplication\" = ?",
                new string[] { appname }, "");
            
            // Currently, we only allow one formula per manifest.
            // Later we might have richer selection process where other fields
            // get to play a role in which formula is used.
            if (LISTformulas.Length > 1)
            {
                context.Response.Write("Error: more than one manifest formula for this app.");
                return;
            }


            if (LISTformulas.Length < 1)
            {
                context.Response.Write("Error: there is no manifest formula for this app.");
                return;
            }

            returnListMVFormula TheFormula = LISTformulas[0];

            if (TheFormula.Formula == null)
            {
                TheFormula.Formula = "";
            }

            string STRformula = TheFormula.Formula.Trim();
            if (STRformula == "") {
                context.Response.Write("Error: the manifest formula for this application has NOT been specified.");
                return;
            }
    
            // We have the formula; now we can evaluate.
            Evaluator ev = new Evaluator(Eval3.eParserSyntax.cSharp, false);
            ev.AddEnvironmentFunctions(this);
            ev.AddEnvironmentFunctions(new ManifestFormulaEvaluatorFunctions(OBJwsent));

            opCode lCode;
            try
            {
                lCode = ev.Parse(STRformula);
            }
            catch (Exception e)
            {
                context.Response.Write("The formula [[" + STRformula + "]] for this app [[" + appname + "]] has parse errors: " + e.ToString());
                return;
            }

            object RESLT;
            try
            {
                RESLT = lCode.value;
            }
            catch (Exception e)
            {
                context.Response.Write("Interpreting the formula for this app resulted in errors: " + e.ToString());
                return;
            }

            context.Response.Write(RESLT.ToString());

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
