using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionMult : FunctionBinary
  {
    public override int PriorityCode => 3;

	  public FunctionMult()
    {
      Lhs = null;
      Rhs = null;
    }

    public FunctionMult(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override string BinaryOperator()
    {
      return " * ";
    }

    public override object Evaluate()
    {
      object obj1 = Lhs.Evaluate();
      object obj2 = Rhs.Evaluate();
      int num1 = (int) Math.Pow(2.0, 16.0);
      if (obj1 is int && obj2 is int)
        return (int) obj1 * (int) obj2;
      double num2 = Lhs.EvaluateDouble() * Rhs.EvaluateDouble();
      if (Math.Abs(num2) < double.MaxValue / num1)
      {
        double num3 = Math.Abs((int) num2 * num1 - num2 * num1);
        if (num3 < 1.0 && num3 > 0.0)
          num2 = (int) num2 * num1 / num1;
        if (num2 == (int) num2)
          return (int) num2;
      }
      return num2;
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
      ValidateIntOperandTypes();
    }
  }
}
