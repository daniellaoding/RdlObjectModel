using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlParametersCollection : IComplexRdlCollection
  {
    private static readonly Dictionary<string, Type> m_properties = InitProperties();

    public string Name => "Parameters";

	  public string ItemName => "Parameter";

	  public IInternalExpression CreateReference()
    {
      return new FunctionParametersCollection();
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
    {
      return new FunctionReportParameter(itemNameExpr);
    }

    public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
    {
      string constantValue = ((ConstantString) propertyNameExpr).ConstantValue;
      return new FunctionReportParameter(itemNameExpr, constantValue);
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
          "Label",
          typeof (object)
        },
        {
          "Count",
          typeof (int)
        },
        {
          "IsMultiValue",
          typeof (bool)
        }
      };
    }
  }
}
