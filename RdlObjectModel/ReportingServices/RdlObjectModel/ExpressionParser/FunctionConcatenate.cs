using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionConcatenate : FunctionBinary
  {
    private readonly TypeCode _TypeCode;
    private readonly TokenTypes _TokenType;

    public override int PriorityCode => 7;

	  public FunctionConcatenate(IInternalExpression lhs, IInternalExpression rhs, TokenTypes tokenType)
    {
      Lhs = lhs;
      Rhs = rhs;
      _TokenType = tokenType;
      _TypeCode = System.TypeCode.String;
    }

    public override TypeCode TypeCode()
    {
      return _TypeCode;
    }

    public override string BinaryOperator()
    {
      return _TokenType == TokenTypes.CONCATENATE ? " & " : " + ";
    }

    public override object Evaluate()
    {
      return Lhs.EvaluateString() + Rhs.EvaluateString();
    }

    public override void Validate(ExpressionValidationContext context)
    {
      ArrayCheck();
    }
  }
}
