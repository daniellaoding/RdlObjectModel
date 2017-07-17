using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public interface IHierarchyMember : IDataScopeService
  {
    Group Group { get; set; }

    IList<SortExpression> SortExpressions { get; set; }

    IEnumerable<IHierarchyMember> Members { get; }

    IEnumerable<IDataScope> GetContainingDataScopes();
  }
}
