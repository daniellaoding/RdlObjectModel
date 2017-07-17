using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class ExpressionParserException : Exception
  {
	  public string Method { get; } = "";

	  public int StartPosition { get; }

	  public int EndPosition { get; }

	  public ExpressionParserException(string messageId)
      : base(messageId)
    {
    }

    public ExpressionParserException(string messageId, string method, int startPosition, int endPosition)
      : base(messageId)
    {
      Method = method;
      StartPosition = startPosition;
      EndPosition = endPosition;
    }

    public ExpressionParserException(string messageId, string method, string expectedvalue, int startPosition, int endPosition)
      : base(messageId)
    {
      Method = method;
      StartPosition = startPosition;
      EndPosition = endPosition;
    }

    public ExpressionParserException(string messageId, string method, int startPosition, int endPosition, params object[] args)
      : base(messageId)
    {
      Method = method;
      StartPosition = startPosition;
      EndPosition = endPosition;
    }

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    private ExpressionParserException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      Method = info.GetString("Method");
      StartPosition = info.GetInt32("StartPosition");
      EndPosition = info.GetInt32("EndPosition");
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("Method", Method);
      info.AddValue("StartPosition", StartPosition);
      info.AddValue("EndPosition", EndPosition);
    }
  }
}
