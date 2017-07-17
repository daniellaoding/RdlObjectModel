using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionRelationalTypeOf : BaseInternalExpression
  {
    private IInternalExpression m_lhs;
    private FunctionType m_typeNameExpr;

    public IInternalExpression Lhs
    {
      get
      {
        return m_lhs;
      }
      set
      {
        m_lhs = value;
      }
    }

    public FunctionType TypeNameExpr
    {
      get
      {
        return m_typeNameExpr;
      }
      set
      {
        m_typeNameExpr = value;
      }
    }

    public override int PriorityCode => 9;

	  public FunctionRelationalTypeOf(IInternalExpression lhs, FunctionType typeNameExpr)
    {
      m_lhs = lhs;
      m_typeNameExpr = typeNameExpr;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Boolean;
    }

    public override object Evaluate()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      StringBuilder stringBuilder = new StringBuilder("TypeOf ");
      stringBuilder.Append(m_lhs.WriteSource(nameChanges));
      stringBuilder.Append(" Is ");
      stringBuilder.Append(m_typeNameExpr.WriteSource(nameChanges));
      return stringBuilder.ToString();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      base.TraverseChildren(callback);
      m_lhs.Traverse(callback);
      m_typeNameExpr.Traverse(callback);
    }
  }
}
