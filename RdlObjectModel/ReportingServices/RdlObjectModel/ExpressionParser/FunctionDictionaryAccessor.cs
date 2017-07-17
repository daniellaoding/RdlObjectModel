using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionDictionaryAccessor : BaseInternalExpression
  {
	  internal IInternalExpression CallTarget { get; }

	  internal string DictionaryArg { get; }

	  internal FunctionDictionaryAccessor(IInternalExpression callTarget, string dictionaryArg)
    {
      CallTarget = callTarget;
      DictionaryArg = dictionaryArg;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      throw new NotImplementedException();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (CallTarget == null)
        return;
      CallTarget.Traverse(callback);
    }
  }
}
