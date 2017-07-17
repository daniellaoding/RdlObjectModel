using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class MapSpatialElementTemplate : ReportObject
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

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0)]
    public ReportExpression<double> OffsetX
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

    [ReportExpressionDefaultValue(typeof (double), 0)]
    public ReportExpression<double> OffsetY
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

    [ReportExpressionDefaultValue("")]
    public ReportExpression Label
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue("")]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [DefaultValue(DataElementOutputTypes.Output)]
    [ValidEnumValues("MapDataElementOutputTypes")]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(8);
      }
      set
      {
        ((EnumProperty) DefinitionStore<MapSpatialElementTemplate, Definition.Properties>.GetProperty(8)).Validate(this, (int) value);
        PropertyStore.SetInteger(8, (int) value);
      }
    }

    [ReportExpressionDefaultValue("")]
    public ReportExpression DataElementLabel
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    public MapSpatialElementTemplate()
    {
    }

    internal MapSpatialElementTemplate(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DataElementOutput = DataElementOutputTypes.Output;
    }

    internal class Definition : DefinitionStore<MapSpatialElementTemplate, Definition.Properties>
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
      }
    }
  }
}
