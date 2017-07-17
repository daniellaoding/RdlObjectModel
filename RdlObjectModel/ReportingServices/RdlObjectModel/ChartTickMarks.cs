namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartTickMarks : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (ChartTickMarksEnabledTypes), ChartTickMarksEnabledTypes.Auto)]
    public ReportExpression<ChartTickMarksEnabledTypes> Enabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartTickMarksEnabledTypes>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartTickMarkTypes), ChartTickMarkTypes.Outside)]
    public ReportExpression<ChartTickMarkTypes> Type
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartTickMarkTypes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 1.0)]
    public ReportExpression<double> Length
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Interval
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartIntervalTypes), ChartIntervalTypes.Default)]
    public ReportExpression<ChartIntervalTypes> IntervalType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> IntervalOffset
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Default)]
    public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public ChartTickMarks()
    {
    }

    internal ChartTickMarks(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Length = 1.0;
    }

    internal class Definition : DefinitionStore<ChartTickMarks, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Enabled,
        Type,
        Style,
        Length,
        Interval,
        IntervalType,
        IntervalOffset,
        IntervalOffsetType,
        PropertyCount,
      }
    }
  }
}
