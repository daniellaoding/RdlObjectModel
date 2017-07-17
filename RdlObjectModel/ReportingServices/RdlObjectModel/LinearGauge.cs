namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class LinearGauge : Gauge
  {
    [ReportExpressionDefaultValue(typeof (Orientations), Orientations.Auto)]
    public ReportExpression<Orientations> Orientation
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<Orientations>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    public LinearGauge()
    {
    }

    internal LinearGauge(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<LinearGauge, Definition.Properties>
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
        Orientation,
        PropertyCount,
      }
    }
  }
}
