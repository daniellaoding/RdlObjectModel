using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionDefaultPropertyOrIndexer : BaseInternalExpression
  {
	  internal IInternalExpression CallTarget { get; }

	  internal List<IInternalExpression> Args { get; }

	  internal FunctionDefaultPropertyOrIndexer(IInternalExpression callTarget, List<IInternalExpression> args)
    {
      CallTarget = callTarget;
      Args = args;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (CallTarget == null)
        return "";
      string str = CallTarget.WriteSource(nameChanges) + "(";
      if (Args != null && Args.Count != 0)
      {
        str += Args[0].WriteSource(nameChanges);
        for (int index = 1; index < Args.Count; ++index)
          str = str + ", " + Args[index].WriteSource(nameChanges);
      }
      return str + ")";
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (CallTarget != null)
        CallTarget.Traverse(callback);
      if (Args == null)
        return;
      foreach (IInternalExpression internalExpression in Args)
        internalExpression.Traverse(callback);
    }
  }
}
