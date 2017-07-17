namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapSize : ReportObject
  {
    public ReportExpression<double> Width
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

    public ReportExpression<double> Height
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

    [ReportExpressionDefaultValue(typeof (MapUnits), MapUnits.Percentage)]
    public ReportExpression<MapUnits> Unit
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapUnits>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public MapSize()
    {
    }

    internal MapSize(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Unit = MapUnits.Percentage;
    }

    internal class Definition : DefinitionStore<MapSize, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Width,
        Height,
        Unit,
        PropertyCount,
      }
    }
  }
}
