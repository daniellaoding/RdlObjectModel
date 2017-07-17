using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionLogicOrElse : FunctionBinary
  {
    public override int PriorityCode => 12;

	  public FunctionLogicOrElse(IInternalExpression lhs, IInternalExpression rhs)
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
      return " OrElse ";
    }

    public override object Evaluate()
    {
      try
      {
        bool boolean = Lhs.EvaluateBoolean();
        if (boolean)
          return boolean;
      }
      catch
      {
      }
      try
      {
        return Rhs.EvaluateBoolean();
      }
      catch
      {
        return "#Error";
      }
    }
  }
}
