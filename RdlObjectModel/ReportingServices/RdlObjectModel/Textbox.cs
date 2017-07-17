using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Textbox : ReportItem
  {
    private string m_defaultName = string.Empty;
    public const bool AllowTypeInHeaderFooter = true;

	  [DefaultValue(false)]
    public bool CanGrow
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

    [DefaultValue(false)]
    public bool CanShrink
    {
      get
      {
        return PropertyStore.GetBoolean(19);
      }
      set
      {
        PropertyStore.SetBoolean(19, value);
      }
    }

    [DefaultValue("")]
    public string HideDuplicates
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

    public ToggleImage ToggleImage
    {
      get
      {
        return (ToggleImage) PropertyStore.GetObject(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    public UserSort UserSort
    {
      get
      {
        return (UserSort) PropertyStore.GetObject(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [DefaultValue(DataElementStyles.Auto)]
    public DataElementStyles DataElementStyle
    {
      get
      {
        return (DataElementStyles) PropertyStore.GetInteger(23);
      }
      set
      {
        PropertyStore.SetInteger(23, (int) value);
      }
    }

    [DefaultValue(false)]
    public bool KeepTogether
    {
      get
      {
        return PropertyStore.GetBoolean(24);
      }
      set
      {
        PropertyStore.SetBoolean(24, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Paragraph>))]
    public IList<Paragraph> Paragraphs
    {
      get
      {
        return (IList<Paragraph>) PropertyStore.GetObject(25);
      }
      set
      {
        PropertyStore.SetObject(25, value);
      }
    }

    [DefaultValue(WatermarkText.None)]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public WatermarkText WatermarkTextbox { get; set; }

	  [DefaultValue("")]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string DefaultName
    {
      get
      {
        return m_defaultName;
      }
      set
      {
        if (!(m_defaultName != value))
          return;
        SavePropertyValue("DefaultName", m_defaultName, (string newValue, out string oldValue) =>
        {
	        oldValue = m_defaultName;
	        m_defaultName = newValue;
        });
        m_defaultName = value ?? string.Empty;
      }
    }

    [XmlIgnore]
    public override bool AllowInHeaderFooter => true;

	  public Textbox()
    {
    }

    internal Textbox(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Paragraphs = new RdlCollection<Paragraph>();
      Paragraphs.Add(new Paragraph());
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      string hideDuplicates = HideDuplicates;
      HideDuplicates = nameChanges.GetNewName(NameChanges.EntryType.DataSet, HideDuplicates);
      if (!(hideDuplicates == HideDuplicates))
        return;
      HideDuplicates = nameChanges.GetNewName(NameChanges.EntryType.Group, HideDuplicates);
    }

    protected override void InitializeForDesigner()
    {
      base.InitializeForDesigner();
      UserSort = new UserSort();
    }

    internal ReportExpression<bool> GetInitialToggleState()
    {
      if (ToggleImage == null)
        return false;
      return ToggleImage.InitialState;
    }

    internal void SetInitialToggleState(ReportExpression<bool> initialToggleState)
    {
      if (initialToggleState.IsExpression || initialToggleState.Value)
      {
        if (ToggleImage == null)
          ToggleImage = new ToggleImage();
        ToggleImage.InitialState = initialToggleState;
      }
      else
        ToggleImage = null;
    }

    internal class Definition : DefinitionStore<Textbox, Definition.Properties>
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
        CanGrow,
        CanShrink,
        HideDuplicates,
        ToggleImage,
        UserSort,
        DataElementStyle,
        KeepTogether,
        Paragraphs,
        PropertyCount,
      }
    }
  }
}
