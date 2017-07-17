using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public interface IDataScopeService
  {
    IEnumerable<IDataScope> GetDataScopesFor(IContainedObject obj);
  }
}
