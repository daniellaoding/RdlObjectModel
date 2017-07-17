using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrLevel : FunctionAggr
  {
    public FunctionAggrLevel(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Double;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string empty = string.Empty;
      if (Scope == null)
        return "Level(" + GetScopeAsStringForWrite(nameChanges) + ")";
      return "Level()";
    }
  }
}
