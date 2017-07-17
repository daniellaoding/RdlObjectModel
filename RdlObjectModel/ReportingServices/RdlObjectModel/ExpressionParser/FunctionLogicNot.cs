using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionLogicNot : FunctionUnary
  {
    public override int PriorityCode => 10;

	  public FunctionLogicNot(IInternalExpression rhs)
    {
      Rhs = rhs;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Boolean;
    }

    public override string UnaryOperator()
    {
      return "Not ";
    }

    public override object Evaluate()
    {
      return !Rhs.EvaluateBoolean();
    }
  }
}
