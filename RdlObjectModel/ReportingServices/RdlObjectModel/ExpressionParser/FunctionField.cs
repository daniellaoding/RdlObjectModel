using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class FunctionField : BaseInternalExpression, IInternalNamedExpression
  {
    private Field _Field;
    private readonly string _Name;
    private readonly IInternalExpression _NameExpr;
    private readonly IInternalExpression _PropertyExpr;

	  public string Name
    {
      get
      {
        if (_Field != null)
          return _Field.Name;
        return _Name;
      }
    }

    public string DisplayName
    {
      get
      {
        if (_Field != null)
          return _Field.Name;
        return _Name;
      }
    }

    public virtual Field Fld
    {
      get
      {
        return _Field;
      }
      internal set
      {
        _Field = value;
      }
    }

    public string PropertyName { get; }

	  public FunctionField(Field field, string property)
    {
      _Field = field;
      PropertyName = string.IsNullOrEmpty(property) ? "Value" : property;
    }

    public FunctionField(string name, string property)
    {
      _Name = name;
      PropertyName = string.IsNullOrEmpty(property) ? "Value" : property;
    }

    public FunctionField(IInternalExpression name)
    {
      _NameExpr = name;
    }

    public FunctionField(IInternalExpression name, IInternalExpression property)
    {
      _NameExpr = name;
      _PropertyExpr = property;
    }

    public override TypeCode TypeCode()
    {
      return System.TypeCode.String;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string str1 = _NameExpr == null ? "!" + Name : (!(_NameExpr is ConstantString) ? "(" + _NameExpr.WriteSource(nameChanges) + ")" : "!" + _NameExpr.EvaluateString());
      string str2 = "Value";
      if (_PropertyExpr != null)
        str2 = !(_PropertyExpr is ConstantString) ? _PropertyExpr.WriteSource(nameChanges) : _PropertyExpr.EvaluateString();
      else if (PropertyName != null)
        str2 = PropertyName;
      return "Fields" + str1 + "." + str2;
    }

    public override object Evaluate()
    {
      return null;
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      base.TraverseChildren(callback);
      if (_NameExpr != null)
        _NameExpr.Traverse(callback);
      if (_PropertyExpr == null)
        return;
      _PropertyExpr.Traverse(callback);
    }
  }
}
