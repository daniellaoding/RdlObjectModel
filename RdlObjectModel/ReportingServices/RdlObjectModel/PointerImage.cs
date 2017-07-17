namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class PointerImage : BaseGaugeImage
  {
    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> HueColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Transparency
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> OffsetX
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> OffsetY
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public PointerImage()
    {
    }

    internal PointerImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<PointerImage, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Source,
        Value,
        MIMEType,
        TransparentColor,
        HueColor,
        Transparency,
        OffsetX,
        OffsetY,
        PropertyCount,
      }
    }
  }
}
