using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class CustomLabel : ReportObject, INamedObject
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

    [ReportExpressionDefaultValue]
    public ReportExpression Text
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> AllowUpsideDown
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(3);
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> FontAngle
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

    [ReportExpressionDefaultValue(typeof (Placements), Placements.Inside)]
    public ReportExpression<Placements> Placement
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<Placements>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> RotateLabel
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

    public TickMarkStyle TickMarkStyle
    {
      get
      {
        return (TickMarkStyle) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UseFontPercent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public CustomLabel()
    {
    }

    internal CustomLabel(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Placement = Placements.Inside;
    }

    internal class Definition : DefinitionStore<CustomLabel, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        Text,
        AllowUpsideDown,
        DistanceFromScale,
        FontAngle,
        Placement,
        RotateLabel,
        TickMarkStyle,
        Value,
        Hidden,
        UseFontPercent,
        PropertyCount,
      }
    }
  }
}
