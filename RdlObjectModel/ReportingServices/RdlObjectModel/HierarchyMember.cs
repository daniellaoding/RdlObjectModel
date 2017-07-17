using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class HierarchyMember : ReportObject, IDataScopeService
  {
    public abstract Group Group { get; set; }

    public HierarchyMember()
    {
    }

    internal HierarchyMember(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    IEnumerable<IDataScope> IDataScopeService.GetDataScopesFor(IContainedObject obj)
    {
      if (!(obj is IDataCell) && !IsChildOf(obj, this))
        throw new InvalidOperationException();
      if (Group == null || obj == Group)
      {
        foreach (IDataScope containingDataScope in GetContainingDataScopes())
          yield return containingDataScope;
      }
      else
        yield return Group;
    }
  }
}
