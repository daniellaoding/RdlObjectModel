using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ComponentMetadata : ReportObject
  {
    private const string DescriptionName = "Description";

    [XmlElement]
    public Guid? ComponentId
    {
      get
      {
        return (Guid?) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement]
    [DefaultValue(false)]
    public bool HideUpdateNotifications
    {
      get
      {
        return PropertyStore.GetBoolean(1);
      }
      set
      {
        PropertyStore.SetBoolean(1, value);
      }
    }

    [XmlElement]
    public string SourcePath
    {
      get
      {
        return (string) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlElement]
    public DateTime? SyncDate
    {
      get
      {
        return (DateTime?) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlArrayItem("UserProperty", typeof (MetadataProperty))]
    public MetadataProperties UserProperties
    {
      get
      {
        return (MetadataProperties) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [XmlIgnore]
    public string Description
    {
      get
      {
        MetadataProperty userProperty = UserProperties["Description"];
        if (userProperty != null)
          return userProperty.Value;
        return null;
      }
      set
      {
        if (string.Equals(Description, value, StringComparison.CurrentCulture))
          return;
        UserProperties.GetProperty("Description", string.Empty).Value = value;
      }
    }

    public ComponentMetadata()
    {
    }

    internal ComponentMetadata(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      UserProperties = new MetadataProperties();
    }

    internal class Definition : DefinitionStore<ComponentMetadata, Definition.Properties>
    {
      internal enum Properties
      {
        ComponentId,
        HideUpdateNotifications,
        SourcePath,
        SyncDate,
        UserProperties,
      }
    }
  }
}
