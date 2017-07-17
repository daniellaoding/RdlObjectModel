namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TopImage : BaseGaugeImage
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

    public TopImage()
    {
    }

    internal TopImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<TopImage, Definition.Properties>
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
