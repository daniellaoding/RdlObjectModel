namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapMarker : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (MapMarkerStyles), MapMarkerStyles.None)]
    public ReportExpression<MapMarkerStyles> MapMarkerStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapMarkerStyles>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public MapMarkerImage MapMarkerImage
    {
      get
      {
        return (MapMarkerImage) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public MapMarker()
    {
    }

    internal MapMarker(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapMarkerStyle = MapMarkerStyles.None;
    }

    internal class Definition : DefinitionStore<MapMarker, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        MapMarkerStyle,
        MapMarkerImage,
        PropertyCount,
      }
    }
  }
}
