using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapColorRangeRule : MapColorRule
  {
    [ReportExpressionDefaultValue(typeof (ReportColor), "Green")]
    public ReportExpression<ReportColor> StartColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor), "Yellow")]
    public ReportExpression<ReportColor> MiddleColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor), "Red")]
    public ReportExpression<ReportColor> EndColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public MapColorRangeRule()
    {
    }

    internal MapColorRangeRule(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      StartColor = new ReportExpression<ReportColor>("Green", CultureInfo.InvariantCulture);
      MiddleColor = new ReportExpression<ReportColor>("Yellow", CultureInfo.InvariantCulture);
      EndColor = new ReportExpression<ReportColor>("Red", CultureInfo.InvariantCulture);
    }

    internal class Definition : DefinitionStore<MapColorRangeRule, Definition.Properties>
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
        StartColor,
        MiddleColor,
        EndColor,
        PropertyCount,
      }
    }
  }
}
