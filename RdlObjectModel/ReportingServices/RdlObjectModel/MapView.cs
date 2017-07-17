using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("MapCustomView", typeof (MapCustomView))]
  [XmlElementClass("MapElementView", typeof (MapElementView))]
  [XmlElementClass("MapDataBoundView", typeof (MapDataBoundView))]
  public abstract class MapView : ReportObject
  {
    public ReportExpression<double> Zoom
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public MapView()
    {
    }

    internal MapView(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal class Definition : DefinitionStore<MapView, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Zoom,
      }
    }
  }
}
