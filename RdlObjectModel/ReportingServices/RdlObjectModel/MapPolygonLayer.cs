using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapPolygonLayer : MapVectorLayer
  {
    public MapPolygonTemplate MapPolygonTemplate
    {
      get
      {
        return (MapPolygonTemplate) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public MapPolygonRules MapPolygonRules
    {
      get
      {
        return (MapPolygonRules) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public MapPointTemplate MapCenterPointTemplate
    {
      get
      {
        return (MapPointTemplate) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public MapPointRules MapCenterPointRules
    {
      get
      {
        return (MapPointRules) PropertyStore.GetObject(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapPolygon>))]
    public IList<MapPolygon> MapPolygons
    {
      get
      {
        return (IList<MapPolygon>) PropertyStore.GetObject(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public MapPolygonLayer()
    {
    }

    internal MapPolygonLayer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapPolygons = new RdlCollection<MapPolygon>();
    }

    internal class Definition : DefinitionStore<MapPolygonLayer, Definition.Properties>
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
        MapPolygonTemplate,
        MapPolygonRules,
        MapCenterPointTemplate,
        MapCenterPointRules,
        MapPolygons,
        PropertyCount,
      }
    }
  }
}
