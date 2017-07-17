using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartEmptyPoints : ReportObject
  {
    public EmptyColorStyle Style
    {
      get
      {
        return (EmptyColorStyle) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ChartMarker ChartMarker
    {
      get
      {
        return (ChartMarker) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ChartDataLabel ChartDataLabel
    {
      get
      {
        return (ChartDataLabel) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression AxisLabel
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

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ChartEmptyPoints()
    {
    }

    internal ChartEmptyPoints(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      CustomProperties = new RdlCollection<CustomProperty>();
    }

    internal class Definition : DefinitionStore<ChartEmptyPoints, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        ChartMarker,
        ChartDataLabel,
        AxisLabel,
        ToolTip,
        ActionInfo,
        CustomProperties,
        PropertyCount,
      }
    }
  }
}
