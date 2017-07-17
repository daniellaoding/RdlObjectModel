using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal class PropertyStore : IPropertyStore
  {
	  private Dictionary<int, object> m_objEntries;
    private Dictionary<int, int> m_intEntries;
    private Dictionary<int, bool> m_boolEntries;
    private Dictionary<int, ReportSize> m_sizeEntries;

    public ReportObject Owner { get; private set; }

	  public IContainedObject Parent { get; set; }

	  public PropertyStore(ReportObject owner)
    {
      Owner = owner;
    }

    internal PropertyStore()
    {
    }

    internal void SetOwner(ReportObject owner)
    {
      Owner = owner;
    }

    public void RemoveProperty(int propertyIndex)
    {
      RemoveObject(propertyIndex);
      RemoveInteger(propertyIndex);
      RemoveBoolean(propertyIndex);
      RemoveSize(propertyIndex);
    }

    public object GetObject(int propertyIndex)
    {
      if (m_objEntries != null && m_objEntries.ContainsKey(propertyIndex))
        return m_objEntries[propertyIndex];
      return null;
    }

    public T GetObject<T>(int propertyIndex)
    {
      object obj = GetObject(propertyIndex);
      if (obj != null)
        return (T) obj;
      return default (T);
    }

    public void SetObject(int propertyIndex, object value)
    {
      if (m_objEntries == null)
        m_objEntries = new Dictionary<int, object>();
      if (value is IContainedObject)
        ((IContainedObject) value).Parent = Owner;
      m_objEntries[propertyIndex] = value;
      if (Owner == null)
        return;
      Owner.OnSetObject(propertyIndex);
    }

    public void RemoveObject(int propertyIndex)
    {
      if (m_objEntries == null)
        return;
      m_objEntries.Remove(propertyIndex);
    }

    public bool ContainsObject(int propertyIndex)
    {
      if (m_objEntries != null)
        return m_objEntries.ContainsKey(propertyIndex);
      return false;
    }

    public int GetInteger(int propertyIndex)
    {
      if (m_intEntries != null && m_intEntries.ContainsKey(propertyIndex))
        return m_intEntries[propertyIndex];
      return 0;
    }

    public void SetInteger(int propertyIndex, int value)
    {
      if (m_intEntries == null)
        m_intEntries = new Dictionary<int, int>();
      m_intEntries[propertyIndex] = value;
    }

    public void RemoveInteger(int propertyIndex)
    {
      if (m_intEntries == null)
        return;
      m_intEntries.Remove(propertyIndex);
    }

    public bool ContainsInteger(int propertyIndex)
    {
      if (m_intEntries != null)
        return m_intEntries.ContainsKey(propertyIndex);
      return false;
    }

    public bool GetBoolean(int propertyIndex)
    {
      if (m_boolEntries != null && m_boolEntries.ContainsKey(propertyIndex))
        return m_boolEntries[propertyIndex];
      return false;
    }

    public void SetBoolean(int propertyIndex, bool value)
    {
      if (m_boolEntries == null)
        m_boolEntries = new Dictionary<int, bool>();
      m_boolEntries[propertyIndex] = value;
    }

    public void RemoveBoolean(int propertyIndex)
    {
      if (m_boolEntries == null)
        return;
      m_boolEntries.Remove(propertyIndex);
    }

    public bool ContainsBoolean(int propertyIndex)
    {
      if (m_boolEntries != null)
        return m_boolEntries.ContainsKey(propertyIndex);
      return false;
    }

    public ReportSize GetSize(int propertyIndex)
    {
      if (m_sizeEntries != null && m_sizeEntries.ContainsKey(propertyIndex))
        return m_sizeEntries[propertyIndex];
      return new ReportSize();
    }

    public void SetSize(int propertyIndex, ReportSize value)
    {
      if (m_sizeEntries == null)
        m_sizeEntries = new Dictionary<int, ReportSize>();
      m_sizeEntries[propertyIndex] = value;
    }

    public void RemoveSize(int propertyIndex)
    {
      if (m_sizeEntries == null)
        return;
      m_sizeEntries.Remove(propertyIndex);
    }

    public bool ContainsSize(int propertyIndex)
    {
      if (m_sizeEntries != null)
        return m_sizeEntries.ContainsKey(propertyIndex);
      return false;
    }

    public void IterateObjectEntries(VisitPropertyObject visitObject)
    {
      if (m_objEntries == null)
        return;
      foreach (int key in m_objEntries.Keys)
        visitObject(key, m_objEntries[key]);
    }
  }
}
