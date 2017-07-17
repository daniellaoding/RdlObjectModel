using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class IndicatorState : ReportObject, INamedObject
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

    public GaugeInputValue StartValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public GaugeInputValue EndValue
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

    public ReportExpression<ReportColor> Color
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public ReportExpression<double> ScaleFactor
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

    public ReportExpression<GaugeStateIndicatorStyles> IndicatorStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<GaugeStateIndicatorStyles>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public IndicatorImage IndicatorImage
    {
      get
      {
        return (IndicatorImage) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public IndicatorState()
    {
    }

    internal IndicatorState(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ScaleFactor = 1.0;
    }

    internal class Definition : DefinitionStore<IndicatorState, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        StartValue,
        EndValue,
        Color,
        ScaleFactor,
        IndicatorStyle,
        IndicatorImage,
        PropertyCount,
      }
    }
  }
}
