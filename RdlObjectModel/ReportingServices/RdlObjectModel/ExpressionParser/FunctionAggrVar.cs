using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrVar : FunctionAggrStandard
  {
    public FunctionAggrVar(List<IInternalExpression> args)
      : base(args)
    {
    }

    internal override string DisplayText()
    {
      return "Var";
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Double;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (_Expr == null)
        return "";
      string empty = string.Empty;
      if (Scope == null)
        return "Var(" + _Expr.WriteSource(nameChanges) + ")";
      string asStringForWrite = GetScopeAsStringForWrite(nameChanges);
      return "Var(" + _Expr.WriteSource(nameChanges) + ", " + asStringForWrite + ")";
    }
  }
}
