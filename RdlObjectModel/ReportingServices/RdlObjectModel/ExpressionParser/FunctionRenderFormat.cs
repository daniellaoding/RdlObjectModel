using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class FunctionRenderFormat : BaseInternalExpression
  {
    private readonly string m_propertyName;

    public FunctionRenderFormat()
    {
      m_propertyName = null;
    }

    public FunctionRenderFormat(string propertyName)
    {
      m_propertyName = propertyName;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      StringBuilder stringBuilder = new StringBuilder("Globals!RenderFormat");
      if (!string.IsNullOrEmpty(m_propertyName))
      {
        stringBuilder.Append(".");
        stringBuilder.Append(m_propertyName);
      }
      return stringBuilder.ToString();
    }
  }
}
