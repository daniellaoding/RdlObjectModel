namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapSpatialDataRegion : MapSpatialData
  {
    public ReportExpression VectorData
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

    public MapSpatialDataRegion()
    {
    }

    internal MapSpatialDataRegion(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapSpatialDataRegion, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        VectorData,
        PropertyCount,
      }
    }
  }
}
