namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapBindingFieldPair : ReportObject
  {
    public ReportExpression FieldName
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ReportExpression BindingExpression
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public MapBindingFieldPair()
    {
    }

    internal MapBindingFieldPair(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapBindingFieldPair, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        FieldName,
        BindingExpression,
        PropertyCount,
      }
    }
  }
}
