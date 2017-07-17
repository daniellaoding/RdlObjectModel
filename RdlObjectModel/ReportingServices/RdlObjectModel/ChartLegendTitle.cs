using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartLegendTitle : ReportObject
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

    [ReportExpressionDefaultValue(typeof (ChartTitleSeparatorTypes), ChartTitleSeparatorTypes.None)]
    public ReportExpression<ChartTitleSeparatorTypes> TitleSeparator
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartTitleSeparatorTypes>>(2);
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

    public ChartLegendTitle()
    {
    }

    internal ChartLegendTitle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartLegendTitle, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Caption,
        CaptionLocID,
        TitleSeparator,
        Style,
        PropertyCount,
      }
    }
  }
}
