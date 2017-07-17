using System;
using System.Security.Principal;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionUserID : BaseInternalExpression
  {
    public override TypeCode TypeCode()
    {
      return System.TypeCode.String;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "User!UserID";
    }

    public override object Evaluate()
    {
      return WindowsIdentity.GetCurrent().Name;
    }
  }
}
