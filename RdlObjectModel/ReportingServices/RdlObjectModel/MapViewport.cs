using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapViewport : MapSubItem
  {
    [ReportExpressionDefaultValue(typeof (MapCoordinateSystems), MapCoordinateSystems.Planar)]
    public ReportExpression<MapCoordinateSystems> MapCoordinateSystem
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapCoordinateSystems>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapProjections), MapProjections.Equirectangular)]
    public ReportExpression<MapProjections> MapProjection
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapProjections>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    public ReportExpression<double> ProjectionCenterX
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

    public ReportExpression<double> ProjectionCenterY
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

    public MapLimits MapLimits
    {
      get
      {
        return (MapLimits) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), "20000")]
    public ReportExpression<double> MaximumZoom
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), "20")]
    public ReportExpression<double> MinimumZoom
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), "0")]
    public ReportExpression<double> SimplificationResolution
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    public MapView MapView
    {
      get
      {
        return (MapView) PropertyStore.GetObject(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize), "10pt")]
    public ReportExpression<ReportSize> ContentMargin
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    public MapGridLines MapMeridians
    {
      get
      {
        return (MapGridLines) PropertyStore.GetObject(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    public MapGridLines MapParallels
    {
      get
      {
        return (MapGridLines) PropertyStore.GetObject(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    public ReportExpression<bool> GridUnderContent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    public MapViewport()
    {
    }

    internal MapViewport(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapCoordinateSystem = MapCoordinateSystems.Planar;
      MapProjection = MapProjections.Equirectangular;
      MaximumZoom = 20000.0;
      MinimumZoom = 20.0;
      SimplificationResolution = 0.0;
      ContentMargin = new ReportExpression<ReportSize>("10pt", CultureInfo.InvariantCulture);
    }

    internal class Definition : DefinitionStore<MapViewport, Definition.Properties>
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
        MapCoordinateSystem,
        MapProjection,
        ProjectionCenterX,
        ProjectionCenterY,
        MapLimits,
        MaximumZoom,
        MinimumZoom,
        MapView,
        ContentMargin,
        MapMeridians,
        MapParallels,
        GridUnderContent,
        SimplificationResolution,
        PropertyCount,
      }
    }
  }
}
