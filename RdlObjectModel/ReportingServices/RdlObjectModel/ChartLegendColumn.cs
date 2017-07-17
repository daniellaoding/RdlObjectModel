using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartLegendColumn : ReportObject, INamedObject
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

    public ChartLegendColumnTypes ColumnType
    {
      get
      {
        return (ChartLegendColumnTypes) PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, (int) value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Value
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

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> MinimumWidth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> MaximumWidth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 200)]
    public ReportExpression<int> SeriesSymbolWidth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 70)]
    public ReportExpression<int> SeriesSymbolHeight
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [XmlChildAttribute("ToolTip", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string ToolTipLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ChartLegendColumn()
    {
    }

    internal ChartLegendColumn(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      SeriesSymbolWidth = 200;
      SeriesSymbolHeight = 70;
    }

    internal class Definition : DefinitionStore<ChartLegendColumn, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        ColumnType,
        Value,
        Style,
        ActionInfo,
        ToolTip,
        ToolTipLocID,
        MinimumWidth,
        MaximumWidth,
        SeriesSymbolWidth,
        SeriesSymbolHeight,
        PropertyCount,
      }
    }
  }
}
