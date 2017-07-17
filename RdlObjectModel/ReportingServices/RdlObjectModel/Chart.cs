using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Chart : DataRegion
  {
    private ChartDataCellScopeService m_chartDataCellScopeService;

	  public ChartCategoryHierarchy ChartCategoryHierarchy
    {
      get
      {
        return (ChartCategoryHierarchy) PropertyStore.GetObject(25);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(25, value);
      }
    }

    public ChartSeriesHierarchy ChartSeriesHierarchy
    {
      get
      {
        return (ChartSeriesHierarchy) PropertyStore.GetObject(26);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(26, value);
      }
    }

    public ChartData ChartData
    {
      get
      {
        return (ChartData) PropertyStore.GetObject(27);
      }
      set
      {
        PropertyStore.SetObject(27, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartArea>))]
    public IList<ChartArea> ChartAreas
    {
      get
      {
        return (IList<ChartArea>) PropertyStore.GetObject(28);
      }
      set
      {
        PropertyStore.SetObject(28, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartLegend>))]
    public IList<ChartLegend> ChartLegends
    {
      get
      {
        return (IList<ChartLegend>) PropertyStore.GetObject(29);
      }
      set
      {
        PropertyStore.SetObject(29, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartTitle>))]
    public IList<ChartTitle> ChartTitles
    {
      get
      {
        return (IList<ChartTitle>) PropertyStore.GetObject(30);
      }
      set
      {
        PropertyStore.SetObject(30, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartPalettes), ChartPalettes.Default)]
    public ReportExpression<ChartPalettes> Palette
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartPalettes>>(31);
      }
      set
      {
        PropertyStore.SetObject(31, value);
      }
    }

    [XmlArrayItem("ChartCustomPaletteColor", typeof (ReportExpression<ReportColor>))]
    [XmlElement(typeof (RdlCollection<ReportExpression<ReportColor>>))]
    public IList<ReportExpression<ReportColor>> ChartCustomPaletteColors
    {
      get
      {
        return (IList<ReportExpression<ReportColor>>) PropertyStore.GetObject(32);
      }
      set
      {
        PropertyStore.SetObject(32, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ChartPaletteHatchBehaviorTypes), ChartPaletteHatchBehaviorTypes.Default)]
    public ReportExpression<ChartPaletteHatchBehaviorTypes> PaletteHatchBehavior
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ChartPaletteHatchBehaviorTypes>>(33);
      }
      set
      {
        PropertyStore.SetObject(33, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> DynamicHeight
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(34);
      }
      set
      {
        PropertyStore.SetObject(34, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportSize))]
    public ReportExpression<ReportSize> DynamicWidth
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(35);
      }
      set
      {
        PropertyStore.SetObject(35, value);
      }
    }

    public ChartBorderSkin ChartBorderSkin
    {
      get
      {
        return (ChartBorderSkin) PropertyStore.GetObject(36);
      }
      set
      {
        PropertyStore.SetObject(36, value);
      }
    }

    public ChartTitle ChartNoDataMessage
    {
      get
      {
        return (ChartTitle) PropertyStore.GetObject(37);
      }
      set
      {
        PropertyStore.SetObject(37, value);
      }
    }

    [XmlIgnore]
    public override IEnumerable<Group> AllGroups
    {
      get
      {
        List<Group> groupList = new List<Group>();
        AddGroupsToList(ChartCategoryHierarchy, groupList);
        AddGroupsToList(ChartSeriesHierarchy, groupList);
        return groupList;
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue(DesignerModes.Chart)]
    public DesignerModes DesignerMode { get; set; }

	  public Chart()
    {
    }

    internal Chart(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartCategoryHierarchy = new ChartCategoryHierarchy();
      ChartSeriesHierarchy = new ChartSeriesHierarchy();
      ChartAreas = new RdlCollection<ChartArea>();
      ChartLegends = new RdlCollection<ChartLegend>();
      ChartTitles = new RdlCollection<ChartTitle>();
      ChartCustomPaletteColors = new RdlCollection<ReportExpression<ReportColor>>();
    }

    internal override IDataCellScopeService GetDataCellScopeServiceImpl()
    {
      if (m_chartDataCellScopeService == null)
        m_chartDataCellScopeService = new ChartDataCellScopeService(this);
      return m_chartDataCellScopeService;
    }

    internal class Definition : DefinitionStore<Chart, Definition.Properties>
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
        ChartCategoryHierarchy,
        ChartSeriesHierarchy,
        ChartData,
        ChartAreas,
        ChartLegends,
        ChartTitles,
        Palette,
        ChartCustomPaletteColors,
        PaletteHatchBehavior,
        DynamicHeight,
        DynamicWidth,
        ChartBorderSkin,
        ChartNoDataMessage,
        PropertyCount,
      }
    }

    public enum DesignerModes
    {
      Chart,
      Sparkline,
      DataBar,
    }

    public static class Defaults
    {
      public const DesignerModes DesignerMode = DesignerModes.Chart;
    }

    private sealed class ChartDataCellScopeService : DataCellScopeServiceImpl
    {
      private readonly Chart m_chart;

      internal ChartDataCellScopeService(Chart chart)
      {
        m_chart = chart;
      }

      protected override IEnumerable<IHierarchy> GetAllHierarchies()
      {
        if (m_chart.ChartCategoryHierarchy != null)
          yield return m_chart.ChartCategoryHierarchy;
        if (m_chart.ChartSeriesHierarchy != null)
          yield return m_chart.ChartSeriesHierarchy;
      }

      protected override int GetDataCellCoordinate(IHierarchy hierarchy, IDataCell dataCell)
      {
        ChartSeries parent = dataCell.Parent as ChartSeries;
        if (hierarchy == m_chart.ChartCategoryHierarchy)
          return parent.ChartDataPoints.IndexOf(dataCell as ChartDataPoint);
        if (hierarchy == m_chart.ChartSeriesHierarchy)
          return m_chart.ChartData.ChartSeriesCollection.IndexOf(parent);
        throw new InvalidOperationException();
      }
    }
  }
}
