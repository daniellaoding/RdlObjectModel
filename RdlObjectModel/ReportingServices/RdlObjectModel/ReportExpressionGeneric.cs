using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public struct ReportExpression<T> : IExpression, IXmlSerializable, IFormattable, IShouldSerialize where T : struct
  {
    private T m_value;
    private string m_expression;
    private static MethodInfo m_parseMethod;
    private static int m_parseMethodArgs;

    public T Value
    {
      get
      {
        return m_value;
      }
      set
      {
        m_value = value;
        m_expression = null;
      }
    }

    object IExpression.Value
    {
      get
      {
        return m_value;
      }
      set
      {
        m_value = (T) value;
      }
    }

    public string Expression
    {
      get
      {
        return m_expression;
      }
      set
      {
        m_expression = value;
        m_value = default (T);
      }
    }

    public bool IsExpression => m_expression != null;

	  public bool IsEmpty
    {
      get
      {
        if (typeof (T) == typeof (ReportSize))
        {
          if (!IsExpression)
            return ((ReportSize) (ValueType) Value).IsEmpty;
          return false;
        }
        if (typeof (T) == typeof (ReportColor) && !IsExpression)
          return ((ReportColor) (ValueType) Value).IsEmpty;
        return false;
      }
    }

    public ReportExpression(T value)
    {
      m_value = value;
      m_expression = null;
    }

    public ReportExpression(string value)
    {
      this = new ReportExpression<T>(value, CultureInfo.CurrentCulture);
    }

    public ReportExpression(string value, IFormatProvider provider)
    {
      m_value = default (T);
      m_expression = null;
      if (string.IsNullOrEmpty(value))
        return;
      Init(value, provider);
    }

    public static explicit operator T(ReportExpression<T> value)
    {
      return value.Value;
    }

    public static implicit operator ReportExpression<T>(T value)
    {
      return new ReportExpression<T>(value);
    }

    public static implicit operator ReportExpression<T>(T? value)
    {
      if (value.HasValue)
        return new ReportExpression<T>(value.Value);
      return new ReportExpression<T>(null, CultureInfo.InvariantCulture);
    }

    public static explicit operator string(ReportExpression<T> value)
    {
      return value.ToString();
    }

    public static bool operator ==(ReportExpression<T> left, ReportExpression<T> right)
    {
      if (left.Value.Equals(right.Value))
        return left.Expression == right.Expression;
      return false;
    }

    public static bool operator ==(ReportExpression<T> left, T right)
    {
      if (!left.IsExpression)
        return left.Value.Equals(right);
      return false;
    }

    public static bool operator ==(T left, ReportExpression<T> right)
    {
      if (!right.IsExpression)
        return right.Value.Equals(left);
      return false;
    }

    public static bool operator ==(ReportExpression<T> left, string right)
    {
      if (left.IsExpression)
        return left.Expression == right;
      return false;
    }

    public static bool operator ==(string left, ReportExpression<T> right)
    {
      if (right.IsExpression)
        return right.Expression == left;
      return false;
    }

    public static bool operator !=(ReportExpression<T> left, ReportExpression<T> right)
    {
      return !(left == right);
    }

    public static bool operator !=(ReportExpression<T> left, T right)
    {
      return !(left == right);
    }

    public static bool operator !=(T left, ReportExpression<T> right)
    {
      return !(left == right);
    }

    public static bool operator !=(ReportExpression<T> left, string right)
    {
      return !(left == right);
    }

    public static bool operator !=(string left, ReportExpression<T> right)
    {
      return !(left == right);
    }

    private void Init(string value, IFormatProvider provider)
    {
      if (ReportExpression.IsExpressionString(value))
        Expression = value;
      else if (typeof (T).IsSubclassOf(typeof (Enum)))
        Value = (T) ReportExpression.ParseEnum(typeof (T), value);
      else if (typeof (T) == typeof (ReportSize))
        Value = (T) (ValueType) ReportSize.Parse(value, provider);
      else if (typeof (T) == typeof (ReportColor))
      {
        Value = (T) (ValueType) ReportColor.Parse(value, provider);
      }
      else
      {
        try
        {
          if (typeof (T) == typeof (bool))
          {
            Value = (T) (ValueType) XmlConvert.ToBoolean(value.ToLowerInvariant());
          }
          else
          {
            MethodInfo parseMethod = GetParseMethod();
            bool? local = null;
            object[] parameters;
            if (m_parseMethodArgs != 1)
              parameters = new object[2]
              {
                value,
                provider
              };
            else
              parameters = new object[1]{ value };
            Value = (T) parseMethod.Invoke(local, parameters);
          }
        }
        catch (TargetInvocationException ex)
        {
          if (ex.InnerException != null)
            throw ex.InnerException;
          throw ex;
        }
      }
    }

    public static ReportExpression<T> Parse(string value, IFormatProvider provider)
    {
      return new ReportExpression<T>(value, provider);
    }

    private MethodInfo GetParseMethod()
    {
      if (m_parseMethodArgs == 0)
      {
        m_parseMethod = typeof (T).GetMethod("Parse", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[2]
        {
          typeof (string),
          typeof (IFormatProvider)
        }, null);
        m_parseMethodArgs = 2;
        if (m_parseMethod == null)
        {
          m_parseMethod = typeof (T).GetMethod("Parse", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[1]
          {
            typeof (string)
          }, null);
          m_parseMethodArgs = 1;
        }
      }
      return m_parseMethod;
    }

    public override string ToString()
    {
      return ToString(null, CultureInfo.CurrentCulture);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      if (IsExpression)
        return m_expression;
      if (typeof (T) == typeof (bool) && provider == CultureInfo.InvariantCulture)
        return !true.Equals(m_value) ? "false" : "true";
      if (typeof (IFormattable).IsAssignableFrom(typeof (T)))
        return ((IFormattable) m_value).ToString(format, provider);
      return m_value.ToString();
    }

    public override bool Equals(object value)
    {
      if (value is ReportExpression<T>)
      {
        if (m_value.Equals(((ReportExpression<T>) value).Value))
          return m_expression == ((ReportExpression<T>) value).Expression;
        return false;
      }
      if (!IsExpression)
        return m_value.Equals(value);
      if (value is string)
        return m_expression == (string) value;
      return false;
    }

    public override int GetHashCode()
    {
      int hashCode = m_value.GetHashCode();
      if (m_expression != null)
        hashCode ^= m_expression.GetHashCode();
      return hashCode;
    }

    public void GetDependencies(IList<ReportObject> dependencies, ReportObject parent)
    {
      ReportExpressionUtils.GetDependencies(dependencies, parent, Expression);
    }

    internal ReportExpression<T> UpdateNamedReferences(NameChanges RenameList)
    {
      Expression = ReportExpressionUtils.UpdateNamedReferences(Expression, RenameList);
      return this;
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      Init(reader.ReadString(), CultureInfo.InvariantCulture);
      reader.Skip();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      writer.WriteString(ToString(null, CultureInfo.InvariantCulture));
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      if (!IsExpression && typeof (IShouldSerialize).IsAssignableFrom(typeof (T)))
        return ((IShouldSerialize) m_value).ShouldSerializeThis();
      return true;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      return SerializationMethod.Auto;
    }
  }
}
