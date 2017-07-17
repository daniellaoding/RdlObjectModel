using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class FunctionFieldsCollection : BaseInternalExpression
  {
    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource()
    {
      return "Fields";
    }
  }
}
