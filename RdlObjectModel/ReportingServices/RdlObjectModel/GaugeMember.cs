using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class GaugeMember : HierarchyMember, IHierarchy, IHierarchyMember, IDataScopeService
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
          throw new ArgumentNullException("value");
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

    [XmlElement("GaugeMember")]
    public GaugeMember ChildGaugeMember
    {
      get
      {
        return (GaugeMember) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    IEnumerable<IHierarchyMember> IHierarchyMember.Members
    {
      get
      {
        yield return ChildGaugeMember;
      }
    }

    IEnumerable<IHierarchyMember> IHierarchy.Members
    {
      get
      {
        yield return ChildGaugeMember;
      }
    }

    public GaugeMember()
    {
    }

    internal GaugeMember(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Group = new Group();
      SortExpressions = new RdlCollection<SortExpression>();
    }

    internal class Definition : DefinitionStore<GaugeMember, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Group,
        SortExpressions,
        ChildGaugeMember,
        PropertyCount,
      }
    }
  }
}
