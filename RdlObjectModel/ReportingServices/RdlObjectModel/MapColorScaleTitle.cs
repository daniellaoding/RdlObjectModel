namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapColorScaleTitle : ReportObject
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

    public MapColorScaleTitle()
    {
    }

    internal MapColorScaleTitle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapColorScaleTitle, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Caption,
        PropertyCount,
      }
    }
  }
}
