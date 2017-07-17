using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class FunctionDataSource : BaseInternalExpression
  {
    private readonly IInternalExpression m_nameExpr;
    private readonly string m_propertyName;

    public FunctionDataSource(IInternalExpression nameExpr)
    {
      m_nameExpr = nameExpr;
    }

    public FunctionDataSource(IInternalExpression nameExpr, string propertyName)
      : this(nameExpr)
    {
      m_propertyName = propertyName;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      StringBuilder stringBuilder = new StringBuilder("DataSources(");
      stringBuilder.Append(nameChanges.GetNewName(NameChanges.EntryType.DataSource, m_nameExpr.WriteSource()));
      stringBuilder.Append(")");
      if (!string.IsNullOrEmpty(m_propertyName))
      {
        stringBuilder.Append(".");
        stringBuilder.Append(m_propertyName);
      }
      return stringBuilder.ToString();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      base.TraverseChildren(callback);
      if (m_nameExpr == null)
        return;
      m_nameExpr.Traverse(callback);
    }
  }
}
