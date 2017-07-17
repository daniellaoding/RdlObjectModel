using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapLineLayer : MapVectorLayer
  {
    public MapLineTemplate MapLineTemplate
    {
      get
      {
        return (MapLineTemplate) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public MapLineRules MapLineRules
    {
      get
      {
        return (MapLineRules) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapLine>))]
    public IList<MapLine> MapLines
    {
      get
      {
        return (IList<MapLine>) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public MapLineLayer()
    {
    }

    internal MapLineLayer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapLines = new RdlCollection<MapLine>();
    }

    internal class Definition : DefinitionStore<MapLineLayer, Definition.Properties>
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
        MapLineTemplate,
        MapLineRules,
        MapLines,
        PropertyCount,
      }
    }
  }
}
