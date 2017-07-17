using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionShiftRight : FunctionBinary
  {
    public override int PriorityCode => 8;

	  public FunctionShiftRight()
    {
    }

    public FunctionShiftRight(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Double;
    }

    public override string BinaryOperator()
    {
      return " >> ";
    }

    public override object Evaluate()
    {
      return (int) Lhs.Evaluate() >> (int) Rhs.Evaluate();
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
      ValidateIntOperandTypes();
    }
  }
}
