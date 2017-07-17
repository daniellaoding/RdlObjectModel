namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal interface ISimpleRdlCollection
  {
    string Name { get; }

    IInternalExpression CreateReference();

    IInternalExpression CreateCollectionReference(IInternalExpression itemNameExpr);

    IInternalExpression CreatePropertyReference(string propertyName);

    bool IsPredefinedCollectionProperty(string name);

    bool IsPredefinedChildCollection(string name, out ISimpleRdlCollection childCollection);
  }
}
