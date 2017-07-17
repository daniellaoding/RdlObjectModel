namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapPoint : MapSpatialElement
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UseCustomPointTemplate
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

    public MapPointTemplate MapPointTemplate
    {
      get
      {
        return (MapPointTemplate) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public MapPoint()
    {
    }

    internal MapPoint(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapPoint, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        VectorData,
        MapFields,
        UseCustomPointTemplate,
        MapPointTemplate,
        PropertyCount,
      }
    }
  }
}
