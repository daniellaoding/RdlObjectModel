using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class TypeContext : LookupContext
  {
    private readonly Type m_type;

	  internal bool AllowNew { get; }

	  internal bool AllowNewArray { get; }

	  internal string MethodFullName
    {
      get
      {
        if (m_type != null && !string.IsNullOrEmpty(m_type.FullName))
          return m_type.FullName;
        return Name;
      }
    }

    internal TypeContext(string name, Type type, bool allowNew, bool allowNewArray, IEnvironmentFilter filter)
      : this(name, type, allowNew, allowNewArray, filter, BindingFlags.Default)
    {
    }

    internal TypeContext(string name, Type type, bool allowNew, bool allowNewArray, IEnvironmentFilter filter, BindingFlags additionalFlags)
      : base(name)
    {
      m_type = type;
      AllowNew = allowNew;
      AllowNewArray = allowNewArray;
      InitMembers(m_members, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | additionalFlags, filter);
    }

    private void InitMembers(Dictionary<string, MemberContext> memberTable, BindingFlags bindingFlags, IEnvironmentFilter filter)
    {
      foreach (MemberInfo member in m_type.GetMembers(bindingFlags))
      {
        switch (member.MemberType)
        {
          case MemberTypes.Field:
          case MemberTypes.Property:
            AddMember(member, memberTable, filter);
            break;
          case MemberTypes.Method:
            if (!((MethodBase) member).IsGenericMethod)
            {
              AddMember(member, memberTable, filter);
              break;
            }
            break;
        }
      }
    }

    private void AddMember(MemberInfo memberDef, Dictionary<string, MemberContext> memberTable, IEnvironmentFilter filter)
    {
      if (!filter.IsAllowedMember(memberDef.Name))
        return;
      MemberContext memberContext1;
      if (memberTable.TryGetValue(memberDef.Name, out memberContext1))
      {
        memberContext1.AddOverload(memberDef);
      }
      else
      {
        MemberContext memberContext2 = new MemberContext(this, memberDef);
        memberTable.Add(memberDef.Name, memberContext2);
      }
    }

    internal bool IsStandardModule()
    {
      return false;
    }
  }
}
