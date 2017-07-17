namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartSmartLabel : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Disabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartAllowOutSidePlotAreaTypes), ChartAllowOutSidePlotAreaTypes.Partial)]
    public ReportExpression<ChartAllowOutSidePlotAreaTypes> AllowOutSidePlotArea
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartAllowOutSidePlotAreaTypes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> CalloutBackColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartCalloutLineAnchorTypes), ChartCalloutLineAnchorTypes.Arrow)]
    public ReportExpression<ChartCalloutLineAnchorTypes> CalloutLineAnchor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartCalloutLineAnchorTypes>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> CalloutLineColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartCalloutLineStyles), ChartCalloutLineStyles.Solid)]
    public ReportExpression<ChartCalloutLineStyles> CalloutLineStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartCalloutLineStyles>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "0.75pt")]
    public ReportExpression<ReportSize> CalloutLineWidth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartCalloutStyles), ChartCalloutStyles.Underline)]
    public ReportExpression<ChartCalloutStyles> CalloutStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartCalloutStyles>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> ShowOverlapped
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> MarkerOverlapping
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "23pt")]
    public ReportExpression<ReportSize> MaxMovingDistance
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

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> MinMovingDistance
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

    public ChartNoMoveDirections ChartNoMoveDirections
    {
      get
      {
        return (ChartNoMoveDirections) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public ChartSmartLabel()
    {
    }

    internal ChartSmartLabel(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartSmartLabel, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Disabled,
        AllowOutSidePlotArea,
        CalloutBackColor,
        CalloutLineAnchor,
        CalloutLineColor,
        CalloutLineStyle,
        CalloutLineWidth,
        CalloutStyle,
        ShowOverlapped,
        MarkerOverlapping,
        MaxMovingDistance,
        MinMovingDistance,
        ChartNoMoveDirections,
        PropertyCount,
      }
    }
  }
}
