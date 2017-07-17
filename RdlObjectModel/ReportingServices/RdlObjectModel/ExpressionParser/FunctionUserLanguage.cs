using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionUserLanguage : BaseInternalExpression
  {
    public override TypeCode TypeCode()
    {
      return System.TypeCode.String;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "User!Language";
    }

    public override object Evaluate()
    {
      return CultureInfo.CurrentCulture.IetfLanguageTag;
    }
  }
}
