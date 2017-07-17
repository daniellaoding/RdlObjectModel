namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class BackFrame : ReportObject
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

    [ReportExpressionDefaultValue(typeof (FrameStyles), FrameStyles.None)]
    public ReportExpression<FrameStyles> FrameStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<FrameStyles>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (FrameShapes), FrameShapes.Default)]
    public ReportExpression<FrameShapes> FrameShape
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<FrameShapes>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 8.0)]
    [ValidValues(0.0, 50.0)]
    public ReportExpression<double> FrameWidth
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

    [ReportExpressionDefaultValue(typeof (GlassEffects), GlassEffects.None)]
    public ReportExpression<GlassEffects> GlassEffect
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<GlassEffects>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public FrameBackground FrameBackground
    {
      get
      {
        return (FrameBackground) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public FrameImage FrameImage
    {
      get
      {
        return (FrameImage) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public BackFrame()
    {
    }

    internal BackFrame(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      FrameWidth = 8.0;
    }

    internal class Definition : DefinitionStore<BackFrame, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        FrameStyle,
        FrameShape,
        FrameWidth,
        GlassEffect,
        FrameBackground,
        FrameImage,
        PropertyCount,
      }
    }
  }
}
