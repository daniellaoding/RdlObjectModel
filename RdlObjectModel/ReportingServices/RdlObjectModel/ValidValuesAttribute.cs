using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [AttributeUsage(AttributeTargets.Property)]
  internal sealed class ValidValuesAttribute : Attribute
  {
	  public object Minimum { get; set; }

	  public object Maximum { get; set; }

	  public ValidValuesAttribute(int minimum, int maximum)
    {
      Minimum = minimum;
      Maximum = maximum;
    }

    public ValidValuesAttribute(double minimum, double maximum)
    {
      Minimum = minimum;
      Maximum = maximum;
    }

    public ValidValuesAttribute(string minimumField, string maximumField)
    {
      Minimum = typeof (Constants).InvokeMember(minimumField, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
      Maximum = typeof (Constants).InvokeMember(maximumField, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
    }
  }
}
