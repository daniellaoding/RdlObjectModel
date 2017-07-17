using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionArrayType : BaseInternalExpression
  {
	  internal IInternalExpression TypeExpr { get; }

	  internal int Rank { get; }

	  internal FunctionArrayType(IInternalExpression typeExpr, int rank)
    {
      TypeExpr = typeExpr;
      Rank = rank;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (TypeExpr == null)
        return "";
      return TypeExpr.WriteSource();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (TypeExpr == null)
        return;
      TypeExpr.Traverse(callback);
    }
  }
}
