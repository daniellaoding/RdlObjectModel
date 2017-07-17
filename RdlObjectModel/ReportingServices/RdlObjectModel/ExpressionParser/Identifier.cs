using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class Identifier : BaseInternalExpression
  {
	  public string Value { get; }

	  public Identifier(string value)
    {
      Value = value;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource()
    {
      return Value;
    }
  }
}
