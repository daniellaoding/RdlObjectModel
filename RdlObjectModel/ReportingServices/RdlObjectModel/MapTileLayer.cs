using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapTileLayer : MapLayer
  {
    [ReportExpressionDefaultValue(typeof (MapTileStyles), MapTileStyles.Road)]
    public ReportExpression<MapTileStyles> TileStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapTileStyles>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapTile>))]
    public IList<MapTile> MapTiles
    {
      get
      {
        return (IList<MapTile>) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ReportExpression<bool> UseSecureConnection
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public MapTileLayer()
    {
    }

    internal MapTileLayer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TileStyle = MapTileStyles.Road;
      MapTiles = new RdlCollection<MapTile>();
    }

    internal class Definition : DefinitionStore<MapTileLayer, Definition.Properties>
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
        TileStyle,
        MapTiles,
        UserSecureConnection,
        PropertyCount,
      }
    }
  }
}
