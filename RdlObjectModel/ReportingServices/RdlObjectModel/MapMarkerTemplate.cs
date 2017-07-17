namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapMarkerTemplate : MapPointTemplate
  {
    public MapMarker MapMarker
    {
      get
      {
        return (MapMarker) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public MapMarkerTemplate()
    {
    }

    internal MapMarkerTemplate(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapMarkerTemplate, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        ActionInfo,
        Hidden,
        OffsetX,
        OffsetY,
        Label,
        ToolTip,
        DataElementName,
        DataElementOutput,
        DataElementLabel,
        Size,
        LabelPlacement,
        MapMarker,
        PropertyCount,
      }
    }
  }
}
