using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class NumericIndicator : GaugePanelItem
  {
    [ReportExpressionDefaultValue(typeof (ResizeModes), ResizeModes.AutoFit)]
    public ReportExpression<ResizeModes> ResizeMode
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ResizeModes>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public GaugeInputValue GaugeInputValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(12);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportColor), "DefaultDecimalDigitColor")]
    public ReportExpression<ReportColor> DecimalDigitColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 1)]
    public ReportExpression<int> DecimalDigits
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportColor), "DefaultDigitColor")]
    public ReportExpression<ReportColor> DigitColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 6)]
    public ReportExpression<int> Digits
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (NumericIndicatorStyles), NumericIndicatorStyles.Mechanical)]
    public ReportExpression<NumericIndicatorStyles> IndicatorStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<NumericIndicatorStyles>>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> LedDimColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    public GaugeInputValue MaximumValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    public GaugeInputValue MinimumValue
    {
      get
      {
        return (GaugeInputValue) PropertyStore.GetObject(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 1.0)]
    public ReportExpression<double> Multiplier
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [ReportExpressionDefaultValueConstant("DefaultGaugeIndicatorNonNumericString")]
    public ReportExpression NonNumericString
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [ReportExpressionDefaultValueConstant("DefaultGaugeIndicatorOutOfRangeString")]
    public ReportExpression OutOfRangeString
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    [XmlElement(typeof (RdlCollection<NumericIndicatorRange>))]
    public IList<NumericIndicatorRange> NumericIndicatorRanges
    {
      get
      {
        return (IList<NumericIndicatorRange>) PropertyStore.GetObject(24);
      }
      set
      {
        PropertyStore.SetObject(24, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportColor), "DefaultSeparatorColor")]
    public ReportExpression<ReportColor> SeparatorColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 1.0)]
    public ReportExpression<double> SeparatorWidth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(26);
      }
      set
      {
        PropertyStore.SetObject(26, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> ShowDecimalPoint
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(27);
      }
      set
      {
        PropertyStore.SetObject(27, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> ShowLeadingZeros
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(28);
      }
      set
      {
        PropertyStore.SetObject(28, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (GaugeShowSigns), GaugeShowSigns.NegativeOnly)]
    public ReportExpression<GaugeShowSigns> ShowSign
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<GaugeShowSigns>>(29);
      }
      set
      {
        PropertyStore.SetObject(29, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> SnappingEnabled
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(30);
      }
      set
      {
        PropertyStore.SetObject(30, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> SnappingInterval
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(31);
      }
      set
      {
        PropertyStore.SetObject(31, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> UseFontPercent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(32);
      }
      set
      {
        PropertyStore.SetObject(32, value);
      }
    }

    public NumericIndicator()
    {
    }

    internal NumericIndicator(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      GaugeInputValue = new GaugeInputValue();
      NumericIndicatorRanges = new RdlCollection<NumericIndicatorRange>();
      DecimalDigitColor = Constants.DefaultDecimalDigitColor;
      DecimalDigits = 1;
      DigitColor = Constants.DefaultDigitColor;
      Digits = 6;
      NonNumericString = "-";
      OutOfRangeString = "#Error";
      SeparatorColor = Constants.DefaultSeparatorColor;
      Multiplier = 1.0;
      SeparatorWidth = 1.0;
    }

    internal class Definition : DefinitionStore<NumericIndicator, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Hidden,
        ToolTip,
        ActionInfo,
        ParentItem,
        ResizeMode,
        GaugeInputValue,
        DecimalDigitColor,
        DecimalDigits,
        DigitColor,
        Digits,
        IndicatorStyle,
        LedDimColor,
        MaximumValue,
        MinimumValue,
        Multiplier,
        NonNumericString,
        OutOfRangeString,
        NumericIndicatorRanges,
        SeparatorColor,
        SeparatorWidth,
        ShowDecimalPoint,
        ShowLeadingZeros,
        ShowSign,
        SnappingEnabled,
        SnappingInterval,
        UseFontPercent,
        PropertyCount,
      }
    }
  }
}
