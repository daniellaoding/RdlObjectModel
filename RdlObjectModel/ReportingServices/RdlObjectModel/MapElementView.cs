using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapElementView : MapView
  {
    public ReportExpression LayerName
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapBindingFieldPair>))]
    public IList<MapBindingFieldPair> MapBindingFieldPairs
    {
      get
      {
        return (IList<MapBindingFieldPair>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public MapElementView()
    {
    }

    internal MapElementView(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapBindingFieldPairs = new RdlCollection<MapBindingFieldPair>();
    }

    internal class Definition : DefinitionStore<MapElementView, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Zoom,
        LayerName,
        MapBindingFieldPairs,
        PropertyCount,
      }
    }
  }
}
