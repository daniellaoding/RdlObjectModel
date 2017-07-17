using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionReportFolder : BaseInternalExpression
  {
    [NonSerialized]
    private string _Folder;

    public string Folder
    {
      get
      {
        return _Folder;
      }
      set
      {
        _Folder = value;
      }
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.String;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "Globals!ReportFolder";
    }

    public override object Evaluate()
    {
      return "";
    }
  }
}
