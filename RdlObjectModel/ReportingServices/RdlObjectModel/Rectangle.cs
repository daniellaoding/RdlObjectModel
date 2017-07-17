using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Rectangle : ReportItem
  {
    public const bool AllowTypeInHeaderFooter = true;

    [XmlElement(typeof (RdlCollection<ReportItem>))]
    public IList<ReportItem> ReportItems
    {
      get
      {
        return (IList<ReportItem>) PropertyStore.GetObject(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    public PageBreak PageBreak
    {
      get
      {
        return (PageBreak) PropertyStore.GetObject(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression PageName
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    [DefaultValue(false)]
    public bool KeepTogether
    {
      get
      {
        return PropertyStore.GetBoolean(20);
      }
      set
      {
        PropertyStore.SetBoolean(20, value);
      }
    }

    [DefaultValue(false)]
    public bool OmitBorderOnPageBreak
    {
      get
      {
        return PropertyStore.GetBoolean(21);
      }
      set
      {
        PropertyStore.SetBoolean(21, value);
      }
    }

    [DefaultValue("")]
    public string LinkToChild
    {
      get
      {
        return (string) PropertyStore.GetObject(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [ValidEnumValues("RectangleDataElementOutputTypes")]
    [DefaultValue(DataElementOutputTypes.Auto)]
    public new DataElementOutputTypes DataElementOutput
    {
      get
      {
        return base.DataElementOutput;
      }
      set
      {
        base.DataElementOutput = value;
      }
    }

    [XmlIgnore]
    public override IEnumerable<ReportItem> ContainedReportItems => ReportItems;

	  [XmlIgnore]
    public override bool AllowInHeaderFooter
    {
      get
      {
        foreach (ReportItem containedReportItem in ContainedReportItems)
        {
          if (!containedReportItem.AllowInHeaderFooter)
            return false;
        }
        return true;
      }
    }

    public Rectangle()
    {
    }

    internal Rectangle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ReportItems = new RdlCollection<ReportItem>();
    }

    internal class Definition : DefinitionStore<Rectangle, Definition.Properties>
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
        ReportItems,
        PageBreak,
        KeepTogether,
        OmitBorderOnPageBreak,
        LinkToChild,
        PageName,
        PropertyCount,
      }
    }
  }
}
