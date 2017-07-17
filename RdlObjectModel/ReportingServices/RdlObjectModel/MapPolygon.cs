namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapPolygon : MapSpatialElement
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UseCustomPolygonTemplate
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

    public MapPolygonTemplate MapPolygonTemplate
    {
      get
      {
        return (MapPolygonTemplate) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UseCustomCenterPointTemplate
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public MapPointTemplate MapCenterPointTemplate
    {
      get
      {
        return (MapPointTemplate) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public MapPolygon()
    {
    }

    internal MapPolygon(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapPolygon, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        VectorData,
        MapFields,
        UseCustomPolygonTemplate,
        MapPolygonTemplate,
        UseCustomCenterPointTemplate,
        MapCenterPointTemplate,
        PropertyCount,
      }
    }
  }
}
