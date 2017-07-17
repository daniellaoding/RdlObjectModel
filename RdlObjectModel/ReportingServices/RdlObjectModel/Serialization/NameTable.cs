using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  internal class NameTable<T>
  {
    private readonly Hashtable table = new Hashtable();

    public IEnumerable<NameKey> Keys
    {
      get
      {
        foreach (object key in table.Keys)
          yield return (NameKey) key;
      }
    }

    public IEnumerable<T> Values
    {
      get
      {
        foreach (object obj in table.Values)
          yield return (T) obj;
      }
    }

    public T this[string name, string ns]
    {
      get
      {
        return (T) table[new NameKey(name, ns)];
      }
      set
      {
        table[new NameKey(name, ns)] = value;
      }
    }

    public void Add(string name, string ns, T value)
    {
      table.Add(new NameKey(name, ns), value);
    }
  }
}
