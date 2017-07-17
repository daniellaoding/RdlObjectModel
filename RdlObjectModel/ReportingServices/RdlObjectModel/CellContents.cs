using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class CellContents : ReportObject
  {
	  public ReportItem ReportItem
    {
      get
      {
        return (ReportItem) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [DefaultValue(1)]
    [ValidValues(1, 2147483647)]
    public int ColSpan
    {
      get
      {
        return PropertyStore.GetInteger(1);
      }
      set
      {
        ((ComparablePropertyDefinition<int>) DefinitionStore<CellContents, Definition.Properties>.GetProperty(1)).Validate(this, value);
        PropertyStore.SetInteger(1, value);
      }
    }

    [ValidValues(1, 2147483647)]
    [DefaultValue(1)]
    public int RowSpan
    {
      get
      {
        return PropertyStore.GetInteger(2);
      }
      set
      {
        ((ComparablePropertyDefinition<int>) DefinitionStore<CellContents, Definition.Properties>.GetProperty(2)).Validate(this, value);
        PropertyStore.SetInteger(2, value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue(false)]
    public bool Selected { get; set; }

	  public CellContents()
    {
    }

    internal CellContents(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ColSpan = 1;
      RowSpan = 1;
    }

    internal class Definition : DefinitionStore<CellContents, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ReportItem,
        ColSpan,
        RowSpan,
      }
    }
  }
}
