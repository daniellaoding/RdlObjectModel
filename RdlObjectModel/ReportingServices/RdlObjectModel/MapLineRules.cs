namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapLineRules : ReportObject
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

    public MapLineRules()
    {
    }

    internal MapLineRules(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

	  internal class Definition : DefinitionStore<MapLineRules, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        MapSizeRule,
        MapColorRule,
        PropertyCount,
      }
    }
  }
}
