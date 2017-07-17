using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Map : ReportItem
  {
    public const bool AllowTypeInHeaderFooter = false;

    public MapViewport MapViewport
    {
      get
      {
        return (MapViewport) PropertyStore.GetObject(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapDataRegion>))]
    public IList<MapDataRegion> MapDataRegions
    {
      get
      {
        return (IList<MapDataRegion>) PropertyStore.GetObject(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapLayer>))]
    public IList<MapLayer> MapLayers
    {
      get
      {
        return (IList<MapLayer>) PropertyStore.GetObject(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapLegend>))]
    public IList<MapLegend> MapLegends
    {
      get
      {
        return (IList<MapLegend>) PropertyStore.GetObject(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapTitle>))]
    public IList<MapTitle> MapTitles
    {
      get
      {
        return (IList<MapTitle>) PropertyStore.GetObject(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    public MapDistanceScale MapDistanceScale
    {
      get
      {
        return (MapDistanceScale) PropertyStore.GetObject(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    public MapColorScale MapColorScale
    {
      get
      {
        return (MapColorScale) PropertyStore.GetObject(24);
      }
      set
      {
        PropertyStore.SetObject(24, value);
      }
    }

    public MapBorderSkin MapBorderSkin
    {
      get
      {
        return (MapBorderSkin) PropertyStore.GetObject(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    public PageBreak PageBreak
    {
      get
      {
        return (PageBreak) PropertyStore.GetObject(26);
      }
      set
      {
        PropertyStore.SetObject(26, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression PageName
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(34);
      }
      set
      {
        PropertyStore.SetObject(34, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapAntiAliasings), MapAntiAliasings.All)]
    public ReportExpression<MapAntiAliasings> AntiAliasing
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapAntiAliasings>>(27);
      }
      set
      {
        PropertyStore.SetObject(27, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapTextAntiAliasingQualities), MapTextAntiAliasingQualities.High)]
    public ReportExpression<MapTextAntiAliasingQualities> TextAntiAliasingQuality
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapTextAntiAliasingQualities>>(28);
      }
      set
      {
        PropertyStore.SetObject(28, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), "25")]
    public ReportExpression<double> ShadowIntensity
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(29);
      }
      set
      {
        PropertyStore.SetObject(29, value);
      }
    }

    [DefaultValue(20000)]
    public int MaximumSpatialElementCount
    {
      get
      {
        return PropertyStore.GetInteger(30);
      }
      set
      {
        PropertyStore.SetInteger(30, value);
      }
    }

    [DefaultValue(1000000)]
    public int MaximumTotalPointCount
    {
      get
      {
        return PropertyStore.GetInteger(31);
      }
      set
      {
        PropertyStore.SetInteger(31, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression TileLanguage
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(32);
      }
      set
      {
        PropertyStore.SetObject(32, value);
      }
    }

    [XmlIgnore]
    public override bool AllowInHeaderFooter => false;

	  public Map()
    {
    }

    internal Map(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapViewport = new MapViewport();
      MapLayers = new RdlCollection<MapLayer>();
      MapLegends = new RdlCollection<MapLegend>();
      MapTitles = new RdlCollection<MapTitle>();
      AntiAliasing = MapAntiAliasings.All;
      TextAntiAliasingQuality = MapTextAntiAliasingQualities.High;
      ShadowIntensity = 25.0;
      MaximumSpatialElementCount = 20000;
      MaximumTotalPointCount = 1000000;
    }

    internal class Definition : DefinitionStore<Map, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Name,
        ActionInfo,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Visibility,
        ToolTip,
        ToolTipLocID,
        DocumentMapLabel,
        DocumentMapLabelLocID,
        Bookmark,
        RepeatWith,
        CustomProperties,
        DataElementName,
        DataElementOutput,
        MapViewport,
        MapDataRegions,
        MapLayers,
        MapLegends,
        MapTitles,
        MapDistanceScale,
        MapColorScale,
        MapBorderSkin,
        PageBreak,
        AntiAliasing,
        TextAntiAliasingQuality,
        ShadowIntensity,
        MaximumSpatialElementCount,
        MaximumTotalPointCount,
        TileLanguage,
        PropertyCount,
        PageName,
      }
    }
  }
}
