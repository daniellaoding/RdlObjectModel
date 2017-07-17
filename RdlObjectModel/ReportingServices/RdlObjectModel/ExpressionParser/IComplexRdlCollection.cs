namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal interface IComplexRdlCollection
  {
    string Name { get; }

    string ItemName { get; }

    IInternalExpression CreateReference();

    IInternalExpression CreateReference(IInternalExpression itemNameExpr);

    IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr);

    bool IsPredefinedItemProperty(string name);

    bool IsPredefinedItemMethod(string name);

    bool IsPredefinedCollectionProperty(string name);
  }
}
