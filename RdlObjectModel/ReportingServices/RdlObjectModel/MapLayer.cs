using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("MapLineLayer", typeof (MapLineLayer))]
  [XmlElementClass("MapPointLayer", typeof (MapPointLayer))]
  [XmlElementClass("MapTileLayer", typeof (MapTileLayer))]
  [XmlElementClass("MapPolygonLayer", typeof (MapPolygonLayer))]
  public abstract class MapLayer : ReportObject, INamedObject
  {
    [XmlAttribute(typeof (string))]
    public string Name
    {
      get
      {
        return PropertyStore.GetObject<string>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapVisibilityModes), MapVisibilityModes.Visible)]
    public ReportExpression<MapVisibilityModes> VisibilityMode
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapVisibilityModes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), "50")]
    public ReportExpression<double> MinimumZoom
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

    [ReportExpressionDefaultValue(typeof (double), "200")]
    public ReportExpression<double> MaximumZoom
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

    [ReportExpressionDefaultValue(typeof (double), "0")]
    public ReportExpression<double> Transparency
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

    public MapLayer()
    {
    }

    internal MapLayer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      VisibilityMode = MapVisibilityModes.Visible;
      MinimumZoom = 50.0;
      MaximumZoom = 200.0;
      Transparency = 0.0;
    }

    internal class Definition : DefinitionStore<MapLayer, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        VisibilityMode,
        MinimumZoom,
        MaximumZoom,
        Transparency,
      }
    }
  }
}
