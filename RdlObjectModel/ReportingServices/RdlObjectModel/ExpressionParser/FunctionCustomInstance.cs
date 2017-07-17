using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal sealed class FunctionCustomInstance : FunctionMultiArgument
  {
    private string _Cls;
    private string _Func;
    private readonly TypeCode _ReturnTypeCode;

    public string Cls
    {
      get
      {
        return _Cls;
      }
      set
      {
        _Cls = value;
      }
    }

    public string Func
    {
      get
      {
        return _Func;
      }
      set
      {
        _Func = value;
      }
    }

    public FunctionCustomInstance(Class rc, string f, IInternalExpression[] a, TypeCode type)
      : base(a)
    {
      _Cls = null;
      _Func = f;
      _ReturnTypeCode = type;
    }

    public override TypeCode TypeCode()
    {
      return _ReturnTypeCode;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return "Not Implemented";
    }
  }
}
