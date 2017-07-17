using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlFieldsCollection : IComplexRdlCollection
  {
    private static readonly Dictionary<string, Type> m_properties = InitProperties();

    public string Name => "Fields";

	  public string ItemName => "Field";

	  public IInternalExpression CreateReference()
    {
      return new FunctionFieldsCollection();
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
    {
      return new FunctionField(itemNameExpr);
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
    {
      return new FunctionField(itemNameExpr, propertyNameExpr);
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
        },
        {
          "IsMissing",
          typeof (bool)
        },
        {
          "UniqueName",
          typeof (string)
        },
        {
          "BackgroundColor",
          typeof (string)
        },
        {
          "Color",
          typeof (string)
        },
        {
          "FontFamily",
          typeof (string)
        },
        {
          "FontSize",
          typeof (string)
        },
        {
          "FontWeight",
          typeof (string)
        },
        {
          "FontStyle",
          typeof (string)
        },
        {
          "TextDecoration",
          typeof (string)
        },
        {
          "FormattedValue",
          typeof (string)
        },
        {
          "Key",
          typeof (object)
        },
        {
          "LevelNumber",
          typeof (int)
        },
        {
          "ParentUniqueName",
          typeof (string)
        }
      };
    }
  }
}
