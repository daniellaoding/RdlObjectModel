namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class GaugeTickMarks : TickMarkStyle
  {
    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Interval
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> IntervalOffset
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public GaugeTickMarks()
    {
    }

    internal GaugeTickMarks(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<GaugeTickMarks, Definition.Properties>
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
        Interval,
        IntervalOffset,
        PropertyCount,
      }
    }
  }
}
