using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class Recursive : BaseInternalExpression
  {
	  public RecursiveOption Value { get; }

	  public Recursive(RecursiveOption v)
    {
      Value = v;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return Value != RecursiveOption.Simple ? "Recursive" : "Simple";
    }
  }
}
