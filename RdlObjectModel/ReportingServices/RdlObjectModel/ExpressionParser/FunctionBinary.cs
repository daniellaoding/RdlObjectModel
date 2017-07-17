using System;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal abstract class FunctionBinary : BaseInternalExpression
  {
    protected static int TwoToThePowerOf16 = 65536;

	  public IInternalExpression Lhs { get; protected set; }

	  public IInternalExpression Rhs { get; protected set; }

	  public FunctionBinary()
    {
      Lhs = null;
      Rhs = null;
    }

    public FunctionBinary(IInternalExpression l, IInternalExpression r)
    {
      Lhs = l;
      Rhs = r;
    }

    public override bool IsConstant()
    {
      if (Lhs.IsConstant())
        return Rhs.IsConstant();
      return false;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string str1 = Lhs.WriteSource(nameChanges);
      string str2 = Rhs.WriteSource(nameChanges);
      if (!Bracketed)
        return str1 + BinaryOperator() + str2;
      return "(" + str1 + BinaryOperator() + str2 + ")";
    }

    public abstract string BinaryOperator();

    public override TypeCode TypeCode()
    {
      return GetGreatestRangeTypeCode();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      Lhs.Traverse(callback);
      Rhs.Traverse(callback);
    }

    protected TypeCode GetGreatestRangeTypeCode()
    {
      if (Lhs != null && Rhs != null)
      {
        TypeCode typeCode1 = Lhs.TypeCode();
        TypeCode typeCode2 = Rhs.TypeCode();
        if (RDLUtil.IsNumericType(typeCode1) && RDLUtil.IsNumericType(typeCode2))
        {
          if (typeCode1 <= typeCode2)
            return typeCode2;
          return typeCode1;
        }
      }
      return System.TypeCode.Double;
    }

    protected void ValidateIntOperandTypes()
    {
      double result;
      if (!IsBoolIntOrDouble(Lhs) && !double.TryParse(Lhs.EvaluateString(), out result))
        RDLExceptionHelper.WriteOperandTypesInvalid(Lhs.WriteSource(), BinaryOperator(), StartColumn, EndColumn);
      if (IsBoolIntOrDouble(Rhs) || double.TryParse(Rhs.EvaluateString(), out result))
        return;
      RDLExceptionHelper.WriteOperandTypesInvalid(Rhs.WriteSource(), BinaryOperator(), StartColumn, EndColumn);
    }

    protected void ArrayCheck()
    {
      if (Lhs.IsArray)
      {
        RDLExceptionHelper.WriteArrayOperand(BinaryOperator(), Lhs.WriteSource(), StartColumn, EndColumn);
      }
      else
      {
        if (!Rhs.IsArray)
          return;
        RDLExceptionHelper.WriteArrayOperand(BinaryOperator(), Rhs.WriteSource(), StartColumn, EndColumn);
      }
    }
  }
}
