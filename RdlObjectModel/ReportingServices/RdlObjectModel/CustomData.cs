using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class CustomData : DataRegionBody, IDataScopeService, IDataScope, IContainedObject, IDataCellScopeService
  {
    private CriDataCellScopeService __criDataCellScopeService;

    public string DataSetName
    {
      get
      {
        return (string) PropertyStore.GetObject(0);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Filter>))]
    public IList<Filter> Filters
    {
      get
      {
        return (IList<Filter>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<SortExpression>))]
    public IList<SortExpression> SortExpressions
    {
      get
      {
        return (IList<SortExpression>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public DataHierarchy DataColumnHierarchy
    {
      get
      {
        return (DataHierarchy) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public DataHierarchy DataRowHierarchy
    {
      get
      {
        return (DataHierarchy) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [XmlElement(typeof (RdlCollection<IList<IList<DataValue>>>))]
    [XmlArrayItem("DataCell", typeof (DataCell), NestingLevel = 1)]
    [XmlArrayItem("DataRow", typeof (DataRow), NestingLevel = 0)]
    public IList<IList<IList<DataValue>>> DataRows
    {
      get
      {
        return (IList<IList<IList<DataValue>>>) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [XmlIgnore]
    public IEnumerable<Group> AllGroups
    {
      get
      {
        List<Group> groupList = new List<Group>();
        DataRegion.AddGroupsToList(DataColumnHierarchy, groupList);
        DataRegion.AddGroupsToList(DataRowHierarchy, groupList);
        return groupList;
      }
    }

    string IDataScope.Name => ((ReportItem) Parent).Name;

	  Group IDataScope.Group => null;

	  public CustomData()
    {
    }

    internal CustomData(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DataSetName = "";
      Filters = new RdlCollection<Filter>();
      SortExpressions = new RdlCollection<SortExpression>();
      DataRows = new RdlCollection<IList<IList<DataValue>>>();
    }

    IEnumerable<IDataScope> IDataScopeService.GetDataScopesFor(IContainedObject obj)
    {
      if (!IsChildOf(obj, this))
        throw new InvalidOperationException();
      IDataCell dataCell = obj as IDataCell;
      if (dataCell != null)
        return ((IDataCellScopeService) this).GetDataCellScopes(dataCell);
      return GetDataScopesForDefaultImpl(obj);
    }

    IEnumerable<IDataScope> IDataCellScopeService.GetDataCellScopes(IDataCell dataCell)
    {
      if (__criDataCellScopeService == null)
        __criDataCellScopeService = new CriDataCellScopeService(this);
      return __criDataCellScopeService.GetDataCellScopes(dataCell);
    }

    internal class Definition : DefinitionStore<CustomData, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataSetName,
        Filters,
        SortExpressions,
        DataColumnHierarchy,
        DataRowHierarchy,
        DataRows,
        PropertyCount,
      }
    }

    private sealed class CriDataCellScopeService : DataCellScopeServiceImpl
    {
      private readonly CustomData m_customData;

      internal CriDataCellScopeService(CustomData customData)
      {
        m_customData = customData;
      }

      protected override IEnumerable<IHierarchy> GetAllHierarchies()
      {
        if (m_customData.DataColumnHierarchy != null)
          yield return m_customData.DataColumnHierarchy;
        if (m_customData.DataRowHierarchy != null)
          yield return m_customData.DataRowHierarchy;
      }

      protected override int GetDataCellCoordinate(IHierarchy hierarchy, IDataCell dataCell)
      {
        int index1 = 0;
        int index2 = 0;
        bool flag = false;
        for (; index1 < m_customData.DataRows.Count; ++index1)
        {
          for (; index2 < m_customData.DataRows[index1].Count; ++index2)
          {
            if (m_customData.DataRows[index1][index2].Contains(dataCell as DataValue))
            {
              flag = true;
              break;
            }
          }
          if (flag)
            break;
        }
        if (flag)
        {
          if (hierarchy == m_customData.DataColumnHierarchy)
            return index1;
          if (hierarchy == m_customData.DataRowHierarchy)
            return index2;
        }
        throw new InvalidOperationException();
      }
    }
  }
}
