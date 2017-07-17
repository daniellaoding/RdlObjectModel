using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ScaleRange : ReportObject, INamedObject
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

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (GaugeBackgroundGradients), GaugeBackgroundGradients.StartToEnd)]
    public ReportExpression<GaugeBackgroundGradients> BackgroundGradientType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<GaugeBackgroundGradients>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ReportExpression<double> DistanceFromScale
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public GaugeInputValue StartValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public GaugeInputValue EndValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public ReportExpression<double> StartWidth
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

    public ReportExpression<double> EndWidth
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

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> InRangeBarPointerColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> InRangeLabelColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> InRangeTickMarksColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    public ReportExpression<Placements> Placement
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<Placements>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public ScaleRange()
    {
    }

    internal ScaleRange(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ScaleRange, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        BackgroundGradientType,
        DistanceFromScale,
        StartValue,
        EndValue,
        StartWidth,
        EndWidth,
        InRangeBarPointerColor,
        InRangeLabelColor,
        InRangeTickMarksColor,
        Placement,
        ToolTip,
        ActionInfo,
        Hidden,
        PropertyCount,
      }
    }
  }
}
