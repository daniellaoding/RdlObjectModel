namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapPolygonTemplate : MapSpatialElementTemplate
  {
    [ReportExpressionDefaultValue(typeof (double), "1")]
    public ReportExpression<double> ScaleFactor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0)]
    public ReportExpression<double> CenterPointOffsetX
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0)]
    public ReportExpression<double> CenterPointOffsetY
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

    [ReportExpressionDefaultValue(typeof (MapAutoBools), MapAutoBools.Auto)]
    public ReportExpression<MapAutoBools> ShowLabel
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapAutoBools>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapPolygonLabelPlacements), MapPolygonLabelPlacements.MiddleCenter)]
    public ReportExpression<MapPolygonLabelPlacements> LabelPlacement
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapPolygonLabelPlacements>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public MapPolygonTemplate()
    {
    }

    internal MapPolygonTemplate(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ScaleFactor = 1.0;
      ShowLabel = MapAutoBools.Auto;
      LabelPlacement = MapPolygonLabelPlacements.MiddleCenter;
    }

    internal class Definition : DefinitionStore<MapPolygonTemplate, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        ActionInfo,
        Hidden,
        OffsetX,
        OffsetY,
        Label,
        ToolTip,
        DataElementName,
        DataElementOutput,
        DataElementLabel,
        ScaleFactor,
        CenterPointOffsetX,
        CenterPointOffsetY,
        ShowLabel,
        LabelPlacement,
        PropertyCount,
      }
    }
  }
}
