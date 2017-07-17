namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartAlignType : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> AxesView
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Cursor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Position
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
    public ReportExpression<bool> InnerPlotPosition
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

    public ChartAlignType()
    {
    }

    internal ChartAlignType(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartAlignType, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        AxesView,
        Cursor,
        Position,
        InnerPlotPosition,
      }
    }
  }
}
