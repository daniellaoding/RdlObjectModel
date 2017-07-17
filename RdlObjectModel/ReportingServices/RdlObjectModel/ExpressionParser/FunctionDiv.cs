using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionDiv : FunctionBinary
  {
    public override int PriorityCode => 3;

	  public FunctionDiv()
    {
    }

    public FunctionDiv(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override string BinaryOperator()
    {
      return " / ";
    }

    public override object Evaluate()
    {
      if (Math.Abs(Rhs.EvaluateDouble()) < 1.0 / TwoToThePowerOf16)
        return "Infinity";
      double num1 = Lhs.EvaluateDouble() / Rhs.EvaluateDouble();
      if (Math.Abs(num1) < double.MaxValue / TwoToThePowerOf16)
      {
        double num2 = Math.Abs((int) num1 * TwoToThePowerOf16 - num1 * TwoToThePowerOf16);
        if (num2 < 1.0 && num2 > 0.0)
          num1 = (int) num1 * TwoToThePowerOf16 / TwoToThePowerOf16;
        if (num1 == (int) num1)
          return (int) num1;
      }
      return num1;
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
      ValidateIntOperandTypes();
    }
  }
}
