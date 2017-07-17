using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal static class RDLUtil
  {
    internal static bool IsIntegerType(TypeCode typeCode)
    {
      switch (typeCode)
      {
        case System.TypeCode.Byte:
        case System.TypeCode.Int16:
        case System.TypeCode.UInt16:
        case System.TypeCode.Int32:
        case System.TypeCode.UInt32:
        case System.TypeCode.Int64:
        case System.TypeCode.UInt64:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsNumericType(TypeCode typeCode)
    {
      if (IsIntegerType(typeCode))
        return true;
      switch (typeCode)
      {
        case System.TypeCode.Single:
        case System.TypeCode.Double:
        case System.TypeCode.Decimal:
          return true;
        default:
          return false;
      }
    }

    public static DataTypes? ConvertToDataType(TypeCode typeCode)
    {
      switch (typeCode)
      {
        case System.TypeCode.DBNull:
        case System.TypeCode.String:
          return new DataTypes?(DataTypes.String);
        case System.TypeCode.Boolean:
          return new DataTypes?(DataTypes.Boolean);
        case System.TypeCode.Byte:
        case System.TypeCode.Int16:
        case System.TypeCode.UInt16:
        case System.TypeCode.Int32:
        case System.TypeCode.UInt32:
          return new DataTypes?(DataTypes.Integer);
        case System.TypeCode.Int64:
        case System.TypeCode.UInt64:
        case System.TypeCode.Single:
        case System.TypeCode.Double:
        case System.TypeCode.Decimal:
          return new DataTypes?(DataTypes.Float);
        case System.TypeCode.DateTime:
          return new DataTypes?(DataTypes.DateTime);
        default:
          return new DataTypes?();
      }
    }

    internal static bool ConvertToBoolean(object value)
    {
      string str = value as string;
      if (value == null || value is DBNull || str != null && str == "")
        return false;
      if (value is int)
        return (int) value > 0;
      return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
    }

    internal static byte ConvertToByte(object value)
    {
      if (value == null || value is DBNull || value is string && (string) value == "")
        return 0;
      return Convert.ToByte(value, CultureInfo.InvariantCulture);
    }

    internal static DateTime ConvertToDateTime(object value)
    {
      if (value != null && !(value is DBNull))
      {
        if (value is string)
        {
          if ((string) value == "")
            goto label_3;
        }
        try
        {
          return Convert.ToDateTime(value, CultureInfo.InvariantCulture);
        }
        catch
        {
          return new DateTime();
        }
      }
label_3:
      return new DateTime();
    }

    public static Decimal ConvertToDecimal(object value)
    {
      if (value is DBNull)
        return new Decimal(0);
      try
      {
        return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
      }
      catch
      {
        return new Decimal(0);
      }
    }

    internal static double ConvertToDouble(object value)
    {
      if (value is DBNull)
        return 0.0;
      try
      {
        return Convert.ToDouble(value, CultureInfo.InvariantCulture);
      }
      catch
      {
        return 0.0;
      }
    }

    internal static short ConvertToInt16(object value)
    {
      if (value is DBNull)
        return 0;
      try
      {
        return Convert.ToInt16(value, CultureInfo.InvariantCulture);
      }
      catch
      {
        return 0;
      }
    }

    internal static int ConvertToInt32(object value)
    {
      if (value is DBNull)
        return 0;
      try
      {
        return Convert.ToInt32(value, CultureInfo.InvariantCulture);
      }
      catch
      {
        return 0;
      }
    }

    internal static long ConvertToInt64(object value)
    {
      if (value is DBNull)
        return 0;
      try
      {
        return Convert.ToInt64(value, CultureInfo.InvariantCulture);
      }
      catch
      {
        return 0;
      }
    }

    internal static float ConvertToSingle(object value)
    {
      if (value is DBNull)
        return 0.0f;
      try
      {
        return Convert.ToSingle(value, CultureInfo.InvariantCulture);
      }
      catch
      {
        return 0.0f;
      }
    }

    public static CultureInfo GetFormatProvider(bool useUserCulture)
    {
      if (!useUserCulture)
        return CultureInfo.InvariantCulture;
      return CultureInfo.CurrentCulture;
    }

    public static string ObjectToString(object value, bool useUserCulture)
    {
      IFormatProvider formatProvider = GetFormatProvider(useUserCulture);
      if (value == null)
        return null;
      if (value is DateTime)
      {
        DateTime dateTime = (DateTime) value;
        if (dateTime.TimeOfDay == TimeSpan.Zero)
        {
          if (useUserCulture)
            return dateTime.ToString("d", formatProvider);
          return dateTime.ToString("yyyy'-'MM'-'dd", formatProvider);
        }
        if (dateTime.Date <= DateTime.MinValue || dateTime.Date >= DateTime.MaxValue)
          return dateTime.ToString("T", formatProvider);
        if (useUserCulture)
          return dateTime.ToString("G", formatProvider);
        return dateTime.ToString("yyyy'-'MM'-'dd HH':'mm':'ss", formatProvider);
      }
      if (useUserCulture && (value is float || value is double || value is Decimal))
        return ((IFormattable) value).ToString("#,0.#############", formatProvider);
      return Convert.ToString(value, formatProvider);
    }

    public static object DefaultDataType(DataTypes dataType)
    {
      switch (dataType)
      {
        case DataTypes.String:
          return string.Empty;
        case DataTypes.Boolean:
          return false;
        case DataTypes.DateTime:
          return DateTime.MinValue;
        case DataTypes.Integer:
          return 0;
        case DataTypes.Float:
          return 0.0;
        default:
          return null;
      }
    }

    internal static bool IsNarrowingConversion(Type fromType, Type toType)
    {
      if (fromType == typeof (int))
        return toType == typeof (ushort) || toType == typeof (short) || (toType == typeof (sbyte) || toType == typeof (byte)) || toType == typeof (bool);
      if (fromType == typeof (double))
        return toType == typeof (float) || toType == typeof (ulong) || (toType == typeof (long) || toType == typeof (uint)) || (toType == typeof (int) || toType == typeof (ushort) || (toType == typeof (short) || toType == typeof (sbyte))) || (toType == typeof (byte) || toType == typeof (bool));
      return fromType == typeof (Decimal) && (toType == typeof (double) || toType == typeof (float) || (toType == typeof (ulong) || toType == typeof (long)) || (toType == typeof (uint) || toType == typeof (int) || (toType == typeof (ushort) || toType == typeof (short))) || (toType == typeof (sbyte) || toType == typeof (byte) || toType == typeof (bool)));
    }

    internal static TypeCode TypeCode(string typeCode)
    {
      if (!string.IsNullOrEmpty(typeCode))
      {
        switch (typeCode.ToUpperInvariant())
        {
          case "SYSTEM.BYTE":
          case "SYSTEM.INT16":
          case "SYSTEM.INT32":
          case "SYSTEM.INT64":
          case "SYSTEM.UINT16":
          case "SYSTEM.UINT32":
          case "SYSTEM.UINT64":
            return System.TypeCode.Int32;
          case "SYSTEM.SINGLE":
          case "SYSTEM.DOUBLE":
            return System.TypeCode.Double;
          case "SYSTEM.DECIMAL":
            return System.TypeCode.Decimal;
          case "SYSTEM.STRING":
            return System.TypeCode.String;
          case "SYSTEM.BOOLEAN":
            return System.TypeCode.Boolean;
          case "SYSTEM.DATETIME":
            return System.TypeCode.DateTime;
        }
      }
      return System.TypeCode.String;
    }
  }
}
