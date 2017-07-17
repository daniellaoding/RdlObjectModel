using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal static class PropertyGridUtils
  {
    public static T GetSingleItemFromContext<T>(ITypeDescriptorContext context) where T : class
    {
      if (context == null)
        return default (T);
      return !IsMultipleItemContext(context) ? context.Instance as T : ((IList) context.Instance)[0] as T;
    }

    public static bool IsMultipleItemContext(ITypeDescriptorContext context)
    {
      if (context != null)
        return context.Instance is IList;
      return false;
    }

    public static List<string> SplitCommaSeparatedExpr(string expression, CultureInfo culture)
    {
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      char ch1 = culture.TextInfo.ListSeparator[0];
      ExpressionStatus expressionStatus = ExpressionStatus.Normal;
      int num = 0;
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      int length = expression.Length;
      for (int index = 0; index < length; ++index)
      {
        char ch2 = expression[index];
        switch (expressionStatus)
        {
          case ExpressionStatus.Normal:
            if (ch2 == ch1)
            {
              stringList.Add(stringBuilder.ToString().Trim());
              stringBuilder = new StringBuilder();
              break;
            }
            if (ch2 == 34)
            {
              stringBuilder.Append(ch2);
              if (index < length - 1)
              {
                if (expression[index + 1] == 34)
                {
                  stringBuilder.Append(expression[index + 1]);
                  ++index;
                  break;
                }
                expressionStatus = ExpressionStatus.Quotation;
                break;
              }
              break;
            }
            if (ch2 == 40)
            {
              stringBuilder.Append(ch2);
              ++num;
              expressionStatus = ExpressionStatus.Brace;
              break;
            }
            stringBuilder.Append(ch2);
            break;
          case ExpressionStatus.Quotation:
            stringBuilder.Append(ch2);
            if (ch2 == 34)
            {
              expressionStatus = ExpressionStatus.Normal;
              break;
            }
            break;
          case ExpressionStatus.Brace:
            stringBuilder.Append(ch2);
            if (ch2 == 34)
            {
              if (index < length - 1)
              {
                if (expression[index + 1] == 34)
                {
                  stringBuilder.Append(expression[index + 1]);
                  ++index;
                  break;
                }
                expressionStatus = ExpressionStatus.BraceQuotation;
                break;
              }
              break;
            }
            if (ch2 == 40)
            {
              ++num;
              break;
            }
            if (ch2 == 41)
            {
              --num;
              if (num == 0)
              {
                expressionStatus = ExpressionStatus.Normal;
                break;
              }
              break;
            }
            break;
          case ExpressionStatus.BraceQuotation:
            stringBuilder.Append(ch2);
            if (ch2 == 34)
            {
              expressionStatus = ExpressionStatus.Brace;
              break;
            }
            break;
        }
      }
      if (stringBuilder.Length > 0)
        stringList.Add(stringBuilder.ToString().Trim());
      return stringList;
    }

    private enum ExpressionStatus
    {
      Normal,
      Quotation,
      Brace,
      BraceQuotation,
    }
  }
}
