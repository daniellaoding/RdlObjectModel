using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionShiftLeft : FunctionBinary
  {
    public override int PriorityCode => 8;

	  public FunctionShiftLeft()
    {
    }

    public FunctionShiftLeft(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Int32;
    }

    public override string BinaryOperator()
    {
      return " << ";
    }

    public override object Evaluate()
    {
      return (int) Lhs.Evaluate() << (int) Rhs.Evaluate();
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
      ValidateIntOperandTypes();
    }
  }
}
