using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class ExpressionParserUnknownIdentifierException : ExpressionParserException
  {
	  internal string Name { get; }

	  public ExpressionParserUnknownIdentifierException(string name, string messageId, string method, int startPosition, int endPosition)
      : base(messageId, method, startPosition, endPosition)
    {
      Name = name;
    }
  }
}
