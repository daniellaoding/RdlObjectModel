using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixHierarchy : ReportObject, IHierarchy
  {
    [XmlElement(typeof (RdlCollection<TablixMember>))]
    public IList<TablixMember> TablixMembers
    {
      get
      {
        return (IList<TablixMember>) PropertyStore.GetObject(0);
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
        foreach (IHierarchyMember tablixMember in TablixMembers)
          yield return tablixMember;
      }
    }

    public TablixHierarchy()
    {
    }

    internal TablixHierarchy(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TablixMembers = new RdlCollection<TablixMember>();
    }

    internal IEnumerable<TablixMember> GetDynamicChildMembers()
    {
      return GetDynamicChildMembers(TablixMembers);
    }

    internal static IEnumerable<TablixMember> GetDynamicChildMembers(IList<TablixMember> members)
    {
      foreach (TablixMember member in members)
      {
        if (member.Group != null)
          yield return member;
      }
    }

    internal class Definition : DefinitionStore<TablixHierarchy, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        TablixMembers,
      }
    }
  }
}
