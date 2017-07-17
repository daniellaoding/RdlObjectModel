using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal interface IInternalExpression
  {
    bool IsArray { get; set; }

    bool Bracketed { get; set; }

    int PriorityCode { get; }

    TypeCode TypeCode();

    bool IsConstant();

    string WriteSource();

    string WriteSource(NameChanges nameChanges);

    object Evaluate();

    string EvaluateString();

    double EvaluateDouble();

    Decimal EvaluateDecimal();

    DateTime EvaluateDateTime();

    bool EvaluateBoolean();

    void Traverse(ProcessInternalExpressionHandler callback);

    void Validate(ExpressionValidationContext context);
  }
}
