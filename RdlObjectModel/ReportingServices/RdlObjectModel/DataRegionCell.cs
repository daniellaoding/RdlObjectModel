using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class DataRegionCell : ReportObject, IDataScopeService, IDataCell, IContainedObject
  {
    public DataRegionCell()
    {
    }

    internal DataRegionCell(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    IEnumerable<IDataScope> IDataScopeService.GetDataScopesFor(IContainedObject obj)
    {
      if (!IsChildOf(obj, this))
        throw new InvalidOperationException();
      return GetContainingDataScopes();
    }
  }
}
