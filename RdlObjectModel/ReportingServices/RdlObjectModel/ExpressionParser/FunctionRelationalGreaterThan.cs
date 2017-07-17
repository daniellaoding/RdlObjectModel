using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionRelationalGreaterThan : FunctionBinary
  {
    public override int PriorityCode => 9;

	  public FunctionRelationalGreaterThan(IInternalExpression lhs, IInternalExpression rhs)
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
      return " > ";
    }

    public override object Evaluate()
    {
      return Lhs.EvaluateDouble() > Rhs.EvaluateDouble();
    }
  }
}
