using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class RdlCollectionBase<T> : Collection<T>, IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable, IContainedObject
  {
    private IContainedObject m_parent;

    [XmlIgnore]
    public IContainedObject Parent
    {
      get
      {
        return m_parent;
      }
      set
      {
        m_parent = value;
        if (!typeof (IContainedObject).IsAssignableFrom(typeof (T)))
          return;
        foreach (T obj in this)
          ((IContainedObject) obj).Parent = value;
      }
    }

    object IList.this[int index]
    {
      get
      {
        return this[index];
      }
      set
      {
        this[index] = (T) value;
      }
    }

    protected RdlCollectionBase()
    {
    }

    protected RdlCollectionBase(IContainedObject parent)
    {
      m_parent = parent;
    }

    protected override void InsertItem(int index, T item)
    {
      if ((object) item is IContainedObject)
        ((IContainedObject) item).Parent = m_parent;
      base.InsertItem(index, item);
    }

    protected override void SetItem(int index, T item)
    {
      if ((object) item is IContainedObject)
        ((IContainedObject) item).Parent = m_parent;
      base.SetItem(index, item);
    }

    int IList.Add(object item)
    {
      Add((T) item);
      return Count - 1;
    }
  }
}
