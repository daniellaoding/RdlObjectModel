using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantDouble : Constant
  {
    public ConstantDouble(string value)
      : base(double.Parse(value, RDLUtil.GetFormatProvider(false)))
    {
    }

    public ConstantDouble(double value)
      : base(value)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Double;
    }
  }
}
