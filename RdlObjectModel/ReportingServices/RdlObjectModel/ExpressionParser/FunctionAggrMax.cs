using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrMax : FunctionAggrStandard
  {
    public FunctionAggrMax(List<IInternalExpression> args)
      : base(args)
    {
    }

    internal override string DisplayText()
    {
      return "Max";
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (_Expr == null)
        return "";
      string empty = string.Empty;
      if (Scope == null)
        return "Max(" + _Expr.WriteSource(nameChanges) + ")";
      string asStringForWrite = GetScopeAsStringForWrite(nameChanges);
      return "Max(" + _Expr.WriteSource(nameChanges) + ", " + asStringForWrite + ")";
    }
  }
}
