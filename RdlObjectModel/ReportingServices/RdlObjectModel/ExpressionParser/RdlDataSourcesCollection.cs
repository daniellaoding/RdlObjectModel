using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlDataSourcesCollection : IComplexRdlCollection
  {
    private static readonly Dictionary<string, Type> m_properties = InitProperties();

    public string Name => "DataSources";

	  public string ItemName => "DataSource";

	  public IInternalExpression CreateReference()
    {
      return new FunctionDataSourcesCollection();
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
    {
      return new FunctionDataSource(itemNameExpr);
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
    {
      string constantValue = ((ConstantString) propertyNameExpr).ConstantValue;
      return new FunctionDataSource(itemNameExpr, constantValue);
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
          "Type",
          typeof (string)
        },
        {
          "DataSourceReference",
          typeof (string)
        }
      };
    }
  }
}
