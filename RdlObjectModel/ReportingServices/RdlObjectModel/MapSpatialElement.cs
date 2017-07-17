using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class MapSpatialElement : ReportObject
  {
    public VectorData VectorData
    {
      get
      {
        return PropertyStore.GetObject<VectorData>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapField>))]
    public IList<MapField> MapFields
    {
      get
      {
        return (IList<MapField>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public MapSpatialElement()
    {
    }

    internal MapSpatialElement(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapFields = new RdlCollection<MapField>();
    }

    internal class Definition : DefinitionStore<MapSpatialElement, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        VectorData,
        MapFields,
      }
    }
  }
}
