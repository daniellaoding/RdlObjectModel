namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartMarker : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (ChartMarkerTypes), ChartMarkerTypes.None)]
    public ReportExpression<ChartMarkerTypes> Type
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartMarkerTypes>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "3.75pt")]
    public ReportExpression<ReportSize> Size
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public EmptyColorStyle Style
    {
      get
      {
        return (EmptyColorStyle) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ChartMarker()
    {
    }

    internal ChartMarker(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartMarker, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Type,
        Size,
        Style,
        PropertyCount,
      }
    }
  }
}
