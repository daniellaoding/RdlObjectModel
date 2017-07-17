namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ScalePin : TickMarkStyle
  {
    [ReportExpressionDefaultValue(typeof (double), 5.0)]
    public ReportExpression<double> Location
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Enable
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public PinLabel PinLabel
    {
      get
      {
        return (PinLabel) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public ScalePin()
    {
    }

    internal ScalePin(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Location = 5.0;
    }

    internal class Definition : DefinitionStore<ScalePin, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        DistanceFromScale,
        Placement,
        EnableGradient,
        GradientDensity,
        TickMarkImage,
        Length,
        Width,
        Shape,
        Hidden,
        Location,
        Enable,
        PinLabel,
        PropertyCount,
      }
    }
  }
}
