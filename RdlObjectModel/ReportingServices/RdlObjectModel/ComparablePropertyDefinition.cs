using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ComparablePropertyDefinition<T> : PropertyDefinition<T>, IPropertyDefinition where T : struct, IComparable
  {
	  public T? Minimum { get; }

	  public T? Maximum { get; }

	  object IPropertyDefinition.Default => Default;

	  object IPropertyDefinition.Minimum => Minimum;

	  object IPropertyDefinition.Maximum => Maximum;

	  public ComparablePropertyDefinition(string name, T? defaultValue)
      : base(name, defaultValue)
    {
    }

    public ComparablePropertyDefinition(string name, T? defaultValue, T? minimum, T? maximum)
      : this(name, defaultValue)
    {
      Minimum = minimum;
      Maximum = maximum;
    }

    void IPropertyDefinition.Validate(object component, object value)
    {
      if (value is T)
        Validate(component, (T) value);
      else if (value is ReportExpression<T>)
      {
        Validate(component, (ReportExpression<T>) value);
      }
      else
      {
        if (!(value is string))
          throw new ArgumentException("Invalid type.");
        Validate(component, (string) value);
      }
    }

    public void Constrain(ref T value)
    {
      if (Minimum.HasValue && Minimum.Value.CompareTo(value) > 0)
      {
        value = Minimum.Value;
      }
      else
      {
        if (!Maximum.HasValue || Maximum.Value.CompareTo(value) >= 0)
          return;
        value = Maximum.Value;
      }
    }

    public void Validate(object component, T value)
    {
      if (Minimum.HasValue && Minimum.Value.CompareTo(value) > 0)
        throw new ArgumentTooSmallException(component, Name, value, Minimum);
      if (Maximum.HasValue && Maximum.Value.CompareTo(value) < 0)
        throw new ArgumentTooLargeException(component, Name, value, Maximum);
    }

    public void Validate(object component, ReportExpression<T> value)
    {
      if (value.IsExpression)
        return;
      Validate(component, value.Value);
    }

    public void Validate(object component, string value)
    {
      Validate(component, new ReportExpression<T>(value, CultureInfo.InvariantCulture));
    }
  }
}
