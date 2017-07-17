using System.Collections;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class TokenList : IEnumerable
  {
    private readonly ArrayList tokens;

    internal int Count => tokens.Count;

	  internal TokenList()
    {
      tokens = new ArrayList();
    }

    internal void Add(ExpressionToken ExpressionToken)
    {
      tokens.Add(ExpressionToken);
    }

    internal void Push(ExpressionToken ExpressionToken)
    {
      tokens.Insert(0, ExpressionToken);
    }

    internal ExpressionToken Peek()
    {
      return (ExpressionToken) tokens[0];
    }

    internal ExpressionToken Extract()
    {
      ExpressionToken token = (ExpressionToken) tokens[0];
      tokens.RemoveAt(0);
      return token;
    }

    public IEnumerator GetEnumerator()
    {
      return tokens.GetEnumerator();
    }
  }
}
