﻿using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ReportSizeConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof (string))
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value == null)
        return null;
      if (!(value is string))
        return base.ConvertFrom(context, culture, value);
      string str = ((string) value).Trim();
      if (str.Length == 0)
        return new ReportSize(0.0, SizeTypes.Invalid);
      return new ReportSize(str, culture);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == typeof (string) && value is ReportSize)
        return ((ReportSize) value).ToString(null, culture);
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
