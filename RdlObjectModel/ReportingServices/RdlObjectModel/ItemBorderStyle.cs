using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [TypeConverter(typeof (ItemBorderStyleConverter))]
  public struct ItemBorderStyle
  {
    public static readonly ItemBorderStyle NoBorders = new ItemBorderStyle(new ReportExpression<BorderStyles>(BorderStyles.None), new ReportExpression<BorderStyles>(), new ReportExpression<BorderStyles>(), new ReportExpression<BorderStyles>(), new ReportExpression<BorderStyles>());

	  [NotifyParentProperty(true)]
    public ReportExpression<BorderStyles> Bottom { get; }


	  public ReportExpression<BorderStyles> Right { get; }

	  [NotifyParentProperty(true)]
    public ReportExpression<BorderStyles> Top { get; }

	  [NotifyParentProperty(true)]
    public ReportExpression<BorderStyles> Left { get; }

	  [NotifyParentProperty(true)]
    public ReportExpression<BorderStyles> Default { get; }

	  public ItemBorderStyle(ReportExpression<BorderStyles> defaultStyle, ReportExpression<BorderStyles> left, ReportExpression<BorderStyles> right, ReportExpression<BorderStyles> top, ReportExpression<BorderStyles> bottom)
    {
      Default = defaultStyle.IsExpression || defaultStyle.Value != BorderStyles.Default ? defaultStyle : (ReportExpression<BorderStyles>) BorderStyles.None;
      Left = left;
      Right = right;
      Top = top;
      Bottom = bottom;
    }

    public static bool IsSpecified(ReportExpression<BorderStyles> borderStyleExpression)
    {
      if (!borderStyleExpression.IsExpression)
        return borderStyleExpression.Value != BorderStyles.Default;
      return true;
    }

    public ItemBorderStyle Modify(BorderSide side, ReportExpression<BorderStyles> style)
    {
      return new ItemBorderStyle((side & BorderSide.Default) == BorderSide.None ? Default : style, (side & BorderSide.Left) == BorderSide.None ? Left : style, (side & BorderSide.Right) == BorderSide.None ? Right : style, (side & BorderSide.Top) == BorderSide.None ? Top : style, (side & BorderSide.Bottom) == BorderSide.None ? Bottom : style);
    }

    public BorderStyles GetDisplayStyle(BorderSide side, double pxWidth)
    {
      BorderStyles style;
      switch (side)
      {
        case BorderSide.Left:
          if (GetBorderStyle(Left, pxWidth, out style))
            return style;
          break;
        case BorderSide.Right:
          if (GetBorderStyle(Right, pxWidth, out style))
            return style;
          break;
        case BorderSide.Top:
          if (GetBorderStyle(Top, pxWidth, out style))
            return style;
          break;
        case BorderSide.Bottom:
          if (GetBorderStyle(Bottom, pxWidth, out style))
            return style;
          break;
      }
      if (GetBorderStyle(Default, pxWidth, out style))
        return style;
      return BorderStyles.None;
    }

    private bool GetBorderStyle(ReportExpression<BorderStyles> expr, double pxWidth, out BorderStyles style)
    {
      style = BorderStyles.None;
      if (expr.IsExpression)
      {
        style = BorderStyles.Solid;
        return true;
      }
      if (expr.Value == BorderStyles.Default)
        return false;
      style = expr.Value != BorderStyles.Double || pxWidth >= 3.0 ? expr.Value : BorderStyles.Solid;
      return true;
    }

    internal sealed class ItemBorderStyleConverter : TypeConverter
    {
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
        if (destinationType == typeof (string))
          return true;
        return base.CanConvertTo(context, destinationType);
      }

      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
        if (sourceType == typeof (string))
          return true;
        return base.CanConvertFrom(context, sourceType);
      }

      private string ConvertToString(ReportExpression<BorderStyles> borderStyleExpression, PropertyDescriptor descriptor)
      {
        return descriptor.Converter.ConvertToString(borderStyleExpression);
      }

      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
        if (!(destinationType == typeof (string)))
          return base.ConvertTo(context, culture, value, destinationType);
        PropertyDescriptor propertyDescriptor = context.PropertyDescriptor;
        ItemBorderStyle itemBorderStyle = (ItemBorderStyle) value;
        if (!IsSpecified(itemBorderStyle.Left) && !IsSpecified(itemBorderStyle.Right) && (!IsSpecified(itemBorderStyle.Top) && !IsSpecified(itemBorderStyle.Bottom)))
          return ConvertToString(itemBorderStyle.Default, propertyDescriptor.GetChildProperties()["Default"]);
        if (culture == null)
          culture = CultureInfo.CurrentCulture;
        string listSeparator = culture.TextInfo.ListSeparator;
        return string.Format(culture, "{0}{5} {1}{5} {2}{5} {3}{5} {4}", (object) ConvertToString(itemBorderStyle.Default, propertyDescriptor.GetChildProperties()["Default"]), (object) ConvertToString(itemBorderStyle.Left, propertyDescriptor.GetChildProperties()["Left"]), (object) ConvertToString(itemBorderStyle.Right, propertyDescriptor.GetChildProperties()["Right"]), (object) ConvertToString(itemBorderStyle.Top, propertyDescriptor.GetChildProperties()["Top"]), (object) ConvertToString(itemBorderStyle.Bottom, propertyDescriptor.GetChildProperties()["Bottom"]), (object) listSeparator);
      }

      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
      {
        if (!(value is string))
          return base.ConvertFrom(context, culture, value);
        string expression = ((string) value).Trim();
        if (expression.Length == 0)
          return new ItemBorderStyle();
        List<string> stringList = PropertyGridUtils.SplitCommaSeparatedExpr(expression, culture);
        if (stringList.Count > 0 && stringList.Count <= 5)
          return new ItemBorderStyle(new ReportExpression<BorderStyles>(stringList[0], culture), stringList.Count > 1 ? new ReportExpression<BorderStyles>(stringList[1], culture) : (ReportExpression<BorderStyles>) new BorderStyles?(), stringList.Count > 2 ? new ReportExpression<BorderStyles>(stringList[2], culture) : (ReportExpression<BorderStyles>) new BorderStyles?(), stringList.Count > 3 ? new ReportExpression<BorderStyles>(stringList[3], culture) : (ReportExpression<BorderStyles>) new BorderStyles?(), stringList.Count > 4 ? new ReportExpression<BorderStyles>(stringList[4], culture) : (ReportExpression<BorderStyles>) new BorderStyles?());
        throw new ArgumentException(SRErrors.BorderFormatError);
      }

      public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
      {
        return true;
      }

      public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
      {
        return new ItemBorderStyle((ReportExpression<BorderStyles>) propertyValues["Default"], (ReportExpression<BorderStyles>) propertyValues["Left"], (ReportExpression<BorderStyles>) propertyValues["Right"], (ReportExpression<BorderStyles>) propertyValues["Top"], (ReportExpression<BorderStyles>) propertyValues["Bottom"]);
      }

      public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
      {
        return TypeDescriptor.GetProperties(typeof (ItemBorderStyle), attributes).Sort(new string[5]
        {
          "Default",
          "Left",
          "Right",
          "Top",
          "Bottom"
        });
      }

      public override bool GetPropertiesSupported(ITypeDescriptorContext context)
      {
        return true;
      }
    }
  }
}
