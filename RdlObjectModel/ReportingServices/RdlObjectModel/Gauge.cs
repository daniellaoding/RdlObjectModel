using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Gauge : GaugePanelItem
  {
    [XmlElement(typeof (RdlCollection<GaugeScale>))]
    public IList<GaugeScale> GaugeScales
    {
      get
      {
        return (IList<GaugeScale>) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public BackFrame BackFrame
    {
      get
      {
        return (BackFrame) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> ClipContent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public TopImage TopImage
    {
      get
      {
        return (TopImage) PropertyStore.GetObject(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> AspectRatio
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public Gauge()
    {
    }

    internal Gauge(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      GaugeScales = new RdlCollection<GaugeScale>();
    }

    internal class Definition : DefinitionStore<Gauge, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Hidden,
        ToolTip,
        ActionInfo,
        ParentItem,
        GaugeScales,
        BackFrame,
        ClipContent,
        TopImage,
        AspectRatio,
        PropertyCount,
      }
    }
  }
}
