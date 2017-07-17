namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartElementPosition : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Top
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Left
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Height
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Width
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

    public ChartElementPosition()
    {
    }

    internal ChartElementPosition(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartElementPosition, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Top,
        Left,
        Height,
        Width,
      }
    }
  }
}
