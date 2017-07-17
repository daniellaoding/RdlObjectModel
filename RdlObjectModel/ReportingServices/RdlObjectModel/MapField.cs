using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapField : ReportObject, INamedObject
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

    public string Value
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

    public MapField()
    {
    }

    internal MapField(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapField, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Value,
        PropertyCount,
      }
    }
  }
}
