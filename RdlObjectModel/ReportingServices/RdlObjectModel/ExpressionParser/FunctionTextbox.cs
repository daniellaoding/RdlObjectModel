using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionTextbox : BaseInternalExpression
  {
    private IInternalExpression m_nameExpr;

	  internal Textbox Textbox { get; }

	  public FunctionTextbox(Textbox tb)
    {
      Textbox = tb;
    }

    public FunctionTextbox(IInternalExpression nameExpr)
    {
      m_nameExpr = nameExpr;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      m_nameExpr = new ConstantString(nameChanges.GetNewName(NameChanges.EntryType.ReportItem, (string) m_nameExpr.Evaluate()));
      return "ReportItems!" + (string) m_nameExpr.Evaluate() + ".Value";
    }

    public override object Evaluate()
    {
      return null;
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (m_nameExpr == null)
        return;
      m_nameExpr.Traverse(callback);
    }
  }
}
