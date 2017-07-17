using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartLegend : ReportObject, INamedObject
  {
    [XmlAttribute(typeof (string))]
    public string Name
    {
      get
      {
        return (string) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartPositions), ChartPositions.RightTop)]
    public ReportExpression<ChartPositions> Position
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartPositions>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartLegendLayouts), ChartLegendLayouts.AutoTable)]
    public ReportExpression<ChartLegendLayouts> Layout
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartLegendLayouts>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [DefaultValue("")]
    public string DockToChartArea
    {
      get
      {
        return (string) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> DockOutsideChartArea
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public ChartElementPosition ChartElementPosition
    {
      get
      {
        return (ChartElementPosition) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    public ChartLegendTitle ChartLegendTitle
    {
      get
      {
        return (ChartLegendTitle) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> AutoFitTextDisabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "7pt")]
    public ReportExpression<ReportSize> MinFontSize
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

    [XmlElement(typeof (RdlCollection<ChartLegendColumn>))]
    public IList<ChartLegendColumn> ChartLegendColumns
    {
      get
      {
        return (IList<ChartLegendColumn>) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartHeaderSeparatorTypes), ChartHeaderSeparatorTypes.None)]
    public ReportExpression<ChartHeaderSeparatorTypes> HeaderSeparator
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartHeaderSeparatorTypes>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> HeaderSeparatorColor
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

    [ReportExpressionDefaultValue(typeof (ChartColumnSeparatorTypes), ChartColumnSeparatorTypes.None)]
    public ReportExpression<ChartColumnSeparatorTypes> ColumnSeparator
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartColumnSeparatorTypes>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> ColumnSeparatorColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 50)]
    public ReportExpression<int> ColumnSpacing
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
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

    [ReportExpressionDefaultValue(typeof (ReportColor))]
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
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

    [ReportExpressionDefaultValue(typeof (ChartLegendReversedTypes), ChartLegendReversedTypes.Auto)]
    public ReportExpression<ChartLegendReversedTypes> Reversed
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartLegendReversedTypes>>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 50)]
    public ReportExpression<int> MaxAutoSize
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 25)]
    public ReportExpression<int> TextWrapThreshold
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    public ChartLegend()
    {
    }

    internal ChartLegend(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartLegendColumns = new RdlCollection<ChartLegendColumn>();
      Position = ChartPositions.RightTop;
      Layout = ChartLegendLayouts.AutoTable;
      ColumnSpacing = 50;
      MaxAutoSize = 50;
      TextWrapThreshold = 25;
    }

    internal class Definition : DefinitionStore<ChartLegend, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Hidden,
        Style,
        Position,
        Layout,
        Docking,
        DockToChartArea,
        DockOutsideChartArea,
        ChartElementPosition,
        ChartLegendTitle,
        AutoFitTextDisabled,
        MinFontSize,
        ChartLegendColumns,
        HeaderSeparator,
        HeaderSeparatorColor,
        ColumnSeparator,
        ColumnSeparatorColor,
        ColumnSpacing,
        InterlacedRows,
        InterlacedRowsColor,
        EquallySpacedItems,
        Reversed,
        MaxAutoSize,
        TextWrapThreshold,
        PropertyCount,
      }
    }
  }
}
