using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionArrayInit : BaseInternalExpression
  {
	  internal List<IInternalExpression> Items { get; }

	  internal FunctionArrayInit(List<IInternalExpression> items)
    {
      Items = items;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string str = "{";
      if (Items != null)
      {
        foreach (IInternalExpression internalExpression in Items)
          str = str + internalExpression.WriteSource() + ",";
        str.Remove(str.Length - 1);
      }
      return str + "}";
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (Items == null)
        return;
      foreach (IInternalExpression internalExpression in Items)
        internalExpression.Traverse(callback);
    }
  }
}
