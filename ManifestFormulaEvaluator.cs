using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using RBSR_AUFW.DB.IWorkspaceEntitlement;

using Eval3;
using RBSR_AUFW.DB.IEntitlement;

/* How to use this evaluator:
 * 
 * 			ev = new Evaluator(Eval3.eParserSyntax.cSharp,false);
 ev.AddEnvironmentFunctions(this);
 ev.AddEnvironmentFunctions(new ManifestFormulaEvaluatorFunctions());
 
 * Watch out for parsing exceptions when doing the parse:
 opCode lCode = ev.Parse(tbExpression.Text);

 * Also watch for exceptions when evaluating:
 object res = lCode.value;
 display this:  Evaluator.ConvertToString(res) 
 * 
 * 
 * 
 * 
 * 
 */

namespace _6MAR_WebApplication
{
  public class ManifestFormulaEvaluatorFunctions : iEvalFunctions, iVariableBag
  {

    public returnGetEntitlement WSENT;

    public ManifestFormulaEvaluatorFunctions
      (returnGetEntitlement _WSENT)
      {
	this.WSENT = _WSENT;
      }



    public int aNumber = 5;
				
    public string[] anArray 
    {
      get 
	{
	  return "TESTING 1 2 3".Split(' ');
	}
    }
		
    public string Description 
    {
      get 
	{
	  return "This module contains all the common functions";
	}
    }
		
    public EvalType EvalType 
    {
      get 
	{
	  return EvalType.Object;
	}
    }
		
    public string Name 
    {
      get 
	{
	  return "EvalFunctions";
	}
    }
		
    public System.Type systemType 
    {
      get 
	{
	  return this.GetType();
	}
    }
		
    public object Value 
    {
      get 
	{
	  return this;
	}
    }
				
    public double Sin(double v) 
    {
      return Math.Sin(v);
    }
		
    public double Cos(double v) 
    {
      return Math.Cos(v);
    }
		
    public DateTime Now() 
    {
      return System.DateTime.Now;
    }
			
    public string Trim(string str) 
    {
      return str.Trim();
    }
		
    public string LeftTrim(string str) 
    {
      return str.TrimStart();
    }
		
    public string RightTrim(string str) 
    {
      return str.TrimEnd();
    }
		
    public string PadLeft(string str, int wantedlen, string addedchar) 
    {
      while ((str.Length < wantedlen)) 
	{
	  str = (addedchar + str);
	  // Warning!!! Optional parameters not supported
	}
      return str;
    }
		
    public double Mod(double x, double y) 
    {
      return (x % y);
    }
		
    public object If(bool cond, object TrueValue, object FalseValue) 
    {
      if (cond) 
	{
	  return TrueValue;
	}
      else 
	{
	  return FalseValue;
	}
    }
				
    public string Lower(string value) 
    {
      return value.ToLower();
    }
		
    public string Upper(string value) 
    {
      return value.ToUpper();
    }
		
    public string WCase(string value) 
    {
      if ((value.Length == 0)) 
	{
	  return "";
	}
      return (value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower());
    }
		
    public DateTime Date(int year, int month, int day) 
    {
      return new DateTime(year, month, day);
    }
		
    public int Year(DateTime d) 
    {
      return d.Year;
    }
		
    public int Month(DateTime d) 
    {
      return d.Month;
    }
		
    public int Day(DateTime d) 
    {
      return d.Day;
    }
				
    string Replace(string Base, string search, string repl) 
    {
      return Base.Replace(search, repl);
    }
		
    public string Substr(string s, int from, int len) 
    {
      if ((s == null)) 
	{
	  return String.Empty;
	}
      // Warning!!! Optional parameters not supported
      from--;
      if ((from < 1)) 
	{
	  from = 0;
	}
      if ((from >= s.Length)) 
	{
	  from = s.Length;
	}
      if ((from +len) > s.Length) 
	{
	  len = (s.Length - from);
	}
      return s.Substring(from, len);
    }
		
    public int Len(string str) 
    {
      return str.Length;
    }
		
    public double Abs(double val) 
    {
      if ((val < 0)) 
	{
	  return (val * -1);
	}
      else 
	{
	  return val;
	}
    }
		
    public int Int(object value) 
    {
      return (int)(value);
    }
		
    public int Trunc(double value, int prec) 
    {
      value = (value - (0.5 / Math.Pow(10, prec)));
      // Warning!!! Optional parameters not supported
      return (int)(Math.Round(value, prec));
    }
		
    public double Dec(object value) 
    {
      return (double)(value);
    }
		
    public double Round(object value) 
    {
      return Math.Round((double)(value));
    }
				
    public string Chr(int c) 
    {
      return ""+(char)(c);
    }
		
    public string ChCR() 
    {
      return "\r";
    }
		
    public string ChLF() 
    {
      return "\n";
    }
		
    public string ChCRLF() 
    {
      return "\r\n";
    }
		
    public double Exp(double Base, double pexp) 
    {
      return Math.Pow(Base, pexp);
    }
		
    public string[] Split(string s, string delimiter) 
    {
      return s.Split(delimiter[0]);
      // Warning!!! Optional parameters not supported
    }
		
    System.DBNull DbNull() 
    {
      return System.DBNull.Value;
    }
		
	
		

		
    public double Sqrt(double v) 
    {
      return Math.Sqrt(v);
    }
		
    public double Power(double v, double e) 
    {
      return Math.Pow(v, e);
    }
		
    public iEvalFunctions InheritedFunctions() 
    {
      return null;
    }

    public System.Type SystemType 
    {
      get 
	{
	  return this.GetType();
	}
    }

    public iEvalTypedValue GetVariable(string varname ) 
    {
      return new Eval3.EvalVariable(varname,GetStringValue(varname),"",typeof(string));
    }

    public string GetStringValue(string varname)
    {
      switch (varname.ToLower())
	{
	case "entitlementname":
	  return WSENT.EntitlementName;
	case "entitlementvalue":
	  return WSENT.EntitlementValue;
	case "fieldsecname":
	  return WSENT.FieldSecName;
	case "fieldsecvalue":
	  return WSENT.FieldSecValue;
	case "level4secname":
	  return WSENT.Level4SecName;
	case "level4secvalue":
	  return WSENT.Level4SecValue;
	case "platform":
	  return WSENT.Platform;
	case "roletype":
	  return WSENT.RoleType;
	case "standardactivity":
	  return WSENT.StandardActivity;
	case "system":
	  return WSENT.System;
	case "authobjname":
	  return WSENT.AuthObjName;
	case "authobjvalue":
	  return WSENT.AuthObjValue;
	default:
	  return "ERROR(" + varname + ")";
	}					
    }
  }
}

