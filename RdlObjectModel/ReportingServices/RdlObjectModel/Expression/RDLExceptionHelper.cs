using System.Globalization;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel.Expression
{
  internal static class RDLExceptionHelper
  {
    public static void WriteEndExpected(string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.EndExpected", currentToken, startCol, endCol);
    }

    public static void WriteExpectedOperand(string method, string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ExpectedOperand", method, currentToken, startCol, endCol);
    }

    public static void WriteInvalidExpression(int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidExpression", "", "", 0, endCol);
    }

    public static void WriteBadTypeOfSyntax(string method, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadTypeOfSyntax", method, startCol, endCol);
    }

    public static void WriteUnknownFunction(string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnknownFunction", currentToken, startCol, endCol);
    }

    public static void WriteArrayOperand(string method, string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ArrayOperand", method, currentToken, startCol, endCol);
    }

    public static void WriteExpectedOperator(string method, string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ExpectedOperator", method, currentToken, startCol, endCol);
    }

    public static void WriteDivisionByZero(string method, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.DivisionByZero", method, startCol, endCol);
    }

    public static void WriteOperandTypesInvalid(string operand, string theOperator, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.OperandTypesInvalid", operand, theOperator, startCol, endCol);
    }

    public static void WriteOverflow(string method, string overflowingValue, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.Overflow", method, startCol, endCol, new object[1]
      {
        overflowingValue
      });
    }

    public static void WriteBadCollectionSyntaxMissingThirdPart(string expression, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntaxMissingThirdPart", expression, startCol, endCol);
    }

    public static void WriteBadCollectionSyntax(string method, string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntax", method, currentToken, startCol, endCol);
    }

    public static void WriteBadSyntax(string method, string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadSyntax", method, currentToken, startCol, endCol);
    }

    public static void WriteBadSyntax(string method, ExpressionToken curToken)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadSyntax", method, curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteBadCollectionSyntaxNoQuote(string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntaxNoQuote", currentToken, startCol, endCol);
    }

    public static void WriteBadCollectionSyntaxParen(string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntaxParen", currentToken, startCol, endCol);
    }

    public static void WriteAggregateErrorsGroups(string function, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.AggregateErrorsGroups", function, startCol, endCol);
    }

    public static void WriteAggregateErrorsNested(string function, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.AggregateErrorsNested", function, startCol, endCol);
    }

    public static void WriteMethodOrPropertyNotFound(string currentToken, string prevExpression, int startCol, int endCol)
    {
      throw new ExpressionParserInvalidMemberNameException(currentToken, "RDLEngine.Error.RDLObjects.Expression.MethodOrPropertyNotFound", currentToken, prevExpression, startCol, endCol);
    }

    public static void WriteMethodOrPropertyNotFound(ExpressionToken curToken, string typeName)
    {
      throw new ExpressionParserInvalidMemberNameException(curToken._Value, "RDLEngine.Error.RDLObjects.Expression.MethodOrPropertyNotFound", curToken.ToString(), typeName, curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteMethodOrPropertyExpected(string currentToken, string prevExpression, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.MethodOrPropertyExpected", currentToken, prevExpression, startCol, endCol);
    }

    public static void WriteUnknownEnumeration(string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnknownEnumeration", currentToken, startCol, endCol);
    }

    public static void WriteIndexNotIntegerType(string methodName, string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.IndexNotIntegerType", currentToken, methodName, startCol, endCol);
    }

    public static void WriteInvalidFunction(string currentToken, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidFunction", currentToken, startCol, endCol);
    }

    public static void WriteReportParameterCollectionSyntaxOnCountOrIsMultiValue(ReportParameter reportParameter, int startCol, int endCol)
    {
      throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ReportParameterCollectionSyntaxOnCountOrIsMultiValue", reportParameter.Name, startCol, endCol);
    }

    public static void WriteUnexpectedToken(TokenTypes expectedToken, ExpressionToken curToken)
    {
      throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Expected token: {0} but found token: {1}", new object[2]
      {
        expectedToken.ToString(),
        curToken._TokenType.ToString()
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteTypeNotFound(string typeName, ExpressionToken curToken)
    {
      throw new ExpressionParserInvalidTypeNameException(typeName, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Type name: '{0}' could not be resolved", new object[1]
      {
        typeName
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteUnknownIdentifier(string name, ExpressionToken curToken)
    {
      throw new ExpressionParserUnknownIdentifierException(name, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Identifier: '{0}' could not be resolved", new object[1]
      {
        name
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteInvalidCollectionItem(string collectionName, string itemName)
    {
      throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Item '{0}' does not exist in the {1} collection.", new object[2]
      {
        collectionName,
        itemName
      }));
    }

    public static void WriteMissingIdentifierForDictionaryOperator(ExpressionToken curToken)
    {
      throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Expected identifier for DictionaryAccessExpression but found: {0}", new object[1]
      {
        curToken._Value
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteInvalidDateTimeLiteral(ExpressionToken curToken)
    {
      throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "{0} is not a valid DateTime literal", new object[1]
      {
        curToken._Value
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteMissingArgumentsForExpression(ExpressionToken curToken)
    {
      throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Expected arguments for IndexExpression or DefaultPropertyExpression but found: {0}", new object[1]
      {
        curToken._Value
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteInvalidArrayType(string typeName, ExpressionToken curToken)
    {
      throw new ExpressionParserInvalidArrayTypeException(typeName, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Type name: '{0}' cannot be used as an array type", new object[1]
      {
        typeName
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }

    public static void WriteInvalidNewType(string typeName, ExpressionToken curToken)
    {
      throw new ExpressionParserInvalidNewTypeException(typeName, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Type name: '{0}' cannot be used with the New operator.", new object[1]
      {
        typeName
      }), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
    }
  }
}
