namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TickMarkImage : BaseGaugeImage
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

    public TickMarkImage()
    {
    }

    internal TickMarkImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<TickMarkImage, Definition.Properties>
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
        PropertyCount,
      }
    }
  }
}
