namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class PointerCap : ReportObject
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

    public CapImage CapImage
    {
      get
      {
        return (CapImage) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> OnTop
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Reflection
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (CapStyles), CapStyles.RoundedDark)]
    public ReportExpression<CapStyles> CapStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<CapStyles>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 26.0)]
    public ReportExpression<double> Width
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public PointerCap()
    {
    }

    internal PointerCap(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Width = 26.0;
    }

    internal class Definition : DefinitionStore<PointerCap, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        CapImage,
        OnTop,
        Reflection,
        CapStyle,
        Hidden,
        Width,
        PropertyCount,
      }
    }
  }
}
