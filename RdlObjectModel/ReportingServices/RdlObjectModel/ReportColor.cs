using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [TypeConverter(typeof (ReportColorConverter))]
  public struct ReportColor : IXmlSerializable, IFormattable, IShouldSerialize
  {
	  private Color m_color;

    public static ReportColor Empty { get; } = new ReportColor();

	  public Color Color
    {
      get
      {
        return m_color;
      }
      set
      {
        m_color = value;
      }
    }

    public bool IsEmpty => Color.Empty == m_color;

	  public ReportColor(Color color)
    {
      m_color = color;
    }

    public ReportColor(string value)
    {
      m_color = Color.Empty;
      Init(value);
    }

    public static bool operator ==(ReportColor left, ReportColor right)
    {
      return left.Color == right.Color;
    }

    public static bool operator !=(ReportColor left, ReportColor right)
    {
      return left.Color != right.Color;
    }

    private void Init(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      Color = RdlStringToColor(value);
    }

    internal static Color RdlStringToColor(string value)
    {
      if (value[0] == 35)
        return RgbStringToColor(value);
      Color color = FromName(value);
      if (!color.IsKnownColor)
      {
        throw new ArgumentException(string.Format(SRErrors.InvalidColor, value));
      }
      return color;
    }

    private static Color RgbStringToColor(string value)
    {
      byte maxValue = byte.MaxValue;
      if (value == "#00ffffff")
        return Color.Transparent;
      if (value == "#00000000")
        return Color.Empty;
      bool flag = true;
      if (value.Length != 7 && value.Length != 9 || value[0] != 35)
      {
        flag = false;
      }
      else
      {
        string str = "abcdefABCDEF";
        for (int index = 1; index < value.Length; ++index)
        {
          if (!char.IsDigit(value[index]) && -1 == str.IndexOf(value[index]))
          {
            flag = false;
            break;
          }
        }
      }
      if (!flag)
      {
        throw new ArgumentException(string.Format(SRErrors.InvalidColor, value));
      }
      int startIndex = 1;
      if (value.Length == 9)
      {
        maxValue = Convert.ToByte(value.Substring(startIndex, 2), 16);
        startIndex += 2;
      }
      byte num1 = Convert.ToByte(value.Substring(startIndex, 2), 16);
      byte num2 = Convert.ToByte(value.Substring(startIndex + 2, 2), 16);
      byte num3 = Convert.ToByte(value.Substring(startIndex + 4, 2), 16);
      return Color.FromArgb(maxValue, num1, num2, num3);
    }

    public static string ColorToRdlString(Color c)
    {
      if (c.IsEmpty)
        return "";
      if (c == Color.Transparent)
        return "#00ffffff";
      if (c.IsNamedColor && !c.IsSystemColor)
        return ToName(c);
      if (c.A == byte.MaxValue)
        return StringUtil.FormatInvariant("#{0:x6}", (object) (c.ToArgb() & 16777215));
      return StringUtil.FormatInvariant("#{0:x8}", (object) c.ToArgb());
    }

    public static ReportColor Parse(string s, IFormatProvider provider)
    {
      return new ReportColor(s);
    }

    public void SetEmpty()
    {
      m_color = Color.Empty;
    }

    internal static Color FromName(string name)
    {
      if (string.Equals(name, "LightGrey", StringComparison.OrdinalIgnoreCase))
        name = "LightGray";
      return Color.FromName(name);
    }

    internal static string ToName(Color color)
    {
      string str = color.Name;
      if (str == "LightGray")
        str = "LightGrey";
      return str;
    }

    public override string ToString()
    {
      return ColorToRdlString(Color);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return ToString();
    }

    public override int GetHashCode()
    {
      return m_color.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is ReportColor))
        return false;
      return this == (ReportColor) obj;
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      Init(reader.ReadString().Trim());
      reader.Skip();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      string text = ToString();
      writer.WriteString(text);
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return !IsEmpty;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      return SerializationMethod.Auto;
    }
  }
}
