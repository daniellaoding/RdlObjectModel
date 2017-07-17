using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionLogicOr : FunctionBinary
  {
    public override int PriorityCode => 12;

	  public FunctionLogicOr(IInternalExpression lhs, IInternalExpression rhs)
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
      return " Or ";
    }

    public override object Evaluate()
    {
      return Lhs.EvaluateBoolean() || (Rhs.EvaluateBoolean() ? true : false);
    }
  }
}
