using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class NameChanges
  {
    private readonly Dictionary<EntryType, IList<NameChangeEntry>> m_entries = new Dictionary<EntryType, IList<NameChangeEntry>>();

    public int Count
    {
      get
      {
        int num = 0;
        foreach (IList<NameChangeEntry> nameChangeEntryList in m_entries.Values)
          num += nameChangeEntryList.Count;
        return num;
      }
    }

    public void Add(EntryType type, string oldName, string newName)
    {
      if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName))
        throw new ArgumentException();
      if (string.Equals(oldName, newName, StringComparison.Ordinal))
        return;
      if (!m_entries.ContainsKey(type))
        m_entries.Add(type, new List<NameChangeEntry>());
      bool flag = false;
      foreach (NameChangeEntry nameChangeEntry in m_entries[type])
      {
        if (string.Equals(nameChangeEntry.OldName, oldName, StringComparison.Ordinal))
        {
          flag = true;
          nameChangeEntry.NewName = newName;
          break;
        }
      }
      if (flag)
        return;
      m_entries[type].Add(new NameChangeEntry(type, oldName, newName));
    }

    public void Add(INamedObject namedObj, string newName)
    {
      if (namedObj == null)
        throw new ArgumentNullException("namedObj");
      if (string.IsNullOrEmpty(newName))
        throw new ArgumentException();
      EntryType type;
      if (namedObj is ReportItem)
        type = EntryType.ReportItem;
      else if (namedObj is Group)
        type = EntryType.Group;
      else if (namedObj is DataSet)
        type = EntryType.DataSet;
      else if (namedObj is DataSource)
        type = EntryType.DataSource;
      else if (namedObj is ReportParameter)
      {
        type = EntryType.ReportParameter;
      }
      else
      {
        if (!(namedObj is EmbeddedImage))
          return;
        type = EntryType.EmbeddedImage;
      }
      Add(type, namedObj.Name, newName);
    }

    public bool HasNameChanged(EntryType type, string oldName)
    {
      if (oldName != null && m_entries.ContainsKey(type))
      {
        foreach (NameChangeEntry nameChangeEntry in m_entries[type])
        {
          if (string.Equals(oldName, nameChangeEntry.OldName, StringComparison.Ordinal))
            return true;
        }
      }
      return false;
    }

    public string GetNewName(EntryType type, string oldName)
    {
      if (oldName != null && m_entries.ContainsKey(type))
      {
        foreach (NameChangeEntry nameChangeEntry in m_entries[type])
        {
          if (string.Equals(oldName, nameChangeEntry.OldName, StringComparison.Ordinal))
            return nameChangeEntry.NewName;
        }
      }
      return oldName;
    }

    private class NameChangeEntry
    {
	    private string m_newName;

      public EntryType Type { get; }

	    public string OldName { get; }

	    public string NewName
      {
        get
        {
          return m_newName;
        }
        set
        {
          if (value == null || string.Equals(m_newName, value, StringComparison.Ordinal))
            return;
          m_newName = value;
        }
      }

      public NameChangeEntry(EntryType type, string oldName, string newName)
      {
        if (oldName == null)
          throw new ArgumentNullException("oldName");
        if (newName == null)
          throw new ArgumentNullException("newName");
        Type = type;
        OldName = oldName;
        m_newName = newName;
      }

      public override bool Equals(object obj)
      {
        if (!(obj is NameChangeEntry))
          return false;
        NameChangeEntry nameChangeEntry = (NameChangeEntry) obj;
        if (Type == nameChangeEntry.Type && string.Equals(OldName, nameChangeEntry.OldName, StringComparison.Ordinal))
          return string.Equals(NewName, nameChangeEntry.NewName, StringComparison.Ordinal);
        return false;
      }

      public override int GetHashCode()
      {
        return OldName.GetHashCode();
      }
    }

    public enum EntryType
    {
      ReportItem,
      Group,
      DataSet,
      DataSource,
      ReportParameter,
      EmbeddedImage,
      Scope,
    }
  }
}
