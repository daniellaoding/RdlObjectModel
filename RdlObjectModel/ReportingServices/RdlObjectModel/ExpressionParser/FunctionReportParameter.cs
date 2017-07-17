using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  [Serializable]
  internal class FunctionReportParameter : BaseInternalExpression, IInternalNamedExpression
  {
	  private readonly IInternalExpression _NameExpr;
    private readonly string _Property;

    public string Name => _NameExpr.EvaluateString();

	  public string DisplayName => _NameExpr.EvaluateString();

	  public ReportParameter Parameter { get; }

	  public bool IsPropertyValue => string.Equals(_Property, "Value", StringComparison.CurrentCultureIgnoreCase);

	  public bool IsPropertyLabel => string.Equals(_Property, "Label", StringComparison.CurrentCultureIgnoreCase);

	  public bool IsPropertyCount => string.Equals(_Property, "Count", StringComparison.CurrentCultureIgnoreCase);

	  public bool IsPropertyIsMultiValue => string.Equals(_Property, "IsMultiValue", StringComparison.CurrentCultureIgnoreCase);

	  public FunctionReportParameter(ReportParameter reportParameter, string property)
    {
      Parameter = reportParameter;
      _Property = property;
      IsArray = reportParameter.MultiValue;
      if (!string.IsNullOrEmpty(_Property))
        return;
      _Property = "Value";
    }

    public FunctionReportParameter(IInternalExpression nameExpr)
    {
      _NameExpr = nameExpr;
    }

    public FunctionReportParameter(IInternalExpression nameExpr, string property)
    {
      _NameExpr = nameExpr;
      _Property = property;
    }

    public override TypeCode TypeCode()
    {
      return !IsPropertyValue ? (!IsPropertyLabel ? (!IsPropertyCount ? (!IsPropertyIsMultiValue ? GetValueTypeCode() : System.TypeCode.Boolean) : System.TypeCode.Int32) : System.TypeCode.String) : GetValueTypeCode();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      StringBuilder stringBuilder = new StringBuilder("Parameters!");
      stringBuilder.Append(nameChanges.GetNewName(NameChanges.EntryType.ReportParameter, (string) _NameExpr.Evaluate()));
      stringBuilder.Append(".");
      stringBuilder.Append(_Property ?? "Value");
      return stringBuilder.ToString();
    }

    protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
    {
      base.TraverseChildren(callback);
      if (_NameExpr == null)
        return;
      _NameExpr.Traverse(callback);
    }

    public override object Evaluate()
    {
      IList<object> asList = EvaluateAsList();
      int? nullable = !Parameter.MultiValue ? new int?(0) : new int?();
      if (!nullable.HasValue)
        return asList;
      if (nullable.Value < asList.Count)
        return asList[nullable.Value];
      return RDLUtil.DefaultDataType(Parameter.DataType);
    }

    private IList<object> EvaluateAsList()
    {
      return null;
    }

    private TypeCode GetValueTypeCode()
    {
      switch (Parameter.DataType)
      {
        case DataTypes.String:
          return System.TypeCode.String;
        case DataTypes.Boolean:
          return System.TypeCode.Boolean;
        case DataTypes.DateTime:
          return System.TypeCode.DateTime;
        case DataTypes.Integer:
          return System.TypeCode.Int32;
        case DataTypes.Float:
          return System.TypeCode.Double;
        default:
          return System.TypeCode.String;
      }
    }
  }
}
