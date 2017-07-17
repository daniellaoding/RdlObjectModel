using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlGlobalsCollection : ISimpleRdlCollection
  {
    private static readonly Dictionary<string, Type> m_properties = InitProperties(false);
    private static readonly Dictionary<string, Type> m_propertiesIgnoreCase = InitProperties(true);
    private static readonly ISimpleRdlCollection m_renderFormatCollection = new RdlRenderFormatCollection();

    public string Name => "Globals";

	  public IInternalExpression CreateReference()
    {
      return new FunctionGlobalsCollection();
    }

    public IInternalExpression CreateCollectionReference(IInternalExpression itemNameExpr)
    {
      if (!(itemNameExpr is ConstantString))
        return new FunctionGlobal(itemNameExpr);
      string constantValue = ((ConstantString) itemNameExpr).ConstantValue;
      Type type;
      if (!m_properties.TryGetValue(constantValue, out type))
        RDLExceptionHelper.WriteInvalidCollectionItem(Name, constantValue);
      return (IInternalExpression) Activator.CreateInstance(type);
    }

    public IInternalExpression CreatePropertyReference(string propertyName)
    {
      Type type;
      if (!m_propertiesIgnoreCase.TryGetValue(propertyName, out type))
        RDLExceptionHelper.WriteInvalidCollectionItem(Name, propertyName);
      return (IInternalExpression) Activator.CreateInstance(type);
    }

    public bool IsPredefinedCollectionProperty(string name)
    {
      return m_propertiesIgnoreCase.ContainsKey(name);
    }

    public bool IsPredefinedChildCollection(string name, out ISimpleRdlCollection childCollection)
    {
      if (string.Equals(name, "RenderFormat", StringComparison.OrdinalIgnoreCase))
      {
        childCollection = m_renderFormatCollection;
        return true;
      }
      childCollection = null;
      return false;
    }

    private static Dictionary<string, Type> InitProperties(bool ignoreCase)
    {
      return new Dictionary<string, Type>(!ignoreCase ? StringUtil.CaseSensitiveComparer : StringUtil.CaseInsensitiveComparer)
      {
        {
          "PageNumber",
          typeof (FunctionPageNumber)
        },
        {
          "TotalPages",
          typeof (FunctionTotalPages)
        },
        {
          "ExecutionTime",
          typeof (FunctionExecutionTime)
        },
        {
          "ReportServerUrl",
          typeof (FunctionReportServerUrl)
        },
        {
          "ReportFolder",
          typeof (FunctionReportFolder)
        },
        {
          "ReportName",
          typeof (FunctionReportName)
        },
        {
          "RenderFormat",
          typeof (FunctionRenderFormat)
        },
        {
          "PageName",
          typeof (FunctionPageName)
        },
        {
          "OverallPageNumber",
          typeof (FunctionOverallPageNumber)
        },
        {
          "OverallTotalPages",
          typeof (FunctionOverallTotalPages)
        }
      };
    }
  }
}
