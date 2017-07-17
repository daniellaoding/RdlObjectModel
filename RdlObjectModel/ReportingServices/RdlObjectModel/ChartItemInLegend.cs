namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartItemInLegend : ReportObject
  {
    [ReportExpressionDefaultValue]
    public ReportExpression LegendText
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
    public ReportExpression ToolTip
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

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
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

    public ChartItemInLegend()
    {
    }

    internal ChartItemInLegend(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartItemInLegend, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        LegendText,
        ToolTip,
        ActionInfo,
        Hidden,
        PropertyCount,
      }
    }
  }
}
