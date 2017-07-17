using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class CustomReportItem : ReportItem
  {
    public const bool AllowTypeInHeaderFooter = true;

    public string Type
    {
      get
      {
        return (string) PropertyStore.GetObject(18);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(18, value);
      }
    }

    public AltReportItem AltReportItem
    {
      get
      {
        return (AltReportItem) PropertyStore.GetObject(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    public CustomData CustomData
    {
      get
      {
        return (CustomData) PropertyStore.GetObject(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [XmlIgnore]
    public override bool AllowInHeaderFooter => true;

	  [XmlIgnore]
    public override IEnumerable<ReportItem> ContainedReportItems
    {
      get
      {
        if (AltReportItem != null && AltReportItem.ReportItem != null)
          yield return AltReportItem.ReportItem;
      }
    }

    public CustomReportItem()
    {
    }

    internal CustomReportItem(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Type = "";
    }

    internal class Definition : DefinitionStore<CustomReportItem, Definition.Properties>
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
        Type,
        AltReportItem,
        CustomData,
        PropertyCount,
      }
    }
  }
}
