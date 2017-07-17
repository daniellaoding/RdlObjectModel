using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionExecutionTime : BaseInternalExpression
  {
    [NonSerialized]
    private DateTime _StartReport;

    internal DateTime StartReport
    {
      get
      {
        return _StartReport;
      }
      set
      {
        _StartReport = value;
      }
    }

    public FunctionExecutionTime()
    {
      StartReport = DateTime.MinValue;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.DateTime;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "Globals!ExecutionTime";
    }

    public override object Evaluate()
    {
      return DateTime.Now;
    }
  }
}
