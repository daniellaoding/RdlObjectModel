using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartTitle : ReportObject, INamedObject
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

    public ReportExpression Caption
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
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

    [ReportExpressionDefaultValue(typeof (ChartPositions), ChartPositions.TopCenter)]
    public ReportExpression<ChartPositions> Position
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartPositions>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [DefaultValue("")]
    public string DockToChartArea
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> DockOutsideChartArea
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

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> DockOffset
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

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (TextOrientations), TextOrientations.Auto)]
    public ReportExpression<TextOrientations> TextOrientation
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextOrientations>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [XmlChildAttribute("Caption", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string CaptionLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlChildAttribute("ToolTip", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string ToolTipLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public ChartTitle()
    {
    }

    internal ChartTitle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Position = ChartPositions.TopCenter;
    }

    internal class Definition : DefinitionStore<ChartTitle, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Caption,
        CaptionLocID,
        Hidden,
        Style,
        Position,
        DockToChartArea,
        DockOutsideChartArea,
        DockOffset,
        ChartElementPosition,
        ToolTip,
        ToolTipLocID,
        ActionInfo,
        TextOrientation,
        PropertyCount,
      }
    }
  }
}
