using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class DataRegion : ReportItem, IDataScopeService, IDataScope, IContainedObject, IDataCellScopeService
  {
    public const bool AllowTypeInHeaderFooter = false;

    [DefaultValue(false)]
    public bool KeepTogether
    {
      get
      {
        return PropertyStore.GetBoolean(18);
      }
      set
      {
        PropertyStore.SetBoolean(18, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression NoRowsMessage
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    [DefaultValue("")]
    public string DataSetName
    {
      get
      {
        return (string) PropertyStore.GetObject(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    public PageBreak PageBreak
    {
      get
      {
        return (PageBreak) PropertyStore.GetObject(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression PageName
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Filter>))]
    public IList<Filter> Filters
    {
      get
      {
        return (IList<Filter>) PropertyStore.GetObject(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    [XmlElement(typeof (RdlCollection<SortExpression>))]
    public IList<SortExpression> SortExpressions
    {
      get
      {
        return (IList<SortExpression>) PropertyStore.GetObject(24);
      }
      set
      {
        PropertyStore.SetObject(24, value);
      }
    }

    [XmlIgnore]
    public override bool AllowInHeaderFooter => false;

	  [XmlIgnore]
    public abstract IEnumerable<Group> AllGroups { get; }

    Group IDataScope.Group => null;

	  public DataRegion()
    {
    }

    internal DataRegion(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      SortExpressions = new RdlCollection<SortExpression>();
      Filters = new RdlCollection<Filter>();
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, DataSetName);
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      Report ancestor = GetAncestor<Report>();
      if (ancestor == null)
        return;
      DataSet dataSetByName = ancestor.GetDataSetByName(DataSetName);
      if (dataSetByName == null || dependencies.Contains(dataSetByName))
        return;
      dependencies.Add(dataSetByName);
    }

    internal static void AddGroupsToList(IHierarchy hierarchy, IList<Group> list)
    {
      AddGroupsToList(hierarchy.Members, list);
    }

    private static void AddGroupsToList(IEnumerable<IHierarchyMember> members, IList<Group> list)
    {
      foreach (IHierarchyMember member in members)
      {
        if (member.Group != null)
          list.Add(member.Group);
        AddGroupsToList(member.Members, list);
      }
    }

    IEnumerable<IDataScope> IDataScopeService.GetDataScopesFor(IContainedObject obj)
    {
      return GetDataScopesForDefaultImpl(obj);
    }

    IEnumerable<IDataScope> IDataCellScopeService.GetDataCellScopes(IDataCell dataCell)
    {
      return GetDataCellScopeServiceImpl().GetDataCellScopes(dataCell);
    }

    internal abstract IDataCellScopeService GetDataCellScopeServiceImpl();

    internal static IEnumerable<IHierarchyMember> GetAllLeafMembers(IHierarchy hierarchy)
    {
      return GetAllMembers(hierarchy, m =>
      {
	      if (m.Members != null)
		      return !m.Members.GetEnumerator().MoveNext();
	      return true;
      });
    }

    internal static IEnumerable<IHierarchyMember> GetAllMembers(IHierarchy hierarchy, Predicate<IHierarchyMember> filter)
    {
      return GetAllMembers(hierarchy.Members, filter);
    }

    internal static IEnumerable<IHierarchyMember> GetAllMembers(IEnumerable<IHierarchyMember> rootMembers, Predicate<IHierarchyMember> filter)
    {
      foreach (IHierarchyMember rootMember in rootMembers)
      {
        if (filter == null || filter(rootMember))
          yield return rootMember;
        foreach (IHierarchyMember allMember in GetAllMembers(rootMember.Members, filter))
          yield return allMember;
      }
    }

    internal class Definition : DefinitionStore<DataRegion, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Name,
        ActionInfo,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Visibility,
        ToolTip,
        ToolTipLocID,
        DocumentMapLabel,
        DocumentMapLabelLocID,
        Bookmark,
        RepeatWith,
        CustomProperties,
        DataElementName,
        DataElementOutput,
        KeepTogether,
        NoRowsMessage,
        DataSetName,
        PageBreak,
        PageName,
        Filters,
        SortExpressions,
      }
    }
  }
}
