using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [DebuggerDisplay("Expression Source : {Source}")]
  [Serializable]
  internal class Expression
  {
    private List<IInternalExpression> _ObjectDependencyList = new List<IInternalExpression>();
    private IInternalExpression _Expression;
    private string _ExpressionSource;
    private bool _IsConstant;
    private ExpressionSubType _SubType;
    private string _Tag;

    public string Source
    {
      get
      {
        return _ExpressionSource;
      }
      set
      {
        if (_ExpressionSource == value)
          return;
        _ExpressionSource = value;
        Validate(true);
      }
    }

    public string SourceNoValidate
    {
      get
      {
        return _ExpressionSource;
      }
      set
      {
        if (_ExpressionSource == value)
          return;
        _ExpressionSource = value;
        InternalExpression = null;
      }
    }

    public ExpressionSubType SubType
    {
      get
      {
        return _SubType;
      }
      set
      {
        if (_SubType == value)
          return;
        _SubType = value;
      }
    }

    public IDictionary FieldScope => GetFieldScope();

	  public IInternalExpression InternalExpression
    {
      get
      {
        return _Expression;
      }
      private set
      {
        if (_Expression == value || _Expression != null && _Expression.Equals(value))
          return;
        _Expression = value;
      }
    }

    public List<IInternalExpression> ObjectDependencyList
    {
      get
      {
        return _ObjectDependencyList;
      }
      protected set
      {
        _ObjectDependencyList = value;
      }
    }

    public Dictionary<string, IIdentifiable> ReportItemsDict => null;

	  internal Expression()
    {
    }

    protected Expression(Report r, IReportLink p)
    {
    }

    internal Expression(Report r, IReportLink p, string tag, XmlNode xNode)
    {
      Source = xNode.InnerText;
      SetTag(tag);
    }

    public Expression(Report r, IReportLink p, string tag, string expression, ExpressionType expectedType)
    {
      Source = expression;
      SetTag(tag);
    }

    public Expression(Report r, IReportLink p, string tag, string expression)
      : this(r, p, tag, expression, ExpressionType.Variant)
    {
    }

    public void Validate()
    {
      Validate(true);
    }

    internal void Validate(bool suppressExceptions)
    {
      _ObjectDependencyList.Clear();
      if (_ExpressionSource == null)
        InternalExpression = null;
      else if (_ExpressionSource.Trim() == "" || IsConstantString())
      {
        InternalExpression = new ConstantNonExpression(_ExpressionSource);
        _IsConstant = true;
        if (_SubType == ExpressionSubType.Calculation)
          return;
        SubType = ExpressionSubType.Text;
      }
      else
      {
        ExpressionParser expressionParser = new ExpressionParser(EnvironmentContext.DefaultEnvironment);
        try
        {
          InternalExpression = expressionParser.Parse(_ExpressionSource);
          _ObjectDependencyList = expressionParser.ObjectDependencyList;
        }
        catch (ExpressionParserException ex)
        {
          InternalExpression = null;
          if (suppressExceptions)
          {
            if (!IsConstantString())
              SubType = ExpressionSubType.Calculation;
            else
              SubType = ExpressionSubType.Text;
          }
          else
            throw;
        }
      }
    }

    internal static void IterateExpressionTree(Action<IInternalExpression> visitExpression, IInternalExpression internExp)
    {
      visitExpression(internExp);
      if (internExp is FunctionUnary)
        IterateExpressionTree(visitExpression, ((FunctionUnary) internExp).Rhs);
      else if (internExp is FunctionBinary)
      {
        IterateExpressionTree(visitExpression, ((FunctionBinary) internExp).Lhs);
        IterateExpressionTree(visitExpression, ((FunctionBinary) internExp).Rhs);
      }
      else if (internExp is FunctionRelationalTypeOf)
      {
        IterateExpressionTree(visitExpression, ((FunctionRelationalTypeOf) internExp).Lhs);
        IterateExpressionTree(visitExpression, ((FunctionRelationalTypeOf) internExp).TypeNameExpr);
      }
      else if (internExp is FunctionMultiArgument)
      {
        foreach (IInternalExpression internExp1 in ((FunctionMultiArgument) internExp).Arguments)
          IterateExpressionTree(visitExpression, internExp1);
      }
      else if (internExp is FunctionMethodOrProperty)
      {
        foreach (IInternalExpression internExp1 in ((FunctionMethodOrProperty) internExp).Args)
          IterateExpressionTree(visitExpression, internExp1);
        IterateExpressionTree(visitExpression, ((FunctionMethodOrProperty) internExp).CallTarget);
      }
      else if (internExp is FunctionDefaultPropertyOrIndexer)
      {
        foreach (IInternalExpression internExp1 in ((FunctionDefaultPropertyOrIndexer) internExp).Args)
          IterateExpressionTree(visitExpression, internExp1);
        IterateExpressionTree(visitExpression, ((FunctionDefaultPropertyOrIndexer) internExp).CallTarget);
      }
      else if (internExp is FunctionLateBoundAccessor)
      {
        foreach (IInternalExpression internExp1 in ((FunctionLateBoundAccessor) internExp).Args)
          IterateExpressionTree(visitExpression, internExp1);
        IterateExpressionTree(visitExpression, ((FunctionLateBoundAccessor) internExp).CallTarget);
      }
      else if (internExp is FunctionNewArray)
      {
        IterateExpressionTree(visitExpression, ((FunctionNewArray) internExp).TypeExpr);
        IterateExpressionTree(visitExpression, ((FunctionNewArray) internExp).InitExpr);
      }
      else if (internExp is FunctionNewObject)
      {
        foreach (IInternalExpression internExp1 in ((FunctionNewObject) internExp).Args)
          IterateExpressionTree(visitExpression, internExp1);
        IterateExpressionTree(visitExpression, ((FunctionNewObject) internExp).TypeExpr);
      }
      else if (internExp is FunctionMemberField)
        IterateExpressionTree(visitExpression, ((FunctionMemberField) internExp).CallTarget);
      else if (internExp is FunctionDictionaryAccessor)
        IterateExpressionTree(visitExpression, ((FunctionDictionaryAccessor) internExp).CallTarget);
      else if (internExp is FunctionArrayInit)
      {
        foreach (IInternalExpression internExp1 in ((FunctionArrayInit) internExp).Items)
          IterateExpressionTree(visitExpression, internExp1);
      }
      else
      {
        if (!(internExp is FunctionArrayType))
          return;
        IterateExpressionTree(visitExpression, ((FunctionArrayType) internExp).TypeExpr);
      }
    }

    public IDictionary GetFieldScope()
    {
      return new Hashtable();
    }

    public string TypeCode()
    {
      if (_Expression != null)
        return "System." + _Expression.TypeCode().ToString();
      return "System.String";
    }

    public bool IsConstant()
    {
      return _IsConstant;
    }

    public bool IsConstantString()
    {
      return IsConstantString(Source);
    }

    public static bool IsConstantString(string expressionSource)
    {
      if (expressionSource != null)
        return !expressionSource.Trim().StartsWith("=", StringComparison.OrdinalIgnoreCase);
      return false;
    }

    public bool IsSourceEmpty()
    {
      return string.IsNullOrEmpty(Source);
    }

    public override string ToString()
    {
      return _ExpressionSource;
    }

    public string GetTag()
    {
      return _Tag;
    }

    protected void SetTag(string tag)
    {
      _Tag = tag;
    }
  }
}
