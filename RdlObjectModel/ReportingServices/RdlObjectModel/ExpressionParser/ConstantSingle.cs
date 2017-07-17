using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantSingle : Constant
  {
    public ConstantSingle(string value)
      : base(float.Parse(value, RDLUtil.GetFormatProvider(false)))
    {
    }

    public ConstantSingle(float value)
      : base(value)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Single;
    }
  }
}
