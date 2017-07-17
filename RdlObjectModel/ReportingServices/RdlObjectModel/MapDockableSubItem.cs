namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class MapDockableSubItem : MapSubItem
  {
    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapPositions), MapPositions.TopCenter)]
    public ReportExpression<MapPositions> Position
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapPositions>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    public ReportExpression<bool> DockOutsideViewport
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
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

    [ReportExpressionDefaultValue("")]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public MapDockableSubItem()
    {
    }

    internal MapDockableSubItem(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Position = MapPositions.TopCenter;
    }

    internal class Definition : DefinitionStore<MapDockableSubItem, Definition.Properties>
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
        ActionInfo,
        Position,
        DockOutsideViewport,
        Hidden,
        ToolTip,
      }
    }
  }
}
