using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionNewArray : BaseInternalExpression
  {
	  internal FunctionArrayType TypeExpr { get; }

	  internal FunctionArrayInit InitExpr { get; }

	  internal FunctionNewArray(FunctionArrayType typeExpr, FunctionArrayInit initExpr)
    {
      TypeExpr = typeExpr;
      InitExpr = initExpr;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "New " + TypeExpr.WriteSource() + "()" + InitExpr.WriteSource();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      TypeExpr.Traverse(callback);
      InitExpr.Traverse(callback);
    }
  }
}
