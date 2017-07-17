using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionPageNumber : BaseInternalExpression
  {
    public override TypeCode TypeCode()
    {
      return System.TypeCode.Double;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "Globals!PageNumber";
    }

    public override object Evaluate()
    {
      return 1;
    }
  }
}
