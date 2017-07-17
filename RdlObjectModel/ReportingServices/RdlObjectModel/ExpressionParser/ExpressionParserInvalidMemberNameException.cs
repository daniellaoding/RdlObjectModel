using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class ExpressionParserInvalidMemberNameException : ExpressionParserException
  {
	  public string MemberName { get; }

	  public ExpressionParserInvalidMemberNameException(string memberName, string messageId, string method, string expectedvalue, int startPosition, int endPosition)
      : base(messageId, method, expectedvalue, startPosition, endPosition)
    {
      MemberName = memberName;
    }
  }
}
