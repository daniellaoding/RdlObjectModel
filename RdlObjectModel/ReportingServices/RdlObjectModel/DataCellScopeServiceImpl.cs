using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class DataCellScopeServiceImpl : IDataCellScopeService
  {
    public IEnumerable<IDataScope> GetDataCellScopes(IDataCell dataCell)
    {
      List<IDataScope> scopes = new List<IDataScope>();
      foreach (IHierarchy allHierarchy in GetAllHierarchies())
      {
        int dataCellCoordinate = GetDataCellCoordinate(allHierarchy, dataCell);
        if (dataCellCoordinate >= 0)
        {
          foreach (IHierarchyMember allLeafMember in DataRegion.GetAllLeafMembers(allHierarchy))
          {
            if (dataCellCoordinate == 0)
            {
              using (IEnumerator<IDataScope> enumerator = allLeafMember.GetDataScopesFor(dataCell).GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  IDataScope leafMemberScope = enumerator.Current;
                  if (!scopes.Contains(leafMemberScope))
                  {
                    scopes.Add(leafMemberScope);
                    yield return leafMemberScope;
                  }
                }
                break;
              }
            }
            else
              --dataCellCoordinate;
          }
        }
      }
    }

    protected abstract IEnumerable<IHierarchy> GetAllHierarchies();

    protected abstract int GetDataCellCoordinate(IHierarchy hierarchy, IDataCell dataCell);
  }
}
