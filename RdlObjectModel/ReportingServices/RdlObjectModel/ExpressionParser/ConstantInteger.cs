using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantInteger : Constant
  {
    public ConstantInteger(string value)
      : base(int.Parse(value, RDLUtil.GetFormatProvider(false)))
    {
    }

    public ConstantInteger(int value)
      : base(value)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int32;
    }
  }
}
