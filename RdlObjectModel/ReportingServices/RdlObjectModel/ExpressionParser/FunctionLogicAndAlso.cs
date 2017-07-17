using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionLogicAndAlso : FunctionBinary
  {
    public override int PriorityCode => 11;

	  public FunctionLogicAndAlso(IInternalExpression lhs, IInternalExpression rhs)
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
      return " AndAlso ";
    }

    public override object Evaluate()
    {
      try
      {
        if (!Lhs.EvaluateBoolean())
          return false;
      }
      catch
      {
        return "#Error";
      }
      try
      {
        return Lhs.EvaluateBoolean() && (Rhs.EvaluateBoolean() ? true : false);
      }
      catch
      {
        return "#Error";
      }
    }
  }
}
