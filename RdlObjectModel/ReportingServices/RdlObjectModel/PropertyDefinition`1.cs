namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class PropertyDefinition<T> : PropertyDefinition, IPropertyDefinition where T : struct
  {
	  public T? Default { get; }

	  object IPropertyDefinition.Default => Default;

	  object IPropertyDefinition.Minimum => null;

	  object IPropertyDefinition.Maximum => null;

	  protected PropertyDefinition(string name, T? defaultValue)
      : base(name)
    {
      Default = defaultValue;
    }

    void IPropertyDefinition.Validate(object component, object value)
    {
    }
  }
}
