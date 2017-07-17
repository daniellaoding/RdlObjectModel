using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapMember : HierarchyMember, IHierarchy, IHierarchyMember, IDataScopeService
  {
    public override Group Group
    {
      get
      {
        return (Group) PropertyStore.GetObject(0);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Group");
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement("MapMember")]
    public MapMember ChildMapMember
    {
      get
      {
        return (MapMember) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<SortExpression>))]
    public IList<SortExpression> SortExpressions
    {
      get
      {
        return null;
      }
      set
      {
      }
    }

    IEnumerable<IHierarchyMember> IHierarchyMember.Members
    {
      get
      {
        yield return ChildMapMember;
      }
    }

    IEnumerable<IHierarchyMember> IHierarchy.Members
    {
      get
      {
        yield return ChildMapMember;
      }
    }

    public MapMember()
    {
    }

    internal MapMember(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Group = new Group();
    }

    internal class Definition : DefinitionStore<MapMember, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Group,
        ChildMapMember,
        PropertyCount,
      }
    }
  }
}
