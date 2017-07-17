namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartBorderSkin : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (ChartBorderSkinTypes), ChartBorderSkinTypes.None)]
    public ReportExpression<ChartBorderSkinTypes> ChartBorderSkinType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartBorderSkinTypes>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ChartBorderSkin()
    {
    }

    internal ChartBorderSkin(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartBorderSkin, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ChartBorderSkinType,
        Style,
        PropertyCount,
      }
    }
  }
}
