namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapColorPaletteRule : MapColorRule
  {
    [ReportExpressionDefaultValue(typeof (MapPalettes), MapPalettes.Random)]
    public ReportExpression<MapPalettes> Palette
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapPalettes>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public MapColorPaletteRule()
    {
    }

    internal MapColorPaletteRule(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Palette = MapPalettes.Random;
    }

    internal class Definition : DefinitionStore<MapColorPaletteRule, Definition.Properties>
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
        Palette,
        PropertyCount,
      }
    }
  }
}
