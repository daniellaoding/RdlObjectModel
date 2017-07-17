using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal sealed class EnumNamesAttribute : Attribute
  {
	  public IList<string> Names { get; }

	  public EnumNamesAttribute(Type type, string field)
    {
      Names = new ReadOnlyCollection<string>((string[]) type.InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture));
    }
  }
}
