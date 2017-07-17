using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionModulus : FunctionBinary
  {
    public override int PriorityCode => 5;

	  public FunctionModulus(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override TypeCode TypeCode()
    {
      return base.TypeCode();
    }

    public override string BinaryOperator()
    {
      return " Mod ";
    }

    public override object Evaluate()
    {
      return Lhs.EvaluateDouble() % Rhs.EvaluateDouble();
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
      ValidateIntOperandTypes();
    }
  }
}
