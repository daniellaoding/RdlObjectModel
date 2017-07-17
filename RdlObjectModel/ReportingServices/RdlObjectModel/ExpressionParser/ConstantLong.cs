using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantLong : Constant
  {
    public ConstantLong(string value)
      : base(long.Parse(value, RDLUtil.GetFormatProvider(false)))
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int64;
    }
  }
}
