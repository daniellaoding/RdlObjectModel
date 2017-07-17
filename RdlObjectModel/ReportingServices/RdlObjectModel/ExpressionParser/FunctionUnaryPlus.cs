using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionUnaryPlus : FunctionUnary
  {
    public override int PriorityCode => 2;

	  public FunctionUnaryPlus()
    {
      Rhs = null;
    }

    public FunctionUnaryPlus(IInternalExpression r)
    {
      Rhs = r;
    }

    public override TypeCode TypeCode()
    {
      return Rhs.TypeCode();
    }

    public override string UnaryOperator()
    {
      return "+ ";
    }

    public override object Evaluate()
    {
      return Rhs.Evaluate();
    }
  }
}
