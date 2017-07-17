using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal abstract class FunctionAggrStandard : FunctionAggr
  {
    public FunctionAggrStandard(List<IInternalExpression> args)
      : base(args)
    {
    }
  }
}
