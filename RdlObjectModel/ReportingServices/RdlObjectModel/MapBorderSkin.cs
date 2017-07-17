namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapBorderSkin : ReportObject
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

    [ReportExpressionDefaultValue(typeof (MapBorderSkinTypes), MapBorderSkinTypes.None)]
    public ReportExpression<MapBorderSkinTypes> MapBorderSkinType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapBorderSkinTypes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public MapBorderSkin()
    {
    }

    internal MapBorderSkin(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapBorderSkinType = MapBorderSkinTypes.None;
    }

    internal class Definition : DefinitionStore<MapBorderSkin, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        MapBorderSkinType,
        PropertyCount,
      }
    }
  }
}
