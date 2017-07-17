using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionReportServerUrl : BaseInternalExpression
  {
    public override TypeCode TypeCode()
    {
      return System.TypeCode.String;
    }

    public override bool IsConstant()
    {
      return true;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "Globals!ReportServerUrl";
    }

    public override object Evaluate()
    {
      return "";
    }
  }
}
