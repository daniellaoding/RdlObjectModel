using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantDateTime : Constant
  {
    public ConstantDateTime(string value)
      : base(Convert.ToDateTime(value, CultureInfo.InvariantCulture))
    {
    }

    public ConstantDateTime(DateTime value)
      : base(value)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.DateTime;
    }
  }
}
