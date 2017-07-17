using System;
using System.Threading;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal class DefinitionStore<T, E>
  {
    private static readonly PropertyStore m_propertyDefinitions = new PropertyStore(null);
    private static readonly ReaderWriterLock m_lock = new ReaderWriterLock();

    protected DefinitionStore()
    {
    }

    public static IPropertyDefinition GetProperty(int index)
    {
      m_lock.AcquireReaderLock(-1);
      try
      {
        IPropertyDefinition propertyDefinition = (IPropertyDefinition) m_propertyDefinitions.GetObject(index);
        if (propertyDefinition != null)
          return propertyDefinition;
      }
      finally
      {
        m_lock.ReleaseReaderLock();
      }
      IPropertyDefinition propertyDefinition1 = PropertyDefinition.Create(typeof (T), Enum.GetName(typeof (E), index));
      m_lock.AcquireWriterLock(-1);
      try
      {
        m_propertyDefinitions.SetObject(index, propertyDefinition1);
      }
      finally
      {
        m_lock.ReleaseWriterLock();
      }
      return propertyDefinition1;
    }
  }
}
