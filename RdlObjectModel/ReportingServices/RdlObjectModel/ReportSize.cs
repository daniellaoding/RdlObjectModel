using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [TypeConverter(typeof (ReportSizeConverter))]
  public struct ReportSize : IComparable, IXmlSerializable, IFormattable, IShouldSerialize
  {
	  internal const double CentimetersPerInch = 2.54;
    internal const double MillimetersPerInch = 25.4;
    internal const double PicasPerInch = 6.0;
    internal const double PointsPerInch = 72.0;
    internal const int DefaultDecimalDigits = 5;
    private static float m_dotsPerInch;
    private static string m_serializationFormat;
    private static int m_serializedDecimalDigits;
    private SizeTypes m_type;

	  public static SizeTypes DefaultType { get; set; } = SizeTypes.Inch;

	  public static int SerializedDecimalDigits
    {
      get
      {
        return m_serializedDecimalDigits;
      }
      set
      {
        if (value <= 0 || value > 99)
          throw new ArgumentException("SerializedDecimalDigits");
        m_serializedDecimalDigits = value;
        m_serializationFormat = "{0:0." + new string('#', value) + "}{1}";
      }
    }

    public static float DotsPerInch
    {
      get
      {
        if (m_dotsPerInch == 0.0)
        {
          using (Bitmap bitmap = new Bitmap(1, 1))
          {
            using (Graphics graphics = Graphics.FromImage(bitmap))
              m_dotsPerInch = graphics.DpiX;
          }
        }
        return m_dotsPerInch;
      }
    }

    public static ReportSize Empty { get; } = new ReportSize();

	  public SizeTypes Type
    {
      get
      {
        if (m_type == SizeTypes.Invalid)
          return DefaultType;
        return m_type;
      }
    }

    public double Value { get; private set; }

	  public double SerializedValue => Math.Round(Value, m_serializedDecimalDigits);

	  public bool IsEmpty => m_type == SizeTypes.Invalid;

	  static ReportSize()
    {
      SerializedDecimalDigits = 5;
    }

    public ReportSize(double value, SizeTypes type)
    {
      Value = value;
      m_type = type;
    }

    public ReportSize(string value)
    {
      this = new ReportSize(value, CultureInfo.CurrentCulture);
    }

    public ReportSize(double value)
    {
      Value = value;
      m_type = DefaultType;
    }

    public ReportSize(string value, IFormatProvider provider)
    {
      this = new ReportSize(value, provider, DefaultType);
    }

    public ReportSize(string value, IFormatProvider provider, SizeTypes defaultType)
    {
      Value = 0.0;
      m_type = SizeTypes.Invalid;
      if (string.IsNullOrEmpty(value))
        return;
      Init(value, provider, defaultType);
    }

    public static bool operator ==(ReportSize left, ReportSize right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(ReportSize left, ReportSize right)
    {
      return !left.Equals(right);
    }

    public static bool operator <(ReportSize left, ReportSize right)
    {
      return left.ToMillimeters() < right.ToMillimeters();
    }

    public static bool operator >(ReportSize left, ReportSize right)
    {
      return left.ToMillimeters() > right.ToMillimeters();
    }

    public static ReportSize operator +(ReportSize size1, ReportSize size2)
    {
      if (size1.IsEmpty)
        size1 = new ReportSize(0.0);
      size1.SetPixels(size1.ToPixels() + size2.ToPixels());
      return size1;
    }

    public static ReportSize operator -(ReportSize size1, ReportSize size2)
    {
      if (size1.IsEmpty)
        size1 = new ReportSize(0.0);
      size1.SetPixels(size1.ToPixels() - size2.ToPixels());
      return size1;
    }

    private void Init(string value, IFormatProvider provider, SizeTypes defaultType)
    {
      if (provider == null)
        provider = CultureInfo.CurrentCulture;
      string str1 = value.Trim();
      int length = str1.Length;
      NumberFormatInfo numberFormatInfo = provider.GetFormat(typeof (NumberFormatInfo)) as NumberFormatInfo ?? CultureInfo.InvariantCulture.NumberFormat;
      int num = -1;
      for (int index = 0; index < length; ++index)
      {
        char c = str1[index];
        if (char.IsDigit(c) || c == numberFormatInfo.NegativeSign[0] || (c == numberFormatInfo.NumberDecimalSeparator[0] || c == numberFormatInfo.NumberGroupSeparator[0]))
          num = index;
        else
          break;
      }
      if (num == -1)
      {
        throw new FormatException(string.Format(SRErrors.UnitParseNoDigits, value));
      }
      if (num < length - 1)
      {
        try
        {
          m_type = GetTypeFromString(str1.Substring(num + 1).Trim().ToLowerInvariant());
        }
        catch (ArgumentException ex)
        {
          throw new FormatException(ex.Message);
        }
      }
      else
      {
        if (defaultType == SizeTypes.Invalid)
        {
          throw new FormatException(string.Format(SRErrors.UnitParseNoUnit, value));
        }
        m_type = defaultType;
      }
      string str2 = str1.Substring(0, num + 1);
      try
      {
        Value = double.Parse(str2, provider);
      }
      catch
      {
        throw new FormatException(string.Format(SRErrors.UnitParseNumericPart, value, str2, m_type.ToString("G")));
      }
    }

    public static ReportSize Parse(string s, IFormatProvider provider)
    {
      return new ReportSize(s, provider);
    }

    public override int GetHashCode()
    {
      return m_type.GetHashCode() << 2 ^ Value.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is ReportSize))
        return false;
      ReportSize reportSize = (ReportSize) obj;
      return reportSize.Value == Value && reportSize.m_type == m_type;
    }

    private static string GetStringFromType(SizeTypes type)
    {
      switch (type)
      {
        case SizeTypes.Inch:
          return "in";
        case SizeTypes.Cm:
          return "cm";
        case SizeTypes.Mm:
          return "mm";
        case SizeTypes.Point:
          return "pt";
        case SizeTypes.Pica:
          return "pc";
        default:
          return string.Empty;
      }
    }

    internal static SizeTypes GetTypeFromString(string value)
    {
      if (value == null || value.Length <= 0)
        return DefaultType;
      if (value.Equals("pt"))
        return SizeTypes.Point;
      if (value.Equals("pc"))
        return SizeTypes.Pica;
      if (value.Equals("in"))
        return SizeTypes.Inch;
      if (value.Equals("mm"))
        return SizeTypes.Mm;
      if (value.Equals("cm"))
        return SizeTypes.Cm;
      throw new ArgumentException(string.Format(SRErrors.InvalidUnitType, value));
    }

    public int ToIntPixels()
    {
      return Convert.ToInt32(ConvertToPixels(Value, m_type));
    }

    public double ToPixels()
    {
      return ConvertToPixels(Value, m_type);
    }

    public void SetPixels(double pixels)
    {
      Value = ConvertToUnits(pixels, m_type);
    }

    public static ReportSize FromPixels(double pixels, SizeTypes type)
    {
      return new ReportSize(ConvertToUnits(pixels, type), type);
    }

    public double ToMillimeters()
    {
      return ConvertToMillimeters(Value, m_type);
    }

    public double ToCentimeters()
    {
      return 0.1 * ConvertToMillimeters(Value, m_type);
    }

    public double ToInches()
    {
      return ConvertToMillimeters(Value, m_type) / 25.4;
    }

    public double ToPoints()
    {
      if (m_type == SizeTypes.Point)
        return Value;
      return ToInches() * 72.0;
    }

    public override string ToString()
    {
      return ToString(null, CultureInfo.CurrentCulture);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      if (IsEmpty)
        return string.Empty;
      return string.Format(provider, m_serializationFormat, new object[2]
      {
        SerializedValue,
        GetStringFromType(m_type)
      });
    }

    internal ReportSize ChangeType(SizeTypes type)
    {
      if (type == m_type)
        return this;
      return new ReportSize(ConvertToUnits(ConvertToPixels(Value, m_type), type), type);
    }

    internal double ConvertToPixels(double value, SizeTypes type)
    {
      switch (type)
      {
        case SizeTypes.Inch:
          value *= DotsPerInch;
          break;
        case SizeTypes.Cm:
          value *= DotsPerInch / 2.54;
          break;
        case SizeTypes.Mm:
          value *= DotsPerInch / 25.4;
          break;
        case SizeTypes.Point:
          value *= DotsPerInch / 72.0;
          break;
        case SizeTypes.Pica:
          value *= DotsPerInch / 6.0;
          break;
      }
      return value;
    }

    internal double ConvertToMillimeters(double value, SizeTypes type)
    {
      switch (type)
      {
        case SizeTypes.Inch:
          value *= 25.4;
          break;
        case SizeTypes.Cm:
          value *= 10.0;
          break;
        case SizeTypes.Point:
          value *= sbyte.MaxValue / 360.0;
          break;
        case SizeTypes.Pica:
          value *= sbyte.MaxValue / 30.0;
          break;
      }
      return value;
    }

    internal static double ConvertToUnits(double pixels, SizeTypes type)
    {
      double num = pixels;
      switch (type)
      {
        case SizeTypes.Inch:
          num /= DotsPerInch;
          break;
        case SizeTypes.Cm:
          num /= DotsPerInch / 2.54;
          break;
        case SizeTypes.Mm:
          num /= DotsPerInch / 25.4;
          break;
        case SizeTypes.Point:
          num /= DotsPerInch / 72.0;
          break;
        case SizeTypes.Pica:
          num /= DotsPerInch / 6.0;
          break;
      }
      return num;
    }

    int IComparable.CompareTo(object value)
    {
      if (!(value is ReportSize))
        throw new ArgumentException("value is not a RdlSize");
      double millimeters1 = ToMillimeters();
      double millimeters2 = ((ReportSize) value).ToMillimeters();
      if (millimeters1 < millimeters2)
        return -1;
      return millimeters1 <= millimeters2 ? 0 : 1;
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      Init(reader.ReadString(), CultureInfo.InvariantCulture, SizeTypes.Invalid);
      reader.Skip();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      string text = ToString(null, CultureInfo.InvariantCulture);
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
