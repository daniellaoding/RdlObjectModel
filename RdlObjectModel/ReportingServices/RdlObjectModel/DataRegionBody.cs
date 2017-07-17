using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataRegionBody : ReportObject, IDataScopeService
  {
    public DataRegionBody()
    {
    }

    internal DataRegionBody(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    IEnumerable<IDataScope> IDataScopeService.GetDataScopesFor(IContainedObject obj)
    {
      if (!IsChildOf(obj, this))
        throw new InvalidOperationException();
      IDataCell dataCell = obj as IDataCell;
      if (dataCell == null)
        return GetContainingDataScopes();
      IDataCellScopeService ancestor = GetAncestor<IDataCellScopeService>();
      if (ancestor != null)
        return ancestor.GetDataCellScopes(dataCell);
      throw new InvalidOperationException();
    }
  }
}
