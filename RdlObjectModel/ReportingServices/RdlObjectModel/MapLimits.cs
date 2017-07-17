namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapLimits : ReportObject
  {
    public ReportExpression<double> MinimumX
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ReportExpression<double> MinimumY
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ReportExpression<double> MaximumX
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ReportExpression<double> MaximumY
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public MapLimits()
    {
    }

    internal MapLimits(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapLimits, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        MinimumX,
        MinimumY,
        MaximumX,
        MaximumY,
        PropertyCount,
      }
    }
  }
}
