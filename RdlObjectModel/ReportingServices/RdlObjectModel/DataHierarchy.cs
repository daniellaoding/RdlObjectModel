using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataHierarchy : ReportObject, IHierarchy
  {
    [XmlElement(typeof (RdlCollection<DataMember>))]
    public IList<DataMember> DataMembers
    {
      get
      {
        return (IList<DataMember>) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    IEnumerable<IHierarchyMember> IHierarchy.Members
    {
      get
      {
        foreach (IHierarchyMember dataMember in DataMembers)
          yield return dataMember;
      }
    }

    public DataHierarchy()
    {
    }

    internal DataHierarchy(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DataMembers = new RdlCollection<DataMember>();
    }

    internal class Definition : DefinitionStore<DataHierarchy, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataMembers,
      }
    }
  }
}
