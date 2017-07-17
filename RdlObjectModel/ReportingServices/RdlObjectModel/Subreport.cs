using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{

  public class Subreport : ReportItem
  {
    public const bool AllowTypeInHeaderFooter = false;

    public string ReportName
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

    [XmlElement(typeof (RdlCollection<Parameter>))]
    public IList<Parameter> Parameters
    {
      get
      {
        return (IList<Parameter>) PropertyStore.GetObject(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression NoRowsMessage
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [DefaultValue(false)]
    public bool MergeTransactions
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

    [DefaultValue(false)]
    public bool KeepTogether
    {
      get
      {
        return PropertyStore.GetBoolean(22);
      }
      set
      {
        PropertyStore.SetBoolean(22, value);
      }
    }

    [DefaultValue(false)]
    public bool OmitBorderOnPageBreak
    {
      get
      {
        return PropertyStore.GetBoolean(23);
      }
      set
      {
        PropertyStore.SetBoolean(23, value);
      }
    }

    [XmlIgnore]
    public override bool AllowInHeaderFooter => false;

	  public Subreport()
    {
    }

    internal Subreport(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ReportName = "";
      Parameters = new RdlCollection<Parameter>();
    }

    internal class Definition : DefinitionStore<Subreport, Definition.Properties>
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
        ReportName,
        Parameters,
        NoRowsMessage,
        MergeTransactions,
        KeepTogether,
        OmitBorderOnPageBreak,
        PropertyCount,
      }
    }
   
  }
}
