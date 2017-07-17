using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionReportName : BaseInternalExpression
  {
    internal string Name;

    public FunctionReportName()
    {
      Name = "";
    }

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
      return "Globals!ReportName";
    }

    public override object Evaluate()
    {
      return "";
    }
  }
}
