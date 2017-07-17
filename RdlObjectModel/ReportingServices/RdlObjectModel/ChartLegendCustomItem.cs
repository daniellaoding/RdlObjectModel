using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartLegendCustomItem : ReportObject, INamedObject
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

    [XmlElement(typeof (RdlCollection<ChartLegendCustomItemCell>))]
    public IList<ChartLegendCustomItemCell> ChartLegendCustomItemCells
    {
      get
      {
        return (IList<ChartLegendCustomItemCell>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ChartMarker ChartMarker
    {
      get
      {
        return (ChartMarker) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartLegendItemSeparatorTypes), ChartLegendItemSeparatorTypes.None)]
    public ReportExpression<ChartLegendItemSeparatorTypes> Separator
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartLegendItemSeparatorTypes>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> SeparatorColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(5);
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

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
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

    public ChartLegendCustomItem()
    {
    }

    internal ChartLegendCustomItem(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartLegendCustomItemCells = new RdlCollection<ChartLegendCustomItemCell>();
    }

    internal class Definition : DefinitionStore<ChartLegendCustomItem, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        ChartLegendCustomItemCells,
        Style,
        ChartMarker,
        Separator,
        SeparatorColor,
        ToolTip,
        ToolTipLocID,
        ActionInfo,
        PropertyCount,
      }
    }
  }
}
