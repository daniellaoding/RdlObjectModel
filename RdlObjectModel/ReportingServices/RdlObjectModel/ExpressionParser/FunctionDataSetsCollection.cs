using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class FunctionDataSetsCollection : BaseInternalExpression
  {
    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource()
    {
      return "DataSets";
    }
  }
}
