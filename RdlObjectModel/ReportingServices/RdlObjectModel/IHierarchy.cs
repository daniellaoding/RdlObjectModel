using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public interface IHierarchy
  {
    IEnumerable<IHierarchyMember> Members { get; }
  }
}
