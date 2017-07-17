using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MetadataValues : RdlCollection<MetadataValue>
  {
    public MetadataValue this[string value]
    {
      get
      {
        foreach (MetadataValue metadataValue in this)
        {
          if (string.Equals(metadataValue.Value, value, StringComparison.CurrentCulture))
            return metadataValue;
        }
        return null;
      }
    }

    public void Add(string value)
    {
      Add(new MetadataValue(value));
    }

    public bool Contains(string value)
    {
      return this[value] != null;
    }

    public MetadataValues DeepClone()
    {
      MetadataValues metadataValues = new MetadataValues();
      foreach (MetadataValue metadataValue in this)
        metadataValues.Add((MetadataValue) metadataValue.DeepClone());
      return metadataValues;
    }
  }
}
