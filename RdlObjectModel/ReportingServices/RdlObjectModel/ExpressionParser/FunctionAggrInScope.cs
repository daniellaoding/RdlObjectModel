using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrInScope : FunctionAggr
  {
    public FunctionAggrInScope(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Boolean;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "InScope(" + GetScopeAsStringForWrite(nameChanges) + ")";
    }
  }
}
