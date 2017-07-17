using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionMaxValue : FunctionAggr
  {
    public FunctionMaxValue(List<IInternalExpression> args)
      : base(args)
    {
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      StringBuilder stringBuilder = new StringBuilder("MaxValue(");
      for (int index = 0; index < m_args.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(",");
        stringBuilder.Append(m_args[index].WriteSource());
      }
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }
  }
}
