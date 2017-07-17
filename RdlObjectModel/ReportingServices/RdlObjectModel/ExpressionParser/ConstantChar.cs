using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantChar : Constant
  {
    public char ConstantValue => (char) valueConst;

	  public ConstantChar(string value)
      : base(char.Parse(value))
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Char;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "\"" + Evaluate() + "\"C";
    }
  }
}
