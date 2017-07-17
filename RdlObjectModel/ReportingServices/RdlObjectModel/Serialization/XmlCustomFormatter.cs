using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  internal class XmlCustomFormatter
  {
    private static readonly string[] allDateTimeFormats = new string[37]
    {
      "yyyy",
      "---dd",
      "--MM-dd",
      "yyyy-MM",
      "yyyy-MM-dd",
      "HH:mm:ss",
      "HH:mm:ss.f",
      "HH:mm:ss.ff",
      "HH:mm:ss.fff",
      "HH:mm:ss.ffff",
      "HH:mm:ss.fffff",
      "HH:mm:ss.ffffff",
      "HH:mm:ss.fffffff",
      "HH:mm:sszzzzzz",
      "HH:mm:ss.fzzzzzz",
      "HH:mm:ss.ffzzzzzz",
      "HH:mm:ss.fffzzzzzz",
      "HH:mm:ss.ffffzzzzzz",
      "HH:mm:ss.fffffzzzzzz",
      "HH:mm:ss.ffffffzzzzzz",
      "HH:mm:ss.fffffffzzzzzz",
      "yyyy-MM-ddTHH:mm:ss",
      "yyyy-MM-ddTHH:mm:ss.f",
      "yyyy-MM-ddTHH:mm:ss.ff",
      "yyyy-MM-ddTHH:mm:ss.fff",
      "yyyy-MM-ddTHH:mm:ss.ffff",
      "yyyy-MM-ddTHH:mm:ss.fffff",
      "yyyy-MM-ddTHH:mm:ss.ffffff",
      "yyyy-MM-ddTHH:mm:ss.fffffff",
      "yyyy-MM-ddTHH:mm:sszzzzzz",
      "yyyy-MM-ddTHH:mm:ss.fzzzzzz",
      "yyyy-MM-ddTHH:mm:ss.ffzzzzzz",
      "yyyy-MM-ddTHH:mm:ss.fffzzzzzz",
      "yyyy-MM-ddTHH:mm:ss.ffffzzzzzz",
      "yyyy-MM-ddTHH:mm:ss.fffffzzzzzz",
      "yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz",
      "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz"
    };
    private static readonly string[] allDateFormats = new string[1]
    {
      "yyyy-MM-dd"
    };
    private static readonly string[] allTimeFormats = new string[16]
    {
      "HH:mm:ss",
      "HH:mm:ss.f",
      "HH:mm:ss.ff",
      "HH:mm:ss.fff",
      "HH:mm:ss.ffff",
      "HH:mm:ss.fffff",
      "HH:mm:ss.ffffff",
      "HH:mm:ss.fffffff",
      "HH:mm:sszzzzzz",
      "HH:mm:ss.fzzzzzz",
      "HH:mm:ss.ffzzzzzz",
      "HH:mm:ss.fffzzzzzz",
      "HH:mm:ss.ffffzzzzzz",
      "HH:mm:ss.fffffzzzzzz",
      "HH:mm:ss.ffffffzzzzzz",
      "HH:mm:ss.fffffffzzzzzz"
    };

    internal static string FromDate(DateTime value)
    {
      return XmlConvert.ToString(value, "yyyy-MM-dd");
    }

    internal static string FromTime(DateTime value)
    {
      return XmlConvert.ToString(value, "HH:mm:ss.fffffffzzzzzz");
    }

    internal static string FromDateTime(DateTime value)
    {
      return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
    }

    internal static string FromChar(char value)
    {
      return XmlConvert.ToString((ushort) value);
    }

    internal static string FromXmlNmToken(string nmToken)
    {
      return XmlConvert.DecodeName(nmToken);
    }

    internal static string FromByteArrayBase64(byte[] value)
    {
      if (value == null)
        return null;
      return Convert.ToBase64String(value);
    }

    internal static string FromEnum(long val, string[] vals, long[] ids)
    {
      long num = val;
      StringBuilder stringBuilder = new StringBuilder();
      int index1 = -1;
      for (int index2 = 0; index2 < ids.Length; ++index2)
      {
        if (ids[index2] == 0L)
          index1 = index2;
        else if (val != 0L)
        {
          if ((ids[index2] & num) == ids[index2])
          {
            if (stringBuilder.Length != 0)
              stringBuilder.Append(" ");
            stringBuilder.Append(vals[index2]);
            val &= ~ids[index2];
          }
        }
        else
          break;
      }
      if (val != 0L)
        return XmlConvert.ToString(num);
      if (stringBuilder.Length == 0 && index1 >= 0)
        stringBuilder.Append(vals[index1]);
      return stringBuilder.ToString();
    }

    internal static DateTime ToDateTime(string value)
    {
      return ToDateTime(value, allDateTimeFormats);
    }

    internal static DateTime ToDateTime(string value, string[] formats)
    {
      return XmlConvert.ToDateTime(value, formats);
    }

    internal static DateTime ToDate(string value)
    {
      return ToDateTime(value, allDateFormats);
    }

    internal static DateTime ToTime(string value)
    {
      return ToDateTime(value, allTimeFormats);
    }

    internal static char ToChar(string value)
    {
      return (char) XmlConvert.ToUInt16(value);
    }

    internal static string ToXmlNmToken(string value)
    {
      return XmlConvert.EncodeNmToken(value);
    }

    internal static byte[] ToByteArrayBase64(string value)
    {
      if (value == null)
        return null;
      value = value.Trim();
      if (value.Length == 0)
        return new byte[0];
      return Convert.FromBase64String(value);
    }

    internal static long ToEnum(string val, Hashtable vals, string typeName)
    {
      long num = 0;
      foreach (object index in val.Split(null))
      {
        object val1 = vals[index];
        if (val1 == null)
          throw new Exception("UnknownConstant");
        num |= (long) val1;
      }
      return num;
    }
  }
}
