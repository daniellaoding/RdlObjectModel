namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapPointRules : ReportObject
  {
    public MapSizeRule MapSizeRule
    {
      get
      {
        return (MapSizeRule) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public MapColorRule MapColorRule
    {
      get
      {
        return (MapColorRule) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public MapMarkerRule MapMarkerRule
    {
      get
      {
        return (MapMarkerRule) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public MapPointRules()
    {
    }

    internal MapPointRules(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapPointRules, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        MapSizeRule,
        MapColorRule,
        MapMarkerRule,
        PropertyCount,
      }
    }
  }
}
