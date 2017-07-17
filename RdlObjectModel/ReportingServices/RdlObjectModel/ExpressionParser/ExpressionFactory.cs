namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal static class ExpressionFactory
  {
    public static Expression CreateExpression(string expressionText, bool validate)
    {
      Expression expression = new Expression();
      if (validate)
        expression.Source = expressionText;
      else
        expression.SourceNoValidate = expressionText;
      return expression;
    }

    public static Expression CreateExpression(string expressionText)
    {
      return CreateExpression(expressionText, false);
    }
  }
}
