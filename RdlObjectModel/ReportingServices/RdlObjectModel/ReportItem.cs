using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("Line", typeof (Line))]
  [XmlElementClass("DataRegionPlaceholder", Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", Type = typeof (DataRegionPlaceholder))]
  [XmlElementClass("Image", typeof (Image))]
  [XmlElementClass("Subreport", typeof (Subreport))]
  [XmlElementClass("Chart", typeof (Chart))]
  [XmlElementClass("GaugePanel", typeof (GaugePanel))]
  [XmlElementClass("Textbox", typeof (Textbox))]
  [XmlElementClass("Rectangle", typeof (Rectangle))]
  [XmlElementClass("Map", typeof (Map))]
  [XmlElementClass("Tablix", typeof (Tablix))]
  [XmlElementClass("CustomReportItem", typeof (CustomReportItem))]
  public abstract class ReportItem : ReportElement, IGlobalNamedObject, IShouldSerialize
  {
    [XmlAttribute(typeof (string))]
    public string Name
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return (ActionInfo) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [DefaultValueConstant("DefaultZeroSize")]
    public ReportSize Top
    {
      get
      {
        return PropertyStore.GetSize(3);
      }
      set
      {
        PropertyStore.SetSize(3, value);
      }
    }

    [DefaultValueConstant("DefaultZeroSize")]
    public ReportSize Left
    {
      get
      {
        return PropertyStore.GetSize(4);
      }
      set
      {
        PropertyStore.SetSize(4, value);
      }
    }

    public ReportSize Height
    {
      get
      {
        return PropertyStore.GetSize(5);
      }
      set
      {
        PropertyStore.SetSize(5, value);
      }
    }

    public ReportSize Width
    {
      get
      {
        return PropertyStore.GetSize(6);
      }
      set
      {
        PropertyStore.SetSize(6, value);
      }
    }

    [DefaultValue(0)]
    [ValidValues(0, 2147483647)]
    public int ZIndex
    {
      get
      {
        return PropertyStore.GetInteger(7);
      }
      set
      {
        ((ComparablePropertyDefinition<int>) DefinitionStore<ReportItem, Definition.Properties>.GetProperty(7)).Validate(this, value);
        PropertyStore.SetInteger(7, value);
      }
    }

    public Visibility Visibility
    {
      get
      {
        return (Visibility) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression DocumentMapLabel
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Bookmark
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [DefaultValue("")]
    public string RepeatWith
    {
      get
      {
        return (string) PropertyStore.GetObject(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [DefaultValue(DataElementOutputTypes.Auto)]
    [ValidEnumValues("ReportItemDataElementOutputTypes")]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(17);
      }
      set
      {
        PropertyStore.SetInteger(17, (int) value);
      }
    }

    [XmlChildAttribute("ToolTip", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string ToolTipLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [XmlChildAttribute("DocumentMapLabel", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string DocumentMapLabelLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [XmlIgnore]
    public virtual IEnumerable<ReportItem> ContainedReportItems
    {
      get
      {
        yield break;
      }
    }

    [XmlIgnore]
    public virtual bool AllowInHeaderFooter => true;

	  protected ReportItem()
    {
    }

    internal ReportItem(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      CustomProperties = new RdlCollection<CustomProperty>();
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return true;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      switch (name)
      {
        case "Top":
          if (Top.Value == 0.0 && Top.Type == ReportSize.DefaultType)
            return SerializationMethod.Never;
          break;
        case "Left":
          if (Left.Value == 0.0 && Left.Type == ReportSize.DefaultType)
            return SerializationMethod.Never;
          break;
      }
      return SerializationMethod.Auto;
    }

    protected override void InitializeForDesigner()
    {
      base.InitializeForDesigner();
      Visibility = new Visibility();
    }

    internal Action GetAction()
    {
      if (ActionInfo != null && ActionInfo.Actions.Count > 0)
        return ActionInfo.Actions[0];
      return null;
    }

    internal void SetAction(Action action)
    {
      if (action != null)
      {
        if (ActionInfo == null)
          ActionInfo = new ActionInfo();
        if (ActionInfo.Actions.Count > 0)
          ActionInfo.Actions[0] = action;
        else
          ActionInfo.Actions.Add(action);
      }
      else
        ActionInfo = null;
    }

    internal class Definition : DefinitionStore<ReportItem, Definition.Properties>
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
        PropertyCount,
      }
    }
  }
}
