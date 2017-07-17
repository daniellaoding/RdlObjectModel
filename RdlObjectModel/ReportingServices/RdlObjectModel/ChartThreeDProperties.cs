namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartThreeDProperties : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Enabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartProjectionModes), ChartProjectionModes.Oblique)]
    public ReportExpression<ChartProjectionModes> ProjectionMode
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartProjectionModes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 0)]
    public ReportExpression<int> Perspective
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 30)]
    public ReportExpression<int> Rotation
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 30)]
    public ReportExpression<int> Inclination
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 100)]
    public ReportExpression<int> DepthRatio
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartShadings), ChartShadings.Real)]
    public ReportExpression<ChartShadings> Shading
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartShadings>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 100)]
    public ReportExpression<int> GapDepth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 7)]
    public ReportExpression<int> WallThickness
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Clustered
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

    public ChartThreeDProperties()
    {
    }

    internal ChartThreeDProperties(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Rotation = 30;
      Inclination = 30;
      DepthRatio = 100;
      GapDepth = 100;
      WallThickness = 7;
    }

    internal class Definition : DefinitionStore<ChartThreeDProperties, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Enabled,
        ProjectionMode,
        Perspective,
        Rotation,
        Inclination,
        DepthRatio,
        Shading,
        GapDepth,
        WallThickness,
        Clustered,
        PropertyCount,
      }
    }
  }
}
