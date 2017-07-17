namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapSizeRule : MapAppearanceRule
  {
    public ReportExpression<ReportSize> StartSize
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    public ReportExpression<ReportSize> EndSize
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public MapSizeRule()
    {
    }

    internal MapSizeRule(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapSizeRule, Definition.Properties>
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
        StartSize,
        EndSize,
        PropertyCount,
      }
    }
  }
}
