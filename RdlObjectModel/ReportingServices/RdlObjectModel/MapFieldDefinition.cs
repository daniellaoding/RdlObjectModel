using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapFieldDefinition : ReportObject, INamedObject
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

    public MapDataTypes DataType
    {
      get
      {
        return (MapDataTypes) PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, (int) value);
      }
    }

    public MapFieldDefinition()
    {
    }

    internal MapFieldDefinition(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapFieldDefinition, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        DataType,
        PropertyCount,
      }
    }
  }
}
