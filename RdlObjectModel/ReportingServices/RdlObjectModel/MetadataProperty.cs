using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MetadataProperty : ReportObject
  {
    [XmlAttribute]
    public string Name
    {
      get
      {
        return (string) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlAttribute]
    public string Description
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlArrayItem("Value", typeof (MetadataValue))]
    public MetadataValues Values
    {
      get
      {
        return (MetadataValues) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlIgnore]
    public string Value
    {
      get
      {
        if (Values != null && Values.Count > 0)
          return Values[0].Value;
        return null;
      }
      set
      {
        if (Values == null)
          Values = new MetadataValues();
        else
          Values.Clear();
        if (value == null)
          return;
        Values.Add(value);
      }
    }

    public MetadataProperty()
    {
    }

    internal MetadataProperty(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public MetadataProperty(string name, string description)
    {
      Name = name;
      Description = description;
    }

    public override void Initialize()
    {
      base.Initialize();
      Values = new MetadataValues();
    }

    internal class Definition : DefinitionStore<MetadataProperty, Definition.Properties>
    {
      internal enum Properties
      {
        Name,
        Description,
        Values,
      }
    }
  }
}
