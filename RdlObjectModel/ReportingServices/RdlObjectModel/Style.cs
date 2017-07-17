using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Style : ReportObject, IShouldSerialize
  {
    private string m_formatSymbolCulture;

    public Border Border
    {
      get
      {
        return (Border) PropertyStore.GetObject(0);
      }
      set
      {
        if (value != null)
        {
          if (value.Color == ReportColor.Empty)
            value.Color = Constants.DefaultBorderColor;
          if (value.Width == ReportSize.Empty)
            value.Width = Constants.DefaultBorderWidth;
        }
        PropertyStore.SetObject(0, value);
      }
    }

    public Border TopBorder
    {
      get
      {
        return (Border) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public Border BottomBorder
    {
      get
      {
        return (Border) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public Border LeftBorder
    {
      get
      {
        return (Border) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public Border RightBorder
    {
      get
      {
        return (Border) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> BackgroundColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (BackgroundGradients), BackgroundGradients.Default)]
    public ReportExpression<BackgroundGradients> BackgroundGradientType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<BackgroundGradients>>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> BackgroundGradientEndColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public BackgroundImage BackgroundImage
    {
      get
      {
        return (BackgroundImage) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (FontStyles), FontStyles.Default)]
    public ReportExpression<FontStyles> FontStyle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<FontStyles>>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue("Arial")]
    public ReportExpression FontFamily
    {
      get
      {
        if (PropertyStore.ContainsObject(10))
          return PropertyStore.GetObject<ReportExpression>(10);
        Report ancestor = GetAncestor<Report>();
        if (ancestor != null)
          return ancestor.DefaultFontFamily ?? "Arial";
        return "Arial";
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultFontSize")]
    public ReportExpression<ReportSize> FontSize
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (FontWeights), FontWeights.Default)]
    public ReportExpression<FontWeights> FontWeight
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<FontWeights>>(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Format
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (TextDecorations), TextDecorations.Default)]
    public ReportExpression<TextDecorations> TextDecoration
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextDecorations>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (TextAlignments), TextAlignments.Default)]
    public ReportExpression<TextAlignments> TextAlign
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextAlignments>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (VerticalAlignments), VerticalAlignments.Default)]
    public ReportExpression<VerticalAlignments> VerticalAlign
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<VerticalAlignments>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportColor), "DefaultColor")]
    public ReportExpression<ReportColor> Color
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> PaddingLeft
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> PaddingRight
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> PaddingTop
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> PaddingBottom
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> LineHeight
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (TextDirections), TextDirections.Default)]
    public ReportExpression<TextDirections> Direction
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextDirections>>(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (WritingModes), WritingModes.Default)]
    public ReportExpression<WritingModes> WritingMode
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<WritingModes>>(24);
      }
      set
      {
        PropertyStore.SetObject(24, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Language
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (UnicodeBiDiTypes), UnicodeBiDiTypes.Normal)]
    public ReportExpression<UnicodeBiDiTypes> UnicodeBiDi
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<UnicodeBiDiTypes>>(26);
      }
      set
      {
        PropertyStore.SetObject(26, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (Calendars), Calendars.Default)]
    public ReportExpression<Calendars> Calendar
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<Calendars>>(27);
      }
      set
      {
        PropertyStore.SetObject(27, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression NumeralLanguage
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(28);
      }
      set
      {
        PropertyStore.SetObject(28, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), 1)]
    [ValidValues(1, 7)]
    public ReportExpression<int> NumeralVariant
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(29);
      }
      set
      {
        DefinitionStore<Style, Definition.Properties>.GetProperty(29).Validate(this, value);
        PropertyStore.SetObject(29, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (TextEffects), TextEffects.Default)]
    public ReportExpression<TextEffects> TextEffect
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextEffects>>(30);
      }
      set
      {
        PropertyStore.SetObject(30, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (BackgroundHatchTypes), BackgroundHatchTypes.Default)]
    public ReportExpression<BackgroundHatchTypes> BackgroundHatchType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<BackgroundHatchTypes>>(31);
      }
      set
      {
        PropertyStore.SetObject(31, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> ShadowColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(32);
      }
      set
      {
        PropertyStore.SetObject(32, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> ShadowOffset
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(33);
      }
      set
      {
        PropertyStore.SetObject(33, value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string FormatSymbolCulture
    {
      get
      {
        return m_formatSymbolCulture ?? string.Empty;
      }
      set
      {
        if (!(m_formatSymbolCulture != value))
          return;
        SavePropertyValue("FormatSymbolCulture", m_formatSymbolCulture, (string newValue, out string oldValue) =>
        {
	        oldValue = m_formatSymbolCulture;
	        m_formatSymbolCulture = newValue;
        });
        m_formatSymbolCulture = value;
      }
    }

    public Style()
    {
    }

    internal Style(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      FontSize = Constants.DefaultFontSize;
      Color = Constants.DefaultColor;
      NumeralVariant = 1;
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return true;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
    {
      if (!(property == "FontFamily") || FontFamily.IsExpression)
        return SerializationMethod.Auto;
      Report ancestor = GetAncestor<Report>();
      string str = "Arial";
      if (ancestor != null)
        str = ancestor.DefaultFontFamily ?? "Arial";
      return FontFamily.Value == str ? SerializationMethod.Never : SerializationMethod.Always;
    }

    protected override void InitializeForDesigner()
    {
      base.InitializeForDesigner();
      BackgroundImage = new BackgroundImage();
    }

    internal ItemBorderStyle GetBorderStyle()
    {
      ReportExpression<BorderStyles> defaultStyle = BorderStyles.None;
      ReportExpression<BorderStyles> left = BorderStyles.Default;
      ReportExpression<BorderStyles> right = BorderStyles.Default;
      ReportExpression<BorderStyles> top = BorderStyles.Default;
      ReportExpression<BorderStyles> bottom = BorderStyles.Default;
      if (Border != null)
        defaultStyle = Border.Style;
      if (LeftBorder != null)
        left = LeftBorder.Style;
      if (RightBorder != null)
        right = RightBorder.Style;
      if (TopBorder != null)
        top = TopBorder.Style;
      if (BottomBorder != null)
        bottom = BottomBorder.Style;
      return new ItemBorderStyle(defaultStyle, left, right, top, bottom);
    }

    internal ItemBorderWidth GetBorderWidth()
    {
      ReportExpression<ReportSize> defaultWidth = new ReportSize?();
      ReportExpression<ReportSize> left = new ReportSize?();
      ReportExpression<ReportSize> right = new ReportSize?();
      ReportExpression<ReportSize> top = new ReportSize?();
      ReportExpression<ReportSize> bottom = new ReportSize?();
      if (Border != null)
        defaultWidth = Border.Width;
      if (LeftBorder != null)
        left = LeftBorder.Width;
      if (RightBorder != null)
        right = RightBorder.Width;
      if (TopBorder != null)
        top = TopBorder.Width;
      if (BottomBorder != null)
        bottom = BottomBorder.Width;
      return new ItemBorderWidth(defaultWidth, left, right, top, bottom);
    }

    internal ItemBorderColor GetBorderColor()
    {
      ReportExpression<ReportColor> defaultColor = new ReportColor?();
      ReportExpression<ReportColor> left = new ReportColor?();
      ReportExpression<ReportColor> right = new ReportColor?();
      ReportExpression<ReportColor> top = new ReportColor?();
      ReportExpression<ReportColor> bottom = new ReportColor?();
      if (Border != null)
        defaultColor = Border.Color;
      if (LeftBorder != null)
        left = LeftBorder.Color;
      if (RightBorder != null)
        right = RightBorder.Color;
      if (TopBorder != null)
        top = TopBorder.Color;
      if (BottomBorder != null)
        bottom = BottomBorder.Color;
      return new ItemBorderColor(defaultColor, left, right, top, bottom);
    }

    internal void SetBorders(ItemBorderStyle borderStyle, ItemBorderWidth borderWidth, ItemBorderColor borderColor)
    {
      if (ItemBorderColor.IsSpecified(borderColor.Default) || ItemBorderStyle.IsSpecified(borderStyle.Default) || ItemBorderWidth.IsSpecified(borderWidth.Default))
      {
        Border = new Border();
        Border.Color = borderColor.Default;
        Border.Style = borderStyle.Default;
        Border.Width = borderWidth.Default;
      }
      else
        Border = null;
      if (ItemBorderColor.IsSpecified(borderColor.Left) || ItemBorderStyle.IsSpecified(borderStyle.Left) || ItemBorderWidth.IsSpecified(borderWidth.Left))
      {
        LeftBorder = new Border();
        LeftBorder.Color = borderColor.Left;
        LeftBorder.Style = borderStyle.Left;
        LeftBorder.Width = borderWidth.Left;
      }
      else
        LeftBorder = null;
      if (ItemBorderColor.IsSpecified(borderColor.Right) || ItemBorderStyle.IsSpecified(borderStyle.Right) || ItemBorderWidth.IsSpecified(borderWidth.Right))
      {
        RightBorder = new Border();
        RightBorder.Color = borderColor.Right;
        RightBorder.Style = borderStyle.Right;
        RightBorder.Width = borderWidth.Right;
      }
      else
        RightBorder = null;
      if (ItemBorderColor.IsSpecified(borderColor.Top) || ItemBorderStyle.IsSpecified(borderStyle.Top) || ItemBorderWidth.IsSpecified(borderWidth.Top))
      {
        TopBorder = new Border();
        TopBorder.Color = borderColor.Top;
        TopBorder.Style = borderStyle.Top;
        TopBorder.Width = borderWidth.Top;
      }
      else
        TopBorder = null;
      if (ItemBorderColor.IsSpecified(borderColor.Bottom) || ItemBorderStyle.IsSpecified(borderStyle.Bottom) || ItemBorderWidth.IsSpecified(borderWidth.Bottom))
      {
        BottomBorder = new Border();
        BottomBorder.Color = borderColor.Bottom;
        BottomBorder.Style = borderStyle.Bottom;
        BottomBorder.Width = borderWidth.Bottom;
      }
      else
        BottomBorder = null;
    }

    internal Padding GetPadding()
    {
      return new Padding(PaddingLeft, PaddingRight, PaddingTop, PaddingBottom);
    }

    internal void SetPadding(Padding padding)
    {
      PaddingLeft = padding.Left;
      PaddingRight = padding.Right;
      PaddingTop = padding.Top;
      PaddingBottom = padding.Bottom;
    }

    internal class Definition : DefinitionStore<Style, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Border,
        TopBorder,
        BottomBorder,
        LeftBorder,
        RightBorder,
        BackgroundColor,
        BackgroundGradientType,
        BackgroundGradientEndColor,
        BackgroundImage,
        FontStyle,
        FontFamily,
        FontSize,
        FontWeight,
        Format,
        TextDecoration,
        TextAlign,
        VerticalAlign,
        Color,
        PaddingLeft,
        PaddingRight,
        PaddingTop,
        PaddingBottom,
        LineHeight,
        Direction,
        WritingMode,
        Language,
        UnicodeBiDi,
        Calendar,
        NumeralLanguage,
        NumeralVariant,
        TextEffect,
        BackgroundHatchType,
        ShadowColor,
        ShadowOffset,
        PropertyCount,
      }
    }
   
  }
}
