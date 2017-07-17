namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class RadialPointer : GaugePointer
  {
    [ReportExpressionDefaultValue(typeof (RadialPointerTypes), RadialPointerTypes.Needle)]
    public ReportExpression<RadialPointerTypes> Type
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<RadialPointerTypes>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    public PointerCap PointerCap
    {
      get
      {
        return (PointerCap) PropertyStore.GetObject(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (NeedleStyles), NeedleStyles.Triangular)]
    public ReportExpression<NeedleStyles> NeedleStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<NeedleStyles>>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    public RadialPointer()
    {
    }

    internal RadialPointer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<RadialPointer, Definition.Properties>
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
        PointerCap,
        NeedleStyle,
        PropertyCount,
      }
    }
  }
}
