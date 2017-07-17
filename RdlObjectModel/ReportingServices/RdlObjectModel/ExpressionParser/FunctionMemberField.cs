using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionMemberField : BaseInternalExpression
  {
	  internal IInternalExpression CallTarget { get; }

	  internal string MemberName { get; }

	  internal FunctionMemberField(IInternalExpression callTarget, string memberName)
    {
      CallTarget = callTarget;
      MemberName = memberName;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (CallTarget != null)
        return CallTarget.WriteSource(nameChanges) + "." + MemberName;
      return MemberName;
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (CallTarget == null)
        return;
      CallTarget.Traverse(callback);
    }
  }
}
