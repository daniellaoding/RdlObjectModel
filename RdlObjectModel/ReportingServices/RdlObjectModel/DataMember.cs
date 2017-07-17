using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataMember : HierarchyMember, IHierarchyMember, IDataScopeService
  {
    public override Group Group
    {
      get
      {
        return (Group) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<SortExpression>))]
    public IList<SortExpression> SortExpressions
    {
      get
      {
        return (IList<SortExpression>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlElement(typeof (RdlCollection<DataMember>))]
    public IList<DataMember> DataMembers
    {
      get
      {
        return (IList<DataMember>) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    IEnumerable<IHierarchyMember> IHierarchyMember.Members
    {
      get
      {
        foreach (IHierarchyMember dataMember in DataMembers)
          yield return dataMember;
      }
    }

    public DataMember()
    {
    }

    internal DataMember(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      SortExpressions = new RdlCollection<SortExpression>();
      CustomProperties = new RdlCollection<CustomProperty>();
      DataMembers = new RdlCollection<DataMember>();
    }

    internal class Definition : DefinitionStore<DataMember, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Group,
        SortExpressions,
        CustomProperties,
        DataMembers,
        PropertyCount,
      }
    }
  }
}
