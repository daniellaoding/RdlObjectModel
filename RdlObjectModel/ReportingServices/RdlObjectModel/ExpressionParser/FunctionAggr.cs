using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal abstract class FunctionAggr : BaseInternalExpression
  {
    public IInternalExpression _Expr;
    public object _Scope;
    protected List<IInternalExpression> m_args;

    public IInternalExpression Expr => _Expr;

	  public object Scope
    {
      get
      {
        return _Scope;
      }
      set
      {
        _Scope = value;
      }
    }

    public FunctionAggr(List<IInternalExpression> args)
    {
      m_args = args;
      if (m_args.Count > 0)
        _Expr = m_args[0];
      if (m_args.Count > 1)
        _Scope = m_args[1];
      else
        _Scope = null;
    }

    public override TypeCode TypeCode()
    {
      return _Expr.TypeCode();
    }

    internal virtual string DisplayText()
    {
      return "Count";
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (m_args == null)
        return;
      foreach (IInternalExpression internalExpression in m_args)
        internalExpression.Traverse(callback);
    }

    internal string GetScopeAsString()
    {
      if (_Scope is ConstantString)
        return ((BaseInternalExpression) _Scope).EvaluateString();
      return null;
    }

    protected string GetScopeAsStringForWrite(NameChanges nameChanges)
    {
      string oldName = GetScopeAsString();
      if (oldName != null && oldName.ToUpperInvariant() != "NOTHING")
        oldName = "\"" + nameChanges.GetNewName(NameChanges.EntryType.Scope, oldName) + "\"";
      return oldName;
    }

    internal string GetScope()
    {
      return null;
    }

    public override object Evaluate()
    {
      switch (TypeCode())
      {
        case System.TypeCode.Boolean:
          return false;
        case System.TypeCode.Int32:
          return 1;
        case System.TypeCode.Double:
          return 1.0;
        case System.TypeCode.Decimal:
          return Convert.ToDecimal(1.0);
        case System.TypeCode.DateTime:
          return DateTime.Now;
        case System.TypeCode.String:
          return "1";
        default:
          return "#Error";
      }
    }
  }
}
