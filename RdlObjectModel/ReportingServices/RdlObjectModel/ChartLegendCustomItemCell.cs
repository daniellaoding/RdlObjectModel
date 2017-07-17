using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartLegendCustomItemCell : ReportObject, INamedObject
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

    public ChartLegendItemCellTypes CellType
    {
      get
      {
        return (ChartLegendItemCellTypes) PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, (int) value);
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

    [DefaultValue(1)]
    [ValidValues(0, 2147483647)]
    public int CellSpan
    {
      get
      {
        return PropertyStore.GetInteger(3);
      }
      set
      {
        ((ComparablePropertyDefinition<int>) DefinitionStore<ChartLegendCustomItemCell, Definition.Properties>.GetProperty(3)).Validate(this, value);
        PropertyStore.SetInteger(3, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(4);
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

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> ImageHeight
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> ImageWidth
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

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> SymbolHeight
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

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> SymbolWidth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [DefaultValue(ChartLegendItemAlignmentTypes.Center)]
    public ChartLegendItemAlignmentTypes Alignment
    {
      get
      {
        return (ChartLegendItemAlignmentTypes) PropertyStore.GetInteger(12);
      }
      set
      {
        PropertyStore.SetInteger(12, (int) value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> TopMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> BottomMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> LeftMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> RightMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [XmlChildAttribute("ToolTip", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string ToolTipLocID
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

    public ChartLegendCustomItemCell()
    {
    }

    internal ChartLegendCustomItemCell(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartLegendCustomItemCell, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        CellType,
        Text,
        CellSpan,
        Style,
        ActionInfo,
        ToolTip,
        ToolTipLocID,
        ImageHeight,
        ImageWidth,
        SymbolHeight,
        SymbolWidth,
        Alignment,
        TopMargin,
        BottomMargin,
        LeftMargin,
        RightMargin,
        PropertyCount,
      }
    }
  }
}
