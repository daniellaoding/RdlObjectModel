namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartDataPointValues : ReportObject
  {
    [ReportExpressionDefaultValue]
    public ReportExpression X
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Y
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Size
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression High
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Low
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Start
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression End
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Mean
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Median
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    public ChartDataPointValues()
    {
    }

    internal ChartDataPointValues(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartDataPointValues, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        X,
        Y,
        Size,
        High,
        Low,
        Start,
        End,
        Mean,
        Median,
        PropertyCount,
      }
    }
  }
}
