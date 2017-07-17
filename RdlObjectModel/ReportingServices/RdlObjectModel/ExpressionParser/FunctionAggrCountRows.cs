using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrCountRows : FunctionAggrStandard
  {
    public FunctionAggrCountRows(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int32;
    }

    internal override string DisplayText()
    {
      return "CountRows";
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string empty = string.Empty;
      if (Scope != null)
        return "CountRows(" + GetScopeAsStringForWrite(nameChanges) + ")";
      return "CountRows()";
    }
  }
}
