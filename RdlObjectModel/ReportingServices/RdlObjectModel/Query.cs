using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Query : QueryBase
  {
    public string DataSourceName
    {
      get
      {
        return (string) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlIgnore]
    public DataSource DataSource
    {
      get
      {
        DataSource dataSource = null;
        if (!string.IsNullOrEmpty(DataSourceName))
        {
          Report ancestor = GetAncestor<Report>();
          if (ancestor != null)
            dataSource = ancestor.GetDataSourceByName(DataSourceName);
        }
        return dataSource;
      }
    }

    [XmlElement(typeof (RdlCollection<QueryParameter>))]
    public IList<QueryParameter> QueryParameters
    {
      get
      {
        return (IList<QueryParameter>) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public Query()
    {
    }

    internal Query(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DataSourceName = "";
      QueryParameters = new RdlCollection<QueryParameter>();
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      DataSourceName = nameChanges.GetNewName(NameChanges.EntryType.DataSource, DataSourceName);
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      if (string.IsNullOrEmpty(DataSourceName))
        return;
      Report ancestor = GetAncestor<Report>();
      if (ancestor == null)
        return;
      DataSource dataSourceByName = ancestor.GetDataSourceByName(DataSourceName);
      if (dataSourceByName == null || dependencies.Contains(dataSourceByName))
        return;
      dependencies.Add(dataSourceByName);
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (!base.RdlSemanticEqualsCore(rdlObj, visitedList))
        return false;
      Query query = rdlObj as Query;
      if (query == null || string.IsNullOrEmpty(DataSourceName) && !string.IsNullOrEmpty(query.DataSourceName) || !string.IsNullOrEmpty(DataSourceName) && string.IsNullOrEmpty(query.DataSourceName))
        return false;
      if (DataSourceName != null)
      {
        bool flag = false;
        Report ancestor1 = rdlObj.GetAncestor<Report>();
        Report ancestor2 = GetAncestor<Report>();
        if (ancestor1 != null && ancestor2 != null)
        {
          DataSource dataSourceByName1 = ancestor1.GetDataSourceByName(query.DataSourceName);
          DataSource dataSourceByName2 = ancestor2.GetDataSourceByName(DataSourceName);
          if (dataSourceByName1 != null && dataSourceByName2 != null)
            flag = dataSourceByName2.RdlSemanticEquals(dataSourceByName1, visitedList);
        }
        if (!flag)
          return false;
      }
      return SemanticCompare(QueryParameters, query.QueryParameters, visitedList);
    }

    internal class Definition : DefinitionStore<Query, Definition.Properties>
    {
      public enum Properties
      {
        CommandType,
        CommandText,
        Timeout,
        DataSourceName,
        QueryParameters,
      }
    }
    
  }
}
