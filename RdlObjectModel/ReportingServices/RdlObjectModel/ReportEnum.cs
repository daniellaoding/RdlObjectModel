using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal struct ReportEnum<T> : IXmlSerializable, IFormattable where T : struct, IConvertible
  {
    private T m_value;
    private static readonly IList<string> m_names;

    public T Value
    {
      get
      {
        return m_value;
      }
      set
      {
        m_value = value;
      }
    }

    static ReportEnum()
    {
      object[] customAttributes = typeof (T).GetCustomAttributes(typeof (EnumNamesAttribute), false);
      if (customAttributes.Length > 0)
        m_names = ((EnumNamesAttribute) customAttributes[0]).Names;
      else
        m_names = Enum.GetNames(typeof (T));
    }

    public ReportEnum(T value)
    {
      m_value = value;
    }

    public ReportEnum(string value)
    {
      m_value = default (T);
      Init(value);
    }

    public static implicit operator T(ReportEnum<T> value)
    {
      return value.Value;
    }

    public static implicit operator ReportEnum<T>(T value)
    {
      return new ReportEnum<T>(value);
    }

    public static bool operator ==(ReportEnum<T> left, ReportEnum<T> right)
    {
      return left.Value.Equals(right.Value);
    }

    public static bool operator !=(ReportEnum<T> left, ReportEnum<T> right)
    {
      return !left.Value.Equals(right.Value);
    }

    public static IList<string> GetNames()
    {
      return m_names;
    }

    private void Init(string value)
    {
      int num = m_names.IndexOf(value);
      if (num < 0)
      {
        throw new ArgumentException(string.Format(SRErrors.InvalidValue, value));
      }
      m_value = (T) Enum.ToObject(typeof (T), num);
    }

    public static ReportEnum<T> Parse(string value, IFormatProvider provider)
    {
      return new ReportEnum<T>(value);
    }

    public override string ToString()
    {
      return m_names[((IConvertible) m_value).ToInt32(CultureInfo.InvariantCulture)];
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return ToString();
    }

    public override bool Equals(object value)
    {
      if (value is ReportEnum<T>)
        return m_value.Equals(((ReportEnum<T>) value).Value);
      return m_value.Equals(value);
    }

    public override int GetHashCode()
    {
      return m_value.GetHashCode();
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      Init(reader.ReadString());
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      writer.WriteString(ToString());
    }
  }
}
