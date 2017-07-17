using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartSeries : ReportObject, INamedObject
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

    [XmlElement(typeof (RdlCollection<ChartDataPoint>))]
    public IList<ChartDataPoint> ChartDataPoints
    {
      get
      {
        return (IList<ChartDataPoint>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartTypes), ChartTypes.Column)]
    public ReportExpression<ChartTypes> Type
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartTypes>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartSubtypes), ChartSubtypes.Plain)]
    public ReportExpression<ChartSubtypes> Subtype
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartSubtypes>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public EmptyColorStyle Style
    {
      get
      {
        return (EmptyColorStyle) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public ChartMarker ChartMarker
    {
      get
      {
        return (ChartMarker) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ChartDataLabel ChartDataLabel
    {
      get
      {
        return (ChartDataLabel) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public ChartEmptyPoints ChartEmptyPoints
    {
      get
      {
        return (ChartEmptyPoints) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [DefaultValue("")]
    public string LegendName
    {
      get
      {
        return (string) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    public ChartItemInLegend ChartItemInLegend
    {
      get
      {
        return (ChartItemInLegend) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [DefaultValue("")]
    public string ChartAreaName
    {
      get
      {
        return (string) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [DefaultValue("")]
    public string ValueAxisName
    {
      get
      {
        return (string) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [DefaultValue("")]
    public string CategoryAxisName
    {
      get
      {
        return (string) PropertyStore.GetObject(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    public ChartSmartLabel ChartSmartLabel
    {
      get
      {
        return (ChartSmartLabel) PropertyStore.GetObject(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    public ChartSeries()
    {
    }

    internal ChartSeries(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartDataPoints = new RdlCollection<ChartDataPoint>();
      CustomProperties = new RdlCollection<CustomProperty>();
      Style = new EmptyColorStyle();
    }

    internal class Definition : DefinitionStore<ChartSeries, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Hidden,
        ChartDataPoints,
        Type,
        Subtype,
        Style,
        ChartMarker,
        ChartDataLabel,
        ChartEmptyPoints,
        CustomProperties,
        LegendName,
        ChartItemInLegend,
        ChartAreaName,
        ValueAxisName,
        CategoryAxisName,
        PropertyCount,
        ChartSmartLabel,
      }
    }
  }
}
