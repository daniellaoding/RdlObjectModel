using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
  internal static class ArrayUtil
  {
    public static T[] ToArray<T>(ICollection<T> items)
    {
      if (items == null)
        throw new ArgumentNullException("items");
      T[] array = new T[items.Count];
      items.CopyTo(array, 0);
      return array;
    }

    public static object[] ToObjectArray<T>(ICollection<T> items)
    {
      if (items == null)
        throw new ArgumentNullException("items");
      object[] objArray = new object[items.Count];
      int num = 0;
      foreach (T obj1 in items)
      {
        object obj2 = obj1;
        objArray[num++] = obj2;
      }
      return objArray;
    }

    public static bool Contains<T>(T[] array, T item)
    {
      return Array.IndexOf(array, item) != -1;
    }
  }
}
