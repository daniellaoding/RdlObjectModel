using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal interface IDataCellScopeService
  {
    IEnumerable<IDataScope> GetDataCellScopes(IDataCell dataCell);
  }
}
