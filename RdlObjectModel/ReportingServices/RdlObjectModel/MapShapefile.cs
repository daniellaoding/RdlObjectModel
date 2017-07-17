using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapShapefile : MapSpatialData
  {
    public ReportExpression Source
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlArrayItem("MapFieldName", typeof (ReportExpression))]
    [XmlElement(typeof (RdlCollection<ReportExpression>))]
    public IList<ReportExpression> MapFieldNames
    {
      get
      {
        return (IList<ReportExpression>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public MapShapefile()
    {
    }

    internal MapShapefile(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapFieldNames = new RdlCollection<ReportExpression>();
    }

    internal class Definition : DefinitionStore<MapShapefile, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Source,
        MapFieldNames,
        PropertyCount,
      }
    }
  }
}
