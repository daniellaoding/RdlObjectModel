namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class ExpressionToken
  {
    internal string _Value;
    internal int _Line;
    internal int _StartColumn;
    internal int _EndColumn;
    internal TokenTypes _TokenType;

    public int StartColumn => _StartColumn - 2;

	  public int EndColumn => _EndColumn - 2;

	  internal ExpressionToken(TokenTypes tokenType, string value, int line, int startCol, int endCol)
    {
      _Value = value;
      _Line = line;
      _StartColumn = startCol;
      _EndColumn = endCol;
      _TokenType = tokenType;
    }

    internal ExpressionToken(TokenTypes type, string value)
      : this(type, value, 0, 0, 0)
    {
    }

    internal ExpressionToken(TokenTypes type)
      : this(type, null, 0, 0, 0)
    {
    }

    public override string ToString()
    {
      return "<" + _TokenType + "> " + _Value;
    }
  }
}
