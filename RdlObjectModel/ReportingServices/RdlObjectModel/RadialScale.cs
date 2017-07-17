using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class RadialScale : GaugeScale
  {
    [ValidValues(0.0, Double.MaxValue)]
    [ReportExpressionDefaultValue(typeof (double), 37.0)]
    public ReportExpression<double> Radius
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    [ValidValues(0.0, 360.0)]
    [ReportExpressionDefaultValue(typeof (double), 20.0)]
    public ReportExpression<double> StartAngle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(24);
      }
      set
      {
        PropertyStore.SetObject(24, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 320.0)]
    [ValidValues(0.0, 360.0)]
    public ReportExpression<double> SweepAngle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    public RadialScale()
    {
    }

    internal RadialScale(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Radius = 37.0;
      StartAngle = 20.0;
      SweepAngle = 320.0;
    }

    internal class Definition : DefinitionStore<RadialScale, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        GaugePointers,
        ScaleRanges,
        Style,
        CustomLabels,
        Interval,
        IntervalOffset,
        Logarithmic,
        LogarithmicBase,
        MaximumValue,
        MinimumValue,
        Multiplier,
        Reversed,
        GaugeMajorTickMarks,
        GaugeMinorTickMarks,
        MaximumPin,
        MinimumPin,
        ScaleLabels,
        TickMarksOnTop,
        ToolTip,
        ActionInfo,
        Hidden,
        Width,
        Radius,
        StartAngle,
        SweepAngle,
        PropertyCount,
      }
    }
  }
}
