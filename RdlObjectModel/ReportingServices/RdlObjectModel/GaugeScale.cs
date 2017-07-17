using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("RadialScale", typeof (RadialScale))]
  [XmlElementClass("LinearScale", typeof (LinearScale))]
  public class GaugeScale : ReportObject, INamedObject
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

    [XmlElement(typeof (RdlCollection<GaugePointer>))]
    public IList<GaugePointer> GaugePointers
    {
      get
      {
        return (IList<GaugePointer>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ScaleRange>))]
    public IList<ScaleRange> ScaleRanges
    {
      get
      {
        return (IList<ScaleRange>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomLabel>))]
    public IList<CustomLabel> CustomLabels
    {
      get
      {
        return (IList<CustomLabel>) PropertyStore.GetObject(4);
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> IntervalOffset
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Logarithmic
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

    [ReportExpressionDefaultValue(typeof (double), 10.0)]
    public ReportExpression<double> LogarithmicBase
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

    public GaugeInputValue MaximumValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    public GaugeInputValue MinimumValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 1.0)]
    public ReportExpression<double> Multiplier
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Reversed
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public GaugeTickMarks GaugeMajorTickMarks
    {
      get
      {
        return (GaugeTickMarks) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public GaugeTickMarks GaugeMinorTickMarks
    {
      get
      {
        return (GaugeTickMarks) PropertyStore.GetObject(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public ScalePin MaximumPin
    {
      get
      {
        return (ScalePin) PropertyStore.GetObject(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public ScalePin MinimumPin
    {
      get
      {
        return (ScalePin) PropertyStore.GetObject(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    public ScaleLabels ScaleLabels
    {
      get
      {
        return (ScaleLabels) PropertyStore.GetObject(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> TickMarksOnTop
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

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 5.0)]
    public ReportExpression<double> Width
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    public GaugeScale()
    {
    }

    internal GaugeScale(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      GaugePointers = new RdlCollection<GaugePointer>();
      ScaleRanges = new RdlCollection<ScaleRange>();
      CustomLabels = new RdlCollection<CustomLabel>();
      LogarithmicBase = 10.0;
      Multiplier = 1.0;
      Width = 5.0;
    }

    internal class Definition : DefinitionStore<GaugeScale, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        GaugePointers,
        ScaleRanges,
        Style,
        CustomLabels,
        Interval,
        IntervalOffset,
        Logarithmic,
        LogarithmicBase,
        MaximumValue,
        MinimumValue,
        Multiplier,
        Reversed,
        GaugeMajorTickMarks,
        GaugeMinorTickMarks,
        MaximumPin,
        MinimumPin,
        ScaleLabels,
        TickMarksOnTop,
        ToolTip,
        ActionInfo,
        Hidden,
        Width,
        PropertyCount,
      }
    }
  }
}
