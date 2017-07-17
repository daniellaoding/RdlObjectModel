using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class ExpressionParserInvalidNewTypeException : ExpressionParserException
  {
	  internal string TypeName { get; }

	  public ExpressionParserInvalidNewTypeException(string typeName, string messageId, string method, int startPosition, int endPosition)
      : base(messageId, method, startPosition, endPosition)
    {
      TypeName = typeName;
    }
  }
}
