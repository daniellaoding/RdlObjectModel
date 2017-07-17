using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartAxis : ReportObject, INamedObject
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

    [ReportExpressionDefaultValue(typeof (ChartVisibleTypes), ChartVisibleTypes.Auto)]
    public ReportExpression<ChartVisibleTypes> Visible
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartVisibleTypes>>(1);
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

    public ChartAxisTitle ChartAxisTitle
    {
      get
      {
        return (ChartAxisTitle) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartAxisMarginVisibleTypes), ChartAxisMarginVisibleTypes.Auto)]
    public ReportExpression<ChartAxisMarginVisibleTypes> Margin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartAxisMarginVisibleTypes>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Interval
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartIntervalTypes), ChartIntervalTypes.Auto)]
    public ReportExpression<ChartIntervalTypes> IntervalType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> IntervalOffset
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Auto)]
    public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> VariableAutoInterval
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> LabelInterval
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

    [ReportExpressionDefaultValue(typeof (ChartIntervalTypes), ChartIntervalTypes.Default)]
    public ReportExpression<ChartIntervalTypes> LabelIntervalType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> LabelIntervalOffset
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

    [ReportExpressionDefaultValue(typeof (ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Default)]
    public ReportExpression<ChartIntervalOffsetTypes> LabelIntervalOffsetType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public ChartGridLines ChartMajorGridLines
    {
      get
      {
        return (ChartGridLines) PropertyStore.GetObject(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public ChartGridLines ChartMinorGridLines
    {
      get
      {
        return (ChartGridLines) PropertyStore.GetObject(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public ChartTickMarks ChartMajorTickMarks
    {
      get
      {
        return (ChartTickMarks) PropertyStore.GetObject(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    public ChartTickMarks ChartMinorTickMarks
    {
      get
      {
        return (ChartTickMarks) PropertyStore.GetObject(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> MarksAlwaysAtPlotEdge
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Reverse
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression CrossAt
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartAxisLocations), ChartAxisLocations.Default)]
    public ReportExpression<ChartAxisLocations> Location
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartAxisLocations>>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Interlaced
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> InterlacedColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartStripLine>))]
    public IList<ChartStripLine> ChartStripLines
    {
      get
      {
        return (IList<ChartStripLine>) PropertyStore.GetObject(24);
      }
      set
      {
        PropertyStore.SetObject(24, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartArrowsTypes), ChartArrowsTypes.None)]
    public ReportExpression<ChartArrowsTypes> Arrows
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartArrowsTypes>>(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    [DefaultValue(false)]
    public bool Scalar
    {
      get
      {
        return PropertyStore.GetBoolean(26);
      }
      set
      {
        PropertyStore.SetBoolean(26, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Minimum
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(27);
      }
      set
      {
        PropertyStore.SetObject(27, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Maximum
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(28);
      }
      set
      {
        PropertyStore.SetObject(28, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> LogScale
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(29);
      }
      set
      {
        PropertyStore.SetObject(29, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 10.0)]
    public ReportExpression<double> LogBase
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(30);
      }
      set
      {
        PropertyStore.SetObject(30, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> HideLabels
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(31);
      }
      set
      {
        PropertyStore.SetObject(31, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Angle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(32);
      }
      set
      {
        PropertyStore.SetObject(32, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> PreventFontShrink
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(33);
      }
      set
      {
        PropertyStore.SetObject(33, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> PreventFontGrow
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(34);
      }
      set
      {
        PropertyStore.SetObject(34, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> PreventLabelOffset
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(35);
      }
      set
      {
        PropertyStore.SetObject(35, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> PreventWordWrap
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(36);
      }
      set
      {
        PropertyStore.SetObject(36, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartLabelRotationTypes), ChartLabelRotationTypes.Rotate90)]
    public ReportExpression<ChartLabelRotationTypes> AllowLabelRotation
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartLabelRotationTypes>>(37);
      }
      set
      {
        PropertyStore.SetObject(37, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), true)]
    public ReportExpression<bool> IncludeZero
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(38);
      }
      set
      {
        PropertyStore.SetObject(38, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> LabelsAutoFitDisabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(39);
      }
      set
      {
        PropertyStore.SetObject(39, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "6pt")]
    public ReportExpression<ReportSize> MinFontSize
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(40);
      }
      set
      {
        PropertyStore.SetObject(40, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "10pt")]
    public ReportExpression<ReportSize> MaxFontSize
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(41);
      }
      set
      {
        PropertyStore.SetObject(41, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> OffsetLabels
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(42);
      }
      set
      {
        PropertyStore.SetObject(42, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> HideEndLabels
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(43);
      }
      set
      {
        PropertyStore.SetObject(43, value);
      }
    }

    public ChartAxisScaleBreak ChartAxisScaleBreak
    {
      get
      {
        return (ChartAxisScaleBreak) PropertyStore.GetObject(44);
      }
      set
      {
        PropertyStore.SetObject(44, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(45);
      }
      set
      {
        PropertyStore.SetObject(45, value);
      }
    }

    [DefaultValue("")]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string SyncScope { get; set; }

	  [DefaultValue(false)]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public bool SyncMinimum { get; set; }

	  [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue(false)]
    public bool SyncMaximum { get; set; }

	  public ChartAxis()
    {
    }

    internal ChartAxis(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartStripLines = new RdlCollection<ChartStripLine>();
      CustomProperties = new RdlCollection<CustomProperty>();
      IntervalType = ChartIntervalTypes.Auto;
      IntervalOffsetType = ChartIntervalOffsetTypes.Auto;
      LabelIntervalType = ChartIntervalTypes.Default;
      LabelIntervalOffsetType = ChartIntervalOffsetTypes.Default;
      LogBase = 10.0;
      IncludeZero = true;
    }

    internal class Definition : DefinitionStore<ChartAxis, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Visible,
        Style,
        ChartAxisTitle,
        Margin,
        Interval,
        IntervalType,
        IntervalOffset,
        IntervalOffsetType,
        VariableAutoInterval,
        LabelInterval,
        LabelIntervalType,
        LabelIntervalOffset,
        LabelIntervalOffsetType,
        ChartMajorGridLines,
        ChartMinorGridLines,
        ChartMajorTickMarks,
        ChartMinorTickMarks,
        MarksAlwaysAtPlotEdge,
        Reverse,
        CrossAt,
        Location,
        Interlaced,
        InterlacedColor,
        ChartStripLines,
        Arrows,
        Scalar,
        Minimum,
        Maximum,
        LogScale,
        LogBase,
        HideLabels,
        Angle,
        PreventFontShrink,
        PreventFontGrow,
        PreventLabelOffset,
        PreventWordWrap,
        AllowLabelRotation,
        IncludeZero,
        LabelsAutoFitDisabled,
        MinFontSize,
        MaxFontSize,
        OffsetLabels,
        HideEndLabels,
        ChartAxisScaleBreak,
        CustomProperties,
        PropertyCount,
      }
    }

    internal static class Defaults
    {
      public const string SyncScope = "";
      public const bool SyncMinimum = false;
      public const bool SyncMaximum = false;
    }
  }
}
