namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class GaugeLabel : GaugePanelItem
  {
    [ReportExpressionDefaultValue]
    public ReportExpression Text
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Angle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ResizeModes), ResizeModes.AutoFit)]
    public ReportExpression<ResizeModes> ResizeMode
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ResizeModes>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> TextShadowOffset
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UseFontPercent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public GaugeLabel()
    {
    }

    internal GaugeLabel(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<GaugeLabel, Definition.Properties>
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
        Text,
        Angle,
        ResizeMode,
        TextShadowOffset,
        UseFontPercent,
        PropertyCount,
      }
    }
  }
}
