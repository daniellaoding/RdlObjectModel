using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal abstract class LookupContext
  {
	  protected Dictionary<string, LookupContext> m_namespaces;
    protected Dictionary<string, LookupContext> m_types;
    protected Dictionary<string, MemberContext> m_members;

    internal string Name { get; }

	  internal LookupContext(string name)
    {
      Name = name;
      m_types = new Dictionary<string, LookupContext>(StringUtil.CaseInsensitiveComparer);
      m_namespaces = new Dictionary<string, LookupContext>(StringUtil.CaseInsensitiveComparer);
      m_members = new Dictionary<string, MemberContext>(StringUtil.CaseInsensitiveComparer);
    }

    protected void InitEnvironment(IEnvironmentFilter filter)
    {
      Dictionary<string, string> specialAliasTable = new Dictionary<string, string>(StringUtil.CaseInsensitiveComparer);
      specialAliasTable["System.Int32"] = "Integer";
      specialAliasTable["System.Int64"] = "Long";
      specialAliasTable["System.Int16"] = "Short";
      specialAliasTable["System.UInt32"] = "UInteger";
      specialAliasTable["System.UInt64"] = "ULong";
      specialAliasTable["System.UInt16"] = "UShort";
      specialAliasTable["System.DateTime"] = "Date";
      ProcessAssembly(Assembly.GetAssembly(typeof (string)), filter, "System", specialAliasTable, "System.Convert", "System.Math");
      ProcessAssembly(Assembly.GetAssembly(typeof (Uri)), filter, "System", null);
      m_namespaces.Add("Global", this);
      MergeMembers(new TypeContext("VBFunctions", typeof (VBFunctions), false, false, DefaultEnvironmentFilter.Instance, BindingFlags.NonPublic));
      MemberContext memberContext = new MemberContext("Code", MemberContext.MemberContextTypes.Property);
      m_members.Add(memberContext.Name, memberContext);
      TypeContext source = new TypeContext("ReportItem", typeof (ReportObjectModel.ReportItem), false, false, DefaultEnvironmentFilter.Instance, BindingFlags.Instance | BindingFlags.NonPublic);
      m_types.Add("Me", source);
      MergeMembers(source);
    }

    protected void ProcessAssembly(Assembly assembly, IEnvironmentFilter filter, string importedNs, Dictionary<string, string> specialAliasTable, params string[] importedTypes)
    {
      Dictionary<string, bool> dictionary = new Dictionary<string, bool>(StringUtil.CaseInsensitiveComparer);
      foreach (string importedType in importedTypes)
        dictionary[importedType] = true;
      string[] strArray1 = SplitNamespace(importedNs);
      foreach (Type type in assembly.GetTypes())
      {
        bool allowNew;
        bool allowNewArray;
        if (!type.IsNested && type.IsPublic && filter.IsAllowedType(type, out allowNew, out allowNewArray))
        {
          string str1 = type.Namespace;
          string[] strArray2 = SplitNamespace(str1);
          bool flag = !string.IsNullOrEmpty(importedNs);
          LookupContext lookupContext1 = this;
          for (int index = 0; index < strArray2.Length; ++index)
          {
            string str2 = strArray2[index];
            LookupContext lookupContext2;
            if (!lookupContext1.m_namespaces.TryGetValue(str2, out lookupContext2))
            {
              lookupContext2 = new NamespaceContext(str2);
              lookupContext1.m_namespaces.Add(str2, lookupContext2);
            }
            lookupContext1 = lookupContext2;
            if (flag)
            {
              if (index == strArray1.Length)
              {
                if (!m_namespaces.ContainsKey(str2))
                  m_namespaces.Add(str2, lookupContext2);
                flag = false;
              }
              else
                flag = StringUtil.EqualsIgnoreCase(str2, strArray1[index]);
            }
          }
          TypeContext source = lookupContext1.AddType(type, allowNew, allowNewArray, filter);
          if (StringUtil.EqualsIgnoreCase(str1, importedNs))
          {
            if (!m_types.ContainsKey(type.Name))
              m_types.Add(type.Name, source);
            if (source.IsStandardModule())
              MergeMembers(source);
          }
          if (dictionary.ContainsKey(type.FullName))
            MergeMembers(source);
          string key;
          if (specialAliasTable != null && specialAliasTable.TryGetValue(type.FullName, out key))
            m_types.Add(key, source);
        }
      }
    }

    private void MergeMembers(TypeContext source)
    {
      foreach (KeyValuePair<string, MemberContext> member in source.m_members)
        m_members[member.Key] = member.Value;
    }

    protected TypeContext AddType(Type type, bool allowNew, bool allowNewArray, IEnvironmentFilter filter)
    {
      string name = type.Name;
      LookupContext lookupContext;
      if (!m_types.TryGetValue(name, out lookupContext))
      {
        lookupContext = new TypeContext(name, type, allowNew, allowNewArray, filter);
        m_types.Add(name, lookupContext);
      }
      foreach (Type nestedType in type.GetNestedTypes())
      {
        bool allowNew1;
        bool allowNewArray1;
        if (filter.IsAllowedType(nestedType, out allowNew1, out allowNewArray1))
          lookupContext.AddType(nestedType, allowNew1, allowNewArray1, filter);
      }
      return (TypeContext) lookupContext;
    }

    private string[] SplitNamespace(string nsStr)
    {
      if (string.IsNullOrEmpty(nsStr))
        return new string[0];
      return nsStr.Split('.');
    }

    internal virtual bool TryMatchMember(string identifier, out MemberContext member)
    {
      return m_members.TryGetValue(identifier, out member);
    }

    internal virtual bool TryMatchSubContext(string identifier, out LookupContext subContext)
    {
      if (m_namespaces.TryGetValue(identifier, out subContext) || m_types.TryGetValue(identifier, out subContext))
        return true;
      subContext = null;
      return false;
    }
  }
}
