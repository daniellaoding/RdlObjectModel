using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantShort : Constant
  {
    public ConstantShort(string value)
      : base(short.Parse(value, RDLUtil.GetFormatProvider(false)))
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int16;
    }
  }
}
