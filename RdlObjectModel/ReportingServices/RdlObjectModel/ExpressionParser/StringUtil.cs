using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal static class StringUtil
  {
    internal static StringComparer CaseSensitiveComparer => StringComparer.Ordinal;

	  internal static StringComparer CaseInsensitiveComparer => StringComparer.OrdinalIgnoreCase;

	  internal static bool EqualsIgnoreCase(string str1, string str2)
    {
      return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
    }

    internal static bool EqualsCaseSensitive(string str1, string str2)
    {
      return string.Equals(str1, str2, StringComparison.Ordinal);
    }
  }
}
