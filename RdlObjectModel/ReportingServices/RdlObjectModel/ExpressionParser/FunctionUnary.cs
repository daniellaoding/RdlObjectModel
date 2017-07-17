using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal abstract class FunctionUnary : BaseInternalExpression
  {
    private IInternalExpression _rhs;

    public IInternalExpression Rhs
    {
      get
      {
        return _rhs;
      }
      set
      {
        _rhs = value;
      }
    }

    public FunctionUnary()
    {
      _rhs = null;
    }

    public FunctionUnary(IInternalExpression r)
    {
      _rhs = r;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string str = Rhs.WriteSource(nameChanges);
      if (Rhs.PriorityCode > PriorityCode)
        str = "(" + str + ")";
      if (Bracketed)
        return "(" + UnaryOperator() + str + ")";
      return UnaryOperator() + str;
    }

    public abstract string UnaryOperator();

    public override bool IsConstant()
    {
      return _rhs.IsConstant();
    }
  }
}
