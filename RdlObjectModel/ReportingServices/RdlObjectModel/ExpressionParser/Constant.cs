using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal abstract class Constant : BaseInternalExpression
  {
    protected object valueConst;

    protected Constant(object value)
    {
      valueConst = value;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.String;
    }

    public override bool IsConstant()
    {
      return true;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return Convert.ToString(Evaluate(), CultureInfo.InvariantCulture);
    }

    public override object Evaluate()
    {
      return valueConst;
    }
  }
}
