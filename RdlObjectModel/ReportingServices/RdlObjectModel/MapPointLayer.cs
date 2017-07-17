using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapPointLayer : MapVectorLayer
  {
    public MapPointTemplate MapPointTemplate
    {
      get
      {
        return (MapPointTemplate) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public MapPointRules MapPointRules
    {
      get
      {
        return (MapPointRules) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapPoint>))]
    public IList<MapPoint> MapPoints
    {
      get
      {
        return (IList<MapPoint>) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public MapPointLayer()
    {
    }

    internal MapPointLayer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapPoints = new RdlCollection<MapPoint>();
    }

    internal class Definition : DefinitionStore<MapPointLayer, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        VisibilityMode,
        MinimumZoom,
        MaximumZoom,
        Transparency,
        MapDataRegionName,
        MapBindingFieldPairs,
        MapFieldDefinitions,
        MapSpatialData,
        DataElementName,
        DataElementOutput,
        MapPointTemplate,
        MapPointRules,
        MapPoints,
        PropertyCount,
      }
    }
  }
}
