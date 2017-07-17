using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantString : Constant
  {
    public string ConstantValue => (string) valueConst;

	  public ConstantString(string value)
      : base(value)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.String;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "\"" + Evaluate() + "\"";
    }
  }
}
