using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Common
{
  internal static class StringUtil
  {
    public static readonly string ClsCompliantIdentifierPattern = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*";
    private static readonly Regex m_digitSuffixRegex = new Regex("(?<suffix>\\d{1,9})$");
    private const string SuffixGroup = "suffix";

    public static int? GetDigitSuffix(string value)
    {
      if (!string.IsNullOrEmpty(value))
      {
        Match match = m_digitSuffixRegex.Match(value);
        if (match.Success)
          return new int?(int.Parse(match.Groups["suffix"].Value, CultureInfo.InvariantCulture));
      }
      return new int?();
    }

    public static string SetDigitSuffix(string value, int suffix)
    {
      int? digitSuffix = GetDigitSuffix(value);
      string replacement = suffix.ToString(CultureInfo.InvariantCulture);
      if (!digitSuffix.HasValue)
        return value + replacement;
      return m_digitSuffixRegex.Replace(value, replacement);
    }

    public static string IncrementDigitSuffix(string value, int defaultSuffix)
    {
      int? digitSuffix = GetDigitSuffix(value);
      if (!digitSuffix.HasValue)
        return SetDigitSuffix(value, defaultSuffix);
      return SetDigitSuffix(value, digitSuffix.Value + 1);
    }

    public static string IncrementDigitSuffix(string value)
    {
      return IncrementDigitSuffix(value, 2);
    }

    public static string TrimToMaxLength(string value, int maxLength)
    {
      value = value ?? string.Empty;
      if (value.Length <= maxLength)
        return value;
      Match match = m_digitSuffixRegex.Match(value);
      if (!match.Success)
        return value.Substring(0, maxLength);
      int length = maxLength - match.Groups["suffix"].Length;
      if (length < 0)
        throw new ArgumentOutOfRangeException("maxLength");
      return value.Substring(0, length) + match.Groups["suffix"].Value;
    }

    public static string RemoveAmpersandEllipsis(string text)
    {
      return RemoveEllipsis(RemoveAmpersand(text));
    }

    public static string RemoveAmpersand(string text)
    {
      if (text != null)
      {
        int startIndex = text.IndexOf('&');
        if (startIndex >= 0)
          return text.Remove(startIndex, 1);
      }
      return text;
    }

    public static string RemoveEllipsis(string text)
    {
      if (text != null)
      {
        int startIndex = text.Length - 3;
        if (startIndex > 0 && text[startIndex] == 46 && (text[startIndex + 1] == 46 && text[startIndex + 2] == 46))
          return text.Remove(startIndex);
      }
      return text;
    }

    public static string RemoveLastSubstring(string text, char separator)
    {
      int length = text.LastIndexOf(separator);
      if (length < 0)
        return text;
      return text.Substring(0, length);
    }

    public static string GetLastSubstring(string text, char separator)
    {
      int num = text.LastIndexOf(separator);
      if (num < 0)
        return text;
      return text.Substring(num + 1);
    }

    public static string FormatInvariant(string format, params object[] args)
    {
      return string.Format(CultureInfo.InvariantCulture, format, args);
    }

    public static string Join(string separator, IList<string> strings)
    {
      return Join(separator, strings, 0, strings.Count);
    }

    public static string Join(string separator, IList<string> strings, int startIndex, int count)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex");
      if (count < 0 || startIndex + count > strings.Count)
        throw new ArgumentOutOfRangeException("count");
      if (count == 0)
        return string.Empty;
      int num1 = 0;
      int length = separator.Length;
      for (int index = 0; index < count; ++index)
        num1 += strings[index + startIndex].Length + length;
      int num2 = num1 - length;
      if (num2 == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder(num2, num2);
      stringBuilder.Append(strings[startIndex]);
      for (int index = 1; index < count; ++index)
      {
        stringBuilder.Append(separator);
        stringBuilder.Append(strings[index + startIndex]);
      }
      return stringBuilder.ToString();
    }

    public static string NormalizeLineBreaks(string s)
    {
      if (string.IsNullOrEmpty(s))
        return string.Empty;
      StringReader stringReader = new StringReader(s);
      StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
      bool flag = true;
      string str;
      while ((str = stringReader.ReadLine()) != null)
      {
        if (flag)
          flag = false;
        else
          stringWriter.WriteLine();
        stringWriter.Write(str);
      }
      return stringWriter.ToString();
    }

    public static bool EqualsTrimmedWithNormalizedLineBreaks(string s1, string s2)
    {
      if (s1 != null)
        s1 = s1.Trim();
      if (s2 != null)
        s2 = s2.Trim();
      return NormalizeLineBreaks(s1) == NormalizeLineBreaks(s2);
    }

    public static string BuildErrorMessage(Exception e)
    {
      if (e.InnerException != null)
        return BuildErrorMessage(e.InnerException) + "\r\n----------------------------\r\n" + e.Message;
      return e.Message;
    }

    public static string MakeCLSNameFromFilename(string path)
    {
      return GetClsCompliantIdentifier(Path.GetFileNameWithoutExtension(path), null);
    }

    public static StringComparer GetClsCompliantComparer()
    {
      return StringComparer.Ordinal;
    }

    public static int CompareClsCompliantIdentifiers(string s1, string s2)
    {
      return CompareClsCompliantIdentifiers(s1, s2, false);
    }

    public static int CompareClsCompliantIdentifiers(string s1, string s2, bool ignoreCase)
    {
      StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
      return string.Compare(s1, s2, comparisonType);
    }

    public static string GetClsCompliantIdentifier(string candidate, string defaultIdentifier)
    {
      StringBuilder stringBuilder = new StringBuilder();
      candidate = candidate ?? string.Empty;
      if (candidate.Length == 0 || !IsClsCompliantIdentifierChar(candidate[0], true))
        stringBuilder.Append(defaultIdentifier);
      for (int index = 0; index < candidate.Length; ++index)
      {
        if (IsClsCompliantIdentifierChar(candidate[index], stringBuilder.Length == 0))
          stringBuilder.Append(candidate[index]);
      }
      return stringBuilder.ToString();
    }

    public static bool IsClsCompliantIdentifier(string s)
    {
      if (string.IsNullOrEmpty(s) || !IsClsCompliantIdentifierChar(s[0], true))
        return false;
      for (int index = 1; index < s.Length; ++index)
      {
        if (!IsClsCompliantIdentifierChar(s[index], false))
          return false;
      }
      return true;
    }

    public static bool IsClsCompliantIdentifierChar(char c, bool firstChar)
    {
      switch (char.GetUnicodeCategory(c))
      {
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.TitlecaseLetter:
        case UnicodeCategory.ModifierLetter:
        case UnicodeCategory.OtherLetter:
        case UnicodeCategory.LetterNumber:
          return true;
        case UnicodeCategory.NonSpacingMark:
        case UnicodeCategory.SpacingCombiningMark:
        case UnicodeCategory.DecimalDigitNumber:
        case UnicodeCategory.Format:
        case UnicodeCategory.ConnectorPunctuation:
          return !firstChar;
        default:
          return false;
      }
    }

    public static string GenerateUniqueName(string candidate, string baseName, Predicate<string> nameExists)
    {
      if (baseName == null)
        throw new ArgumentNullException("baseName");
      if (!string.IsNullOrEmpty(candidate))
      {
        if (!nameExists(candidate))
          return candidate;
        baseName = candidate;
      }
      int num = 1;
      while (true)
      {
        candidate = baseName + num;
        if (nameExists(candidate))
          ++num;
        else
          break;
      }
      return candidate;
    }

    public static string SplitName(string name)
    {
      return Regex.Replace(name, "(\\p{Ll})(\\p{Lu})|_+", "$1 $2");
    }

    public static IEqualityComparer<string> CreateComparer(CultureInfo culture, CompareOptions compareOptions)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      if (compareOptions == CompareOptions.None || compareOptions == CompareOptions.IgnoreCase)
        return StringComparer.Create(culture, compareOptions == CompareOptions.IgnoreCase);
      return new StringComparerWithOptions(culture, compareOptions);
    }

    private sealed class StringComparerWithOptions : EqualityComparer<string>
    {
      private readonly CompareInfo m_compareInfo;
      private readonly CompareOptions m_options;

      internal StringComparerWithOptions(CultureInfo culture, CompareOptions compareOptions)
      {
        m_compareInfo = culture.CompareInfo;
        m_options = compareOptions;
      }

      public override bool Equals(string x, string y)
      {
        return m_compareInfo.Compare(x, y, m_options) == 0;
      }

      public override int GetHashCode(string obj)
      {
        return m_compareInfo.GetSortKey(obj, m_options).GetHashCode();
      }
    }
  }
}
