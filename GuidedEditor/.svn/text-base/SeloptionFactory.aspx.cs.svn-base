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
using System.Xml;
using System.Text;

namespace _6MAR_WebApplication.GuidedEditor
{
    public partial class SeloptionFactory : System.Web.UI.Page
    {

        // The colname should be without the "c_u_" prefix.
        //    e.g. "Platform"

        static public void GENERATE(HttpResponse resp, Page thepage,
            string colname, string appscope, string selectedvalue)
        {
            SqlDataSource DS = null;

            if (appscope == null)
            {
                // NO FILTERING SO NO WHERE CLAUSE
                DS = new SqlDataSource(
                    ConfigurationManager.ConnectionStrings["afwac_sv6ConnectionString"].ConnectionString,
                    "SELECT DISTINCT c_u_" + colname + " FROM [t_RBSR_AUFW_u_Entitlement]");
            }
            else
            {
                DS = new SqlDataSource(
                    ConfigurationManager.ConnectionStrings["afwac_sv6ConnectionString"].ConnectionString,
                    "SELECT DISTINCT c_u_" + colname + " FROM [t_RBSR_AUFW_u_Entitlement] WHERE " +
                    "c_u_Application = '" + appscope + "'");
            }


            IEnumerable result = DS.Select(new DataSourceSelectArguments("c_u_" + colname));

            IEnumerator xenum = result.GetEnumerator();


            XmlTextWriter writer = new XmlTextWriter(resp.OutputStream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("complete");

            while (xenum.MoveNext())
            {
                string thestr = (xenum.Current as DataRowView).Row[0] as string;
                if (thestr != null)
                {
                    thestr = thestr.Trim();
                    if (thestr != "")
                    {
                        writer.WriteStartElement("option");
                        writer.WriteAttributeString("value", thestr);
                        if (thestr == selectedvalue)
                        {
                            writer.WriteAttributeString("selected", "1");
                        }
                        writer.WriteString(thestr);
                        writer.WriteEndElement();
                    }
                }
            }


            writer.WriteEndElement(); // Ends the "complete" element
            writer.WriteEndDocument();
            writer.Close();

        }

    }
}
