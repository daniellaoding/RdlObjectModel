using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal abstract class BaseInternalExpression : IInternalExpression
  {
	  public virtual int PriorityCode => 0;

	  public virtual int StartColumn => 0;

	  public virtual int EndColumn => 0;

	  public bool IsArray { get; set; }

	  public bool Bracketed { get; set; }

	  public virtual TypeCode TypeCode()
	  {
		  return System.TypeCode.Empty;
	  }

    public virtual string WriteSource(NameChanges nameChanges)
    {
      return "";
    }

    public virtual string WriteSource()
    {
      return WriteSource(null);
    }

    public virtual bool IsConstant()
    {
      return false;
    }

    public virtual object Evaluate()
    {
      return null;
    }

    public string EvaluateString()
    {
      return Convert.ToString(Evaluate(), CultureInfo.CurrentCulture);
    }

    public string EvaluateString(bool useUserCulture)
    {
      return RDLUtil.ObjectToString(Evaluate(), useUserCulture);
    }

    public double EvaluateDouble()
    {
      return RDLUtil.ConvertToDouble(Evaluate());
    }

    public Decimal EvaluateDecimal()
    {
      return RDLUtil.ConvertToDecimal(Evaluate());
    }

    public DateTime EvaluateDateTime()
    {
      return RDLUtil.ConvertToDateTime(Evaluate());
    }

    public bool EvaluateBoolean()
    {
      return RDLUtil.ConvertToBoolean(Evaluate());
    }

    public override bool Equals(object obj)
    {
      if (this == obj)
        return true;
      if (obj is BaseInternalExpression && GetType() == obj.GetType())
        return WriteSource() == ((BaseInternalExpression) obj).WriteSource();
      return false;
    }

    public override int GetHashCode()
    {
      return WriteSource().GetHashCode();
    }

    public void Traverse(ProcessInternalExpressionHandler callback)
    {
      callback(this);
      TraverseChildren(callback);
    }

    protected virtual void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
    }

    public virtual void Validate(ExpressionValidationContext context)
    {
    }

    protected bool IsBoolIntOrDouble(IInternalExpression exp)
    {
      if (exp.TypeCode() == System.TypeCode.Boolean || RDLUtil.IsNumericType(exp.TypeCode()))
        return true;
      if (exp.TypeCode() != System.TypeCode.Object)
        return false;
      if (!(exp.Evaluate() is bool) && !(exp.Evaluate() is int) && (!(exp.Evaluate() is Decimal) && !(exp.Evaluate() is float)))
        return exp.Evaluate() is double;
      return true;
    }

    protected bool IsBoolOrInt(IInternalExpression exp)
    {
      if (exp.TypeCode() == System.TypeCode.Boolean || RDLUtil.IsIntegerType(exp.TypeCode()))
        return true;
      if (exp.TypeCode() != System.TypeCode.Object)
        return false;
      if (!(exp.Evaluate() is bool))
        return exp.Evaluate() is int;
      return true;
    }
  }
}
