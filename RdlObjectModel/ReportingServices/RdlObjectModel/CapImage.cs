namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class CapImage : BaseGaugeImage
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

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> OffsetX
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> OffsetY
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

    public CapImage()
    {
    }

    internal CapImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<CapImage, Definition.Properties>
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
        OffsetX,
        OffsetY,
        PropertyCount,
      }
    }
  }
}
