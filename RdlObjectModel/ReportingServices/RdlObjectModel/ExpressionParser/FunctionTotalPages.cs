using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionTotalPages : BaseInternalExpression
  {
    private int _RuntimePageCount;

    public int RuntimePageCount
    {
      get
      {
        return _RuntimePageCount;
      }
      set
      {
        _RuntimePageCount = value;
      }
    }

    public FunctionTotalPages()
    {
      _RuntimePageCount = 1;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Double;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "Globals!TotalPages";
    }

    public override object Evaluate()
    {
      return 1;
    }
  }
}
