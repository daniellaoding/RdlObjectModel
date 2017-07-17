namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapLocation : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (double), "0")]
    public ReportExpression<double> Left
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

    [ReportExpressionDefaultValue(typeof (double), "0")]
    public ReportExpression<double> Top
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

    public MapLocation()
    {
    }

    internal MapLocation(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Left = 0.0;
      Top = 0.0;
      Unit = MapUnits.Percentage;
    }

    internal class Definition : DefinitionStore<MapLocation, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Left,
        Top,
        Unit,
        PropertyCount,
      }
    }
  }
}
