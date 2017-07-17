using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapCustomColorRule : MapColorRule
  {
    [XmlElement(typeof (RdlCollection<ReportExpression<ReportColor>>))]
    [XmlArrayItem("MapCustomColor", typeof (ReportExpression<ReportColor>))]
    public IList<ReportExpression<ReportColor>> MapCustomColors
    {
      get
      {
        return (IList<ReportExpression<ReportColor>>) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public MapCustomColorRule()
    {
    }

    internal MapCustomColorRule(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapCustomColors = new RdlCollection<ReportExpression<ReportColor>>();
    }

    internal class Definition : DefinitionStore<MapCustomColorRule, Definition.Properties>
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
        ShowInColorScale,
        MapCustomColors,
        PropertyCount,
      }
    }
  }
}
