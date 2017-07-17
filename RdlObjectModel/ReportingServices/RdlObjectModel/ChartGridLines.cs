namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartGridLines : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (ChartGridLinesEnabledTypes), ChartGridLinesEnabledTypes.Auto)]
    public ReportExpression<ChartGridLinesEnabledTypes> Enabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartGridLinesEnabledTypes>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Interval
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

    [ReportExpressionDefaultValue(typeof (ChartIntervalTypes), ChartIntervalTypes.Default)]
    public ReportExpression<ChartIntervalTypes> IntervalType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> IntervalOffset
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

    [ReportExpressionDefaultValue(typeof (ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Default)]
    public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public ChartGridLines()
    {
    }

    internal ChartGridLines(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartGridLines, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Hidden,
        Style,
        Interval,
        IntervalType,
        IntervalOffset,
        IntervalOffsetType,
        PropertyCount,
      }
    }
  }
}
