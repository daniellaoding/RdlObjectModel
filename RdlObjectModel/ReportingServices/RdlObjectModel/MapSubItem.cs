namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class MapSubItem : ReportObject
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

    public MapLocation MapLocation
    {
      get
      {
        return (MapLocation) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public MapSize MapSize
    {
      get
      {
        return (MapSize) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "0in")]
    public ReportExpression<ReportSize> LeftMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "0in")]
    public ReportExpression<ReportSize> RightMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "0in")]
    public ReportExpression<ReportSize> TopMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "0in")]
    public ReportExpression<ReportSize> BottomMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), "0")]
    public ReportExpression<int> ZIndex
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public MapSubItem()
    {
    }

    internal MapSubItem(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ZIndex = 0;
    }

    internal class Definition : DefinitionStore<MapSubItem, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        MapLocation,
        MapSize,
        LeftMargin,
        RightMargin,
        TopMargin,
        BottomMargin,
        ZIndex,
      }
    }
  }
}
