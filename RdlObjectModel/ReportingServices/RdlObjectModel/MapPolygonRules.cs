namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapPolygonRules : ReportObject
  {
    public MapColorRule MapColorRule
    {
      get
      {
        return (MapColorRule) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public MapPolygonRules()
    {
    }

    internal MapPolygonRules(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapPolygonRules, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        MapColorRule,
        PropertyCount,
      }
    }
  }
}
