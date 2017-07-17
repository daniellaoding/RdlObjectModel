using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlVariablesCollection : IComplexRdlCollection
  {
    private static readonly Dictionary<string, Type> m_properties = InitProperties();
    private static readonly Dictionary<string, Type> m_methods = InitMethods();

    public string Name => "Variables";

	  public string ItemName => "Variable";

	  public IInternalExpression CreateReference()
    {
      return new FunctionVariablesCollection();
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
    {
      return new FunctionVariable(itemNameExpr);
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
    {
      string constantValue = ((ConstantString) propertyNameExpr).ConstantValue;
      return new FunctionVariable(itemNameExpr, constantValue);
    }

    public bool IsPredefinedItemProperty(string name)
    {
      return m_properties.ContainsKey(name);
    }

    public bool IsPredefinedItemMethod(string name)
    {
      return m_methods.ContainsKey(name);
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

    private static Dictionary<string, Type> InitMethods()
    {
      return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer)
      {
        {
          "SetValue",
          typeof (object)
        }
      };
    }
  }
}
