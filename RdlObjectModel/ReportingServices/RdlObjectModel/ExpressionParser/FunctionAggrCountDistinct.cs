using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrCountDistinct : FunctionAggrStandard
  {
    public FunctionAggrCountDistinct(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int32;
    }

    internal override string DisplayText()
    {
      return "CountDistinct";
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (_Expr == null)
        return "";
      string empty = string.Empty;
      if (Scope == null)
        return "CountDistinct(" + _Expr.WriteSource(nameChanges) + ")";
      string asStringForWrite = GetScopeAsStringForWrite(nameChanges);
      return "CountDistinct(" + _Expr.WriteSource(nameChanges) + ", " + asStringForWrite + ")";
    }
  }
}
