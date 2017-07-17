using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrRowNumber : FunctionAggr
  {
    public FunctionAggrRowNumber(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int32;
    }

    internal override string DisplayText()
    {
      return "RowNumber";
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string empty = string.Empty;
      if (Scope != null)
        return "RowNumber(" + GetScopeAsStringForWrite(nameChanges) + ")";
      return "RowNumber(Nothing)";
    }
  }
}
