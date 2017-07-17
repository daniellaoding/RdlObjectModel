using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionExp : FunctionBinary
  {
    public override int PriorityCode => 1;

	  public FunctionExp()
    {
    }

    public FunctionExp(IInternalExpression lhs, IInternalExpression rhs)
    {
      if (rhs is FunctionExp)
      {
        Lhs = new FunctionExp(lhs, ((FunctionBinary) rhs).Lhs);
        Rhs = ((FunctionBinary) rhs).Rhs;
      }
      else
      {
        Lhs = lhs;
        Rhs = rhs;
      }
    }

    public override string BinaryOperator()
    {
      return "^";
    }

    public override object Evaluate()
    {
      return Math.Pow(Lhs.EvaluateDouble(), Rhs.EvaluateDouble());
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
      ValidateIntOperandTypes();
    }
  }
}
