
namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Class : ReportObject
  {
    public string ClassName
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

    public string InstanceName
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

    public Class()
    {
    }

    internal Class(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<Class, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ClassName,
        InstanceName,
      }
    }
  }
}
