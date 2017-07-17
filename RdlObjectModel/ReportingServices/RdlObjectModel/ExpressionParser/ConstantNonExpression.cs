using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class ConstantNonExpression : Constant
  {
    public ConstantNonExpression(string value)
      : base(value)
    {
    }
  }
}
