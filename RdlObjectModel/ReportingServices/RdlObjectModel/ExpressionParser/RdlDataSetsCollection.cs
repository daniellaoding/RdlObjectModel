using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlDataSetsCollection : IComplexRdlCollection
  {
    private static readonly Dictionary<string, Type> m_properties = InitProperties();

    public string Name => "DataSets";

	  public string ItemName => "DataSet";

	  public IInternalExpression CreateReference()
    {
      return new FunctionDataSetsCollection();
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
    {
      return new FunctionDataSet(itemNameExpr);
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
    {
      string constantValue = ((ConstantString) propertyNameExpr).ConstantValue;
      return new FunctionDataSet(itemNameExpr, constantValue);
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
          "CommandText",
          typeof (string)
        },
        {
          "RewrittenCommandText",
          typeof (string)
        },
        {
          "ExecutionTime",
          typeof (DateTime)
        }
      };
    }
  }
}
