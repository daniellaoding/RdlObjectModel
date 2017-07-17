using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [TypeConverter(typeof (PageBreakConverter))]
  public class PageBreak : ReportObject, IShouldSerialize
  {
    public BreakLocations BreakLocation
    {
      get
      {
        return (BreakLocations) PropertyStore.GetInteger(0);
      }
      set
      {
        PropertyStore.SetInteger(0, (int) value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Disabled
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
    public ReportExpression<bool> ResetPageNumber
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

    public PageBreak()
    {
    }

    internal PageBreak(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return BreakLocation != BreakLocations.None;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      return SerializationMethod.Auto;
    }

    internal class Definition : DefinitionStore<PageBreak, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        BreakLocation,
        Disabled,
        ResetPageNumber,
      }
    }

    internal sealed class PageBreakConverter : TypeConverter
    {
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
        if (sourceType == typeof (string))
          return true;
        return base.CanConvertFrom(context, sourceType);
      }

      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
        if (destinationType == typeof (string))
          return true;
        return base.CanConvertTo(context, destinationType);
      }

      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
      {
        if (!(value is string))
          return base.ConvertFrom(context, culture, value);
        string str = (string) value;
        if (str == SRRdl.None)
          return null;
        return new PageBreak()
        {
	        BreakLocation = (BreakLocations) Enum.Parse(typeof (BreakLocations), str, true)
        };
      }

      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
        if (value != null && !(value is PageBreak) || !(destinationType == typeof (string)))
          return base.ConvertTo(context, culture, value, destinationType);
        PageBreak pageBreak = (PageBreak) value;
        if (pageBreak == null)
          return SRRdl.None;
        return pageBreak.BreakLocation.ToString();
      }

      public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
      {
        return true;
      }

      public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
      {
        return true;
      }

      public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
      {
        List<PageBreak> pageBreakList = new List<PageBreak>();
        BreakLocations[] values = (BreakLocations[]) Enum.GetValues(typeof (BreakLocations));
        pageBreakList.Add(null);
        bool flag = context.Instance is Group;
        foreach (BreakLocations breakLocations in values)
        {
          if (flag || breakLocations != BreakLocations.Between)
            pageBreakList.Add(new PageBreak()
            {
              BreakLocation = breakLocations
            });
        }
        return new StandardValuesCollection(pageBreakList);
      }
    }
  }
}
