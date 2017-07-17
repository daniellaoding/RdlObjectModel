using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class ExpressionParser
  {
    private static List<Type> TypeList = new List<Type>();
	  private TokenList tokens;
    private ExpressionToken curToken;
    private ExpressionToken prevToken;
    private ExpressionToken olderToken;
    private bool _InTypeOf;
    private readonly EnvironmentContext m_environment;

    internal List<IInternalExpression> ObjectDependencyList { get; private set; }

	  internal ExpressionParser(EnvironmentContext environment)
    {
      m_environment = environment;
    }

    internal IInternalExpression Parse(string expression)
    {
      ObjectDependencyList = new List<IInternalExpression>();
      if (expression != null)
        expression.Trim();
      if (expression.Trim() == "" || expression.Trim().Substring(0, 1) != "=")
        return new ConstantNonExpression(expression);
      return ParseExpr(new StringReader(expression)) ?? new ConstantNonExpression(expression);
    }

    private IInternalExpression ParseExpr(TextReader reader)
    {
      IInternalExpression result = null;
      tokens = new Lexer(reader).Lex();
      if (tokens.Peek()._TokenType == TokenTypes.EQUAL)
      {
        GetNextToken();
        GetNextToken();
        if (curToken._TokenType == TokenTypes.EOF)
          RDLExceptionHelper.WriteExpectedOperator("RDLEngine.Error.RDLObjects.Expression.ConstantOrIdentifier", curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
        MatchExprXor(out result);
      }
      if (curToken._TokenType != TokenTypes.EOF)
        RDLExceptionHelper.WriteEndExpected(curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
      return result;
    }

    private void MatchExprXor(out IInternalExpression result)
    {
      MatchExprXor(false, out result);
    }

    private void MatchExprXor(bool inTypeOf, out IInternalExpression result)
    {
      bool inTypeOf1 = _InTypeOf;
      _InTypeOf = inTypeOf;
      IInternalExpression result1;
      MatchExprOrOrElse(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.XOR)
      {
        GetNextToken();
        if (curToken._TokenType == TokenTypes.EOF)
          RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", curToken.StartColumn, curToken.EndColumn);
        IInternalExpression result2;
        MatchExprOrOrElse(out result2);
        if (result2 == null)
          RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", curToken.StartColumn, curToken.EndColumn);
        if (tokenType == TokenTypes.XOR)
          result = new FunctionLogicXor(result1, result2);
        result1 = result;
      }
      _InTypeOf = inTypeOf1;
    }

    private void MatchExprOrOrElse(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprAndAndAlso(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.OR || tokenType == TokenTypes.ORELSE)
      {
        GetNextToken();
        if (curToken._TokenType == TokenTypes.EOF)
          RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", curToken.StartColumn, curToken.EndColumn);
        IInternalExpression result2;
        MatchExprAndAndAlso(out result2);
        if (result2 == null)
          RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", curToken.StartColumn, curToken.EndColumn);
        switch (tokenType)
        {
          case TokenTypes.OR:
            result = new FunctionLogicOr(result1, result2);
            break;
          case TokenTypes.ORELSE:
            result = new FunctionLogicOrElse(result1, result2);
            break;
        }
        result1 = result;
      }
    }

    private void MatchExprAndAndAlso(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprNot(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.AND || tokenType == TokenTypes.ANDALSO)
      {
        GetNextToken();
        if (curToken._TokenType == TokenTypes.EOF)
          RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", curToken.StartColumn, curToken.EndColumn);
        IInternalExpression result2;
        MatchExprNot(out result2);
        if (result2 == null)
          RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", curToken.StartColumn, curToken.EndColumn);
        switch (tokenType)
        {
          case TokenTypes.AND:
            result = new FunctionLogicAnd(result1, result2);
            break;
          case TokenTypes.ANDALSO:
            result = new FunctionLogicAndAlso(result1, result2);
            break;
        }
        result1 = result;
      }
    }

    private void MatchExprNot(out IInternalExpression result)
    {
      TokenTypes tokenType = curToken._TokenType;
      if (curToken._TokenType == TokenTypes.EOF)
        RDLExceptionHelper.WriteInvalidExpression(curToken.EndColumn);
      if (tokenType == TokenTypes.NOT)
        GetNextToken();
      if (curToken._TokenType == TokenTypes.EOF)
        RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", curToken.StartColumn, curToken.EndColumn);
      MatchExprRelational(out result);
      if (result == null)
        RDLExceptionHelper.WriteExpectedOperand("NOT", "Boolean", curToken.StartColumn, curToken.EndColumn);
      if (tokenType != TokenTypes.NOT)
        return;
      result = new FunctionLogicNot(result);
    }

    private void MatchExprRelationalTypeOf(out IInternalExpression result)
    {
      if (curToken._TokenType == TokenTypes.TYPEOF)
      {
        GetNextToken();
        IInternalExpression result1;
        MatchExprXor(true, out result1);
        if (curToken._TokenType != TokenTypes.IS)
          RDLExceptionHelper.WriteExpectedOperand("IS", "Type", prevToken.StartColumn, prevToken.EndColumn);
        GetNextToken();
        IInternalExpression result2;
        if (!MatchTypeName(m_environment, out result2))
          RDLExceptionHelper.WriteTypeNotFound(curToken._Value, curToken);
        result = new FunctionRelationalTypeOf(result1, (FunctionType) result2);
      }
      else
        MatchExprNew(out result);
    }

    private void MatchExprRelational(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprShift(out result1);
      result = result1;
      TokenTypes tokenType;
      while (((tokenType = curToken._TokenType) == TokenTypes.EQUAL || tokenType == TokenTypes.NOTEQUAL || (tokenType == TokenTypes.GREATERTHAN || tokenType == TokenTypes.GREATERTHANOREQUAL) || (tokenType == TokenTypes.LESSTHAN || tokenType == TokenTypes.LESSTHANOREQUAL || (tokenType == TokenTypes.LIKE || tokenType == TokenTypes.IS)) || tokenType == TokenTypes.ISNOT) && (tokenType != TokenTypes.IS || !_InTypeOf))
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprShift(out result2);
        switch (tokenType)
        {
          case TokenTypes.EQUAL:
            result = new FunctionRelationalEqual(result1, result2);
            break;
          case TokenTypes.GREATERTHAN:
            result = new FunctionRelationalGreaterThan(result1, result2);
            break;
          case TokenTypes.GREATERTHANOREQUAL:
            result = new FunctionRelationalGreaterThanEqual(result1, result2);
            break;
          case TokenTypes.IS:
            result = new FunctionRelationalIs(result1, result2);
            break;
          case TokenTypes.ISNOT:
            result = new FunctionRelationalIsNot(result1, result2);
            break;
          case TokenTypes.LESSTHAN:
            result = new FunctionRelationalLessThan(result1, result2);
            break;
          case TokenTypes.LESSTHANOREQUAL:
            result = new FunctionRelationalLessThanEqual(result1, result2);
            break;
          case TokenTypes.LIKE:
            result = new FunctionRelationalLike(result1, result2);
            break;
          case TokenTypes.NOTEQUAL:
            result = new FunctionRelationalNotEqual(result1, result2);
            break;
        }
        result1 = result;
      }
    }

    private void MatchExprShift(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprConcatenate(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.SHIFTLEFT || tokenType == TokenTypes.SHIFTRIGHT)
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprConcatenate(out result2);
        switch (tokenType)
        {
          case TokenTypes.SHIFTLEFT:
            result = new FunctionShiftLeft(result1, result2);
            break;
          case TokenTypes.SHIFTRIGHT:
            result = new FunctionShiftRight(result1, result2);
            break;
        }
        result1 = result;
      }
    }

    private void MatchExprConcatenate(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprAddSub(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.CONCATENATE)
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprAddSub(out result2);
        if (tokenType == TokenTypes.CONCATENATE)
          result = new FunctionConcatenate(result1, result2, TokenTypes.CONCATENATE);
        result1 = result;
      }
    }

    private void MatchExprAddSub(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprMod(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.PLUS || tokenType == TokenTypes.MINUS)
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprMod(out result2);
        switch (tokenType)
        {
          case TokenTypes.MINUS:
            result = new FunctionMinus(result1, result2);
            break;
          case TokenTypes.PLUS:
            result = new FunctionPlus(result1, result2);
            break;
        }
        result1 = result;
      }
    }

    private void MatchExprMod(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprIntDiv(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.MODULUS)
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprIntDiv(out result2);
        if (tokenType == TokenTypes.MODULUS)
          result = new FunctionModulus(result1, result2);
        result1 = result;
      }
    }

    private void MatchExprIntDiv(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprMultDiv(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.BACKWARDSLASH)
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprMultDiv(out result2);
        if (tokenType == TokenTypes.BACKWARDSLASH)
          result = new FunctionIntDiv(result1, result2);
        result1 = result;
      }
    }

    private void MatchExprMultDiv(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprUnary(out result1);
      result = result1;
      TokenTypes tokenType;
      while ((tokenType = curToken._TokenType) == TokenTypes.FORWARDSLASH || tokenType == TokenTypes.STAR)
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprUnary(out result2);
        switch (tokenType)
        {
          case TokenTypes.FORWARDSLASH:
            result = new FunctionDiv(result1, result2);
            break;
          case TokenTypes.STAR:
            result = new FunctionMult(result1, result2);
            break;
        }
        result1 = result;
      }
    }

    private void MatchExprUnary(out IInternalExpression result)
    {
      TokenTypes tokenType = curToken._TokenType;
      switch (tokenType)
      {
        case TokenTypes.PLUS:
        case TokenTypes.MINUS:
          GetNextToken();
          break;
      }
      if (curToken._TokenType == TokenTypes.PLUS || curToken._TokenType == TokenTypes.MINUS)
        MatchExprUnary(out result);
      else
        MatchExprExponent(out result);
      if (tokenType == TokenTypes.MINUS)
      {
        result = new FunctionUnaryMinus(result);
      }
      else
      {
        if (tokenType != TokenTypes.PLUS)
          return;
        result = new FunctionUnaryPlus(result);
      }
    }

    private void MatchExprExponent(out IInternalExpression result)
    {
      IInternalExpression result1;
      MatchExprMethodCall(out result1);
      if (curToken._TokenType == TokenTypes.EXP)
      {
        GetNextToken();
        IInternalExpression result2;
        MatchExprUnary(out result2);
        result = new FunctionExp(result1, result2);
      }
      else
        result = result1;
    }

    private void MatchExprMethodCall(out IInternalExpression result)
    {
      MatchExprRelationalTypeOf(out result);
      while (true)
      {
        switch (curToken._TokenType)
        {
          case TokenTypes.BANG:
            GetNextToken();
            if (curToken._TokenType != TokenTypes.IDENTIFIER)
              RDLExceptionHelper.WriteMissingIdentifierForDictionaryOperator(curToken);
            result = new FunctionDictionaryAccessor(result, curToken._Value);
            GetNextToken();
            continue;
          case TokenTypes.LPAREN:
            List<IInternalExpression> args;
            if (!MatchFunctionArgs(out args))
              RDLExceptionHelper.WriteMissingArgumentsForExpression(curToken);
            result = new FunctionDefaultPropertyOrIndexer(result, args);
            continue;
          case TokenTypes.PERIOD:
            GetNextToken();
            result = MatchObjectMethod(result);
            continue;
          default:
            goto label_9;
        }
      }
label_9:;
    }

    private void MatchExprNew(out IInternalExpression result)
    {
      if (curToken._TokenType == TokenTypes.NEW)
      {
        GetNextToken();
        IInternalExpression result1;
        if (!MatchTypeName(m_environment, out result1))
          RDLExceptionHelper.WriteTypeNotFound(curToken._Value, curToken);
        TypeContext typeContext = ((FunctionType) result1).TypeContext;
        if (curToken._TokenType == TokenTypes.PERIOD)
          RDLExceptionHelper.WriteTypeNotFound(((FunctionType) result1).TypeContext.Name + "." + curToken._Value, curToken);
        bool flag = false;
        int rank = 0;
        List<IInternalExpression> args = new List<IInternalExpression>();
        if (curToken._TokenType == TokenTypes.LPAREN)
        {
          GetNextToken();
          while (curToken._TokenType != TokenTypes.RPAREN)
          {
            ++rank;
            if (curToken._TokenType == TokenTypes.COMMA)
            {
              GetNextToken();
              if (args.Count == 0)
              {
                flag = true;
                continue;
              }
            }
            else if (args != null && args.Count > 0)
              RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.COMMA, curToken);
            if (curToken._TokenType == TokenTypes.INTEGER && tokens.Peek()._TokenType == TokenTypes.IDENTIFIER && StringUtil.EqualsIgnoreCase(tokens.Peek()._Value, "To"))
            {
              GetNextToken();
              GetNextToken();
              flag = true;
            }
            IInternalExpression result2;
            MatchExprXor(out result2);
            args.Add(result2);
          }
          ConsumeExpectedToken(TokenTypes.RPAREN);
        }
        while (curToken._TokenType == TokenTypes.LPAREN && (tokens.Peek()._TokenType == TokenTypes.RPAREN || tokens.Peek()._TokenType == TokenTypes.COMMA))
        {
          result1 = new FunctionArrayType(result1, rank);
          flag = true;
          rank = 1;
          GetNextToken();
          while (curToken._TokenType == TokenTypes.COMMA)
          {
            ++rank;
            GetNextToken();
          }
          ConsumeExpectedToken(TokenTypes.RPAREN);
        }
        FunctionArrayInit result3;
        if (MatchArrayInitExpr(out result3))
          flag = true;
        else if (flag)
          RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.LCURLY, curToken);
        if (flag)
        {
          if (!typeContext.AllowNewArray)
            RDLExceptionHelper.WriteInvalidArrayType(typeContext.Name, curToken);
          FunctionArrayType typeExpr = new FunctionArrayType(result1, rank);
          result = new FunctionNewArray(typeExpr, result3);
        }
        else
        {
          if (!typeContext.AllowNew)
            RDLExceptionHelper.WriteInvalidNewType(typeContext.Name, curToken);
          result = new FunctionNewObject((FunctionType) result1, args);
        }
      }
      else
        MatchExprParen(out result);
    }

    private bool MatchArrayInitExpr(out FunctionArrayInit result)
    {
      if (curToken._TokenType == TokenTypes.LCURLY)
      {
        GetNextToken();
        List<IInternalExpression> items = new List<IInternalExpression>();
        while (curToken._TokenType != TokenTypes.RCURLY)
        {
          if (items.Count > 0)
            ConsumeExpectedToken(TokenTypes.COMMA);
          FunctionArrayInit result1;
          if (MatchArrayInitExpr(out result1))
          {
            items.Add(result1);
          }
          else
          {
            IInternalExpression result2;
            MatchExprXor(out result2);
            items.Add(result2);
          }
        }
        ConsumeExpectedToken(TokenTypes.RCURLY);
        result = new FunctionArrayInit(items);
        return true;
      }
      result = null;
      return false;
    }

    private void MatchExprParen(out IInternalExpression result)
    {
      if (curToken._TokenType == TokenTypes.LPAREN)
      {
        GetNextToken();
        MatchExprXor(out result);
        ConsumeExpectedToken(TokenTypes.RPAREN);
        result.Bracketed = true;
      }
      else
        MatchBaseDataType(out result);
    }

    private void MatchBaseDataType(out IInternalExpression result)
    {
      if (MatchIdentifierOrFunction(out result))
        return;
      try
      {
        switch (curToken._TokenType)
        {
          case TokenTypes.LONG:
            result = new ConstantLong(curToken._Value);
            goto label_36;
          case TokenTypes.QUOTE:
            result = new ConstantString(curToken._Value);
            goto label_36;
          case TokenTypes.SHORT:
            result = new ConstantShort(curToken._Value);
            goto label_36;
          case TokenTypes.SINGLE:
            result = new ConstantSingle(curToken._Value);
            goto label_36;
          case TokenTypes.TRUE:
            result = new ConstantBoolean(curToken._Value);
            goto label_36;
          case TokenTypes.UINTEGER:
            result = new ConstantLong(curToken._Value);
            goto label_36;
          case TokenTypes.ULONG:
            result = new ConstantDecimal(curToken._Value);
            goto label_36;
          case TokenTypes.USHORT:
            result = new ConstantInteger(curToken._Value);
            goto label_36;
          case TokenTypes.CHAR:
            result = new ConstantChar(curToken._Value);
            goto label_36;
          case TokenTypes.CONCATENATE:
            ExpressionToken expressionToken = tokens.Peek();
            if (expressionToken._TokenType == TokenTypes.IDENTIFIER && expressionToken._Value.Length > 1)
            {
              string upperInvariant = expressionToken._Value.ToUpperInvariant();
              if (upperInvariant[0] == 72)
              {
                int result1;
                if (int.TryParse(upperInvariant.Substring(1), NumberStyles.AllowHexSpecifier, RDLUtil.GetFormatProvider(false), out result1))
                  result = new ConstantInteger(result1);
                else
                  break;
              }
              else if (upperInvariant[0] == 79)
              {
                for (int index = 1; index < upperInvariant.Length; ++index)
                {
                  char ch = upperInvariant[index];
                  if (ch < 48 || ch > 55)
                    goto label_31;
                }
	            double num;
                if (Double.TryParse(upperInvariant, out num) && num <= int.MaxValue)
                  result = new ConstantInteger((int) num);
                else
                  break;
              }
              else
                break;
              GetNextToken();
              goto label_36;
            }
            else
              break;
          case TokenTypes.DATETIME:
            try
            {
              result = new ConstantDateTime(curToken._Value);
              goto label_36;
            }
            catch (FormatException ex)
            {
              RDLExceptionHelper.WriteInvalidDateTimeLiteral(curToken);
              goto label_36;
            }
          case TokenTypes.DECIMAL:
            result = new ConstantDecimal(curToken._Value);
            goto label_36;
          case TokenTypes.DOUBLE:
            result = new ConstantDouble(curToken._Value);
            goto label_36;
          case TokenTypes.FALSE:
            result = new ConstantBoolean(curToken._Value);
            goto label_36;
          case TokenTypes.IDENTIFIER:
            if (StringUtil.EqualsIgnoreCase(curToken._Value, "Nothing"))
            {
              result = new FunctionNothing();
              goto label_36;
            }
            else
              break;
          case TokenTypes.INTEGER:
            result = new ConstantInteger(curToken._Value);
            goto label_36;
        }
label_31:
        if (curToken._TokenType == TokenTypes.IDENTIFIER)
          RDLExceptionHelper.WriteUnknownIdentifier(curToken._Value, curToken);
        else
          RDLExceptionHelper.WriteExpectedOperator("RDLEngine.Error.RDLObjects.Expression.ConstantOrIdentifier", curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
      }
      catch (OverflowException ex)
      {
        RDLExceptionHelper.WriteOverflow("Integer", curToken._Value, curToken.StartColumn, curToken.EndColumn);
      }
label_36:
      GetNextToken();
    }

    private bool MatchIdentifierOrFunction(out IInternalExpression result)
    {
      if (!MatchRdlFunctionOrCollection(out result))
      {
        LookupContext environment = m_environment;
        if (!MatchTypeName(environment, out result))
          return MatchMemberAccessor(environment, null, out result);
      }
      return true;
    }

    private bool MatchRdlFunctionOrCollection(out IInternalExpression result)
    {
      result = null;
      if (curToken._TokenType != TokenTypes.IDENTIFIER)
        return false;
      string name = curToken._Value;
      ReportObjectModelContext objectModelContext = m_environment.ReportObjectModelContext;
      RdlFunctionDefinition functionDef;
      if (objectModelContext.TryMatchRdlFunction(name, out functionDef))
      {
        GetNextToken();
        List<IInternalExpression> internalExpressionList = MatchRdlFunctionArgs(objectModelContext, functionDef);
        object instance = Activator.CreateInstance(functionDef.NodeType, new object[1]
        {
          internalExpressionList
        });
        result = (IInternalExpression) instance;
        return true;
      }
      switch (name.ToUpperInvariant())
      {
        case "FIELDS":
          result = MatchComplexRdlCollection(objectModelContext.Fields, true);
          if (result != null)
            ObjectDependencyList.Add(result);
          return true;
        case "PARAMETERS":
          result = MatchComplexRdlCollection(objectModelContext.Parameters, false);
          if (result != null)
            ObjectDependencyList.Add(result);
          return true;
        case "REPORTITEMS":
          result = MatchComplexRdlCollection(objectModelContext.ReportItems, false);
          if (result != null)
            ObjectDependencyList.Add(result);
          return true;
        case "DATASOURCES":
          result = MatchComplexRdlCollection(objectModelContext.DataSources, false);
          if (result != null)
            ObjectDependencyList.Add(result);
          return true;
        case "DATASETS":
          result = MatchComplexRdlCollection(objectModelContext.DataSets, false);
          if (result != null)
            ObjectDependencyList.Add(result);
          return true;
        case "VARIABLES":
          result = MatchComplexRdlCollection(objectModelContext.Variables, false);
          return true;
        case "GLOBALS":
          result = MatchSimpleRdlCollection(objectModelContext.Globals);
          return true;
        case "USER":
          result = MatchSimpleRdlCollection(objectModelContext.User);
          return true;
        default:
          return false;
      }
    }

    private IInternalExpression MatchSimpleRdlCollection(ISimpleRdlCollection itemCollection)
    {
      GetNextToken();
      IInternalExpression itemNameExpr;
      IInternalExpression internalExpression;
      if (MatchCollectionAccessor("item", out itemNameExpr))
        internalExpression = itemCollection.CreateCollectionReference(itemNameExpr);
      else if (curToken._TokenType == TokenTypes.PERIOD && tokens.Peek()._TokenType == TokenTypes.IDENTIFIER)
      {
        GetNextToken();
        string str = curToken._Value;
        ISimpleRdlCollection childCollection;
        if (itemCollection.IsPredefinedChildCollection(str, out childCollection))
          internalExpression = MatchSimpleRdlCollection(childCollection);
        else if (itemCollection.IsPredefinedCollectionProperty(str))
        {
          internalExpression = itemCollection.CreatePropertyReference(str);
          GetNextToken();
        }
        else
        {
          RDLExceptionHelper.WriteMethodOrPropertyNotFound(curToken, itemCollection.Name);
          internalExpression = null;
          GetNextToken();
        }
      }
      else
        internalExpression = itemCollection.CreateReference();
      return internalExpression;
    }

    private IInternalExpression MatchComplexRdlCollection(IComplexRdlCollection itemCollection, bool allowLevelTwoCollection)
    {
      GetNextToken();
      IInternalExpression itemNameExpr1;
      IInternalExpression internalExpression1;
      if (MatchCollectionAccessor("item", out itemNameExpr1))
      {
        IInternalExpression itemNameExpr2;
        if (allowLevelTwoCollection && MatchCollectionAccessor("properties", out itemNameExpr2))
          internalExpression1 = itemCollection.CreateReference(itemNameExpr1, itemNameExpr2);
        else if (curToken._TokenType == TokenTypes.PERIOD && tokens.Peek()._TokenType == TokenTypes.IDENTIFIER)
        {
          GetNextToken();
          if (itemCollection.IsPredefinedItemProperty(curToken._Value))
          {
            internalExpression1 = itemCollection.CreateReference(itemNameExpr1, new ConstantString(curToken._Value));
            GetNextToken();
          }
          else if (itemCollection.IsPredefinedItemMethod(curToken._Value))
          {
            string methodName = curToken._Value;
            GetNextToken();
            List<IInternalExpression> args;
            if (!MatchFunctionArgs(out args))
            {
              RDLExceptionHelper.WriteMissingArgumentsForExpression(curToken);
              internalExpression1 = null;
            }
            else
              internalExpression1 = new FunctionMethodOrProperty(new FunctionVariable(itemNameExpr1), methodName, args);
          }
          else
          {
            RDLExceptionHelper.WriteMethodOrPropertyNotFound(curToken, itemCollection.ItemName);
            internalExpression1 = null;
            GetNextToken();
          }
        }
        else
          internalExpression1 = itemCollection.CreateReference(itemNameExpr1);
      }
      else
      {
        if (curToken._TokenType == TokenTypes.PERIOD && tokens.Peek()._TokenType == TokenTypes.IDENTIFIER)
        {
          GetNextToken();
          IInternalExpression internalExpression2;
          if (itemCollection.IsPredefinedCollectionProperty(curToken._Value))
          {
            internalExpression2 = itemCollection.CreateReference(itemNameExpr1, new ConstantString(curToken._Value));
          }
          else
          {
            RDLExceptionHelper.WriteMethodOrPropertyNotFound(curToken, itemCollection.Name);
            internalExpression2 = null;
          }
          GetNextToken();
        }
        internalExpression1 = itemCollection.CreateReference();
      }
      return internalExpression1;
    }

    private bool MatchCollectionAccessor(string indexerPropertyName, out IInternalExpression itemNameExpr)
    {
      itemNameExpr = null;
      switch (curToken._TokenType)
      {
        case TokenTypes.BANG:
          GetNextToken();
          if (curToken._TokenType != TokenTypes.IDENTIFIER)
            RDLExceptionHelper.WriteMissingIdentifierForDictionaryOperator(curToken);
          itemNameExpr = new ConstantString(curToken._Value);
          GetNextToken();
          return true;
        case TokenTypes.LPAREN:
          GetNextToken();
          MatchExprXor(out itemNameExpr);
          ConsumeExpectedToken(TokenTypes.RPAREN);
          return true;
        case TokenTypes.PERIOD:
          ExpressionToken expressionToken = tokens.Peek();
          if (expressionToken._TokenType == TokenTypes.IDENTIFIER && StringUtil.EqualsIgnoreCase(expressionToken._Value, indexerPropertyName))
          {
            GetNextToken();
            string str = curToken._Value;
            GetNextToken();
            ConsumeExpectedToken(TokenTypes.LPAREN);
            MatchExprXor(out itemNameExpr);
            ConsumeExpectedToken(TokenTypes.RPAREN);
            return true;
          }
          break;
      }
      return false;
    }

    private bool MatchTypeName(LookupContext context, out IInternalExpression result)
    {
      result = null;
      if (curToken._TokenType != TokenTypes.IDENTIFIER)
        return false;
      string identifier = curToken._Value;
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      LookupContext subContext;
      while (context.TryMatchSubContext(identifier, out subContext))
      {
        context = subContext;
        if (flag)
        {
          GetNextToken();
          stringBuilder.Append(".");
        }
        stringBuilder.Append(identifier);
        flag = true;
        GetNextToken();
        if (curToken._TokenType == TokenTypes.PERIOD)
        {
          ExpressionToken expressionToken = tokens.Peek();
          if (expressionToken._TokenType != TokenTypes.IDENTIFIER)
            RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.IDENTIFIER, curToken);
          identifier = expressionToken._Value;
        }
      }
      if (!flag)
        return false;
      if (!(context is TypeContext))
      {
        stringBuilder.Append(".");
        stringBuilder.Append(identifier);
        RDLExceptionHelper.WriteTypeNotFound(stringBuilder.ToString(), curToken);
      }
      result = new FunctionType((TypeContext) context);
      return true;
    }

    private bool MatchMemberAccessor(LookupContext context, IInternalExpression callTarget, out IInternalExpression result)
    {
      result = null;
      if (curToken._TokenType != TokenTypes.IDENTIFIER)
        return false;
      string str = curToken._Value;
      MemberContext member;
      if (!context.TryMatchMember(str, out member))
        return false;
      GetNextToken();
      if (callTarget == null)
        callTarget = new FunctionType(member.OwningType);
      switch (member.MemberContextType)
      {
        case MemberContext.MemberContextTypes.Method:
          List<IInternalExpression> args1;
          MatchFunctionArgs(out args1);
          result = new FunctionMethodOrProperty(callTarget, str, args1);
          if (str.ToUpperInvariant() == "CODE")
          {
            ObjectDependencyList.Add(result);
            break;
          }
          break;
        case MemberContext.MemberContextTypes.Field:
          result = new FunctionMemberField(callTarget, str);
          if (str.ToUpperInvariant() == "CODE")
          {
            ObjectDependencyList.Add(result);
            break;
          }
          break;
        case MemberContext.MemberContextTypes.Property:
          List<IInternalExpression> args2;
          MatchFunctionArgs(out args2);
          result = new FunctionMethodOrProperty(callTarget, str, args2);
          if (str.ToUpperInvariant() == "CODE")
          {
            ObjectDependencyList.Add(result);
            break;
          }
          break;
        case MemberContext.MemberContextTypes.Unknown:
          List<IInternalExpression> args3;
          MatchFunctionArgs(out args3);
          result = new FunctionLateBoundAccessor(callTarget, str, args3);
          break;
      }
      return true;
    }

    private List<IInternalExpression> MatchRdlFunctionArgs(ReportObjectModelContext context, RdlFunctionDefinition funcDef)
    {
      ConsumeExpectedToken(TokenTypes.LPAREN);
      List<IInternalExpression> internalExpressionList = new List<IInternalExpression>(funcDef.Args.Length);
      for (int index = 0; index < funcDef.Args.Length; ++index)
      {
        RdlFunctionArg rdlFunctionArg = funcDef.Args[index];
        if (curToken._TokenType == TokenTypes.RPAREN)
        {
          if (rdlFunctionArg.IsRequired)
            RDLExceptionHelper.WriteBadSyntax(funcDef.Name, curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
          else
            break;
        }
        if (internalExpressionList.Count > 0)
          ConsumeExpectedToken(TokenTypes.COMMA);
        IInternalExpression result = null;
        switch (rdlFunctionArg.ArgType)
        {
          case RdlArgTypes.Scope:
            switch (curToken._TokenType)
            {
              case TokenTypes.IDENTIFIER:
                if (!StringUtil.EqualsIgnoreCase("Nothing", curToken._Value))
                  RDLExceptionHelper.WriteBadSyntax(funcDef.Name, curToken);
                result = new ConstantString(curToken._Value);
                GetNextToken();
                break;
              case TokenTypes.QUOTE:
                result = new ConstantString(curToken._Value);
                ObjectDependencyList.Add(result);
                GetNextToken();
                break;
              default:
                RDLExceptionHelper.WriteBadSyntax(funcDef.Name, curToken);
                break;
            }
	        break;
          case RdlArgTypes.Recursive:
            if (curToken._TokenType != TokenTypes.IDENTIFIER)
              RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.IDENTIFIER, curToken);
            string str2 = curToken._Value;
            if (StringUtil.EqualsIgnoreCase("Recursive", str2))
              result = new Recursive(RecursiveOption.Recursive);
            else if (StringUtil.EqualsIgnoreCase("Simple", str2))
              result = new Recursive(RecursiveOption.Simple);
            else
              RDLExceptionHelper.WriteBadSyntax(funcDef.Name, curToken);
            GetNextToken();
            break;
          case RdlArgTypes.AggregateFunction:
            if (curToken._TokenType != TokenTypes.IDENTIFIER)
              RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.IDENTIFIER, curToken);
            RdlFunctionDefinition functionDef;
            if (!context.TryMatchRdlFunction(curToken._Value, out functionDef))
              RDLExceptionHelper.WriteBadSyntax(funcDef.Name, curToken);
            result = new Identifier(curToken._Value);
            GetNextToken();
            break;
          default:
            MatchExprXor(out result);
            break;
        }
        internalExpressionList.Add(result);
        if (rdlFunctionArg.IsVarArg)
          --index;
      }
      ConsumeExpectedToken(TokenTypes.RPAREN);
      return internalExpressionList;
    }

    private bool MatchFunctionArgs(out List<IInternalExpression> args)
    {
      args = null;
      if (curToken._TokenType != TokenTypes.LPAREN)
        return false;
      GetNextToken();
      int num = 0;
      args = new List<IInternalExpression>();
      while (curToken._TokenType != TokenTypes.RPAREN)
      {
        if (num != 0)
        {
          if (curToken._TokenType == TokenTypes.COMMA)
            GetNextToken();
          else
            RDLExceptionHelper.WriteInvalidFunction(curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
        }
        IInternalExpression result = null;
        MatchExprXor(out result);
        if (result == null)
          RDLExceptionHelper.WriteExpectedOperator("',' or ')'", curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
        args.Add(result);
        ++num;
      }
      GetNextToken();
      return true;
    }

    private bool MatchDefaultPropertyOrIndexer(IInternalExpression callTarget, out IInternalExpression result)
    {
      List<IInternalExpression> args;
      if (MatchFunctionArgs(out args))
      {
        result = new FunctionDefaultPropertyOrIndexer(callTarget, args);
        return true;
      }
      result = null;
      return false;
    }

    private IInternalExpression MatchObjectMethod(IInternalExpression prevExpression)
    {
      LookupContext context = null;
      string prevExpression1 = "";
      if (prevExpression is FunctionType)
      {
        TypeContext typeContext = ((FunctionType) prevExpression).TypeContext;
        context = typeContext;
        prevExpression1 = typeContext.Name;
      }
      if (context == null)
        context = m_environment.LateBoundContext;
      IInternalExpression result;
      if (!MatchMemberAccessor(context, prevExpression, out result))
      {
        if (curToken._TokenType != TokenTypes.EOF)
          RDLExceptionHelper.WriteMethodOrPropertyNotFound(curToken._Value, prevExpression1, prevToken.StartColumn - 2, prevToken.EndColumn - 2);
        else
          RDLExceptionHelper.WriteMethodOrPropertyExpected(curToken._Value, prevExpression1, curToken.StartColumn, curToken.EndColumn);
      }
      return result;
    }

    private void GetNextToken()
    {
      olderToken = prevToken;
      prevToken = curToken;
      curToken = tokens.Extract();
    }

    private void ConsumeExpectedToken(TokenTypes tokenType)
    {
      if (curToken._TokenType != tokenType)
        RDLExceptionHelper.WriteUnexpectedToken(tokenType, curToken);
      GetNextToken();
    }

    private ExpressionToken FindOldTokenByType(TokenTypes tokenType)
    {
      if (olderToken != null && olderToken._TokenType == tokenType)
        return olderToken;
      if (prevToken == null || prevToken._TokenType != tokenType)
        return curToken;
      return prevToken;
    }
  }
}
