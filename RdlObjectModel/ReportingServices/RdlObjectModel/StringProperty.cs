namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class StringProperty : PropertyDefinition, IPropertyDefinition
  {
    private readonly string m_default;

    public object Default => m_default;

	  object IPropertyDefinition.Minimum => null;

	  object IPropertyDefinition.Maximum => null;

	  public StringProperty(string name, string defaultValue)
      : base(name)
    {
      m_default = defaultValue;
    }

    void IPropertyDefinition.Validate(object component, object value)
    {
    }
  }
}
