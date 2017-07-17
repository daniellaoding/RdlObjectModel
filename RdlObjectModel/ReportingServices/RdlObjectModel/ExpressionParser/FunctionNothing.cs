using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class FunctionNothing : BaseInternalExpression
  {
    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "Nothing";
    }

    public override object Evaluate()
    {
      return null;
    }
  }
}
