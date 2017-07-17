using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class NumericIndicatorRange : ReportObject, INamedObject
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

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> DecimalDigitColor
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

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> DigitColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public NumericIndicatorRange()
    {
    }

    internal NumericIndicatorRange(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<NumericIndicatorRange, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        StartValue,
        EndValue,
        DecimalDigitColor,
        DigitColor,
        PropertyCount,
      }
    }
  }
}
