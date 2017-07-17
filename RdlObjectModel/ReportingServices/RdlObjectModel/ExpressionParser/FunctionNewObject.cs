using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionNewObject : BaseInternalExpression
  {
	  internal FunctionType TypeExpr { get; }

	  internal List<IInternalExpression> Args { get; }

	  internal FunctionNewObject(FunctionType typeExpr, List<IInternalExpression> args)
    {
      TypeExpr = typeExpr;
      Args = args;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      StringBuilder stringBuilder = new StringBuilder("New");
      stringBuilder.Append(TypeExpr.WriteSource(nameChanges));
      stringBuilder.Append("(");
      if (Args != null)
      {
        for (int index = 0; index < Args.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(Args[index].WriteSource(nameChanges));
        }
      }
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      TypeExpr.Traverse(callback);
      if (Args == null)
        return;
      foreach (IInternalExpression internalExpression in Args)
        internalExpression.Traverse(callback);
    }
  }
}
