namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartLegendColumnHeader : ReportObject
  {
    [ReportExpressionDefaultValue]
    public ReportExpression Value
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

    public ChartLegendColumnHeader()
    {
    }

    internal ChartLegendColumnHeader(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartLegendColumnHeader, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Value,
        Style,
        PropertyCount,
      }
    }
  }
}
