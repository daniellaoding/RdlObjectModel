namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapBucket : ReportObject
  {
    public ReportExpression StartValue
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

    public ReportExpression EndValue
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

    public MapBucket()
    {
    }

    internal MapBucket(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapBucket, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        StartValue,
        EndValue,
        PropertyCount,
      }
    }
  }
}
