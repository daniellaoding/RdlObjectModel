using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionMinus : FunctionBinary
  {
    public override int PriorityCode => 6;

	  public FunctionMinus()
    {
      Lhs = null;
      Rhs = null;
    }

    public FunctionMinus(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override string BinaryOperator()
    {
      return " - ";
    }

    public override object Evaluate()
    {
      object obj1 = Lhs.Evaluate();
      object obj2 = Rhs.Evaluate();
      int num1 = 65536;
      if (obj1 is int && obj2 is int)
        return Convert.ToInt32(obj1, CultureInfo.CurrentCulture) - Convert.ToInt32(obj2, CultureInfo.CurrentCulture);
      if (obj1 is DateTime && obj2 is DateTime)
        return Convert.ToDateTime(obj1, CultureInfo.CurrentCulture) - Convert.ToDateTime(obj2, CultureInfo.CurrentCulture);
      double num2 = Lhs.EvaluateDouble() - Rhs.EvaluateDouble();
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
