using System;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionRelationalLike : FunctionBinary
  {
    public override int PriorityCode => 9;

	  public FunctionRelationalLike(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Boolean;
    }

    public override string BinaryOperator()
    {
      return " Like ";
    }

    public override object Evaluate()
    {
      return Regex.Match(Lhs.EvaluateString(), "^" + Rhs.EvaluateString().Replace("*", "(\\w){0,}").Replace("?", "(\\w){1}").Replace("[!", "[^").Replace("#", "[0-9]") + "\\z").Success;
    }
  }
}
