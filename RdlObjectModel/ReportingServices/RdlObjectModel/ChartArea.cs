using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartArea : ReportObject, INamedObject
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

    [XmlElement(typeof (RdlCollection<ChartAxis>))]
    public IList<ChartAxis> ChartCategoryAxes
    {
      get
      {
        return (IList<ChartAxis>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartAxis>))]
    public IList<ChartAxis> ChartValueAxes
    {
      get
      {
        return (IList<ChartAxis>) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public ChartThreeDProperties ChartThreeDProperties
    {
      get
      {
        return (ChartThreeDProperties) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartAlignOrientations), ChartAlignOrientations.None)]
    public ReportExpression<ChartAlignOrientations> AlignOrientation
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartAlignOrientations>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ChartAlignType ChartAlignType
    {
      get
      {
        return (ChartAlignType) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [DefaultValue("")]
    public string AlignWithChartArea
    {
      get
      {
        return (string) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    public ChartElementPosition ChartElementPosition
    {
      get
      {
        return (ChartElementPosition) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    public ChartElementPosition ChartInnerPlotPosition
    {
      get
      {
        return (ChartElementPosition) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> EquallySizedAxesFont
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

    public ChartArea()
    {
    }

    internal ChartArea(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartCategoryAxes = new RdlCollection<ChartAxis>();
      ChartValueAxes = new RdlCollection<ChartAxis>();
    }

    internal class Definition : DefinitionStore<ChartArea, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Hidden,
        ChartCategoryAxes,
        ChartValueAxes,
        ChartThreeDProperties,
        Style,
        AlignOrientation,
        ChartAlignType,
        AlignWithChartArea,
        ChartElementPosition,
        ChartInnerPlotPosition,
        EquallySizedAxesFont,
        PropertyCount,
      }
    }
  }
}
