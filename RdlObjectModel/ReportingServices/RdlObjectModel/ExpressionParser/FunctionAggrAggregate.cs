using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrAggregate : FunctionAggr
  {
    public FunctionAggrAggregate(List<IInternalExpression> args)
      : base(args)
    {
    }

    internal override string DisplayText()
    {
      return "Aggregate";
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (_Expr == null)
        return "";
      string empty = string.Empty;
      if (Scope == null)
        return "Aggregate(" + _Expr.WriteSource(nameChanges) + ")";
      string asStringForWrite = GetScopeAsStringForWrite(nameChanges);
      return "Aggregate(" + _Expr.WriteSource(nameChanges) + ", " + asStringForWrite + ")";
    }
  }
}
