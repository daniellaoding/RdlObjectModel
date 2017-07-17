namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TickMarkStyle : ReportObject
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> DistanceFromScale
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (Placements), Placements.Inside)]
    public ReportExpression<Placements> Placement
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<Placements>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> EnableGradient
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

    [ReportExpressionDefaultValue(typeof (double), 30.0)]
    [ValidValues(0.0, 100.0)]
    public ReportExpression<double> GradientDensity
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

    public TickMarkImage TickMarkImage
    {
      get
      {
        return (TickMarkImage) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public ReportExpression<double> Length
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

    public ReportExpression<double> Width
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MarkerStyles), MarkerStyles.Rectangle)]
    public ReportExpression<MarkerStyles> Shape
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MarkerStyles>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    public TickMarkStyle()
    {
    }

    internal TickMarkStyle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Placement = Placements.Inside;
      Shape = MarkerStyles.Rectangle;
      GradientDensity = 30.0;
    }

    internal class Definition : DefinitionStore<TickMarkStyle, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        DistanceFromScale,
        Placement,
        EnableGradient,
        GradientDensity,
        TickMarkImage,
        Length,
        Width,
        Shape,
        Hidden,
        PropertyCount,
      }
    }
  }
}
