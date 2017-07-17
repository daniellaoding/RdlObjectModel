using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartDataPoint : DataRegionCell
  {
    public ChartDataPointValues ChartDataPointValues
    {
      get
      {
        return (ChartDataPointValues) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ChartDataLabel ChartDataLabel
    {
      get
      {
        return (ChartDataLabel) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression AxisLabel
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

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(4);
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

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [DefaultValue(DataElementOutputTypes.ContentsOnly)]
    [ValidEnumValues("DataPointDataElementOutputTypes")]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(8);
      }
      set
      {
        ((EnumProperty) DefinitionStore<ChartDataPoint, Definition.Properties>.GetProperty(8)).Validate(this, (int) value);
        PropertyStore.SetInteger(8, (int) value);
      }
    }

    public ChartItemInLegend ChartItemInLegend
    {
      get
      {
        return (ChartItemInLegend) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    public ChartDataPoint()
    {
    }

    internal ChartDataPoint(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartDataPointValues = new ChartDataPointValues();
      CustomProperties = new RdlCollection<CustomProperty>();
      DataElementOutput = DataElementOutputTypes.ContentsOnly;
    }

    internal class Definition : DefinitionStore<ChartDataPoint, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ChartDataPointValues,
        ChartDataLabel,
        AxisLabel,
        ToolTip,
        ActionInfo,
        Style,
        ChartMarker,
        DataElementName,
        DataElementOutput,
        ChartItemInLegend,
        CustomProperties,
        PropertyCount,
      }
    }
  }
}
