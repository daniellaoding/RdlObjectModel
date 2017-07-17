namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Thermometer : ReportObject
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

    [ReportExpressionDefaultValue(typeof (double), 5.0)]
    public ReportExpression<double> BulbOffset
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

    [ReportExpressionDefaultValue(typeof (double), 50.0)]
    public ReportExpression<double> BulbSize
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

    [ReportExpressionDefaultValue(typeof (ThermometerStyles), ThermometerStyles.Standard)]
    public ReportExpression<ThermometerStyles> ThermometerStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ThermometerStyles>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public Thermometer()
    {
    }

    internal Thermometer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      BulbOffset = 5.0;
      BulbSize = 50.0;
    }

    internal class Definition : DefinitionStore<Thermometer, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        BulbOffset,
        BulbSize,
        ThermometerStyle,
        PropertyCount,
      }
    }
  }
}
