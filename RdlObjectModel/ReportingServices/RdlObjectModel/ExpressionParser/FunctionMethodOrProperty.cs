using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionMethodOrProperty : BaseInternalExpression
  {
	  internal IInternalExpression CallTarget { get; }

	  internal string MethodName { get; }

	  internal List<IInternalExpression> Args { get; }

	  internal FunctionMethodOrProperty(IInternalExpression callTarget, string methodName, List<IInternalExpression> args)
    {
      CallTarget = callTarget;
      MethodName = methodName;
      Args = args;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (CallTarget != null)
      {
        bool flag = true;
        FunctionType callTarget = CallTarget as FunctionType;
        if (callTarget != null)
          flag = callTarget.TypeContext == null ? MethodName.ToUpperInvariant() != "CODE" : !callTarget.TypeContext.IsStandardModule();
        if (flag)
          stringBuilder.AppendFormat("{0}{1}", CallTarget.WriteSource(nameChanges), ".");
      }
      stringBuilder.Append(MethodName);
      if (Args != null && Args.Count != 0)
      {
        stringBuilder.Append("(");
        stringBuilder.Append(Args[0].WriteSource(nameChanges));
        for (int index = 1; index < Args.Count; ++index)
        {
          stringBuilder.Append(", ");
          stringBuilder.Append(Args[index].WriteSource(nameChanges));
        }
        stringBuilder.Append(")");
      }
      return stringBuilder.ToString();
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
