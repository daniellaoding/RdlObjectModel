using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionPageName : BaseInternalExpression
  {
    internal string Name;

    public FunctionPageName()
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
      return "Globals!PageName";
    }

    public override object Evaluate()
    {
      return "";
    }
  }
}
