using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartStripLine : ReportObject
  {
    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Title
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (TextOrientations), TextOrientations.Auto)]
    public ReportExpression<TextOrientations> TextOrientation
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextOrientations>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(4);
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
        return PropertyStore.GetObject<ReportExpression<double>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartIntervalTypes), ChartIntervalTypes.Auto)]
    public ReportExpression<ChartIntervalTypes> IntervalType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> IntervalOffset
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Auto)]
    public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> StripWidth
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

    [ReportExpressionDefaultValue(typeof (ChartStripWidthTypes), ChartStripWidthTypes.Auto)]
    public ReportExpression<ChartStripWidthTypes> StripWidthType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartStripWidthTypes>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [XmlChildAttribute("ToolTip", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string ToolTipLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public ChartStripLine()
    {
    }

    internal ChartStripLine(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      IntervalType = ChartIntervalTypes.Auto;
      IntervalOffsetType = ChartIntervalOffsetTypes.Auto;
    }

    internal class Definition : DefinitionStore<ChartStripLine, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Title,
        TextOrientation,
        ActionInfo,
        ToolTip,
        ToolTipLocID,
        Interval,
        IntervalType,
        IntervalOffset,
        IntervalOffsetType,
        StripWidth,
        StripWidthType,
        PropertyCount,
      }
    }
  }
}
