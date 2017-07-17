namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapGridLines : ReportObject
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

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

    public ReportExpression<bool> ShowLabels
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapLabelPositions), MapLabelPositions.Near)]
    public ReportExpression<MapLabelPositions> LabelPosition
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapLabelPositions>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public MapGridLines()
    {
    }

    internal MapGridLines(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      LabelPosition = MapLabelPositions.Near;
    }

    internal class Definition : DefinitionStore<MapGridLines, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Hidden,
        Interval,
        ShowLabels,
        LabelPosition,
        PropertyCount,
      }
    }
  }
}
