using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal static class VBFunctions
  {
    public static char CChar(object o)
    {
      return Convert.ToChar(o, CultureInfo.CurrentCulture);
    }

    public static short CShort(object o)
    {
      return RDLUtil.ConvertToInt16(o);
    }

    public static int CUShort(object o)
    {
      throw new NotImplementedException();
    }

    public static Decimal CDec(object o)
    {
      return RDLUtil.ConvertToDecimal(o);
    }

    public static object CObj(object o)
    {
      return Convert.ToBoolean(o, CultureInfo.CurrentCulture);
    }

    public static bool CBool(object o)
    {
      return RDLUtil.ConvertToBoolean(o);
    }

    public static byte CByte(object o)
    {
      return RDLUtil.ConvertToByte(o);
    }

    public static short CSByte(object o)
    {
      throw new NotImplementedException();
    }

    public static DateTime CDate(object o)
    {
      return RDLUtil.ConvertToDateTime(o);
    }

    public static int CInt(object o)
    {
      return RDLUtil.ConvertToInt32(o);
    }

    public static long CUInt(object o)
    {
      throw new NotImplementedException();
    }

    public static long CLng(object o)
    {
      return RDLUtil.ConvertToInt64(o);
    }

    public static Decimal CULng(object o)
    {
      throw new NotImplementedException();
    }

    public static float CSng(object o)
    {
      return RDLUtil.ConvertToSingle(o);
    }

    public static string CStr(object o)
    {
      return Convert.ToString(o, CultureInfo.CurrentCulture);
    }

    public static double CDbl(object o)
    {
      return RDLUtil.ConvertToDouble(o);
    }

    public static object DirectCast(object o, string typeName)
    {
      throw new NotImplementedException();
    }

    public static object TryCast(object o, string typeName)
    {
      throw new NotImplementedException();
    }

    public static object CType(object o, string typeName)
    {
      throw new NotImplementedException();
    }

    public static Type GetType(string typeName)
    {
      throw new NotImplementedException();
    }
  }
}
