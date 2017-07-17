using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("LinearPointer", typeof (LinearPointer))]
  [XmlElementClass("RadialPointer", typeof (RadialPointer))]
  public class GaugePointer : ReportObject, INamedObject
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

    public GaugeInputValue GaugeInputValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (BarStartTypes), BarStartTypes.ScaleStart)]
    public ReportExpression<BarStartTypes> BarStart
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<BarStartTypes>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> DistanceFromScale
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public PointerImage PointerImage
    {
      get
      {
        return (PointerImage) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public ReportExpression<double> MarkerLength
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

    [ReportExpressionDefaultValue(typeof (MarkerStyles), MarkerStyles.Triangle)]
    public ReportExpression<MarkerStyles> MarkerStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MarkerStyles>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public ReportExpression<Placements> Placement
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<Placements>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> SnappingEnabled
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
    public ReportExpression<double> SnappingInterval
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

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public ReportExpression<double> Width
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public GaugePointer()
    {
    }

    internal GaugePointer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MarkerStyle = MarkerStyles.Triangle;
    }

    internal class Definition : DefinitionStore<GaugePointer, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        GaugeInputValue,
        BarStart,
        DistanceFromScale,
        PointerImage,
        MarkerLength,
        MarkerStyle,
        Placement,
        SnappingEnabled,
        SnappingInterval,
        ToolTip,
        ActionInfo,
        Hidden,
        Width,
        PropertyCount,
      }
    }
  }
}
