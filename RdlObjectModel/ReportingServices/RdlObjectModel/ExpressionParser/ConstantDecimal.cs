using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantDecimal : Constant
  {
    public ConstantDecimal(string value)
      : base(Decimal.Parse(value, RDLUtil.GetFormatProvider(false)))
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Decimal;
    }
  }
}
