using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionUnaryMinus : FunctionUnary
  {
    public override int PriorityCode => 2;

	  public FunctionUnaryMinus()
    {
      Rhs = null;
    }

    public FunctionUnaryMinus(IInternalExpression r)
    {
      Rhs = r;
    }

    public override TypeCode TypeCode()
    {
      return Rhs.TypeCode();
    }

    public override string UnaryOperator()
    {
      return "- ";
    }

    public override object Evaluate()
    {
      return 0.0 - Rhs.EvaluateDouble();
    }
  }
}
