using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MetadataProperties : RdlCollection<MetadataProperty>
  {
    public MetadataProperty this[string name]
    {
      get
      {
        foreach (MetadataProperty metadataProperty in this)
        {
          if (string.Equals(metadataProperty.Name, name, StringComparison.CurrentCulture))
            return metadataProperty;
        }
        return null;
      }
    }

    public void Add(string name, string description)
    {
      Add(new MetadataProperty(name, description));
    }

    public bool Contains(string name)
    {
      return this[name] != null;
    }

    public MetadataProperties DeepClone()
    {
      MetadataProperties metadataProperties = new MetadataProperties();
      foreach (MetadataProperty metadataProperty in this)
        metadataProperties.Add((MetadataProperty) metadataProperty.DeepClone());
      return metadataProperties;
    }

    internal MetadataProperty GetProperty(string name, string description)
    {
      MetadataProperty metadataProperty1 = this[name];
      if (metadataProperty1 != null)
        return metadataProperty1;
      MetadataProperty metadataProperty2 = new MetadataProperty(name, description);
      Add(metadataProperty2);
      return metadataProperty2;
    }
  }
}
