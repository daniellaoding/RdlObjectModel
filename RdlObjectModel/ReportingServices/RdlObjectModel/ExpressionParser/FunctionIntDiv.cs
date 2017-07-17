using System;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionIntDiv : FunctionBinary
  {
    public override int PriorityCode => 4;

	  public FunctionIntDiv()
    {
    }

    public FunctionIntDiv(IInternalExpression lhs, IInternalExpression rhs)
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
      return " \\ ";
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
      ValidateIntOperandTypes();
      if (Rhs is FunctionReportParameter || Rhs is FunctionField || (int) Rhs.EvaluateDouble() != 0)
        return;
      RDLExceptionHelper.WriteDivisionByZero(Lhs.WriteSource() + " \\ " + Rhs.WriteSource(), StartColumn, EndColumn);
    }

    public override object Evaluate()
    {
      if (Math.Abs(Convert.ToDouble(Rhs.EvaluateDouble())) < 1.0 / TwoToThePowerOf16)
        return "Infinity";
      return (int) (Lhs.EvaluateDouble() / Rhs.EvaluateDouble());
    }
  }
}
