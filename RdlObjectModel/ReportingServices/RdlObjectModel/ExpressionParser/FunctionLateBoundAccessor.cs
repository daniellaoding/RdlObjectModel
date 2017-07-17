using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionLateBoundAccessor : BaseInternalExpression
  {
	  internal IInternalExpression CallTarget { get; }

	  internal string MethodName { get; }

	  internal List<IInternalExpression> Args { get; }

	  internal FunctionLateBoundAccessor(IInternalExpression callTarget, string methodName, List<IInternalExpression> args)
    {
      CallTarget = callTarget;
      MethodName = methodName;
      Args = args;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string str1 = CallTarget.WriteSource(nameChanges) + "." + MethodName;
      if (!CallTarget.Bracketed)
        return str1;
      string str2 = str1 + "(";
      if (Args != null && Args.Count != 0)
      {
        str2 += Args[0].WriteSource(nameChanges);
        for (int index = 1; index < Args.Count; ++index)
          str2 = str2 + ", " + Args[index].WriteSource(nameChanges);
      }
      return str2 + ")";
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
