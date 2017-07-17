using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionPlus : FunctionBinary
  {
    public override int PriorityCode => 6;

	  public FunctionPlus()
    {
    }

    public FunctionPlus(IInternalExpression lhs, IInternalExpression rhs)
    {
      Lhs = lhs;
      Rhs = rhs;
    }

    public override string BinaryOperator()
    {
      return " + ";
    }

    public override object Evaluate()
    {
      object obj1 = Lhs.Evaluate();
      object obj2 = Rhs.Evaluate();
      int num1 = 65536;
      if (obj1 is int && obj2 is int)
        return (int) obj1 + (int) obj2;
      double num2 = Lhs.EvaluateDouble() + Rhs.EvaluateDouble();
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
      if (AreTypesCompatibleForAdd(Lhs, Rhs))
        return;
      RDLExceptionHelper.WriteExpectedOperator("'&'", "'+'", StartColumn, EndColumn);
    }

    private bool AreTypesCompatibleForAdd(IInternalExpression lhs, IInternalExpression rhs)
    {
      TypeCode typeCode1 = lhs.TypeCode();
      TypeCode typeCode2 = rhs.TypeCode();
      if (lhs is FunctionNothing)
        return typeCode2 != System.TypeCode.DBNull;
      if (rhs is FunctionNothing)
        return typeCode1 != System.TypeCode.DBNull;
      DataTypes? dataType1 = RDLUtil.ConvertToDataType(typeCode1);
      DataTypes? dataType2 = RDLUtil.ConvertToDataType(typeCode2);
      if (dataType1.HasValue && (dataType1.Value == DataTypes.Integer || dataType1.Value == DataTypes.Float || dataType1.Value == DataTypes.Boolean) && (dataType2.HasValue && (dataType2.Value == DataTypes.Integer || dataType2.Value == DataTypes.Float || dataType2.Value == DataTypes.Boolean)) || typeCode1 == System.TypeCode.String && typeCode2 == System.TypeCode.DateTime || typeCode1 == System.TypeCode.DateTime && typeCode2 == System.TypeCode.String)
        return true;
      if (EqualityComparer<DataTypes?>.Default.Equals(dataType1, dataType2))
        return true;
      try
      {
        ValidateIntOperandTypes();
      }
      catch
      {
        return false;
      }
      return true;
    }
  }
}
