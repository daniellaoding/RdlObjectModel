using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class Lexer
  {
    private TokenList tokens;
    private readonly CharReader textRun;
    private bool bFinalPassNeeded;

    internal Lexer(TextReader file)
    {
      tokens = new TokenList();
      textRun = new CharReader(file);
    }

    internal TokenList Lex()
    {
      for (ExpressionToken ExpressionToken = ReadToken(); ExpressionToken != null; ExpressionToken = ReadToken())
        tokens.Add(ExpressionToken);
      tokens.Add(new ExpressionToken(TokenTypes.EOF, null, textRun.Line, textRun.Column, textRun.Column));
      FinalPass();
      return tokens;
    }

    private void FinalPass()
    {
      if (!bFinalPassNeeded)
        return;
      TokenList tokenList = new TokenList();
      ExpressionToken ExpressionToken = tokens.Extract();
      do
      {
        if (ExpressionToken._TokenType == TokenTypes.LESSTHAN && tokens.Peek()._TokenType == TokenTypes.GREATERTHAN)
        {
          tokens.Extract();
          tokenList.Add(new ExpressionToken(TokenTypes.NOTEQUAL));
        }
        else if (ExpressionToken._TokenType == TokenTypes.LESSTHAN && tokens.Peek()._TokenType == TokenTypes.LESSTHAN)
        {
          tokens.Extract();
          tokenList.Add(new ExpressionToken(TokenTypes.SHIFTLEFT));
        }
        else if (ExpressionToken._TokenType == TokenTypes.GREATERTHAN && tokens.Peek()._TokenType == TokenTypes.GREATERTHAN)
        {
          tokens.Extract();
          tokenList.Add(new ExpressionToken(TokenTypes.SHIFTRIGHT));
        }
        else
          tokenList.Add(ExpressionToken);
        ExpressionToken = tokens.Extract();
      }
      while (ExpressionToken._TokenType != TokenTypes.EOF);
      tokenList.Add(ExpressionToken);
      tokens = tokenList;
    }

    private ExpressionToken ReadToken()
    {
      while (!textRun.FileEnd())
      {
        char ch = textRun.Get();
        if (!char.IsWhiteSpace(ch))
        {
          switch (ch)
          {
            case '\\':
              return new ExpressionToken(TokenTypes.BACKWARDSLASH, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '^':
              return new ExpressionToken(TokenTypes.EXP, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '{':
              return new ExpressionToken(TokenTypes.LCURLY, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '}':
              return new ExpressionToken(TokenTypes.RCURLY, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '“':
            case '”':
            case '"':
              return ProcessQuote();
            case '!':
              return new ExpressionToken(TokenTypes.BANG, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '#':
              return ProcessDateTime(ch);
            case '&':
              return new ExpressionToken(TokenTypes.CONCATENATE, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '(':
              return new ExpressionToken(TokenTypes.LPAREN, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case ')':
              return new ExpressionToken(TokenTypes.RPAREN, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '*':
              return new ExpressionToken(TokenTypes.STAR, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '+':
              return new ExpressionToken(TokenTypes.PLUS, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case ',':
              return new ExpressionToken(TokenTypes.COMMA, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '-':
              return new ExpressionToken(TokenTypes.MINUS, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '/':
              if (textRun.Peek != 42)
                return new ExpressionToken(TokenTypes.FORWARDSLASH, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
              int num1 = textRun.Get();
              ProcessComment();
              continue;
            case '<':
              bFinalPassNeeded = true;
              if (textRun.Peek == 61)
              {
                int num2 = textRun.Get();
                return new ExpressionToken(TokenTypes.LESSTHANOREQUAL, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
              }
              if (textRun.Peek == 62)
              {
                int num2 = textRun.Get();
                return new ExpressionToken(TokenTypes.NOTEQUAL, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
              }
              if (textRun.Peek != 60)
                return new ExpressionToken(TokenTypes.LESSTHAN, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
              int num3 = textRun.Get();
              return new ExpressionToken(TokenTypes.SHIFTLEFT, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '=':
              return new ExpressionToken(TokenTypes.EQUAL, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            case '>':
              bFinalPassNeeded = true;
              if (textRun.Peek == 61)
              {
                int num2 = textRun.Get();
                return new ExpressionToken(TokenTypes.GREATERTHANOREQUAL, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
              }
              if (textRun.Peek != 62)
                return new ExpressionToken(TokenTypes.GREATERTHAN, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
              int num4 = textRun.Get();
              return new ExpressionToken(TokenTypes.SHIFTRIGHT, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
            default:
              if (char.IsDigit(ch))
                return ProcessNumber(ch);
              if (ch == 46)
              {
                if (char.IsDigit(textRun.Peek))
                  return ProcessNumber(ch);
                return new ExpressionToken(TokenTypes.PERIOD, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
              }
              if (char.IsLetter(ch) || ch == 95)
                return ProcessWord(ch);
              return new ExpressionToken(TokenTypes.OTHER, ch.ToString(), textRun.Line, textRun.Column, textRun.Column);
          }
        }
      }
      return null;
    }

    private void ProcessComment()
    {
      while (!textRun.FileEnd())
      {
        if (textRun.Get() == 42 && textRun.Peek == 47)
        {
          int num = textRun.Get();
          return;
        }
      }
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnterminatedComment", "Lexer", 0, 0);
    }

    private ExpressionToken ProcessQuote()
    {
      int line = textRun.Line;
      int column = textRun.Column;
      string empty = string.Empty;
      while (!textRun.FileEnd())
      {
        char ch = textRun.Get();
        if (IsDoubleQuote(ch))
        {
          if (IsDoubleQuote(textRun.Peek))
          {
            ch = textRun.Get();
          }
          else
          {
            if (empty.Length != 1 || textRun.Peek != 67 && textRun.Peek != 99)
              return new ExpressionToken(TokenTypes.QUOTE, empty, line, column, textRun.Column);
            int num = textRun.Get();
            return new ExpressionToken(TokenTypes.CHAR, empty, line, column, textRun.Column);
          }
        }
        empty += ch.ToString();
      }
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnterminatedString", "Lexer", column - 2, column - 1);
    }

    private bool IsDoubleQuote(char ch)
    {
      if (ch != 34 && ch != 8220)
        return ch == 8221;
      return true;
    }

    private ExpressionToken ProcessWord(char ch)
    {
      int line = textRun.Line;
      int column = textRun.Column;
      string str = ch.ToString();
      while (!textRun.FileEnd() && (char.IsLetterOrDigit(textRun.Peek) || textRun.Peek == 95))
        str += (string) (object) textRun.Get();
      switch (str.ToUpperInvariant())
      {
        case "MOD":
          return new ExpressionToken(TokenTypes.MODULUS, str, textRun.Line, textRun.Column, textRun.Column);
        case "LIKE":
          return new ExpressionToken(TokenTypes.LIKE, str, textRun.Line, textRun.Column, textRun.Column);
        case "IS":
          return new ExpressionToken(TokenTypes.IS, str, textRun.Line, textRun.Column, textRun.Column);
        case "ISNOT":
          return new ExpressionToken(TokenTypes.IS, str, textRun.Line, textRun.Column, textRun.Column);
        case "TYPEOF":
          return new ExpressionToken(TokenTypes.TYPEOF, str, textRun.Line, textRun.Column, textRun.Column);
        case "NOT":
          return new ExpressionToken(TokenTypes.NOT, str, textRun.Line, textRun.Column, textRun.Column);
        case "AND":
          return new ExpressionToken(TokenTypes.AND, str, textRun.Line, textRun.Column, textRun.Column);
        case "ANDALSO":
          return new ExpressionToken(TokenTypes.ANDALSO, str, textRun.Line, textRun.Column, textRun.Column);
        case "OR":
          return new ExpressionToken(TokenTypes.OR, str, textRun.Line, textRun.Column, textRun.Column);
        case "ORELSE":
          return new ExpressionToken(TokenTypes.ORELSE, str, textRun.Line, textRun.Column, textRun.Column);
        case "XOR":
          return new ExpressionToken(TokenTypes.XOR, str, textRun.Line, textRun.Column, textRun.Column);
        case "TRUE":
          return new ExpressionToken(TokenTypes.TRUE, str, textRun.Line, textRun.Column, textRun.Column);
        case "FALSE":
          return new ExpressionToken(TokenTypes.FALSE, str, textRun.Line, textRun.Column, textRun.Column);
        case "NEW":
          return new ExpressionToken(TokenTypes.NEW, str, textRun.Line, textRun.Column, textRun.Column);
        default:
          return new ExpressionToken(TokenTypes.IDENTIFIER, str, line, column, textRun.Column);
      }
    }

    private ExpressionToken ProcessDateTime(char ch)
    {
      int line = textRun.Line;
      int column = textRun.Column;
      ConsumeWhiteSpace();
      int firstNumber = ReadIntegerOnly(column, 1, 2);
      int month = 1;
      int day = 1;
      int year = 1;
      int hour = 0;
      int minute = 0;
      int second = 0;
      ch = textRun.Peek;
      if (ch == 45 || ch == 47)
      {
        char ch1 = ch;
        int num = textRun.Get();
        month = firstNumber;
        day = ReadIntegerOnly(column, 1, 2);
        if (textRun.Get() != ch1)
          throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, textRun.Column);
        year = ReadIntegerOnly(column, 4, 4);
        ConsumeWhiteSpace();
        if (textRun.Peek != 35 && !ReadTime(ReadIntegerOnly(column, 1, 2), column, out hour, out minute, out second))
          throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, textRun.Column);
      }
      else if (!ReadTime(firstNumber, column, out hour, out minute, out second))
        throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, textRun.Column);
      ConsumeWhiteSpace();
      if (textRun.Get() != 35)
        throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, textRun.Column);
      return new ExpressionToken(TokenTypes.DATETIME, new DateTime(year, month, day, hour, minute, second, CultureInfo.InvariantCulture.Calendar).ToString(CultureInfo.InvariantCulture));
    }

    private bool ReadTime(int firstNumber, int startCol, out int hour, out int minute, out int second)
    {
      hour = firstNumber;
      minute = 0;
      second = 0;
      char peek = textRun.Peek;
      bool flag = false;
      if (peek == 58)
      {
        int num1 = textRun.Get();
        minute = ReadIntegerOnly(startCol, 1, 2);
        if (textRun.Peek == 58)
        {
          int num2 = textRun.Get();
          second = ReadIntegerOnly(startCol, 1, 2);
        }
      }
      else
        flag = true;
      ConsumeWhiteSpace();
      switch (textRun.Peek)
      {
        case 'A':
        case 'a':
          int num3 = textRun.Get();
          switch (textRun.Get())
          {
            case 'M':
            case 'm':
              if (hour > 12)
                return false;
              if (hour == 12)
              {
                hour = 0;
                break;
              }
              break;
            default:
              return false;
          }
	        break;
        case 'P':
        case 'p':
          int num4 = textRun.Get();
          switch (textRun.Get())
          {
            case 'M':
            case 'm':
              if (hour > 12)
                return false;
              hour += 12;
              break;
            default:
              return false;
          }
	        break;
        default:
          if (flag)
            return false;
          break;
      }
      return true;
    }

    private bool ConsumeWhiteSpace()
    {
      bool flag = false;
      while (!textRun.FileEnd() && char.IsWhiteSpace(textRun.Peek))
      {
        flag = true;
        int num = textRun.Get();
      }
      return flag;
    }

    private int ReadIntegerOnly(int startCol, int minChars, int maxChars)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num1 = 0;
      while (!textRun.FileEnd())
      {
        char peek = textRun.Peek;
        if (char.IsDigit(peek))
        {
          int num2 = textRun.Get();
          stringBuilder.Append(peek);
          ++num1;
        }
        else
          break;
      }
      int result;
      if (num1 < minChars || num1 > maxChars || !int.TryParse(stringBuilder.ToString(), out result))
        throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", startCol, textRun.Column);
      return result;
    }

    private ExpressionToken ProcessNumber(char ch)
    {
      int line = textRun.Line;
      int column = textRun.Column;
      bool flag1 = false;
      TokenTypes tokenType = TokenTypes.EOF;
      string s = ch.ToString();
      bool flag2 = ch == 46;
      while (!textRun.FileEnd())
      {
        char peek1 = textRun.Peek;
        char upperInvariant = char.ToUpperInvariant(peek1);
        if (!char.IsWhiteSpace(peek1))
        {
          if (char.IsDigit(peek1))
          {
            s += (string) (object) textRun.Get();
          }
          else
          {
            if (!flag2 && !flag1)
            {
              if (upperInvariant == 83)
              {
                tokenType = TokenTypes.SHORT;
                int num = textRun.Get();
                break;
              }
              if (upperInvariant == 73 || upperInvariant == 37)
              {
                tokenType = TokenTypes.INTEGER;
                int num = textRun.Get();
                break;
              }
              if (upperInvariant == 76 || upperInvariant == 38)
              {
                tokenType = TokenTypes.LONG;
                int num = textRun.Get();
                break;
              }
              if (upperInvariant == 85)
              {
                switch (char.ToUpperInvariant(textRun.Peek2))
                {
                  case 'S':
                    tokenType = TokenTypes.USHORT;
                    int num1 = textRun.Get();
                    int num2 = textRun.Get();
                    goto label_34;
                  case 'I':
                    tokenType = TokenTypes.UINTEGER;
                    int num3 = textRun.Get();
                    int num4 = textRun.Get();
                    goto label_34;
                  case 'L':
                    tokenType = TokenTypes.ULONG;
                    int num5 = textRun.Get();
                    int num6 = textRun.Get();
                    goto label_34;
                }
              }
            }
            if (upperInvariant == 68 || upperInvariant == 64)
            {
              tokenType = TokenTypes.DECIMAL;
              int num = textRun.Get();
              break;
            }
            if (upperInvariant == 82 || upperInvariant == 35)
            {
              tokenType = TokenTypes.DOUBLE;
              int num = textRun.Get();
              break;
            }
            if (upperInvariant == 70)
            {
              tokenType = TokenTypes.SINGLE;
              int num = textRun.Get();
              break;
            }
            if (upperInvariant == 33)
            {
              char peek2 = textRun.Peek2;
              if (!char.IsLetter(peek2) && peek2 != 95)
              {
                tokenType = TokenTypes.SINGLE;
                int num = textRun.Get();
                break;
              }
              break;
            }
            if (upperInvariant == 69 && !flag1)
            {
              string str = s + textRun.Get();
              char peek2 = textRun.Peek;
              if (char.IsDigit(peek2) || peek2 == 45 || peek2 == 43)
              {
                flag1 = true;
                if (!char.IsDigit(peek2))
                {
                  s = str + textRun.Get();
                  if (char.IsDigit(textRun.Peek))
                    continue;
                }
                else
                {
                  s = str + textRun.Get();
                  continue;
                }
              }
              throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidConstant", "Lexer", column - 2, column - 1);
            }
            if (!flag2 && !flag1 && (peek1 == 46 && char.IsDigit(textRun.Peek2)))
            {
              flag2 = true;
              s += (string) (object) textRun.Get();
            }
            else
              break;
          }
        }
        else
          break;
      }
label_34:
      if (tokenType == TokenTypes.EOF)
      {
        if (flag1 || flag2)
        {
          tokenType = TokenTypes.DOUBLE;
        }
        else
        {
          tokenType = TokenTypes.INTEGER;
          int result;
          if (!int.TryParse(s, out result))
            tokenType = TokenTypes.LONG;
        }
      }
      return new ExpressionToken(tokenType, s, line, column, textRun.Column);
    }
  }
}
