namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal abstract class FunctionMultiArgument : BaseInternalExpression
  {
    private IInternalExpression[] _args;

    public IInternalExpression[] Arguments
    {
      get
      {
        return _args;
      }
      protected set
      {
        _args = value ?? new IInternalExpression[0];
      }
    }

    protected FunctionMultiArgument(IInternalExpression[] args)
    {
      Arguments = args;
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      if (_args == null)
        return;
      foreach (IInternalExpression internalExpression in _args)
        internalExpression.Traverse(callback);
    }
  }
}
