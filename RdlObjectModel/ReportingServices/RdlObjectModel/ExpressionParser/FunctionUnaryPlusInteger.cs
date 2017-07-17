using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionUnaryPlusInteger : FunctionUnary
  {
    public override int PriorityCode => 2;

	  public FunctionUnaryPlusInteger()
    {
      Rhs = null;
    }

    public FunctionUnaryPlusInteger(IInternalExpression r)
    {
      Rhs = r;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int32;
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
