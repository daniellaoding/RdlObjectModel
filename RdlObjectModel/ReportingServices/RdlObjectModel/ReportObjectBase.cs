using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class ReportObjectBase : IContainedObject
  {
	  [XmlIgnore]
    internal IPropertyStore PropertyStore { get; }

	  [XmlIgnore]
    public IContainedObject Parent
    {
      get
      {
        return PropertyStore.Parent;
      }
      set
      {
        PropertyStore.Parent = value;
      }
    }

    protected ReportObjectBase()
    {
      PropertyStore = WrapPropertyStore(new PropertyStore((ReportObject) this));
      Initialize();
    }

    internal ReportObjectBase(IPropertyStore propertyStore)
    {
      PropertyStore = WrapPropertyStore(propertyStore);
    }

    public virtual void Initialize()
    {
    }

    internal virtual IPropertyStore WrapPropertyStore(IPropertyStore propertyStore)
    {
      return propertyStore;
    }
  }
}
