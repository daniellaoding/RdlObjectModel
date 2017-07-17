using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapColorScale : MapDockableSubItem
  {
    public MapColorScaleTitle MapColorScaleTitle
    {
      get
      {
        return (MapColorScaleTitle) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "2.25pt")]
    public ReportExpression<ReportSize> TickMarkLength
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

    [ReportExpressionDefaultValue(typeof (ReportColor), "Black")]
    public ReportExpression<ReportColor> ColorBarBorderColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), "1")]
    public ReportExpression<int> LabelInterval
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression LabelFormat
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapLabelPlacements), MapLabelPlacements.Alternate)]
    public ReportExpression<MapLabelPlacements> LabelPlacement
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapLabelPlacements>>(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapLabelBehaviors), MapLabelBehaviors.Auto)]
    public ReportExpression<MapLabelBehaviors> LabelBehavior
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapLabelBehaviors>>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    public ReportExpression<bool> HideEndLabels
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor), "White")]
    public ReportExpression<ReportColor> RangeGapColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    public ReportExpression NoDataText
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    public MapColorScale()
    {
    }

    internal MapColorScale(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TickMarkLength = new ReportExpression<ReportSize>("2.25pt", CultureInfo.InvariantCulture);
      ColorBarBorderColor = new ReportExpression<ReportColor>("Black", CultureInfo.InvariantCulture);
      LabelInterval = 1;
      LabelFormat = "#,##0.##";
      LabelPlacement = MapLabelPlacements.Alternate;
      LabelBehavior = MapLabelBehaviors.Auto;
      RangeGapColor = new ReportExpression<ReportColor>("White", CultureInfo.InvariantCulture);
    }

    internal class Definition : DefinitionStore<MapColorScale, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        MapLocation,
        MapSize,
        LeftMargin,
        RightMargin,
        TopMargin,
        BottomMargin,
        ZIndex,
        ActionInfo,
        MapPosition,
        DockOutsideViewport,
        Hidden,
        ToolTip,
        MapColorScaleTitle,
        TickMarkLength,
        ColorBarBorderColor,
        LabelInterval,
        LabelFormat,
        LabelPlacement,
        LabelBehavior,
        HideEndLabels,
        RangeGapColor,
        NoDataText,
        PropertyCount,
      }
    }
  }
}
