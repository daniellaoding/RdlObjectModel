using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class RdlCollection<T> : RdlCollectionBase<T>
  {
    public event EventHandler CollectionChanged;

    public RdlCollection()
    {
    }

    public RdlCollection(IContainedObject parent)
      : base(parent)
    {
    }

    public RdlCollection(IEnumerable<T> items)
    {
      if (items == null)
        return;
      foreach (T obj in items)
        Add(obj);
    }

    protected virtual void OnCollectionChanged(EventArgs e)
    {
      if (CollectionChanged != null)
        CollectionChanged(this, e);
      if (!(Parent is ReportObject))
        return;
      ((ReportObject) Parent).OnChildPropertyChanged(0, null, this);
    }

    protected override void InsertItem(int index, T item)
    {
      InsertItemCore(index, item, true);
    }

    protected override void RemoveItem(int index)
    {
      RemoveItemCore(index, true);
    }

    protected override void SetItem(int index, T item)
    {
      SetItemCore(index, item, true);
    }

    protected override void ClearItems()
    {
      ClearItemsCore(true);
    }

    private void InsertItemCore(int index, T item, bool addUndoCommand)
    {
      IContainedObject itemParent = GetItemParent(item);
      base.InsertItem(index, item);
      OnCollectionChanged(EventArgs.Empty);
    }

    private void RemoveItemCore(int index, bool addUndoCommand)
    {
      T obj = this[index];
      base.RemoveItem(index);
      OnCollectionChanged(EventArgs.Empty);
    }

    private T SetItemCore(int index, T item, bool addUndoCommand)
    {
      IContainedObject itemParent = GetItemParent(item);
      T oldItem = this[index];
      base.SetItem(index, item);
      if (!Equals(oldItem, item))
      {
        OnCollectionChanged(EventArgs.Empty);
      }
      return oldItem;
    }

    private void ClearItemsCore(bool addUndoCommand)
    {
      T[] array = ArrayUtil.ToArray(this);
      base.ClearItems();
      if (array.Length <= 0)
        return;
      OnCollectionChanged(EventArgs.Empty);
    }

    private static IContainedObject GetItemParent(T item)
    {
      IContainedObject containedObject = (object) item as IContainedObject;
      if (containedObject != null)
        return containedObject.Parent;
      return null;
    }

    private static void SetItemParent(T item, IContainedObject parent)
    {
      IContainedObject containedObject = (object) item as IContainedObject;
      if (containedObject == null)
        return;
      containedObject.Parent = parent;
    }

    private sealed class InsertItemCommand
    {
      private readonly RdlCollection<T> m_list;
      private readonly int m_index;
      private readonly T m_item;
      private readonly IContainedObject m_previousParent;

      public string Description => string.Format(SRRdl.InsertItemCommand, typeof (T).Name);

	    internal InsertItemCommand(RdlCollection<T> list, int index, T item, IContainedObject previousParent)
      {
        m_list = list;
        m_index = index;
        m_item = item;
        m_previousParent = previousParent;
      }

      public void Execute()
      {
        m_list.InsertItemCore(m_index, m_item, false);
      }

      public void Unexecute()
      {
        m_list.RemoveItemCore(m_index, false);
        SetItemParent(m_item, m_previousParent);
      }
    }

    private sealed class RemoveItemCommand
    {
      private readonly RdlCollection<T> m_list;
      private readonly int m_index;
      private readonly T m_item;

      public string Description => string.Format(SRRdl.RemoveItemCommand, typeof (T).Name);

	    internal RemoveItemCommand(RdlCollection<T> list, int index, T item)
      {
        m_list = list;
        m_index = index;
        m_item = item;
      }

      public void Execute()
      {
        m_list.RemoveItemCore(m_index, false);
      }

      public void Unexecute()
      {
        m_list.InsertItemCore(m_index, m_item, false);
      }
    }

    private sealed class SetItemCommand
    {
      private readonly RdlCollection<T> m_list;
      private readonly int m_index;
      private readonly T m_oldItem;
      private readonly T m_newItem;
      private readonly IContainedObject m_newItemPreviousParent;

      public string Description => string.Format(SRRdl.SetItemCommand, typeof (T).Name);

	    internal SetItemCommand(RdlCollection<T> list, int index, T oldItem, T newItem, IContainedObject newItemPreviousParent)
      {
        m_list = list;
        m_index = index;
        m_oldItem = oldItem;
        m_newItem = newItem;
        m_newItemPreviousParent = newItemPreviousParent;
      }

      public void Execute()
      {
        m_list.SetItemCore(m_index, m_newItem, false);
      }

      public void Unexecute()
      {
        m_list.SetItemCore(m_index, m_oldItem, false);
        SetItemParent(m_newItem, m_newItemPreviousParent);
      }
    }

    private sealed class ClearItemsCommand
    {
      private readonly RdlCollection<T> m_list;
      private readonly T[] m_items;

      public string Description => string.Format(SRRdl.ClearItemsCommand, typeof (T).Name);

	    internal ClearItemsCommand(RdlCollection<T> list, T[] items)
      {
        m_list = list;
        m_items = items;
      }

      public void Execute()
      {
        m_list.ClearItemsCore(false);
      }
    }
  }
}
