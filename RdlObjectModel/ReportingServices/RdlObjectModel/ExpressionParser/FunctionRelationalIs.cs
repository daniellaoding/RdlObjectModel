using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionRelationalIs : FunctionBinary
  {
    public override int PriorityCode => 9;

	  public FunctionRelationalIs(IInternalExpression lhs, IInternalExpression rhs)
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
      return " Is ";
    }

    public override object Evaluate()
    {
      return Lhs.Evaluate().Equals(Rhs.Evaluate());
    }
  }
}
