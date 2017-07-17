namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class FrameImage : BaseGaugeImage
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> ClipImage
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public FrameImage()
    {
    }

    internal FrameImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<FrameImage, Definition.Properties>
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
        ClipImage,
        PropertyCount,
      }
    }
  }
}
