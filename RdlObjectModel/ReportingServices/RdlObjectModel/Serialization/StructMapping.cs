using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  internal class StructMapping : TypeMapping
  {
    public NameTable<MemberMapping> Elements;
    public NameTable<MemberMapping> Attributes;
	  private List<UseTypeInfo> m_useTypes;

    public List<MemberMapping> Members { get; }

	  public StructMapping(Type type)
      : base(type)
    {
      Elements = new NameTable<MemberMapping>();
      Attributes = new NameTable<MemberMapping>();
      Members = new List<MemberMapping>();
    }

    public MemberMapping GetAttribute(string name, string ns)
    {
      return Attributes[name, ns];
    }

    public MemberMapping GetElement(string name, string ns)
    {
      return Elements[name, ns];
    }

    public void AddUseTypeInfo(string name, string ns)
    {
      UseTypeInfo useTypeInfo = new UseTypeInfo();
      useTypeInfo.Name = name;
      useTypeInfo.Namespace = ns;
      if (m_useTypes == null)
        m_useTypes = new List<UseTypeInfo>();
      m_useTypes.Add(useTypeInfo);
    }

    public List<MemberMapping> GetTypeNameElements()
    {
      if (m_useTypes == null)
        return null;
      List<MemberMapping> memberMappingList = new List<MemberMapping>();
      foreach (UseTypeInfo useType in m_useTypes)
        memberMappingList.Add(Elements[useType.Name, useType.Namespace]);
      return memberMappingList;
    }

    internal void ResolveChildAttributes()
    {
      if (ChildAttributes == null)
        return;
      for (int index = 0; index < ChildAttributes.Count; ++index)
      {
        MemberMapping childAttribute = ChildAttributes[index];
        GetElement(((XmlChildAttributeAttribute) childAttribute.XmlAttributes.XmlAttribute).ElementName, "").AddChildAttribute(childAttribute);
      }
      ChildAttributes = null;
    }

    private struct UseTypeInfo
    {
      public string Name;
      public string Namespace;
    }
  }
}
