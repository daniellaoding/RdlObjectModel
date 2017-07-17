namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapLine : MapSpatialElement
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UseCustomLineTemplate
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public MapLineTemplate MapLineTemplate
    {
      get
      {
        return (MapLineTemplate) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public MapLine()
    {
    }

    internal MapLine(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapLine, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        VectorData,
        MapFields,
        UseCustomLineTemplate,
        MapLineTemplate,
        PropertyCount,
      }
    }
  }
}
