using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("MapCustomColorRule", typeof (MapCustomColorRule))]
  [XmlElementClass("MapColorPaletteRule", typeof (MapColorPaletteRule))]
  [XmlElementClass("MapColorRangeRule", typeof (MapColorRangeRule))]
  public abstract class MapColorRule : MapAppearanceRule
  {
    public ReportExpression<bool> ShowInColorScale
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    public MapColorRule()
    {
    }

    internal MapColorRule(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapColorRule, Definition.Properties>
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
      }
    }
  }
}
