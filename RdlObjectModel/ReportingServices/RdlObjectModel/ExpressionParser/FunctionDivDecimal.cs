using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionDivDecimal : FunctionBinary
  {
    public override int PriorityCode => 3;

	  public FunctionDivDecimal()
    {
      Lhs = null;
      Rhs = null;
    }

    public FunctionDivDecimal(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Decimal;
    }

    public override string BinaryOperator()
    {
      return " / ";
    }

    public override object Evaluate()
    {
      Decimal num1 = new Decimal(0);
      if (Math.Abs(Rhs.EvaluateDouble()) < 1.0 / TwoToThePowerOf16)
        return "Infinity";
      Decimal num2 = Lhs.EvaluateDecimal() / Rhs.EvaluateDecimal();
      if (Math.Abs(num2) < new Decimal(-1, -1, -1, false, 0) / TwoToThePowerOf16)
      {
        Decimal num3 = Math.Abs((int) num2 * TwoToThePowerOf16 - num2 * TwoToThePowerOf16);
        if (num3 < new Decimal(1) && num3 > new Decimal(0))
          num2 = (int) num2 * TwoToThePowerOf16 / TwoToThePowerOf16;
        if (num2 == (int) num2)
          return (int) num2;
      }
      return num2;
    }
  }
}
