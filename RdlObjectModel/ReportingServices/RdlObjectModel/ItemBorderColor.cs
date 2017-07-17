using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [TypeConverter(typeof (ItemBorderColorConverter))]
  public struct ItemBorderColor
  {
    public static readonly ItemBorderColor DefaultColor = new ItemBorderColor(new ReportExpression<ReportColor>(new ReportColor(Color.Black)), new ReportExpression<ReportColor>(), new ReportExpression<ReportColor>(), new ReportExpression<ReportColor>(), new ReportExpression<ReportColor>());

	  public ReportExpression<ReportColor> Bottom { get; }

	  public ReportExpression<ReportColor> Right { get; }

	  public ReportExpression<ReportColor> Top { get; }

	  public ReportExpression<ReportColor> Left { get; }

	  public ReportExpression<ReportColor> Default { get; }

	  public ItemBorderColor(ReportExpression<ReportColor> defaultColor, ReportExpression<ReportColor> left, ReportExpression<ReportColor> right, ReportExpression<ReportColor> top, ReportExpression<ReportColor> bottom)
    {
      Default = defaultColor;
      if (!Default.IsExpression && Default.Value.IsEmpty)
        Default = new ReportColor(Color.Black);
      Left = left;
      Right = right;
      Top = top;
      Bottom = bottom;
    }

    public static bool IsSpecified(ReportExpression<ReportColor> color)
    {
      if (!color.IsExpression)
        return !color.Value.IsEmpty;
      return true;
    }

    public ItemBorderColor Modify(BorderSide side, ReportExpression<ReportColor> color)
    {
      return new ItemBorderColor((side & BorderSide.Default) == BorderSide.None ? Default : color, (side & BorderSide.Left) == BorderSide.None ? Left : color, (side & BorderSide.Right) == BorderSide.None ? Right : color, (side & BorderSide.Top) == BorderSide.None ? Top : color, (side & BorderSide.Bottom) == BorderSide.None ? Bottom : color);
    }

    public ReportColor GetDisplayColor(BorderSide side)
    {
      switch (side)
      {
        case BorderSide.Left:
          if (Left.IsExpression)
            return new ReportColor(Color.Silver);
          if (!Left.Value.IsEmpty)
            return Left.Value;
          break;
        case BorderSide.Right:
          if (Right.IsExpression)
            return new ReportColor(Color.Silver);
          if (!Right.Value.IsEmpty)
            return Right.Value;
          break;
        case BorderSide.Top:
          if (Top.IsExpression)
            return new ReportColor(Color.Silver);
          if (!Top.Value.IsEmpty)
            return Top.Value;
          break;
        case BorderSide.Bottom:
          if (Bottom.IsExpression)
            return new ReportColor(Color.Silver);
          if (!Bottom.Value.IsEmpty)
            return Bottom.Value;
          break;
      }
      if (Default.IsExpression)
        return new ReportColor(Color.Silver);
      if (Default.Value.IsEmpty)
        return new ReportColor(Color.Black);
      return Default.Value;
    }

    internal sealed class ItemBorderColorConverter : TypeConverter
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

      private string ConvertToString(ReportExpression<ReportColor> color, PropertyDescriptor descriptor)
      {
        return descriptor.Converter.ConvertToString(color);
      }

      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
        if (!(destinationType == typeof (string)))
          return base.ConvertTo(context, culture, value, destinationType);
        PropertyDescriptor propertyDescriptor = context.PropertyDescriptor;
        ItemBorderColor itemBorderColor = (ItemBorderColor) value;
        if (!IsSpecified(itemBorderColor.Left) && !IsSpecified(itemBorderColor.Right) && (!IsSpecified(itemBorderColor.Top) && !IsSpecified(itemBorderColor.Bottom)))
          return ConvertToString(itemBorderColor.Default, propertyDescriptor.GetChildProperties()["Default"]);
        string listSeparator = culture.TextInfo.ListSeparator;
        return string.Format(culture, "{0}{5} {1}{5} {2}{5} {3}{5} {4}", (object) ConvertToString(itemBorderColor.Default, propertyDescriptor.GetChildProperties()["Default"]), (object) ConvertToString(itemBorderColor.Left, propertyDescriptor.GetChildProperties()["Left"]), (object) ConvertToString(itemBorderColor.Right, propertyDescriptor.GetChildProperties()["Right"]), (object) ConvertToString(itemBorderColor.Top, propertyDescriptor.GetChildProperties()["Top"]), (object) ConvertToString(itemBorderColor.Bottom, propertyDescriptor.GetChildProperties()["Bottom"]), (object) listSeparator);
      }

      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
      {
        if (!(value is string))
          return base.ConvertFrom(context, culture, value);
        string expression = ((string) value).Trim();
        if (expression.Length == 0)
          return new ItemBorderColor();
        List<string> stringList = PropertyGridUtils.SplitCommaSeparatedExpr(expression, culture);
        if (stringList.Count > 0 && stringList.Count <= 5)
          return new ItemBorderColor(new ReportExpression<ReportColor>(stringList[0], culture), stringList.Count > 1 ? new ReportExpression<ReportColor>(stringList[1], culture) : (ReportExpression<ReportColor>) new ReportColor?(), stringList.Count > 2 ? new ReportExpression<ReportColor>(stringList[2], culture) : (ReportExpression<ReportColor>) new ReportColor?(), stringList.Count > 3 ? new ReportExpression<ReportColor>(stringList[3], culture) : (ReportExpression<ReportColor>) new ReportColor?(), stringList.Count > 4 ? new ReportExpression<ReportColor>(stringList[4], culture) : (ReportExpression<ReportColor>) new ReportColor?());
        throw new ArgumentException(SRErrors.BorderFormatError);
      }

      public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
      {
        return true;
      }

      public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
      {
        return new ItemBorderColor((ReportExpression<ReportColor>) propertyValues["Default"], (ReportExpression<ReportColor>) propertyValues["Left"], (ReportExpression<ReportColor>) propertyValues["Right"], (ReportExpression<ReportColor>) propertyValues["Top"], (ReportExpression<ReportColor>) propertyValues["Bottom"]);
      }

      public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
      {
        return TypeDescriptor.GetProperties(typeof (ItemBorderColor), attributes).Sort(new string[5]
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
