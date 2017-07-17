namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class RadialGauge : Gauge
  {
    [ReportExpressionDefaultValue(typeof (double), 50.0)]
    public ReportExpression<double> PivotX
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 50.0)]
    public ReportExpression<double> PivotY
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    public RadialGauge()
    {
    }

    internal RadialGauge(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      PivotX = 50.0;
      PivotY = 50.0;
    }

    internal class Definition : DefinitionStore<RadialGauge, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Hidden,
        ToolTip,
        ActionInfo,
        ParentItem,
        GaugeScales,
        BackFrame,
        ClipContent,
        TopImage,
        AspectRatio,
        PivotX,
        PivotY,
        PropertyCount,
      }
    }
  }
}
