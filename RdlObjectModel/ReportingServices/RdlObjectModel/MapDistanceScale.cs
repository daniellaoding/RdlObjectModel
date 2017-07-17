using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapDistanceScale : MapDockableSubItem
  {
    [ReportExpressionDefaultValue(typeof (ReportColor), "White")]
    public ReportExpression<ReportColor> ScaleColor
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

    [ReportExpressionDefaultValue(typeof (ReportColor), "DarkGray")]
    public ReportExpression<ReportColor> ScaleBorderColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public MapDistanceScale()
    {
    }

    internal MapDistanceScale(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ScaleColor = new ReportExpression<ReportColor>("White", CultureInfo.InvariantCulture);
      ScaleBorderColor = new ReportExpression<ReportColor>("DarkGray", CultureInfo.InvariantCulture);
    }

    internal class Definition : DefinitionStore<MapDistanceScale, Definition.Properties>
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
        ScaleColor,
        ScaleBorderColor,
        PropertyCount,
      }
    }
  }
}
