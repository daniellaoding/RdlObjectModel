using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionUnaryMinusInteger : FunctionUnary
  {
    public override int PriorityCode => 2;

	  public FunctionUnaryMinusInteger()
    {
      Rhs = null;
    }

    public FunctionUnaryMinusInteger(IInternalExpression r)
    {
      Rhs = r;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int32;
    }

    public override string UnaryOperator()
    {
      return "- ";
    }

    public override object Evaluate()
    {
      return Convert.ToInt32(0.0 - Rhs.EvaluateDouble());
    }
  }
}
