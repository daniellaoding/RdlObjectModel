using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionLogicXor : FunctionBinary
  {
    public override int PriorityCode => 13;

	  public FunctionLogicXor(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Boolean;
    }

    public override string BinaryOperator()
    {
      return " Xor ";
    }

    public override object Evaluate()
    {
      object obj1 = Lhs.Evaluate();
      object obj2 = Rhs.Evaluate();
      if (obj1 is bool && obj2 is bool)
        return Convert.ToInt32(Lhs.EvaluateBoolean()) + Convert.ToInt32(Rhs.EvaluateBoolean()) == 1;
      if ((obj1 is int || obj1 is double) && (obj2 is int || obj2 is double))
        return Convert.ToInt32(Lhs.EvaluateDouble()) ^ Convert.ToInt32(Rhs.EvaluateDouble());
      if ((!(obj1 is int) && !(obj1 is double) || !(obj2 is bool)) && (!(obj1 is bool) || !(obj2 is int) && !(obj2 is double)))
        return "#Error";
      if (obj1 is bool)
      {
        if (Lhs.EvaluateBoolean())
          return (int) Rhs.EvaluateDouble();
        return 0;
      }
      if (Rhs.EvaluateBoolean())
        return (int) Lhs.EvaluateDouble();
      return 0;
    }
  }
}
