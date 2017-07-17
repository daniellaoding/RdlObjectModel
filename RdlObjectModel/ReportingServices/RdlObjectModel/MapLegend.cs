using System.Globalization;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapLegend : MapDockableSubItem, INamedObject
  {
    [XmlAttribute(typeof (string))]
    public string Name
    {
      get
      {
        return PropertyStore.GetObject<string>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapLegendLayouts), MapLegendLayouts.AutoTable)]
    public ReportExpression<MapLegendLayouts> Layout
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapLegendLayouts>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public MapLegendTitle MapLegendTitle
    {
      get
      {
        return (MapLegendTitle) PropertyStore.GetObject(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public ReportExpression<bool> AutoFitTextDisabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "7pt")]
    public ReportExpression<ReportSize> MinFontSize
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    public ReportExpression<bool> InterlacedRows
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor), "LightGray")]
    public ReportExpression<ReportColor> InterlacedRowsColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    public ReportExpression<bool> EquallySpacedItems
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

    [ReportExpressionDefaultValue(typeof (int), "25")]
    public ReportExpression<int> TextWrapThreshold
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    public MapLegend()
    {
    }

    internal MapLegend(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Layout = MapLegendLayouts.AutoTable;
      MinFontSize = new ReportExpression<ReportSize>("7pt", CultureInfo.InvariantCulture);
      InterlacedRowsColor = new ReportExpression<ReportColor>("LightGray", CultureInfo.InvariantCulture);
      TextWrapThreshold = 25;
    }

    internal class Definition : DefinitionStore<MapLegend, Definition.Properties>
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
        Name,
        Layout,
        MapLegendTitle,
        AutoFitTextDisabled,
        MinFontSize,
        InterlacedRows,
        InterlacedRowsColor,
        EquallySpacedItems,
        TextWrapThreshold,
        PropertyCount,
      }
    }
  }
}
