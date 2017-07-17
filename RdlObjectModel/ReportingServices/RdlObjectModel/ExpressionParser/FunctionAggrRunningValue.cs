using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionAggrRunningValue : FunctionAggr
  {
	  public string AggregateName { get; }

	  public FunctionAggrRunningValue(List<IInternalExpression> args)
      : base(args)
    {
      AggregateName = ((Identifier) args[1]).Value;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (_Expr == null)
        return "";
      string empty = string.Empty;
      if (Scope != null)
      {
        string asStringForWrite = GetScopeAsStringForWrite(nameChanges);
        return "RunningValue(" + _Expr.WriteSource(nameChanges) + ", " + AggregateName + ", " + asStringForWrite + ")";
      }
      return "RunningValue(" + _Expr.WriteSource(nameChanges) + ", " + AggregateName + ")";
    }
  }
}
