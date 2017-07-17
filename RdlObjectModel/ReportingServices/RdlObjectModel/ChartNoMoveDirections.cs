namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartNoMoveDirections : ReportObject
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Up
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
    public ReportExpression<bool> Left
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
    public ReportExpression<bool> Right
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
    public ReportExpression<bool> Down
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UpLeft
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UpRight
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> DownLeft
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> DownRight
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public ChartNoMoveDirections()
    {
    }

    internal ChartNoMoveDirections(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ChartNoMoveDirections, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Up,
        Left,
        Right,
        Down,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
        PropertyCount,
      }
    }
  }
}
