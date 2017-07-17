using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class GaugePanel : DataRegion
  {
    private GaugeDataCellScopeService __gaugeDataCellScopeService;

    [XmlElement(typeof (RdlCollection<LinearGauge>))]
    public IList<LinearGauge> LinearGauges
    {
      get
      {
        return (IList<LinearGauge>) PropertyStore.GetObject(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    [XmlElement(typeof (RdlCollection<RadialGauge>))]
    public IList<RadialGauge> RadialGauges
    {
      get
      {
        return (IList<RadialGauge>) PropertyStore.GetObject(26);
      }
      set
      {
        PropertyStore.SetObject(26, value);
      }
    }

    [XmlElement(typeof (RdlCollection<NumericIndicator>))]
    public IList<NumericIndicator> NumericIndicators
    {
      get
      {
        return (IList<NumericIndicator>) PropertyStore.GetObject(27);
      }
      set
      {
        PropertyStore.SetObject(27, value);
      }
    }

    [XmlElement(typeof (RdlCollection<StateIndicator>))]
    public IList<StateIndicator> StateIndicators
    {
      get
      {
        return (IList<StateIndicator>) PropertyStore.GetObject(28);
      }
      set
      {
        PropertyStore.SetObject(28, value);
      }
    }

    [XmlElement(typeof (RdlCollection<GaugeImage>))]
    public IList<GaugeImage> GaugeImages
    {
      get
      {
        return (IList<GaugeImage>) PropertyStore.GetObject(29);
      }
      set
      {
        PropertyStore.SetObject(29, value);
      }
    }

    [XmlElement(typeof (RdlCollection<GaugeLabel>))]
    public IList<GaugeLabel> GaugeLabels
    {
      get
      {
        return (IList<GaugeLabel>) PropertyStore.GetObject(30);
      }
      set
      {
        PropertyStore.SetObject(30, value);
      }
    }

    public GaugeMember GaugeMember
    {
      get
      {
        return (GaugeMember) PropertyStore.GetObject(31);
      }
      set
      {
        PropertyStore.SetObject(31, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (AntiAliasingTypes), AntiAliasingTypes.All)]
    public ReportExpression<AntiAliasingTypes> AntiAliasing
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<AntiAliasingTypes>>(32);
      }
      set
      {
        PropertyStore.SetObject(32, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> AutoLayout
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(33);
      }
      set
      {
        PropertyStore.SetObject(33, value);
      }
    }

    public BackFrame BackFrame
    {
      get
      {
        return (BackFrame) PropertyStore.GetObject(34);
      }
      set
      {
        PropertyStore.SetObject(34, value);
      }
    }

    [ValidValues(0.0, 100.0)]
    [ReportExpressionDefaultValue(typeof (double), 25.0)]
    public ReportExpression<double> ShadowIntensity
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(35);
      }
      set
      {
        PropertyStore.SetObject(35, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (TextAntiAliasingQualityTypes), TextAntiAliasingQualityTypes.High)]
    public ReportExpression<TextAntiAliasingQualityTypes> TextAntiAliasingQuality
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<TextAntiAliasingQualityTypes>>(36);
      }
      set
      {
        PropertyStore.SetObject(36, value);
      }
    }

    public TopImage TopImage
    {
      get
      {
        return (TopImage) PropertyStore.GetObject(37);
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
        for (GaugeMember next = GaugeMember; next != null; next = next.ChildGaugeMember)
        {
          if (next.Group != null)
            yield return next.Group;
        }
      }
    }

    public GaugePanel()
    {
    }

    internal GaugePanel(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      LinearGauges = new RdlCollection<LinearGauge>();
      RadialGauges = new RdlCollection<RadialGauge>();
      NumericIndicators = new RdlCollection<NumericIndicator>();
      StateIndicators = new RdlCollection<StateIndicator>();
      GaugeImages = new RdlCollection<GaugeImage>();
      GaugeLabels = new RdlCollection<GaugeLabel>();
      ShadowIntensity = 25.0;
    }

    internal override IDataCellScopeService GetDataCellScopeServiceImpl()
    {
      if (__gaugeDataCellScopeService == null)
        __gaugeDataCellScopeService = new GaugeDataCellScopeService(this);
      return __gaugeDataCellScopeService;
    }

    internal class Definition : DefinitionStore<GaugePanel, Definition.Properties>
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
        LinearGauges,
        RadialGauges,
        NumericIndicators,
        StateIndicators,
        GaugeImages,
        GaugeLabels,
        GaugeMember,
        AntiAliasing,
        AutoLayout,
        BackFrame,
        ShadowIntensity,
        TextAntiAliasingQuality,
        TopImage,
        PropertyCount,
      }
    }

    private sealed class GaugeDataCellScopeService : DataCellScopeServiceImpl
    {
      private readonly GaugePanel m_gauge;

      internal GaugeDataCellScopeService(GaugePanel gauge)
      {
        m_gauge = gauge;
      }

      protected override IEnumerable<IHierarchy> GetAllHierarchies()
      {
        if (m_gauge.GaugeMember != null)
          yield return m_gauge.GaugeMember;
      }

      protected override int GetDataCellCoordinate(IHierarchy hierarchy, IDataCell dataCell)
      {
        return 0;
      }
    }
  }
}
