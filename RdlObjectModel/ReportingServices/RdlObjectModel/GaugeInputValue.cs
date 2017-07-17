using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class GaugeInputValue : ReportObject
  {
    public ReportExpression Value
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

    [ReportExpressionDefaultValue(typeof (FormulaTypes), FormulaTypes.None)]
    public ReportExpression<FormulaTypes> Formula
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<FormulaTypes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> MinPercent
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
    public ReportExpression<double> MaxPercent
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Multiplier
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

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> AddConstant
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ValidEnumValues("GaugeInputValueDataElementOutputTypes")]
    [DefaultValue(DataElementOutputTypes.Output)]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(7);
      }
      set
      {
        ((EnumProperty) DefinitionStore<GaugeInputValue, Definition.Properties>.GetProperty(7)).Validate(this, (int) value);
        PropertyStore.SetInteger(7, (int) value);
      }
    }

    public GaugeInputValue()
    {
    }

    internal GaugeInputValue(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DataElementOutput = DataElementOutputTypes.Output;
    }

    internal class Definition : DefinitionStore<GaugeInputValue, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Value,
        Formula,
        MinPercent,
        MaxPercent,
        Multiplier,
        AddConstant,
        DataElementName,
        DataElementOutput,
        PropertyCount,
      }
    }
  }
}
