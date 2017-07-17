namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartAxisScaleBreak : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Enabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartBreakLineTypes), ChartBreakLineTypes.Ragged)]
    public ReportExpression<ChartBreakLineTypes> BreakLineType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartBreakLineTypes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 25)]
    public ReportExpression<int> CollapsibleSpaceThreshold
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 2)]
    public ReportExpression<int> MaxNumberOfBreaks
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 1.5)]
    public ReportExpression<double> Spacing
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

    [ReportExpressionDefaultValue(typeof (ChartIncludeZeroTypes), ChartIncludeZeroTypes.Auto)]
    public ReportExpression<ChartIncludeZeroTypes> IncludeZero
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartIncludeZeroTypes>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ChartAxisScaleBreak()
    {
    }

    internal ChartAxisScaleBreak(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Spacing = 1.5;
      CollapsibleSpaceThreshold = 25;
      MaxNumberOfBreaks = 2;
    }

    internal class Definition : DefinitionStore<ChartAxisScaleBreak, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Enabled,
        BreakLineType,
        CollapsibleSpaceThreshold,
        MaxNumberOfBreaks,
        Spacing,
        IncludeZero,
        Style,
        PropertyCount,
      }
    }
  }
}
