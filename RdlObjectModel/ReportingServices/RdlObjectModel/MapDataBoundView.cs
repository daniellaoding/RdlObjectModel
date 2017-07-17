namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapDataBoundView : MapView
  {
    public MapDataBoundView()
    {
    }

    internal MapDataBoundView(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapDataBoundView, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Zoom,
        PropertyCount,
      }
    }
  }
}
