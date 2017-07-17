using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantBoolean : Constant
  {
    public ConstantBoolean(string value)
      : base(Convert.ToBoolean(value, CultureInfo.InvariantCulture))
    {
    }

    public ConstantBoolean(bool value)
      : base(value)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Boolean;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return !(bool) Evaluate() ? "False" : "True";
    }
  }
}
