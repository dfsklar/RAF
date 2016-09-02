<%@ Page ContentType="text/xml" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Text" %>

<script language='C#' runat="server">

    // URL query parameters are as follows:
    //     appscope=...   (if present, the value is an Application to scope on
    //     qcol=coltoobtain  (for example qcol=Platform)
    
    protected void Page_Load(object sender, EventArgs e)
    {
        _6MAR_WebApplication.GuidedEditor.SeloptionFactory.GENERATE(Response, this, 
		  		  this.Request.Params.Get("qcol"),
				  this.Request.Params.Get("appscope"),
				  this.Request.Params.Get("select")
				  );
    }
    
    
</script>

