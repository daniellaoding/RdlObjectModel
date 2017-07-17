using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapTile : ReportObject, INamedObject
  {
    [XmlElement("Name")]
    public string Name
    {
      get
      {
        return PropertyStore.GetObject<string>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public string TileData
    {
      get
      {
        return PropertyStore.GetObject<string>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public string MIMEType
    {
      get
      {
        return PropertyStore.GetObject<string>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public MapTile()
    {
    }

    internal MapTile(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapTile, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        TileData,
        MIMEType,
        PropertyCount,
      }
    }
  }
}
