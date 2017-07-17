using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlReportItemsCollection : IComplexRdlCollection
  {
    private static readonly Dictionary<string, Type> m_properties = InitProperties();

    public string Name => "ReportItems";

	  public string ItemName => "ReportItem";

	  public IInternalExpression CreateReference()
    {
      return new FunctionReportItemsCollection();
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
    {
      return new FunctionTextbox(itemNameExpr);
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
    {
      return new FunctionTextbox(itemNameExpr);
    }

    public bool IsPredefinedItemProperty(string name)
    {
      return m_properties.ContainsKey(name);
    }

    public bool IsPredefinedItemMethod(string name)
    {
      return false;
    }

    public bool IsPredefinedCollectionProperty(string name)
    {
      return false;
    }

    private static Dictionary<string, Type> InitProperties()
    {
      return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer)
      {
        {
          "Value",
          typeof (object)
        }
      };
    }
  }
}
