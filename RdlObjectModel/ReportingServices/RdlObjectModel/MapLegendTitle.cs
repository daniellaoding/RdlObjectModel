using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapLegendTitle : ReportObject
  {
    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(0);
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

    [ReportExpressionDefaultValue(typeof (MapLegendTitleSeparators), MapLegendTitleSeparators.None)]
    public ReportExpression<MapLegendTitleSeparators> TitleSeparator
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapLegendTitleSeparators>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor), "Gray")]
    public ReportExpression<ReportColor> TitleSeparatorColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public MapLegendTitle()
    {
    }

    internal MapLegendTitle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TitleSeparator = MapLegendTitleSeparators.None;
      TitleSeparatorColor = new ReportExpression<ReportColor>("Gray", CultureInfo.InvariantCulture);
    }

    internal class Definition : DefinitionStore<MapLegendTitle, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Caption,
        TitleSeparator,
        TitleSeparatorColor,
        PropertyCount,
      }
    }
  }
}
