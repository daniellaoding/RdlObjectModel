using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionRelationalEqual : FunctionBinary
  {
    public override int PriorityCode => 9;

	  public FunctionRelationalEqual(IInternalExpression lhs, IInternalExpression rhs)
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
      return " = ";
    }

    public override object Evaluate()
    {
      object obj1 = Lhs.Evaluate();
      object obj2 = Rhs.Evaluate();
      if (obj1 == null || obj2 == null)
        return false;
      if (obj1.GetType() != obj2.GetType())
      {
        try
        {
          obj1 = Convert.ToDouble(obj1, CultureInfo.CurrentCulture);
          obj2 = Convert.ToDouble(obj2, CultureInfo.CurrentCulture);
        }
        catch
        {
          return false;
        }
      }
      return obj1.Equals(obj2);
    }
  }
}
