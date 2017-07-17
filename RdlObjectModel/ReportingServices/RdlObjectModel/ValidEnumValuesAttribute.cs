using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [AttributeUsage(AttributeTargets.Property)]
  internal sealed class ValidEnumValuesAttribute : Attribute
  {
	  public IList<int> ValidValues { get; set; }

	  public ValidEnumValuesAttribute(string field)
      : this(typeof (InternalConstants), field)
    {
    }

    public ValidEnumValuesAttribute(Type type, string field)
    {
      ValidValues = new ReadOnlyCollection<int>((int[]) type.InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture));
    }
  }
}
