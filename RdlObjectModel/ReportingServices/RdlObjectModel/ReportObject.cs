using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class ReportObject : ReportObjectBase, IComponent
  {
    private ComponentMetadata m_componentMetadata;
    private ISite __site;

	  [DefaultValue(null)]
    [XmlElement(typeof (ComponentMetadata), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/componentdefinition")]
    public ComponentMetadata ComponentMetadata
    {
      get
      {
        return m_componentMetadata;
      }
      set
      {
        if (m_componentMetadata == value)
          return;
        SavePropertyValue("ComponentMetadata", m_componentMetadata, (ComponentMetadata newValue, out ComponentMetadata oldValue) =>
        {
	        oldValue = m_componentMetadata;
	        m_componentMetadata = newValue;
        });
        m_componentMetadata = value;
        if (m_componentMetadata == null)
          return;
        m_componentMetadata.Parent = this;
      }
    }

    [DefaultValue(false)]
    [XmlIgnore]
    public static bool DesignerModel { get; set; }

	  [XmlIgnore]
    [Browsable(false)]
    public ISite Site
    {
      get
      {
        if (__site == null)
        {
          ReportObject parent = Parent as ReportObject;
          if (parent != null)
            return parent.Site;
        }
        return __site;
      }
      set
      {
        __site = value;
      }
    }

    internal event EventHandler<PropertyChangedEventArgs> PropertyChanged;

    internal event EventHandler<PropertyChangedEventArgs> ChildPropertyChanged;

    event EventHandler IComponent.Disposed
    {
      add
      {
      }
      remove
      {
      }
    }

    static ReportObject()
    {
      DesignerModel = true;
    }

    protected ReportObject()
    {
    }

    internal ReportObject(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public virtual object DeepClone()
    {
      Type type = GetType();
      PropertyStore propertyStore = new PropertyStore();
      ReportObject instance = (ReportObject) Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[1]
      {
        propertyStore
      }, null);
      propertyStore.SetOwner(instance);
      CopyTo(instance, null);
      return instance;
    }

    private void CopyTo(ReportObject clone, ICollection<string> membersToExclude)
    {
      foreach (MemberMapping member in ((StructMapping) TypeMapper.GetTypeMapping(GetType())).Members)
      {
        if (member.HasValue(this) && (membersToExclude == null || !membersToExclude.Contains(member.Name)))
        {
          object obj = member.GetValue(this);
          member.SetValue(clone, CloneObject(obj));
        }
      }
    }

    protected static object CloneObject(object obj)
    {
      if (obj is ReportObject)
        obj = ((ReportObject) obj).DeepClone();
      else if (obj is IList)
        obj = CloneList((IList) obj);
      return obj;
    }

    private static object CloneList(IList obj)
    {
      IList instance = (IList) Activator.CreateInstance(obj.GetType());
      foreach (object obj1 in obj)
        instance.Add(CloneObject(obj1));
      return instance;
    }

    internal virtual void OnSetObject(int propertyIndex)
    {
    }

    internal T GetAncestor<T>() where T : class
    {
      for (IContainedObject parent = Parent; parent != null; parent = parent.Parent)
      {
        if (parent is T)
          return (T) parent;
      }
      return default (T);
    }

    internal virtual void UpdateNamedReferences(NameChanges nameChanges)
    {
      if (nameChanges.Count <= 0)
        return;
      Dictionary<int, IExpression> updatedExpressions = new Dictionary<int, IExpression>();
      PropertyStore.IterateObjectEntries((propertyIndex, value) =>
      {
	      if (value is ReportExpression)
	      {
		      ReportExpression reportExpression = ((ReportExpression) value).UpdateNamedReferences(nameChanges);
		      if (string.Equals(reportExpression.Value, ((ReportExpression) value).Value, StringComparison.Ordinal))
			      return;
		      updatedExpressions.Add(propertyIndex, reportExpression);
	      }
	      else if (value is ReportExpression<bool>)
	      {
		      ReportExpression<bool> reportExpression = ((ReportExpression<bool>) value).UpdateNamedReferences(nameChanges);
		      if (string.Equals(reportExpression.Expression, ((ReportExpression<bool>) value).Expression, StringComparison.Ordinal))
			      return;
		      updatedExpressions.Add(propertyIndex, reportExpression);
	      }
	      else if (value is ReportExpression<long>)
	      {
		      ReportExpression<long> reportExpression = ((ReportExpression<long>) value).UpdateNamedReferences(nameChanges);
		      if (string.Equals(reportExpression.Expression, ((ReportExpression<long>) value).Expression, StringComparison.Ordinal))
			      return;
		      updatedExpressions.Add(propertyIndex, reportExpression);
	      }
	      else if (value is ReportExpression<double>)
	      {
		      ReportExpression<double> reportExpression = ((ReportExpression<double>) value).UpdateNamedReferences(nameChanges);
		      if (string.Equals(reportExpression.Expression, ((ReportExpression<double>) value).Expression, StringComparison.Ordinal))
			      return;
		      updatedExpressions.Add(propertyIndex, reportExpression);
	      }
	      else if (value is ReportObject)
	      {
		      ((ReportObject) value).UpdateNamedReferences(nameChanges);
	      }
	      else
	      {
		      if (!(value is IList))
			      return;
		      foreach (object obj in (IEnumerable) value)
		      {
			      if (obj is ReportObject)
				      ((ReportObject) obj).UpdateNamedReferences(nameChanges);
		      }
	      }
      });
      foreach (int key in updatedExpressions.Keys)
        PropertyStore.SetObject(key, updatedExpressions[key]);
    }

    internal IList<ReportObject> GetDependencies()
    {
      IList<ReportObject> dependencies = new List<ReportObject>();
      GetDependenciesCore(dependencies);
      return dependencies;
    }

    protected virtual void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      PropertyStore.IterateObjectEntries((propertyIndex, value) =>
      {
	      if (value is IExpression)
		      ((IExpression) value).GetDependencies(dependencies, this);
	      else if (value is ReportObject)
	      {
		      ((ReportObject) value).GetDependenciesCore(dependencies);
	      }
	      else
	      {
		      if (!(value is IList))
			      return;
		      foreach (object obj in (IEnumerable) value)
		      {
			      if (obj is ReportObject)
				      ((ReportObject) obj).GetDependenciesCore(dependencies);
			      else if (obj is ReportExpression)
				      ((ReportExpression) obj).GetDependencies(dependencies, this);
		      }
	      }
      });
    }

    public override void Initialize()
    {
      base.Initialize();
      if (!DesignerModel)
        return;
      InitializeForDesigner();
    }

    protected virtual void InitializeForDesigner()
    {
    }

    internal override IPropertyStore WrapPropertyStore(IPropertyStore propertyStore)
    {
      if (DesignerModel)
        return new DesignerPropertyStore(propertyStore);
      return base.WrapPropertyStore(propertyStore);
    }

    protected internal virtual void OnPropertyChanged(int propertyIndex, object oldValue, object newValue)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyIndex, oldValue, newValue));
      OnChildPropertyChanged(propertyIndex, oldValue, newValue);
    }

    protected internal virtual void OnChildPropertyChanged(int propertyIndex, object oldValue, object newValue)
    {
      if (ChildPropertyChanged != null)
        ChildPropertyChanged(this, new PropertyChangedEventArgs(propertyIndex, oldValue, newValue));
      if (Parent == null || !(Parent is ReportObject))
        return;
      ((ReportObject) Parent).OnChildPropertyChanged(propertyIndex, oldValue, newValue);
    }

    protected void SavePropertyValue<T>(string propertyName, T value, SwapValue<T> swapValue)
    {
    }

    internal ReportObject DeepClone(ICollection<string> membersToExclude)
    {
      ReportObject instance = (ReportObject) Activator.CreateInstance(GetType());
      CopyTo(instance, membersToExclude);
      if (!(instance is Report))
        instance.Parent = Parent;
      return instance;
    }

    public IEnumerable<IDataScope> GetContainingDataScopes()
    {
      IDataScopeService ancestor = GetAncestor<IDataScopeService>();
      if (ancestor != null)
        return ancestor.GetDataScopesFor(this);
      return new IDataScope[0];
    }

    protected IEnumerable<IDataScope> GetDataScopesForDefaultImpl(IContainedObject obj)
    {
      if (!IsChildOf(obj, this) || !(this is IDataScopeService) || !(this is IDataScope))
        throw new InvalidOperationException();
      yield return (IDataScope) this;
    }

    internal static bool IsChildOf(IContainedObject obj, IContainedObject parent)
    {
      return FindParent(obj, pObj => pObj == parent) != null;
    }

    internal static IContainedObject FindParent(IContainedObject obj, Predicate<IContainedObject> predicate)
    {
      if (obj != null)
        obj = obj.Parent;
      while (obj != null && !predicate(obj))
        obj = obj.Parent;
      return obj;
    }

    internal static int Compare<T>(T ro1, T ro2) where T : ReportObject
    {
      return Compare(ro1, ro2, null);
    }

    internal static int Compare<T>(T ro1, T ro2, ICollection<string> membersToExclude) where T : ReportObject
    {
      if (ro1 == null && ro2 == null)
        return 0;
      if (ro1 == null)
        return -1;
      if (ro2 == null)
        return 1;
      foreach (MemberMapping member in ((StructMapping) TypeMapper.GetTypeMapping(ro2.GetType())).Members)
      {
        if (membersToExclude == null || !membersToExclude.Contains(member.Name))
        {
          object obj1 = member.GetValue(ro1);
          object obj2 = member.GetValue(ro2);
          if (obj1 != null || obj2 != null)
          {
            if (obj1 == null)
              return -1;
            if (obj2 == null)
              return 1;
            int num = !typeof (ReportObject).IsAssignableFrom(member.Type) ? (!member.Type.IsGenericType || !typeof (RdlCollection<>).IsAssignableFrom(member.Type.GetGenericTypeDefinition()) ? (!typeof (IComparable).IsAssignableFrom(member.Type) ? (obj1.Equals(obj2) ? 0 : 1) : ((IComparable) obj1).CompareTo(obj2)) : Compare<ReportObject>((IEnumerable) obj1, (IEnumerable) obj2, membersToExclude)) : Compare((ReportObject) obj1, (ReportObject) obj2, membersToExclude);
            if (num != 0)
              return num;
          }
        }
      }
      return 0;
    }

    internal static int Compare<T>(IEnumerable ro1, IEnumerable ro2) where T : ReportObject
    {
      return Compare<T>(ro1, ro2, null);
    }

    internal static int Compare<T>(IEnumerable ro1, IEnumerable ro2, ICollection<string> membersToExclude) where T : ReportObject
    {
      if (ro1 == null && ro2 == null)
        return 0;
      if (ro1 == null)
        return -1;
      if (ro2 == null)
        return 1;
      IEnumerator enumerator1 = ro1.GetEnumerator();
      IEnumerator enumerator2 = ro2.GetEnumerator();
      bool flag1 = enumerator1.MoveNext();
      bool flag2;
      for (flag2 = enumerator2.MoveNext(); flag1 && flag2; flag2 = enumerator2.MoveNext())
      {
        int num = Compare(enumerator1.Current as T, enumerator2.Current as T, membersToExclude);
        if (num != 0)
          return num;
        flag1 = enumerator1.MoveNext();
      }
      return flag1 != flag2 ? 1 : 0;
    }

    internal bool RdlSemanticEquals(ReportObject rdlObj)
    {
      return RdlSemanticEquals(rdlObj, new List<ReportObject>());
    }

    internal bool RdlSemanticEquals(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (rdlObj == null)
        throw new ArgumentNullException("rdlObj");
      if (visitedList == null)
        throw new ArgumentNullException("visitedList");
      if (ReferenceEquals(this, rdlObj))
        return true;
      return RdlSemanticEqualsCore(rdlObj, visitedList);
    }

    protected virtual bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      return false;
    }

    protected static bool CheckVisitedAndUpdate(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (visitedList.Contains(rdlObj))
        return true;
      visitedList.Add(rdlObj);
      return false;
    }

    protected static bool CompareReportParamterExpression(ReportExpression local, ReportObject localParent, ReportExpression compareObj, ReportObject compareObjParent, ICollection<ReportObject> visitedList)
    {
      IList<ReportObject> dependencies1 = new List<ReportObject>();
      local.GetDependencies(dependencies1, localParent);
      IList<ReportObject> dependencies2 = new List<ReportObject>();
      compareObj.GetDependencies(dependencies2, compareObjParent);
      if (dependencies1.Count != dependencies2.Count)
        return false;
      if (dependencies1.Count == 1 && dependencies1[0] is ReportParameter && dependencies2[0] is ReportParameter)
      {
        if (!dependencies1[0].RdlSemanticEquals(dependencies2[0], visitedList))
          return false;
      }
      else if (!local.Equals(compareObj))
        return false;
      return true;
    }

    protected static bool SemanticCompare<T>(IList<T> x, IList<T> y, ICollection<ReportObject> visitedList) where T : ReportObject
    {
      if (x != null && y == null || x == null && y != null)
        return false;
      if (x != null)
      {
        if (x.Count != y.Count)
          return false;
        for (int index = 0; index < x.Count; ++index)
        {
          if (!x[index].RdlSemanticEqualsCore(y[index], visitedList))
            return false;
        }
      }
      return true;
    }

    protected static bool SemanticCompare<T>(T x, T y, ICollection<ReportObject> visitedList) where T : ReportObject
    {
      return (x == null || y != null) && (x != null || y == null) && (x == null || x.RdlSemanticEqualsCore(y, visitedList));
    }

    void IDisposable.Dispose()
    {
    }

    protected delegate void SwapValue<T>(T newValue, out T oldValue);

    private sealed class PropertyChangeCommand<T>
    {
      private readonly ReportObject m_object;
      private readonly string m_propertyName;
      private readonly SwapValue<T> m_swapValue;
      private T m_value;

      public string Description => string.Format(SRRdl.PropertyChangeCommand, m_object.GetType().Name, m_propertyName);

	    internal PropertyChangeCommand(ReportObject obj, string propertyName, SwapValue<T> swapValue, T value)
      {
        m_object = obj;
        m_propertyName = propertyName;
        m_swapValue = swapValue;
        m_value = value;
      }

      public void Execute()
      {
        T oldValue;
        m_swapValue(m_value, out oldValue);
        m_value = oldValue;
      }

      public void Unexecute()
      {
        Execute();
      }
    }

    internal class PropertyChangedEventArgs : EventArgs
    {
	    public int PropertyIndex { get; }

	    public object OldValue { get; }

	    public object NewValue { get; }

	    internal PropertyChangedEventArgs(int propertyIndex, object oldValue, object newValue)
      {
        PropertyIndex = propertyIndex;
        OldValue = oldValue;
        NewValue = newValue;
      }
    }
  }
}
