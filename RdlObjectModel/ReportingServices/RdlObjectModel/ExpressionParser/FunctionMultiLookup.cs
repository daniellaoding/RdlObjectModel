using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionMultiLookup : FunctionAggr
  {
    public FunctionMultiLookup(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (m_args == null || m_args.Count != 4)
        return "";
      return "MultiLookup(" + m_args[0].WriteSource(nameChanges) + ", " + m_args[1].WriteSource(nameChanges) + ", " + m_args[2].WriteSource(nameChanges) + ", " + m_args[3].WriteSource(nameChanges) + ")";
    }
  }
}
