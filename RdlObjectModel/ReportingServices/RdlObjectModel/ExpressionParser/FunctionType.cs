using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class FunctionType : BaseInternalExpression
  {
    private string m_typeName;
    private Type m_type;

	  internal TypeContext TypeContext { get; }

	  internal FunctionType(IReportLink container, string typeName)
    {
      m_typeName = typeName;
    }

    internal FunctionType(TypeContext typeContext)
    {
      TypeContext = typeContext;
      if (TypeContext == null)
        return;
      m_typeName = typeContext.Name;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.Object;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      if (string.IsNullOrEmpty(TypeContext.MethodFullName))
        return "<<UNKNOWN>>";
      return TypeContext.MethodFullName;
    }

    public override object Evaluate()
    {
      return m_type;
    }
  }
}
