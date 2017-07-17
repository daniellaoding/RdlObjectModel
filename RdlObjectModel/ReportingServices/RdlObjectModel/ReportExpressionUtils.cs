using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal class ReportExpressionUtils
  {
    internal static void GetDependencies(IList<ReportObject> dependencies, ReportObject parent, string Expression)
    {
      if (parent == null)
        throw new ArgumentNullException("parent");
      if (dependencies == null)
        throw new ArgumentNullException("dependencies");
      if (string.IsNullOrEmpty(Expression))
        return;
      ReportObject reportObject = null;
      ExpressionParser.Expression expression = ExpressionFactory.CreateExpression(Expression, true);
      if (expression == null || expression.ObjectDependencyList == null || expression.ObjectDependencyList.Count <= 0)
        return;
      foreach (IInternalExpression objectDependency in expression.ObjectDependencyList)
      {
        if (objectDependency is FunctionField)
        {
          if (((FunctionField) objectDependency).Fld != null)
            reportObject = ((FunctionField) objectDependency).Fld.GetAncestor<DataSet>();
          if (reportObject == null)
          {
            IList<DataSet> dataSets = parent.GetAncestor<Report>().DataSets;
            if (dataSets.Count == 1 && parent.GetAncestor<DataSet>() != dataSets[0])
              reportObject = dataSets[0];
          }
        }
        else if (!(objectDependency is FunctionTextbox))
        {
          if (objectDependency is FunctionReportParameter)
          {
            foreach (ReportParameter reportParameter in parent.GetAncestor<Report>().ReportParameters)
            {
              if (string.Equals(reportParameter.Name, ((FunctionReportParameter) objectDependency).Name, StringComparison.Ordinal))
              {
                reportObject = reportParameter;
                break;
              }
            }
          }
          else if (objectDependency is ConstantString)
          {
            foreach (DataSet dataSet in parent.GetAncestor<Report>().DataSets)
            {
              if (string.Equals(dataSet.Name, ((ConstantString) objectDependency).ConstantValue, StringComparison.Ordinal))
              {
                reportObject = dataSet;
                break;
              }
            }
          }
          else if (objectDependency is FunctionMethodOrProperty && ((FunctionMethodOrProperty) objectDependency).MethodName.ToUpperInvariant() == "CODE" && parent.GetAncestor<Report>().Code != null)
            reportObject = new CodeObject(parent.GetAncestor<Report>().Code);
        }
        if (reportObject != null && !dependencies.Contains(reportObject))
          dependencies.Add(reportObject);
      }
    }

    internal static string UpdateNamedReferences(string Expression, NameChanges RenameList)
    {
      ExpressionParser.Expression expression = ExpressionFactory.CreateExpression(Expression, true);
      if (expression == null || expression.InternalExpression == null || expression.ObjectDependencyList.Count == 0)
        return Expression;
      if (expression.InternalExpression is ConstantNonExpression)
        return expression.InternalExpression.WriteSource(RenameList);
      return "=" + expression.InternalExpression.WriteSource(RenameList);
    }
  }
}
