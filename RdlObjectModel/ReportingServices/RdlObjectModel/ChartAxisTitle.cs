using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartAxisTitle : ReportObject
  {
    public ReportExpression Caption
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartAxisTitlePositions), ChartAxisTitlePositions.Center)]
    public ReportExpression<ChartAxisTitlePositions> Position
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartAxisTitlePositions>>(2);
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

    [ReportExpressionDefaultValue(typeof (TextOrientations), TextOrientations.Auto)]
    public ReportExpression<TextOrientations> TextOrientation
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextOrientations>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [XmlChildAttribute("Caption", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string CaptionLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ChartAxisTitle()
    {
    }

    internal ChartAxisTitle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartAxisTitle, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Caption,
        CaptionLocID,
        Position,
        Style,
        TextOrientation,
        PropertyCount,
      }
    }
  }
}
