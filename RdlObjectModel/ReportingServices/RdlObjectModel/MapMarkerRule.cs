using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapMarkerRule : MapAppearanceRule
  {
    [XmlElement(typeof (RdlCollection<MapMarker>))]
    public IList<MapMarker> MapMarkers
    {
      get
      {
        return (IList<MapMarker>) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    public MapMarkerRule()
    {
    }

    internal MapMarkerRule(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapMarkers = new RdlCollection<MapMarker>();
    }

    internal class Definition : DefinitionStore<MapMarkerRule, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataValue,
        DistributionType,
        BucketCount,
        StartValue,
        EndValue,
        MapBuckets,
        LegendName,
        LegendText,
        DataElementName,
        DataElementOutput,
        MapMarkers,
        PropertyCount,
      }
    }
  }
}
