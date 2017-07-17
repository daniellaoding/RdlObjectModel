using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapTitle : MapDockableSubItem, INamedObject
  {
    [XmlAttribute(typeof (string))]
    public string Name
    {
      get
      {
        return (string) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    public ReportExpression Text
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), "0")]
    public ReportExpression<double> Angle
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

    public ReportExpression<ReportSize> TextShadowOffset
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

    public MapTitle()
    {
    }

    internal MapTitle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Angle = 0.0;
    }

    internal class Definition : DefinitionStore<MapTitle, Definition.Properties>
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
        MapPosition,
        DockOutsideViewport,
        Hidden,
        ToolTip,
        Name,
        Text,
        Angle,
        TextShadowOffset,
        PropertyCount,
      }
    }
  }
}
