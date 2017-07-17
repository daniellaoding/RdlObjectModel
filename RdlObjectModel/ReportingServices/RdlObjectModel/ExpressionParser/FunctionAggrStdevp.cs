using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrStdevp : FunctionAggrStandard
  {
    public FunctionAggrStdevp(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Double;
    }

    internal override string DisplayText()
    {
      return "StDevP";
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (_Expr == null)
        return "";
      string empty = string.Empty;
      if (Scope == null)
        return "StDevP(" + _Expr.WriteSource(nameChanges) + ")";
      string asStringForWrite = GetScopeAsStringForWrite(nameChanges);
      return "StDevP(" + _Expr.WriteSource(nameChanges) + ", " + asStringForWrite + ")";
    }
  }
}
