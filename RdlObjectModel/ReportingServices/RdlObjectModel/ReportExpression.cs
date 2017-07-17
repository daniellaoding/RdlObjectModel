using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public struct ReportExpression : IExpression, IXmlSerializable, IFormattable
  {
    private static readonly Regex m_nonConstantRegex = new Regex("^\\s*=", RegexOptions.Compiled);
    private string m_value;

	  public string Value
    {
      get
      {
        return m_value ?? "";
      }
      set
      {
        m_value = value;
      }
    }

    public DataTypes DataType { get; set; }

	  object IExpression.Value
    {
      get
      {
        return Value;
      }
      set
      {
        Value = (string) value;
      }
    }

    public string Expression
    {
      get
      {
        return Value;
      }
      set
      {
        Value = value;
      }
    }

    public EvaluationMode EvaluationMode { get; set; }

	  public bool IsExpression
    {
      get
      {
        if (EvaluationMode == EvaluationMode.Auto)
          return IsExpressionString(m_value);
        return false;
      }
    }

    public static ReportExpression Empty => new ReportExpression();

	  public bool IsEmpty => string.IsNullOrEmpty(ToString());

	  public ReportExpression(string value)
    {
      m_value = value;
      DataType = DataTypes.String;
      EvaluationMode = EvaluationMode.Auto;
    }

    public ReportExpression(string value, EvaluationMode evaluationMode)
    {
      m_value = value;
      DataType = DataTypes.String;
      EvaluationMode = evaluationMode;
    }

    public ReportExpression(double value)
    {
      this = new ReportExpression(value.ToString(CultureInfo.InvariantCulture));
      DataType = DataTypes.Float;
    }

    public ReportExpression(bool value)
    {
      this = new ReportExpression(XmlConvert.ToString(value));
      DataType = DataTypes.Boolean;
    }

    public ReportExpression(DateTime value)
    {
      this = new ReportExpression(value.ToString(CultureInfo.InvariantCulture));
      DataType = DataTypes.DateTime;
    }

    public ReportExpression(int value)
    {
      this = new ReportExpression(value.ToString(CultureInfo.InvariantCulture));
      DataType = DataTypes.Integer;
    }

    public static explicit operator string(ReportExpression value)
    {
      return value.Value;
    }

    public static implicit operator ReportExpression(string value)
    {
      return new ReportExpression(value);
    }

    public static bool operator ==(ReportExpression left, ReportExpression right)
    {
      return left.Equals(right);
    }

    public static bool operator ==(ReportExpression left, string right)
    {
      return left.Equals(right);
    }

    public static bool operator ==(string left, ReportExpression right)
    {
      return right.Equals(left);
    }

    public static bool operator !=(ReportExpression left, ReportExpression right)
    {
      return !left.Equals(right);
    }

    public static bool operator !=(ReportExpression left, string right)
    {
      return !left.Equals(right);
    }

    public static bool operator !=(string left, ReportExpression right)
    {
      return !right.Equals(left);
    }

    public override string ToString()
    {
      return Value;
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return Value;
    }

    public void GetDependencies(IList<ReportObject> dependencies, ReportObject parent)
    {
      ReportExpressionUtils.GetDependencies(dependencies, parent, Expression);
    }

    internal ReportExpression UpdateNamedReferences(NameChanges RenameList)
    {
      Expression = ReportExpressionUtils.UpdateNamedReferences(Expression, RenameList);
      return this;
    }

    public static bool IsExpressionString(string value)
    {
      if (value != null)
        return m_nonConstantRegex.IsMatch(value);
      return false;
    }

    public override bool Equals(object value)
    {
      if (value is ReportExpression)
      {
        ReportExpression reportExpression = (ReportExpression) value;
        if (Value == reportExpression.Value && IsExpression == reportExpression.IsExpression)
          return DataType == reportExpression.DataType;
        return false;
      }
      if (value is string)
        return Equals(new ReportExpression((string) value ?? ""));
      if (value == null)
        return Value == "";
      return false;
    }

    public override int GetHashCode()
    {
      return Value.GetHashCode();
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      string attribute1 = reader.GetAttribute("DataType");
      if (attribute1 != null)
        DataType = (DataTypes) ParseEnum(typeof (DataTypes), attribute1);
      string attribute2 = reader.GetAttribute("EvaluationMode");
      if (attribute2 != null)
        EvaluationMode = (EvaluationMode) ParseEnum(typeof (EvaluationMode), attribute2);
      m_value = reader.ReadString();
      reader.Skip();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      if (DataType != DataTypes.String)
        writer.WriteAttributeString("DataType", DataType.ToString());
      if (EvaluationMode != EvaluationMode.Auto)
        writer.WriteAttributeString("EvaluationMode", EvaluationMode.ToString());
      if (Value.Length <= 0)
        return;
      if (Value.Trim().Length == 0)
        writer.WriteAttributeString("xml", "space", null, "preserve");
      writer.WriteString(Value);
    }

    internal static object ParseEnum(Type type, string value)
    {
      int index = Array.IndexOf(Enum.GetNames(type), value);
      if (index < 0)
      {
        throw new ArgumentException(string.Format(SRErrors.InvalidValue, value));
      }
      return Enum.GetValues(type).GetValue(index);
    }

    public static string BuildFieldReference(string fieldName)
    {
      return BuildFieldReference(fieldName, null);
    }

    public static string BuildFieldReference(string fieldName, string property)
    {
      return "Fields!" + fieldName + "." + (property ?? "Value");
    }

    public static string BuildGlobalReference(string property)
    {
      return "Globals!" + property;
    }

    public static string BuildUserReference(string property)
    {
      return "User!" + property;
    }

    public static string BuildNullValue()
    {
      return "Nothing";
    }

    public static string BuildParameterReference(string paramName)
    {
      return BuildParameterReference(paramName, null);
    }

    public static string BuildParameterReference(string paramName, string property)
    {
      return "Parameters!" + paramName + "." + (property ?? "Value");
    }

    public static string BuildFunctionCall(string funcName, params string[] args)
    {
      if (args == null)
        return funcName + "()";
      return funcName + "(" + string.Join(", ", args) + ")";
    }

    internal static bool TryBuildArray(IList<ReportExpression?> subexpressions, out ReportExpression? expression)
    {
      if (subexpressions == null)
        throw new ArgumentNullException("subexpressions");
      if (subexpressions.Count == 1)
      {
        expression = subexpressions[0];
        return true;
      }
      if (subexpressions.Count > 1)
      {
        StringBuilder stringBuilder = new StringBuilder("=new Object() {");
        bool flag = true;
        foreach (ReportExpression? subexpression in subexpressions)
        {
          if (subexpression.HasValue)
          {
            if (!flag)
              stringBuilder.Append(", ");
            string str = subexpression.Value.Value;
            if (subexpression.Value.IsExpression)
            {
              str = str.TrimStart();
              if (str.StartsWith("="))
                str.Remove(0, 1);
            }
            if (subexpression.Value.DataType == DataTypes.String && !str.StartsWith("\""))
              str = string.Format("\"{0}\"", str);
            stringBuilder.Append(str);
            flag = false;
          }
          else
            break;
        }
        stringBuilder.Append("}");
        expression = new ReportExpression?(new ReportExpression(stringBuilder.ToString()));
        return true;
      }
      expression = new ReportExpression?();
      return false;
    }

    internal static bool TryParseArray(ReportExpression expression, out IList<ReportExpression> subexpressions)
    {
      ExpressionParser.Expression expression1 = ExpressionFactory.CreateExpression(expression.Value, true);
      if (expression1 != null)
      {
        FunctionNewArray internalExpression1 = expression1.InternalExpression as FunctionNewArray;
        if (internalExpression1 != null)
        {
          FunctionArrayInit initExpr = internalExpression1.InitExpr;
          if (initExpr != null)
          {
            subexpressions = new List<ReportExpression>(initExpr.Items.Count);
            foreach (IInternalExpression internalExpression2 in initExpr.Items)
            {
              string str = internalExpression2.WriteSource();
              if (!internalExpression2.IsConstant())
                str = "=" + str;
              subexpressions.Add(str);
            }
            return true;
          }
        }
      }
      subexpressions = null;
      return false;
    }

    public static string BuildStringLiteral(string literal)
    {
      if (literal == null)
        throw new ArgumentNullException("literal");
      StringBuilder stringBuilder = new StringBuilder(literal);
      stringBuilder.Replace("\"", "\"\"");
      stringBuilder.Replace(Environment.NewLine, "\" & Environment.NewLine & \"");
      stringBuilder.Replace(":", "\" & Chr(58) & \"");
      stringBuilder.Insert(0, '"');
      stringBuilder.Append('"');
      return stringBuilder.ToString();
    }

    public static bool IsAggregateExpression(string expression)
    {
      bool flag = false;
      foreach (string aggregateFunction in Functions.AllAggregateFunctions)
      {
        if (expression.StartsWith("=" + aggregateFunction + "(", StringComparison.OrdinalIgnoreCase))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public static string GetFieldReference(string expression)
    {
      Match match = Regex.Match(expression, "^=Fields!(\\w+)\\.Value$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
      if (!match.Success)
        return null;
      return match.Groups[1].Value;
    }

    public static class Collections
    {
      public const string DataSets = "DataSets";
      public const string DataSources = "DataSources";
      public const string Fields = "Fields";
      public const string Globals = "Globals";
      public const string Parameters = "Parameters";
      public const string ReportItems = "ReportItems";
      public const string User = "User";
      public const string Variables = "Variables";
    }

    public static class FieldProperties
    {
      public static readonly ReadOnlyCollection<string> All = new ReadOnlyCollection<string>(new string[1]
      {
	      "Value"
      });
      public const string Value = "Value";
    }

    public static class GlobalProperties
    {
      public static readonly ReadOnlyCollection<string> All = new ReadOnlyCollection<string>(new string[11]
      {
	      "ExecutionTime",
	      "OverallPageNumber",
	      "OverallTotalPages",
	      "PageName",
	      "PageNumber",
	      "RenderFormat.Name",
	      "RenderFormat.IsInteractive",
	      "ReportFolder",
	      "ReportName",
	      "ReportServerUrl",
	      "TotalPages"
      });
      public const string ExecutionTime = "ExecutionTime";
      public const string OverallPageNumber = "OverallPageNumber";
      public const string OverallTotalPages = "OverallTotalPages";
      public const string PageName = "PageName";
      public const string PageNumber = "PageNumber";
      public const string RenderFormatName = "RenderFormat.Name";
      public const string RenderFormatIsInteractive = "RenderFormat.IsInteractive";
      public const string ReportFolder = "ReportFolder";
      public const string ReportName = "ReportName";
      public const string ReportServerUrl = "ReportServerUrl";
      public const string TotalPages = "TotalPages";
    }

    public static class ParameterProperties
    {
      public static readonly ReadOnlyCollection<string> All = new ReadOnlyCollection<string>(new string[2]
      {
	      "Value",
	      "Label"
      });
      public const string Value = "Value";
      public const string Label = "Label";
    }

    public static class UserProperties
    {
      public static readonly ReadOnlyCollection<string> All = new ReadOnlyCollection<string>(new string[2]
      {
	      "UserID",
	      "Language"
      });
      public const string UserID = "UserID";
      public const string Language = "Language";
    }

    public static class Functions
    {
      public static readonly ReadOnlyCollection<string> AllAggregateFunctions = new ReadOnlyCollection<string>(new string[14]
      {
	      "Sum",
	      "Avg",
	      "Max",
	      "Min",
	      "Count",
	      "CountDistinct",
	      "StDev",
	      "StDevP",
	      "Var",
	      "VarP",
	      "First",
	      "Last",
	      "Previous",
	      "Aggregate"
      });
      public const string Sum = "Sum";
      public const string Avg = "Avg";
      public const string Max = "Max";
      public const string Min = "Min";
      public const string Count = "Count";
      public const string CountDistinct = "CountDistinct";
      public const string CountRows = "CountRows";
      public const string StDev = "StDev";
      public const string StDevP = "StDevP";
      public const string Var = "Var";
      public const string VarP = "VarP";
      public const string First = "First";
      public const string Last = "Last";
      public const string Previous = "Previous";
      public const string RunningValue = "RunningValue";
      public const string RowNumber = "RowNumber";
      public const string Aggregate = "Aggregate";
    }
  }
}
