using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("MapSpatialDataRegion", typeof (MapSpatialDataRegion))]
  [XmlElementClass("MapSpatialDataSet", typeof (MapSpatialDataSet))]
  [XmlElementClass("MapShapefile", typeof (MapShapefile))]
  public abstract class MapSpatialData : ReportObject
  {
    public MapSpatialData()
    {
    }

    internal MapSpatialData(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }
  }
}
