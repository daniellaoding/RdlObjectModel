namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapCustomView : MapView
  {
    [ReportExpressionDefaultValue(typeof (double), "50")]
    public ReportExpression<double> CenterX
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

    [ReportExpressionDefaultValue(typeof (double), "50")]
    public ReportExpression<double> CenterY
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

    public MapCustomView()
    {
    }

    internal MapCustomView(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      CenterX = 50.0;
      CenterY = 50.0;
    }

    internal class Definition : DefinitionStore<MapCustomView, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Zoom,
        CenterX,
        CenterY,
        PropertyCount,
      }
    }
  }
}
