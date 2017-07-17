using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public interface IDataScope : IContainedObject
  {
    string Name { get; }

    Group Group { get; }

    IEnumerable<IDataScope> GetContainingDataScopes();
  }
}
