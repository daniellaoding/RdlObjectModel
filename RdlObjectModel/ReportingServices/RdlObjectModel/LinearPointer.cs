namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class LinearPointer : GaugePointer
  {
    [ReportExpressionDefaultValue(typeof (LinearPointerTypes), LinearPointerTypes.Marker)]
    public ReportExpression<LinearPointerTypes> Type
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<LinearPointerTypes>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public Thermometer Thermometer
    {
      get
      {
        return (Thermometer) PropertyStore.GetObject(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    public LinearPointer()
    {
    }

    internal LinearPointer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<LinearPointer, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        GaugeInputValue,
        BarStart,
        DistanceFromScale,
        PointerImage,
        MarkerLength,
        MarkerStyle,
        Placement,
        SnappingEnabled,
        SnappingInterval,
        ToolTip,
        ActionInfo,
        Hidden,
        Width,
        Type,
        Thermometer,
        PropertyCount,
      }
    }
  }
}
