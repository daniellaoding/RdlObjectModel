using System.Globalization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("MapMarkerTemplate", typeof (MapMarkerTemplate))]
  public abstract class MapPointTemplate : MapSpatialElementTemplate
  {
    [ReportExpressionDefaultValue(typeof (ReportSize), "5.25pt")]
    public ReportExpression<ReportSize> Size
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapPointLabelPlacements), MapPointLabelPlacements.Bottom)]
    public ReportExpression<MapPointLabelPlacements> LabelPlacement
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapPointLabelPlacements>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public MapPointTemplate()
    {
    }

    internal MapPointTemplate(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Size = new ReportExpression<ReportSize>("5.25pt", CultureInfo.InvariantCulture);
      LabelPlacement = MapPointLabelPlacements.Bottom;
    }

    internal class Definition : DefinitionStore<MapPointTemplate, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        ActionInfo,
        Hidden,
        OffsetX,
        OffsetY,
        Label,
        ToolTip,
        DataElementName,
        DataElementOutput,
        DataElementLabel,
        Size,
        LabelPlacement,
      }
    }
  }
}
