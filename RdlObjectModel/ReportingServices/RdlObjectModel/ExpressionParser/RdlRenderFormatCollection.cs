using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlRenderFormatCollection : ISimpleRdlCollection
  {
    private static readonly Dictionary<string, bool> m_propertiesIgnoreCase = InitProperties();

    public string Name => "RenderFormat";

	  public IInternalExpression CreateReference()
    {
      return new FunctionRenderFormat();
    }

    public IInternalExpression CreateCollectionReference(IInternalExpression itemNameExpr)
    {
      RDLExceptionHelper.WriteInvalidCollectionItem(Name, itemNameExpr.WriteSource());
      return null;
    }

    public IInternalExpression CreatePropertyReference(string propertyName)
    {
      if (!m_propertiesIgnoreCase.ContainsKey(propertyName))
        RDLExceptionHelper.WriteInvalidCollectionItem(Name, propertyName);
      return new FunctionRenderFormat(propertyName);
    }

    public bool IsPredefinedCollectionProperty(string name)
    {
      return m_propertiesIgnoreCase.ContainsKey(name);
    }

    public bool IsPredefinedChildCollection(string name, out ISimpleRdlCollection childCollection)
    {
      childCollection = null;
      return false;
    }

    private static Dictionary<string, bool> InitProperties()
    {
      return new Dictionary<string, bool>(StringUtil.CaseInsensitiveComparer)
      {
        {
          "Name",
          true
        },
        {
          "IsInteractive",
          true
        },
        {
          "DeviceInfo",
          true
        }
      };
    }
  }
}
