using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Tablix : DataRegion
  {
	  private TablixDataCellScopeService __tablixDataCellScopeService;

    public TablixCorner TablixCorner
    {
      get
      {
        return (TablixCorner) PropertyStore.GetObject(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    public TablixBody TablixBody
    {
      get
      {
        return (TablixBody) PropertyStore.GetObject(26);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(26, value);
      }
    }

    public TablixHierarchy TablixColumnHierarchy
    {
      get
      {
        return (TablixHierarchy) PropertyStore.GetObject(27);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(27, value);
      }
    }

    public TablixHierarchy TablixRowHierarchy
    {
      get
      {
        return (TablixHierarchy) PropertyStore.GetObject(28);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(28, value);
      }
    }

    [DefaultValue(LayoutDirections.LTR)]
    public LayoutDirections LayoutDirection
    {
      get
      {
        return (LayoutDirections) PropertyStore.GetInteger(29);
      }
      set
      {
        PropertyStore.SetInteger(29, (int) value);
      }
    }

    [DefaultValue(0)]
    [ValidValues(0, 2147483647)]
    public int GroupsBeforeRowHeaders
    {
      get
      {
        return PropertyStore.GetInteger(30);
      }
      set
      {
        ((ComparablePropertyDefinition<int>) DefinitionStore<Tablix, Definition.Properties>.GetProperty(30)).Validate(this, value);
        PropertyStore.SetInteger(30, value);
      }
    }

    [DefaultValue(false)]
    public bool RepeatColumnHeaders
    {
      get
      {
        return PropertyStore.GetBoolean(31);
      }
      set
      {
        PropertyStore.SetBoolean(31, value);
      }
    }

    [DefaultValue(false)]
    public bool RepeatRowHeaders
    {
      get
      {
        return PropertyStore.GetBoolean(32);
      }
      set
      {
        PropertyStore.SetBoolean(32, value);
      }
    }

    [DefaultValue(false)]
    public bool FixedColumnHeaders
    {
      get
      {
        return PropertyStore.GetBoolean(33);
      }
      set
      {
        PropertyStore.SetBoolean(33, value);
      }
    }

    [DefaultValue(false)]
    public bool FixedRowHeaders
    {
      get
      {
        return PropertyStore.GetBoolean(34);
      }
      set
      {
        PropertyStore.SetBoolean(34, value);
      }
    }

    [DefaultValue(false)]
    public bool OmitBorderOnPageBreak
    {
      get
      {
        return PropertyStore.GetBoolean(35);
      }
      set
      {
        PropertyStore.SetBoolean(35, value);
      }
    }

    [DefaultValue(false)]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public bool IsFragment { get; set; }

	  [XmlIgnore]
    public override IEnumerable<ReportItem> ContainedReportItems
    {
      get
      {
        List<ReportItem> reportItemList = new List<ReportItem>();
        AddReportItemsToList(TablixColumnHierarchy.TablixMembers, reportItemList);
        AddReportItemsToList(TablixRowHierarchy.TablixMembers, reportItemList);
        foreach (TablixRow tablixRow in TablixBody.TablixRows)
        {
          foreach (TablixCell tablixCell in tablixRow.TablixCells)
          {
            if (tablixCell.CellContents != null && tablixCell.CellContents.ReportItem != null)
              reportItemList.Add(tablixCell.CellContents.ReportItem);
          }
        }
        if (TablixCorner != null)
        {
          foreach (IEnumerable<TablixCornerCell> tablixCornerRow in TablixCorner.TablixCornerRows)
          {
            foreach (TablixCornerCell tablixCornerCell in tablixCornerRow)
            {
              if (tablixCornerCell.CellContents != null && tablixCornerCell.CellContents.ReportItem != null)
                reportItemList.Add(tablixCornerCell.CellContents.ReportItem);
            }
          }
        }
        return reportItemList;
      }
    }

    [XmlIgnore]
    public override IEnumerable<Group> AllGroups
    {
      get
      {
        List<Group> groupList = new List<Group>();
        AddGroupsToList(TablixColumnHierarchy, groupList);
        AddGroupsToList(TablixRowHierarchy, groupList);
        return groupList;
      }
    }

    [XmlIgnore]
    public IEnumerable<TablixMember> AllDynamicMembers
    {
      get
      {
        foreach (Group allGroup in AllGroups)
          yield return allGroup.ParentTablixMember;
      }
    }

    public Tablix()
    {
    }

    internal Tablix(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TablixBody = new TablixBody();
      TablixColumnHierarchy = new TablixHierarchy();
      TablixRowHierarchy = new TablixHierarchy();
    }

    internal override IDataCellScopeService GetDataCellScopeServiceImpl()
    {
      if (__tablixDataCellScopeService == null)
        __tablixDataCellScopeService = new TablixDataCellScopeService(this);
      return __tablixDataCellScopeService;
    }

    private static void AddReportItemsToList(IList<TablixMember> tablixMembers, IList<ReportItem> list)
    {
      foreach (TablixMember tablixMember in tablixMembers)
      {
        if (tablixMember.TablixHeader != null && tablixMember.TablixHeader.CellContents != null && tablixMember.TablixHeader.CellContents.ReportItem != null)
          list.Add(tablixMember.TablixHeader.CellContents.ReportItem);
        if (tablixMember.TablixMembers.Count > 0)
          AddReportItemsToList(tablixMember.TablixMembers, list);
      }
    }

    internal class Definition : DefinitionStore<Tablix, Definition.Properties>
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
        TablixCorner,
        TablixBody,
        TablixColumnHierarchy,
        TablixRowHierarchy,
        LayoutDirection,
        GroupsBeforeRowHeaders,
        RepeatColumnHeaders,
        RepeatRowHeaders,
        FixedColumnHeaders,
        FixedRowHeaders,
        OmitBorderOnPageBreak,
        PropertyCount,
      }
    }

    private sealed class TablixDataCellScopeService : DataCellScopeServiceImpl
    {
      private readonly Tablix m_tablix;

      internal TablixDataCellScopeService(Tablix tablix)
      {
        m_tablix = tablix;
      }

      protected override IEnumerable<IHierarchy> GetAllHierarchies()
      {
        if (m_tablix.TablixColumnHierarchy != null)
          yield return m_tablix.TablixColumnHierarchy;
        if (m_tablix.TablixRowHierarchy != null)
          yield return m_tablix.TablixRowHierarchy;
      }

      protected override int GetDataCellCoordinate(IHierarchy hierarchy, IDataCell dataCell)
      {
        TablixRow parent = dataCell.Parent as TablixRow;
	      if (hierarchy == m_tablix.TablixColumnHierarchy)
	      {
		      if (parent != null)
		      {
			      return parent.TablixCells.IndexOf(dataCell as TablixCell);
		      }
	      }
	      if (hierarchy == m_tablix.TablixRowHierarchy)
	      {
		      return m_tablix.TablixBody.TablixRows.IndexOf(parent);
	      }
	      throw new InvalidOperationException();
      }
    }
  }
}
